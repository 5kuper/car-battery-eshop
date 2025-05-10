import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'

import HomeView from './views/HomeView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/products',
    name: 'products',
    component: HomeView,
  },
  {
    path: '/contact',
    name: 'contact',
    component: HomeView,
  },
  {
    path: '/reviews',
    name: 'reviews',
    component: HomeView,
  },
  {
    path: '/cart',
    name: 'cart',
    component: HomeView,
  },
]

const router = createRouter({
  routes,
  history: createWebHistory(),
})

export default router
