export interface Response<T> {
  isSuccess: boolean
  data: T
  error?: Error
}

export interface Error {
  code: string
  msg?: string
  details: ErrorDetails[]
}

export interface ErrorDetails {
  id: string
  msg: string
}
