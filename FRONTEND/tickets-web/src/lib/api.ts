import { session } from '../lib/session'

const API_URL = import.meta.env.VITE_API_URL as string

export class ApiError extends Error {
    status: number

    constructor(status: number, message: string){
        super(message)
        this.status = status
    }
}

async function readMessage(response: Response): Promise<string> {

    try{
        const body = await response.json()
        if(body.errors) {
            return Object.values(body.errors as Record<string, string[]>).flat().join(" ")
        }

        return body.detail ?? body.title ?? "Ocurrio un error"

    }catch {
        return "Ocurrio un Error"
    }
}

export async function apiFetch<T>(path: string, options: RequestInit = {}): Promise<T> {
    const headers = new Headers(options.headers)
    headers.set("Content-Type", "application/json")

    const token = session.getToken()
    if(token) headers.set("Authorization", `Bearer ${token}`)

    const response = await fetch(`${API_URL}${path}`, {...options, headers})

    // Token vencido o inválido: se cierra la sesión y se vuelve al login.
    // (El propio login queda fuera: ahí un 401 significa "clave incorrecta".)

    if(response.status === 401 && token && !path.startsWith("/api/auth/login")) {
        session.clear()
        window.location.href = "/login"
    }

    if(!response.ok){
        throw new ApiError(response.status, await readMessage(response))
    }

    return (await response.json()) as T
}