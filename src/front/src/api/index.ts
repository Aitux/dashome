import axios, { AxiosInstance } from 'axios'

export class Api {
  readonly client: AxiosInstance

  constructor(basePath: string) {

    this.client = axios.create({
      baseURL: new URL(basePath, import.meta.env.VITE_API_URL as string).href,
    })

    this.client.interceptors.request.use((config) => {
      const token = localStorage.getItem('token')
      if (token) {
        config.headers = config.headers || {}
        config.headers['Authorization'] = `Bearer ${token}`
      }
      return config
    })
  }
}
