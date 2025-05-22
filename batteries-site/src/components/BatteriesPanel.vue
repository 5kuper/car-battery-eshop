<script setup lang="ts">
import ProductGrid from '@/components/ProductGrid.vue'

import { ref } from 'vue';
import type Product from '@/models/product';
import { getBatteries } from '@/api/batteries/batteryApi';
import { type Battery, StartPowerRating } from '@/api/batteries/contracts/batteryApiContracts';

const products = ref<Product[]>([])

fetchBatteries()

async function fetchBatteries() {
  const batteries = await getBatteries()
  products.value = batteries.map(mapBatteryToProduct)
}

function mapBatteryToProduct(battery: Battery): Product {
  return {
    id: battery.id,
    name: `${battery.name} ${battery.capacity}Ah`,
    imageUrl: battery.imageUrl,
    tags: [`Пусковой ток ${battery.startPower} ${StartPowerRating[battery.startPowerRating]}`, `${battery.voltage}V`],
    price: battery.price,
  };
}
</script>

<template>
  <ProductGrid :products="products" />
</template>
