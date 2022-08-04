import React, { useEffect } from 'react'
import { AppShell, ScrollArea } from '@mantine/core'
import { Outlet, useNavigate } from 'react-router-dom'
import DHeader from '../components/common/d-header'
import { useAuth } from 'oidc-react'
import { useSelector } from 'react-redux'
import { RootState } from '../app/store'

const MainLayout = () => {
  const navigate = useNavigate()
  const isAuthenticated = useSelector((state: RootState) => state.authentication.isAuthenticated)

  useEffect(() => {
    console.log('MainLayout auth', isAuthenticated)
    if(!isAuthenticated) {
      navigate('/auth/login')
    }
  })

  return (
    <div>
      <AppShell padding={'md'} header={<DHeader/>}>
        <ScrollArea style={{height: 'calc(100vh - 92px)'}}>
          <Outlet></Outlet>
        </ScrollArea>
      </AppShell>
    </div>
  )
}

export default MainLayout
