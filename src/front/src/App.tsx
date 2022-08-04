import './App.css'
import Routes from './routes'
import { createEmotionCache, MantineProvider } from '@mantine/core'
import theme from './theme'
import { AuthProvider, useAuth } from 'oidc-react'
import { useNavigate } from 'react-router-dom'
import AuthLoginIndex from './components/auth/login'

const appendCache = createEmotionCache({ key: 'mantine', prepend: false })

function App() {
  return (
    <div className="App">
        <MantineProvider
          theme={theme}
          withGlobalStyles
        >
          <Routes></Routes>
        </MantineProvider>
    </div>
  )
}

export default App
