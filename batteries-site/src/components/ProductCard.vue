<script setup lang="ts">
import type Product from '@/models/product'

defineProps<{
  product: Product,
  allowEditing: boolean
}>()

const emit = defineEmits<{
  (e: 'edit', id: string): void
}>()
</script>

<template>
  <div class="product-card">
    <img :src="product.imageUrl" :alt="product.name" class="product-image">

    <button v-if="allowEditing" class="edit-button">
      <span class="material-icons" @click="emit('edit', product.id)">edit</span>
    </button>

    <div class="product-details">
      <h3 class="product-name">{{ product.name }}</h3>
      <div class="product-tags">
        <span v-for="tag in product.tags" :key="tag" class="product-tag">{{ tag }}</span>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/icon?family=Material+Icons');

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

.edit-button {
  background-color: #6200ea;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 8px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background-color 0.2s;
  position: absolute;
  top: 10px;
  right: 10px;
}

.edit-button:hover {
  background-color: #7c4dff;
}

.material-icons {
  font-size: 20px;
}
</style>
