import React from 'react'
import { Button, Card, createStyles, Divider, Image, Text } from '@mantine/core'
import { Link, useNavigate } from 'react-router-dom'
import { DTextInput } from '../../common/d-text-input'
import { DPasswordInput } from '../../common/d-password-input'
import { IconAt, IconLock } from '@tabler/icons'
import {useAuth} from 'oidc-react'
import { authApi } from './api'
import { useInputState } from '@mantine/hooks'
import { setAuthentication, setUser } from '../authenticationSlice'
import { callBackApi } from '../callback/api'
import { useDispatch } from 'react-redux'

const useStyles = createStyles(() => ({
  container: {
    width: 512
  }
}))

const AuthLoginIndex = () => {
  const { classes } = useStyles()
  const [email, setEmail] = useInputState('')
  const [password, setPassword] = useInputState('')

  const dispatch = useDispatch()
  const auth = useAuth()
  const navigate = useNavigate()

  const login = async () => {
    const response = await authApi.login(email, password)
    console.log('login response', response)
    dispatch(setAuthentication({ isAuthenticated: true, token: response.token }))
    const getMeResponse = await callBackApi.getMe()
    dispatch(setUser(getMeResponse.user))
    navigate('/')
  }

  const goToOAuthLogin = async () => {
    await auth.signIn()
  }

  return (
    <div>
      <Card
        className={`${classes.container} flex flex-col gap-3 items-center `}
        px={50}
        py={36}
      >
        <Image
          src={'/icon.svg'}
          height={45}
        />
        <Text size={'sm'}>
          Accédez à votre compte
        </Text>
        <DTextInput
          label={'Adresse mail'}
          className={'w-full'}
          icon={<IconAt size={18}/>}
          value={email}
          onChange={setEmail}
        />
        <div className={'flex flex-col items-end gap-1 w-full'}>
          <DPasswordInput
            label={'Password'}
            icon={<IconLock size={18}/>}
            value={password}
            onChange={setPassword}
          />
          <Button
            component={Link}
            to={'/auth/forgot-password'}
            compact
            variant={'white'}
            className={'font-normal'}
            size={'xs'}
          >
            Mot de passe oublié ?
          </Button>
        </div>
        <Button
          radius={'xl'}
          variant={'filled'}
          className={'font-normal'}
          fullWidth
          onClick={login}
        >
          Se connecter
        </Button>
        <Divider
          className={'w-full'}
          my={'xs'}
          label={<Text
            size={10}
            weight={'bold'}
            color={'gray'}
          >OU</Text>}
          labelPosition={'center'}
        ></Divider>
        <Button
          radius={'xl'}
          variant={'outline'}
          fullWidth
          style={{fontWeight: 500}}
          onClick={goToOAuthLogin}
        >
          Se connecter avec Aitux Security
        </Button>
      </Card>
    </div>
  )
}

export default AuthLoginIndex
