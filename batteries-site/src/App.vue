<script setup lang="ts">
import SiteHeader from './components/SiteHeader.vue'
import AuthModal from './components/modals/AuthModal.vue';
</script>

<template>
  <SiteHeader />
  <main>
    <RouterView />

    <AuthModal />
  </main>
</template>

<style scoped></style>
