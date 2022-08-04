import React from 'react'
import { Button } from '@mantine/core'
import { Link } from 'react-router-dom'

const DHeaderLinks = () => {
  return (
    <div>
      <Button  variant={'filled'} size={'md'} color={'dark'}>Wiki</Button>
      <Button component={Link} to={'/auth/login'} variant={'white'} size={'md'} color={'dark'}>Login</Button>
    </div>
  )
}

export default DHeaderLinks
