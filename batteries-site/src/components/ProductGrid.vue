<script setup lang="ts">
import ProductCard from './ProductCard.vue'

import type Product from '@/models/product'

defineProps<{
  products: Product[]
}>()
</script>

<template>
  <div class="product-grid">
    <ProductCard v-for="product in products" :key="product.id" :product="product" />
  </div>
</template>

<style scoped>
.product-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
  padding: 20px;
  justify-content: start;
}
</style>
