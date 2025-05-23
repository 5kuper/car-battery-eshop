<script setup lang="ts">
import ProductGrid from './ProductGrid.vue'
import BatteryFormModal from './modals/BatteryFormModal.vue'
import JustButton from './common/JustButton.vue'

import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import type Product from '@/models/product'

import { getBatteries } from '@/api/batteries/batteryApi'
import { type Battery, StartPowerRating, mapBatteryToForm } from '@/api/batteries/contracts/batteryApiContracts'

const router = useRouter()
const authStore = useAuthStore()

let batteries: Battery[]
const products = ref<Product[]>([])
const isFetching = ref(false)

const editingBatteryId = ref<string | undefined>(undefined)
const editingBatteryForm = computed(() => {
  if (!editingBatteryId.value) {
    return undefined
  }
  const battery = batteries.find(p => p.id === editingBatteryId.value)
  if (!battery) {
    return undefined
  }
  return mapBatteryToForm(battery)
})

fetchBatteries()

async function fetchBatteries() {
  isFetching.value = true;
  batteries = await getBatteries()
  isFetching.value = false;
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

function openBatteryAdding() {
  router.push({ query: { modal: 'battery' } })
}

function openBatteryEditing(id: string) {
  editingBatteryId.value = id
  router.push({ query: { modal: 'battery' } })
}

function onBatteryFormClosed() {
  editingBatteryId.value = undefined
}
</script>

<template>
  <BatteryFormModal :battery-id="editingBatteryId" :existing-data="editingBatteryForm" @close="onBatteryFormClosed"
    @update="fetchBatteries" />

  <div class="flex gap-2 mr-5 mt-5 justify-end">
    <JustButton text="Обновить" :loading="isFetching" @click="fetchBatteries" />
    <JustButton v-if="authStore.isAdmin" text="Добавить" @click="openBatteryAdding" />
  </div>

  <ProductGrid :products="products" :allow-editing="authStore.isAdmin!!" @edit="(id) => openBatteryEditing(id)" />
</template>
