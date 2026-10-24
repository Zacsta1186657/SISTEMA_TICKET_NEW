import { Navigate, Route, Routes } from "react-router"
import LoginPage from "./features/auth/LoginPage"
import ProtectedRoute from "./features/auth/ProtectedRoute"
import DashboardPage from './pages/DashboardPage'
import UnderConstructionPage from './pages/UnderConstructionPage'
import AppLayout from "./layout/AppLayout"
import ProfilePage from "./pages/ProfilePage"

export default  function App() {

  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />

       {/* Todo lo de aquí adentro exige sesión y se muestra dentro del diseño con el menú */}
       <Route element={<ProtectedRoute />}>
        <Route element={<AppLayout />}>
          <Route index element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardPage />} />
          {/* Aquí se irán agregando las pantallas reales: perfiles, personas, usuarios... */}
          <Route path="/Administration/profiles" element={<ProfilePage />} />
          <Route path="*" element={<UnderConstructionPage />} />
        </Route>
       </Route>
    </Routes>
  )
}


