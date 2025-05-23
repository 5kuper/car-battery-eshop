export interface UserInfo {
  id: number
  username: string
  isAdmin: boolean
}

export interface LoginRequest {
  username: string
  password: string
}

export interface RegisterRequest {
  username: string
  password: string
}

export interface AuthResponse {
  token: string
}
