import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'

import HomeView from './views/HomeView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/catalog',
    name: 'catalog',
    component: HomeView,
  },
  {
    path: '/contacts',
    name: 'contacts',
    component: HomeView,
  },
]

const router = createRouter({
  routes,
  history: createWebHistory(),
})

export default router
