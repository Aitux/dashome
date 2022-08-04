import { Api } from '../../../../api'
import { LoginResult } from '../models/login'

export class AuthApi extends Api {
  constructor() {
    super('auth')
  }

  async login(email: string, password: string): Promise<LoginResult> {
    const response = await this.client.post('/login', {
      email,
      password,
    })
    return response.data
  }
}

export const authApi = new AuthApi()
