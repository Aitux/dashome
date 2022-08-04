import React, { useCallback, useEffect } from 'react'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import { setAuthentication, setUser } from '../authenticationSlice'
import { useAuth } from 'oidc-react'
import { callBackApi } from './api'

const AuthCallback = () => {
  const navigate = useNavigate()
  const [searchParams, _] = useSearchParams()
  const redirectUri = searchParams.get('redirect_uri')
  const dispatch = useDispatch()
  const auth = useAuth()

  const loadUser = useCallback(async () => {
    const response = await callBackApi.getMe()

    dispatch(setUser(response?.user))
  }, [])

  useEffect(() => {
    if(!auth.isLoading) {
      loadUser().catch(console.error)
      console.log('auth callback', auth)

      dispatch(setAuthentication({ isAuthenticated: true, token: auth.userData?.id_token }))

      if (redirectUri) {
        navigate(redirectUri)
      } else {
        navigate('/')
      }
    }
  }, [auth])
  return (
    <div>

    </div>
  )
}

export default AuthCallback
