import { apiFetch } from '../../lib/api'
import type { LoginResponse, MenuNode } from '../../types/auth'

export function login(userName: string, password: string) {
    return apiFetch<LoginResponse>("/api/auth/login" , {
        method: "POST",
        body: JSON.stringify({userName, password})
    })
}

export function getMenu(){
    return apiFetch<MenuNode[]>("/api/auth/menu")
}