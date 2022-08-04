import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import './index.css'
import { BrowserRouter } from 'react-router-dom'
import { Provider, useDispatch, useStore } from 'react-redux'
import { AuthProvider } from 'oidc-react'
import { store } from './app/store'
import { setAuthentication } from './components/auth/authenticationSlice'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <BrowserRouter>
    <Provider store={store}>
      <AuthProvider
        autoSignIn={false}
        authority={import.meta.env.VITE_OAUTH_AUTHORIZE_URL}
        clientId={import.meta.env.VITE_OAUTH_CLIENT_ID}
        clientSecret={import.meta.env.VITE_OAUTH_CLIENT_SECRET}
        redirectUri={import.meta.env.VITE_OAUTH_REDIRECT_URI}
        responseType={'code'}
        onBeforeSignIn={() => {
          console.log('onBeforeSignIn')
        }
        }
      >
        <App/>
      </AuthProvider>
    </Provider>
  </BrowserRouter>
)
