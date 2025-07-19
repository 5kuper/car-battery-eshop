import { defineStore } from 'pinia'
import { type UserInfo } from '@/api/batteries/models/authModels'
import { authRequests } from '@/api/batteries/requests/authRequests'

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
      this.user = (await authRequests.getUserInfo()).data
    },
    async login(username: string, password: string) {
      const resp = await authRequests.login({ username, password })
      localStorage.setItem('token', resp.data.token)
      await this.fetchUser()
    },
    async register(username: string, password: string) {
      const resp = await authRequests.register({ username, password })
      localStorage.setItem('token', resp.data.token)
      await this.fetchUser()
    },
    logout() {
      localStorage.removeItem('token')
      this.user = null
    },
  },
})
