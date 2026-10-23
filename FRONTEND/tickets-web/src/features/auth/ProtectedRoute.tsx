import { Navigate, Outlet } from "react-router"
import { session } from "../../lib/session"

// Si no hay token válido, manda al login. Si lo hay, deja pasar a las rutas de adentro.
export default function ProtectedRoute() {
  return session.isLoggedIn() ? <Outlet /> : <Navigate to="/login" replace />
}