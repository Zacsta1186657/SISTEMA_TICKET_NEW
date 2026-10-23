import { Outlet } from "react-router"
import { SidebarInset, SidebarProvider, SidebarTrigger } from "../components/ui/sidebar"
import { MenuProvider } from "../features/menu/MenuProvider"
import AppSidebar from "./AppSiderbar"

export default function AppLayout() {
    return(
        <MenuProvider>
            <SidebarProvider>
                <AppSidebar />
                <SidebarInset>
                    <header className="flex h-12 items-center gap-2 border-b px-4">
                        <SidebarTrigger />
                    </header>
                    <main className="flex-1 p-6">
                        <Outlet />
                    </main>
                </SidebarInset>
            </SidebarProvider>
        </MenuProvider>
    )
}