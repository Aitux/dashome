import { createSlice } from '@reduxjs/toolkit'
import { getObject } from '../../tools/local-storage-tools'
import { IUser } from '../../models/user'

const initialState = {
  isAuthenticated: !!localStorage.getItem('token'),
  user: getObject('user') as IUser | null,
  token: localStorage.getItem('token') as string | null,
} as IAuthenticationState

interface IAuthenticationState {
  isAuthenticated: boolean
  user?: IUser
  token?: string
}

const authenticationSlice = createSlice({
  name: 'authentication',
  initialState,
  reducers: {
    setAuthentication: (state, action) => {
      console.log('setAuthentication', action.payload)
      state.isAuthenticated = action.payload.isAuthenticated
      state.token = action.payload.token
      localStorage.setItem('token', action.payload.token)
    },
    clearAuthentication: (state) => {
      state.isAuthenticated = false
      state.user = undefined
      state.token = undefined
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    },
    setUser: (state, action) => {
      state.user = action.payload
      localStorage.setItem('user', JSON.stringify(action.payload))
    }
  }
})

export const { setAuthentication, clearAuthentication, setUser } = authenticationSlice.actions
export default authenticationSlice.reducer
