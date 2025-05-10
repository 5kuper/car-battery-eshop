import httpClient from './httpClient'
import type { LoginRequest, RegisterRequest, AuthResponse } from './contracts/authApiContracts'

export const login = async (data: LoginRequest): Promise<AuthResponse> => {
  const response = await httpClient.post<AuthResponse>('/api/Auth/login', null, { params: data })
  return response.data
}

export const register = async (data: RegisterRequest): Promise<AuthResponse> => {
  const response = await httpClient.post<AuthResponse>('/api/Auth/register', null, { params: data })
  return response.data
}
