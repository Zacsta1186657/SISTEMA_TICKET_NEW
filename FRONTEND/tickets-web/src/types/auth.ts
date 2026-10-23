export interface MenuNode {
    menuId: number
    code: string
    name: string 
    url: string | null
    icon: string | null
    sortOrder: number
    children: MenuNode[] 
}

export interface LoginResponse {
    token: string
    expiresAt: string
    mustChangePassword: boolean
    menu: MenuNode[]    
}