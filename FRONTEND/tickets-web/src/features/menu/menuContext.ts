import { createContext } from 'react'
import type { MenuNode } from '../../types/auth'

export interface MenuContextValue {
    menu: MenuNode[]
    loading: boolean
    error: string | null
}

export const MenuContext = createContext<MenuContextValue | null>(null)