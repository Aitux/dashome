import React from 'react'
import { createStyles } from '@mantine/core'
import { Outlet } from 'react-router-dom'

const useStyles = createStyles(() => ({
  container: {
    background: 'linear-gradient(116.71deg, #2E51BA 0%, #4FC6E7 100.35%)',
    height: '100vh',
  }
}))

const AuthLayout = () => {
  const {classes} = useStyles()

  return (
    <div className={`${classes.container} flex items-center justify-center`}>
      <Outlet></Outlet>
    </div>
  )
}

export default AuthLayout
