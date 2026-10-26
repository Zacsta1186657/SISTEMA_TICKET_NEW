import { useEffect, useMemo, useState } from "react"
import { Badge } from "../../components/ui/badge"
import { Button } from "../../components/ui/button"
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "../../components/ui/table"
import { Input } from "../../components/ui/input"
import { ApiError } from "../../lib/api"
import { exportToExcel, type ExcelColumn } from "../../lib/excel"
import ProfileFormDialog from "./ProfileFormDialog"
import { getProfiles } from "./profilesApi"
import type { Profile } from "./types"

function formatDate(value: string) {
    return new Date(value).toLocaleString("es-PE", { dateStyle: "short", timeStyle: "short" })
}

// Minúsculas y sin tildes, para que "descripcion" encuentre "Descripción"
function normalize(text: string) {
    return text.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase()
}

const statusText = (profile: Profile) => (profile.isActive ? "Activo" : "Inactivo")

// Columnas del Excel (mismas que la tabla)
const excelColumns: ExcelColumn<Profile>[] = [
    { header: "ID", width: 8, value: (p) => p.profileId },
    { header: "Nombre", width: 28, value: (p) => p.name },
    { header: "Descripción", width: 45, value: (p) => p.description },
    { header: "Estado", width: 12, value: statusText },
    { header: "Creado", width: 20, value: (p) => formatDate(p.createdAt) },
]

export default function ProfilesPage() {
    const [profiles, setProfiles] = useState<Profile[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState<string | null>(null)
    const [dialogOpen, setDialogOpen] = useState(false)
    const [search, setSearch] = useState("")
    const [exporting, setExporting] = useState(false)
    // Al cambiar este número, el listado se vuelve a pedir al backend
    const [reloadKey, setReloadKey] = useState(0)

    useEffect(() => {
        let cancelled = false

        getProfiles()
            .then((data) => {
                if (cancelled) return
                setProfiles(data)
                setError(null)
            })
            .catch((err) => {
                if (cancelled) return
                setError(err instanceof ApiError ? err.message : "No se pudo cargar los perfiles.")
            })
            .finally(() => {
                if (!cancelled) setLoading(false)
            })

        return () => {
            cancelled = true
        }
    }, [reloadKey])

    // Busca el texto en todas las columnas de la tabla
    const filtered = useMemo(() => {
        const term = normalize(search.trim())
        if (!term) return profiles

        return profiles.filter((p) =>
            normalize(
                [p.profileId, p.name, p.description ?? "", statusText(p), formatDate(p.createdAt)].join(" "),
            ).includes(term),
        )
    }, [profiles, search])

    async function handleExport() {
        setExporting(true)
        try {
            // Exporta lo que se ve en pantalla (con el filtro de búsqueda aplicado)
            await exportToExcel("perfiles", "Perfiles", excelColumns, filtered)
        } finally {
            setExporting(false)
        }
    }

    return (
        <div className="flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-2xl font-semibold">Perfiles</h1>
                    <p className="text-sm text-muted-foreground">
                        Grupos de acceso que se asignan a los usuarios.
                    </p>
                </div>
                <Button onClick={() => setDialogOpen(true)}>Nuevo perfil</Button>
            </div>

            {error && (
                <p role="alert" className="text-sm text-destructive">
                    {error}
                </p>
            )}

            <div className="flex items-center justify-between gap-4">
                <Input
                    className="max-w-sm"
                    placeholder="Buscar por ID, nombre, descripción, estado o fecha..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
                <Button
                    variant="outline"
                    onClick={handleExport}
                    disabled={exporting || filtered.length === 0}
                >
                    {exporting ? "Exportando..." : "Exportar a Excel"}
                </Button>
            </div>

            <div className="rounded-md border">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead className="w-20">ID</TableHead>
                            <TableHead>Nombre</TableHead>
                            <TableHead>Descripción</TableHead>
                            <TableHead className="w-28">Estado</TableHead>
                            <TableHead className="w-44">Creado</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {loading ? (
                            <TableRow>
                                <TableCell colSpan={5} className="text-center text-muted-foreground">
                                    Cargando perfiles...
                                </TableCell>
                            </TableRow>
                        ) : filtered.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan={5} className="text-center text-muted-foreground">
                                    {profiles.length === 0
                                        ? "No hay perfiles registrados."
                                        : "Ningún perfil coincide con la búsqueda."}
                                </TableCell>
                            </TableRow>
                        ) : (
                            filtered.map((profile) => (
                                <TableRow key={profile.profileId}>
                                    <TableCell>{profile.profileId}</TableCell>
                                    <TableCell className="font-medium">{profile.name}</TableCell>
                                    <TableCell>{profile.description ?? "—"}</TableCell>
                                    <TableCell>
                                        <Badge variant={profile.isActive ? "default" : "secondary"}>
                                            {statusText(profile)}
                                        </Badge>
                                    </TableCell>
                                    <TableCell>{formatDate(profile.createdAt)}</TableCell>
                                </TableRow>
                            ))
                        )}
                    </TableBody>
                </Table>
            </div>

            <p className="text-sm text-muted-foreground">
                {filtered.length} de {profiles.length} perfiles
            </p>

            <ProfileFormDialog
                open={dialogOpen}
                onOpenChange={setDialogOpen}
                onCreated={() => setReloadKey((key) => key + 1)}
            />
        </div>
    )
}