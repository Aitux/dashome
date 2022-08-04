const authEndpoint = import.meta.env.VITE_OAUTH_AUTHORIZE_URL

export const getAuthorizeUrl = () => {
  return `${authEndpoint}?response_type=code&client_id=${import.meta.env.VITE_OAUTH_CLIENT_ID}&redirect_uri=${import.meta.env.VITE_OAUTH_REDIRECT_URI}&scope=${import.meta.env.VITE_OAUTH_SCOPE}`
}
