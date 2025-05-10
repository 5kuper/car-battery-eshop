<script setup lang="ts">
import type Product from '@/models/product'

defineProps<{
  product: Product
}>()
</script>

<template>
  <div class="product-card">
    <img :src="product.imageUrl" :alt="product.name" class="product-image">

    <div class="product-details">
      <h3 class="product-name">{{ product.name }}</h3>

      <div class="product-tags">
        <span v-for="tag in product.tags" :key="tag" class="product-tag">{{ tag }}</span>
      </div>

      <p class="product-price">{{ product.price }} руб</p>
    </div>
  </div>
</template>

<style scoped>
.product-card {
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease-in-out;
  width: 300px;
}

.product-card:hover {
  transform: scale(1.05);
}

.product-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.product-details {
  padding: 16px;
}

.product-name {
  font-size: 1.2rem;
  margin-bottom: 8px;
}

.product-tags {
  display: flex;
  flex-wrap: wrap;
  margin-bottom: 8px;
}

.product-tag {
  background-color: #f0f0f0;
  padding: 4px 8px;
  border-radius: 4px;
  margin-right: 4px;
  margin-bottom: 4px;
  font-size: 0.8rem;
}

.product-price {
  font-size: 1.1rem;
  font-weight: bold;
  color: #333;
}
</style>
