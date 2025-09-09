<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter()

function openAuth() {
  router.push({ query: { modal: 'auth' } })
}
</script>

<template>
  <header class="site-header">
    <div class="header-container">
      <RouterLink to="/" class="logo flex items-center mr-5">
        <span class="logo-icon" aria-hidden="true"></span>
        <h1 class="text-3xl">Флагман</h1>
      </RouterLink>

      <nav class="main-nav mt-2">
        <ul>
          <li>
            <RouterLink to="/catalog">Каталог</RouterLink>
          </li>
          <li>
            <RouterLink to="/contacts">Контакты</RouterLink>
          </li>
        </ul>
      </nav>

      <div class="flex flex-row space-x-5 ml-auto">
        <!-- <RouterLink to="/cart" class="icon-btn">
          <span class="material-symbols-outlined"> shopping_cart </span>
          <span class="cart-count">{{ cartCount }}</span>
        </RouterLink> -->
        <div class="icon-btn" @click="openAuth" @keydown.enter="openAuth" tabindex="0" role="button">
          <span class="material-symbols-outlined cursor-pointer">account_circle</span>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200&display=swap');

.site-header {
  --hover-color: #FF7518;

  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 0.5rem 0;
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

.logo {
  color: #333;
  transition: color .2s ease-in-out;
}

.logo:hover,
.logo:focus {
  color: var(--hover-color);
}

.logo-icon {
  width: 50px;
  height: 50px;
  display: inline-block;
  background-color: currentColor;
  -webkit-mask: url('../assets/logo.svg') no-repeat center / contain;
  mask: url('../assets/logo.svg') no-repeat center / contain;
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

.main-nav a:hover,
.main-nav a:focus {
  color: var(--hover-color);
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

.icon-btn:hover,
.icon-btn:focus {
  color: var(--hover-color);
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
