import {
  BarChart3,
  CalendarDays,
  Circle,
  FileText,
  Home,
  KeyRound,
  LayoutDashboard,
  List,
  Lock,
  Menu,
  Plus,
  Settings,
  Shield,
  Ticket,
  User,
  Users,
  type LucideIcon,
} from "lucide-react"

// Texto que viene en la columna Icon de seg.Menu  →  ícono que se dibuja.
// Para usar un ícono nuevo: impórtalo arriba y agrégalo aquí.
const icons: Record<string, LucideIcon> = {
  dashboard: LayoutDashboard,
  menu: Menu,
  calendar: CalendarDays,
  list: List,
  plus: Plus,
  settings: Settings,
  users: Users,
  user: User,
  key: KeyRound,
  lock: Lock,
  shield: Shield,
  file: FileText,
  ticket: Ticket,
  home: Home,
  chart: BarChart3,
}

export function getMenuIcon(name: string | null): LucideIcon {
  return (name && icons[name.toLowerCase()]) || Circle
}