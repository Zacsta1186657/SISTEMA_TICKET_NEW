import { useState } from "react"
import { Button } from "../../components/ui/button"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from "../../components/ui/dialog"
import { Input } from "../../components/ui/input"
import { Label } from "../../components/ui/label"
import { Textarea } from "../../components/ui/textarea"
import { ApiError } from "../../lib/api"
import { createProfile } from "./profilesApi"

// Largos iguales a las columnas de seg.Profile
const NAME_MAX = 50
const DESCRIPTION_MAX = 200

interface Props {
    open: boolean
    onOpenChange: (open: boolean) => void
    onCreated: () => void
}

export default function ProfileFormDialog({ open, onOpenChange, onCreated }: Props) {
    const [name, setName] = useState("")
    const [description, setDescription] = useState("")
    const [error, setError] = useState<string | null>(null)
    const [saving, setSaving] = useState(false)

    function handleOpenChange(next: boolean) {
        // Al cerrar se limpia el formulario
        if (!next) {
            setName("")
            setDescription("")
            setError(null)
        }
        onOpenChange(next)
    }

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault()
        setError(null)

        if (!name.trim()) {
            setError("El nombre es obligatorio")
            return
        }

        setSaving(true)

        try {
            await createProfile({
                name: name.trim(),
                description: description.trim() || null,
            })

            handleOpenChange(false)
            onCreated()

        } catch (err) {
            setError(err instanceof ApiError ? err.message : "No se pudo conectar con el servidor.")
        } finally {
            setSaving(false)
        }
    }

    return (
        <Dialog open={open} onOpenChange={handleOpenChange}>
            <DialogContent>
                <form onSubmit={handleSubmit} className="flex flex-col gap-4">
                    <DialogHeader>
                        <DialogTitle>Nuevo perfil</DialogTitle>
                        <DialogDescription>
                            Un perfil agrupa los menús a los que podrá entrar un usuario.
                        </DialogDescription>
                    </DialogHeader>

                    <div className="grid gap-2">
                        <Label htmlFor="profileName">
                            Nombre <span className="text-destructive">*</span>
                        </Label>
                        <Input
                            id="profileName"
                            maxLength={NAME_MAX}
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    </div>

                    <div className="grid gap-2">
                        <Label htmlFor="profileDescription">Descripción</Label>
                        <Textarea
                            id="profileDescription"
                            maxLength={DESCRIPTION_MAX}
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                        />
                    </div>

                    {error && (
                        <p role="alert" className="text-sm text-destructive">
                            {error}
                        </p>
                    )}

                    <DialogFooter>
                        <Button type="button" variant="outline" onClick={() => handleOpenChange(false)}>
                            Cancelar
                        </Button>
                        <Button type="submit" disabled={saving}>
                            {saving ? "Guardando..." : "Guardar"}
                        </Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}