const TOKEN_KEY = "tickets.token"
const EXPIRES_API = "tickets.expiresAt"

export const session = {
    save(token: string, expiresAt: string) {
        localStorage.setItem(TOKEN_KEY, token)
        localStorage.setItem(EXPIRES_API, expiresAt)
    },

    // Devuelve el token, o null si no hay o ya vencio
    getToken(): string | null {
        const token = localStorage.getItem(TOKEN_KEY)
        const expiresAt = localStorage.getItem(EXPIRES_API)
        if(!token || !expiresAt) return null

        if(new Date(expiresAt).getTime() <= Date.now()) {
            session.clear()
            return null
        }

        return token
    },

    isLoggedIn(): boolean {
        return session.getToken() !== null
    },

    // Nombre de usuario guardado dentro del token (claim "unique_name")
    getUserName(): string | null {
        const token = session.getToken()
        if (!token) return null
        try {
        const base64 = token.split(".")[1].replace(/-/g, "+").replace(/_/g, "/")
        const bytes = Uint8Array.from(atob(base64), (c) => c.charCodeAt(0))
        const payload = JSON.parse(new TextDecoder().decode(bytes))
        return payload.unique_name ?? null
        } catch {
        return null
        }
    },

    clear() {
        localStorage.removeItem(TOKEN_KEY)
        localStorage.removeItem(EXPIRES_API)
    },
}

