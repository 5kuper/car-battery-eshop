import { defineStore } from 'pinia'
import { type UserInfo } from '@/api/batteries/contracts/authApiContracts'
import { authApi } from '@/api/batteries/authApi'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as UserInfo | null,
  }),
  getters: {
    isAuthenticated: (state) => !!state.user,
    isAdmin: (state) => state.user?.isAdmin,
  },
  actions: {
    async fetchUser() {
      this.user = await authApi.getUserInfo()
    },
    async login(username: string, password: string) {
      const result = await authApi.login({ username, password })
      localStorage.setItem('token', result.token)
      await this.fetchUser()
    },
    async register(username: string, password: string) {
      const result = await authApi.register({ username, password })
      localStorage.setItem('token', result.token)
      await this.fetchUser()
    },
    logout() {
      localStorage.removeItem('token')
      this.user = null
    },
  },
})
