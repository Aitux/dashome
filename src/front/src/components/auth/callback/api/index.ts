import { Api } from '../../../../api'
import { GetMeResult } from '../models/get-me'

const RESOURCE = 'auth'

class CallbackApi extends Api{
  constructor() {
    super(RESOURCE)
  }

  async getMe(): Promise<GetMeResult> {
    return (await this.client.get('/me')).data
  }
}

export const callBackApi = new CallbackApi()
