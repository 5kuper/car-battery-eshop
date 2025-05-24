<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router';

const router = useRouter()

const cartCount = computed(() => 5)

function openAuth() {
  router.push({ query: { modal: 'auth' } })
}
</script>

<template>
  <header class="site-header">
    <div class="header-container">
      <RouterLink to="/" class="mr-5">
        <img class="h-[35px] block" src="../assets/logo.svg" alt="Логотип" />
      </RouterLink>

      <!-- <nav class="main-nav">
        <ul>
          <li>
            <RouterLink to="/products">Товары</RouterLink>
          </li>
          <li>
            <RouterLink to="/contact">Контакты</RouterLink>
          </li>
          <li>
            <RouterLink to="/reviews">Отзывы</RouterLink>
          </li>
        </ul>
      </nav> -->

      <div class="flex flex-row space-x-5 ml-auto">
        <!-- <RouterLink to="/cart" class="icon-btn">
          <span class="material-symbols-outlined"> shopping_cart </span>
          <span class="cart-count">{{ cartCount }}</span>
        </RouterLink> -->
        <div class="icon-btn">
          <span @click="openAuth" @keydown.enter="openAuth" class="material-symbols-outlined cursor-pointer"
            tabindex="0" role="button">
            account_circle
          </span>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200&display=swap');

.site-header {
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 1rem 0;
  position: sticky;
  top: 0;
  z-index: 100;
  font-family: 'Roboto', sans-serif;
}

.header-container {
  display: flex;
  align-items: center;
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
}

.main-nav ul {
  list-style: none;
  margin: 0;
  display: flex;
}

.main-nav li {
  margin-right: 1.5rem;
}

.main-nav a {
  color: #333;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.2s ease-in-out;
}

.main-nav a:hover {
  color: #007bff;
}

.material-symbols-outlined {
  font-variation-settings:
    'FILL' 0,
    'wght' 400,
    'GRAD' 0,
    'opsz' 24;
  line-height: 1;
}

.icon-btn {
  color: #555;
  text-decoration: none;
  position: relative;
  transition: color 0.2s ease-in-out;
  display: flex;
  align-items: center;
}

.icon-btn:hover {
  color: #007bff;
}

.cart-count {
  position: absolute;
  top: -0.5rem;
  right: -0.5rem;
  background-color: #dc3545;
  color: #fff;
  font-size: 0.7rem;
  padding: 0.1rem 0.4rem;
  border-radius: 50%;
}
</style>
