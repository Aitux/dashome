import { Route, Routes } from 'react-router-dom'
import HomeIndex from './components/home'
import MainLayout from './layouts/main-layout'
import AuthLayout from './layouts/auth-layout'
import AuthLoginIndex from './components/auth/login'
import AuthCallback from './components/auth/callback'

export default () => (
  <Routes>
    <Route path={'/callback'} element={<AuthCallback/>}/>
    <Route element={<MainLayout/>}>
      <Route path={'/'} element={<HomeIndex/>}></Route>
    </Route>
    <Route element={<AuthLayout/>}>
      <Route path={'/auth/login'} element={<AuthLoginIndex/>}></Route>
    </Route>
  </Routes>
)
