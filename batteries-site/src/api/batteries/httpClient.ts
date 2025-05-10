import axios, {
  type AxiosInstance,
  type InternalAxiosRequestConfig,
  type AxiosResponse,
  AxiosError,
} from 'axios'

const httpClient: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
})

httpClient.interceptors.request.use((cfg: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token')
  if (token) {
    cfg.headers.Authorization = `Bearer ${token}`
  }
  return cfg
})

httpClient.interceptors.response.use(
  (resp: AxiosResponse) => resp,
  (err: AxiosError) => {
    console.error('API error:', err)
    return Promise.reject(err)
  },
)

export default httpClient
