import { ChevronRight, LogOut, Ticket } from "lucide-react"
import { Link, NavLink, useLocation, useNavigate } from "react-router"
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "../components/ui/collapsible"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
  useSidebar
} from "../components/ui/sidebar"
import { useMenu } from "../features/menu/userMenu"
import { session } from "../lib/session"
import type { MenuNode } from "../types/auth"
import { getMenuIcon } from "./menuIcons"


// ¿Esta opción es la pantalla en la que estoy? (sin distinguir mayúsculas: /Administracion = /administracion)
function useIsActive(){
    const { pathname } = useLocation()
    return (url: string | null) => url !== null && url.toLowerCase() === pathname.toLowerCase()
}

// Opciones dentro de un submenú (nivel 2 en adelante)
function SubItem({ node }: { node: MenuNode }) {
  const isActive = useIsActive()

  return (
    <SidebarMenuSubItem>
      {node.url ? (
        <SidebarMenuSubButton render={<NavLink to={node.url} />} isActive={isActive(node.url)}>
          <span>{node.name}</span>
        </SidebarMenuSubButton>
      ) : (
        <span className="px-2 text-xs text-muted-foreground">{node.name}</span>
      )}

      {node.children.length > 0 && (
        <SidebarMenuSub>
          {node.children.map((child) => (
            <SubItem key={child.menuId} node={child} />
          ))}
        </SidebarMenuSub>
      )}
    </SidebarMenuSubItem>
  )
}

// Opciones directas de un grupo (nivel 1)
function Item({ node }: { node: MenuNode }) {
  const isActive = useIsActive()
  const { state, setOpen } = useSidebar()
  const Icon = getMenuIcon(node.icon)

  // Con hijos: se despliega
  if (node.children.length > 0) {
    return (
      <Collapsible defaultOpen className="group/collapsible">
        <SidebarMenuItem>
          <CollapsibleTrigger 
            render={<SidebarMenuButton tooltip={node.name} />}
            // Con el menú colapsado a íconos, un clic lo abre para poder ver las opciones
            onClick={() => state === "collapsed" && setOpen(true)}
            >
            <Icon />
            <span>{node.name}</span>
            <ChevronRight className="ml-auto transition-transform group-data-open/collapsible:rotate-90" />
          </CollapsibleTrigger>
          <CollapsibleContent>
            <SidebarMenuSub>
              {node.children.map((child) => (
                <SubItem key={child.menuId} node={child} />
              ))}
            </SidebarMenuSub>
          </CollapsibleContent>
        </SidebarMenuItem>
      </Collapsible>
    )
  }

  // Sin hijos: es un enlace directo
  return (
    <SidebarMenuItem>
      <SidebarMenuButton 
        render={<NavLink to={node.url ?? "#"} />} 
        isActive={isActive(node.url)}>
        <Icon />
        <span>{node.name}</span>
      </SidebarMenuButton>
    </SidebarMenuItem>
  )
}

export default function AppSidebar() {
  const navigate = useNavigate()
  const { menu, loading, error } = useMenu()
  const userName = session.getUserName() ?? "Usuario"

  function handleLogout() {
    session.clear()
    navigate("/login", { replace: true })
  }

  return (
    <Sidebar collapsible="icon">
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" render={<Link to="/dashboard" />}>
              <div className="flex aspect-square size-8 items-center justify-center rounded-lg bg-primary text-primary-foreground">
                <Ticket className="size-4" />
              </div>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-semibold">Tickets</span>
                <span className="truncate text-xs">Cambio de guardia</span>
              </div>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>

      <SidebarContent>
        {loading && <p className="px-4 py-2 text-sm text-muted-foreground">Cargando menú...</p>}
        {error && <p className="px-4 py-2 text-sm text-destructive">{error}</p>}

        {/* Cada raíz del árbol (módulo principal) es un grupo con su título */}
        {menu.map((root) => (
          <SidebarGroup key={root.menuId}>
            {root.children.length > 0 && <SidebarGroupLabel>{root.name}</SidebarGroupLabel>}
            <SidebarGroupContent>
              <SidebarMenu>
                {(root.children.length > 0 ? root.children : [root]).map((node) => (
                  <Item key={node.menuId} node={node} />
                ))}
              </SidebarMenu>
            </SidebarGroupContent>
          </SidebarGroup>
        ))}
      </SidebarContent>

      <SidebarFooter>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" tooltip="Cerrar sesión" onClick={handleLogout}>
              <div className="flex aspect-square size-8 items-center justify-center rounded-lg bg-muted text-sm font-semibold uppercase">
                {userName.charAt(0)}
              </div>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-semibold">{userName}</span>
                <span className="truncate text-xs">Cerrar sesión</span>
              </div>
              <LogOut className="ml-auto size-4" />
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarFooter>
    </Sidebar>
  )
}