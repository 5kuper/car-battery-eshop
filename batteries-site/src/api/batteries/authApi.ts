import httpClient from './httpClient'

import type {
  UserInfo,
  LoginRequest,
  RegisterRequest,
  AuthResponse,
} from './contracts/authApiContracts'

export const authApi = {
  getUserInfo: async (): Promise<UserInfo> => {
    const response = await httpClient.get<UserInfo>('/api/Auth/user-info')
    return response.data
  },
  login: async (data: LoginRequest): Promise<AuthResponse> => {
    const response = await httpClient.post<AuthResponse>('/api/Auth/login', null, {
      params: data,
    })
    return response.data
  },
  register: async (data: RegisterRequest): Promise<AuthResponse> => {
    const response = await httpClient.post<AuthResponse>('/api/Auth/register', null, {
      params: data,
    })
    return response.data
  },
}
