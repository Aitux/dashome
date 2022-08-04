import React from 'react'
import { Avatar, Container, createStyles, Header, Image } from '@mantine/core'
import DHeaderLinks from './d-header-links'
import sizes from '../../constants/sizes'
import { useSelector } from 'react-redux'
import { RootState } from '../../app/store'

const HEADER_HEIGHT = 60
const LOGO_URI = '/icon.svg'

const useStyles = createStyles(() => ({
  inner: {
    height: HEADER_HEIGHT
  }
}))

const DHeader = () => {
  const {classes} = useStyles()

  const user = useSelector((state: RootState) => state.authentication.user)

  return (
    <div>
      <Header height={HEADER_HEIGHT} fixed>
        <Container className={`${classes.inner} flex items-center gap-12 justify-between`} fluid>
          <div>
            <Image src={LOGO_URI} />
          </div>
          <DHeaderLinks/>
          <div className={'flex items-center gap-3'}>
            <Avatar src={'./vite.svg'}/>
            {user && (<span>{user.firstName} {user.lastName}</span>)}
          </div>
        </Container>
      </Header>
    </div>
  )
}

export default DHeader
