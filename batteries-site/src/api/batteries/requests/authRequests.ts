import httpClient from '../httpClient'

import type { Response } from '../response'

import type {
  UserInfo,
  LoginRequestData,
  RegisterRequestData,
  AuthResponseData,
} from '../models/authModels'

export const authRequests = {
  getUserInfo: async (): Promise<Response<UserInfo>> => {
    const result = await httpClient.get<Response<UserInfo>>('/api/Auth/user-info')
    return result.data
  },
  login: async (data: LoginRequestData): Promise<Response<AuthResponseData>> => {
    const result = await httpClient.post<Response<AuthResponseData>>('/api/Auth/login', data)
    return result.data
  },
  register: async (data: RegisterRequestData): Promise<Response<AuthResponseData>> => {
    const result = await httpClient.post<Response<AuthResponseData>>('/api/Auth/register', data)
    return result.data
  },
}
