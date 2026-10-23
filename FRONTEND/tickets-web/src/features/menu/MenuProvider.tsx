// Pide el menú del usuario al backend una vez que la pantalla principal se monta.
// Como se monta al entrar (y al recargar con F5), el menú siempre queda cargado

import { useState, useEffect, type ReactNode } from "react";
import type { MenuNode } from "../../types/auth";
import { getMenu } from "../../features/auth/authApi"
import { ApiError } from "../../lib/api"
import { MenuContext } from "./menuContext"

export function MenuProvider({ children } : {children: ReactNode}) {
    const [menu, setMenu] = useState<MenuNode[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState<string | null>(null)

    useEffect(() => {
    let cancelled = false

    getMenu()
      .then((data) => {
        if (!cancelled) setMenu(data)
      })
      .catch((err) => {
        if (!cancelled) {
          setError(err instanceof ApiError ? err.message : "No se pudo cargar el menú.")
        }
      })
      .finally(() => {
        if (!cancelled) setLoading(false)
      })

    return () => {
      cancelled = true
    }
  }, [])

  return <MenuContext value={{ menu, loading, error }}>{children}</MenuContext>

}