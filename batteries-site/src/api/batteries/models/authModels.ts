export interface UserInfo {
  id: number
  username: string
  isAdmin: boolean
}

export interface LoginRequestData {
  username: string
  password: string
}

export interface RegisterRequestData {
  username: string
  password: string
}

export interface AuthResponseData {
  token: string
}
