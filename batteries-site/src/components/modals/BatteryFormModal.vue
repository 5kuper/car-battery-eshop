<script lang="ts" setup>
import ModalBase from '@/components/ModalBase.vue'
import JustInput from '@/components/common/JustInput.vue'
import JustSelect from '@/components/common/JustSelect.vue'
import JustImageInput from '../common/JustImageInput.vue'
import JustButton from '@/components/common/JustButton.vue'

import { ref, computed, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import { batteryRequests } from '@/api/batteries/requests/products/batteryRequests'
import { generalProductsRequests } from '@/api/batteries/requests/products/generalProductsRequests'
import { type BatteryForm, StartPowerRating } from '@/api/batteries/models/batteryModels'

const route = useRoute()
const router = useRouter()

const props = defineProps<{
  batteryId?: string
  existingData?: BatteryForm,
  imageUrl?: string
}>()

const emit = defineEmits<{
  (e: 'close'): void
  (e: 'update'): void
}>()

watch(
  () => props.existingData,
  (data) => {
    if (data) form.value = { ...data }
  },
  { immediate: true }
)

const form = ref<BatteryForm>({
  name: '',
  country: undefined,
  inStock: false,
  price: 0,
  warrantyMonths: 0,

  model: undefined,
  specs: {
    voltage: 12,
    capacity: 0,
    startPower: 0,
    startPowerRating: StartPowerRating.EN
  },
  tags: []
})

const imageFile = ref<File | null>(null)

const loading = ref(false)
const error = ref<string | null>(null)

const editing = computed(() => !!props.batteryId)

async function submit() {
  loading.value = true
  error.value = null

  try {
    let id = props.batteryId

    if (id) {
      await batteryRequests.updateBattery(id, form.value)
    } else {
      const result = await batteryRequests.createBattery(form.value)
      id = result.data.id
    }

    if (imageFile.value) {
      await generalProductsRequests.updateImage(id, imageFile.value)
    }

    close(true)
  } catch {
    error.value = 'Ошибка'
  } finally {
    loading.value = false
  }
}

async function deleteBtn() {
  await batteryRequests.deleteBattery(props.batteryId!)
  close(true)
}

function close(updated: boolean) {
  router.replace({ query: { ...route.query, modal: undefined } })
  if (updated) {
    emit('update')
  }
}

function handleClosing() {
  form.value = {
    name: '',
    country: undefined,
    inStock: false,
    price: 0,
    warrantyMonths: 0,

    model: undefined,
    specs: {
      voltage: 12,
      capacity: 0,
      startPower: 0,
      startPowerRating: StartPowerRating.EN
    },
    tags: []
  }
  imageFile.value = null
  emit('close')
}
</script>

<template>
  <ModalBase id="battery" @close="handleClosing">
    <form @submit.prevent="submit" class="space-y-3">
      <h2 class="text-xl font-semibold">{{ editing ? 'Редактировать' : 'Добавить' }} АКБ</h2>

      <JustInput v-model="form.name" id="battery-form-name" label="Название" />
      <div class="flex space-x-3">
        <div class="space-y-2">
          <JustInput v-model="form.specs.capacity" id="battery-form-capacity" label="Ёмкость" type="number" />
          <JustInput v-model="form.specs.voltage" id="battery-form-voltage" label="Напряжение" type="number" />
          <JustInput v-model="form.specs.startPower" id="battery-form-start-power" label="Пусковой ток" type="number" />
          <JustSelect v-model="form.specs.startPowerRating" id="battery-from-start-power-rating"
            label="Стандарт пускового тока" :options="[
              { value: StartPowerRating.SAE, label: 'SAE' },
              { value: StartPowerRating.EN, label: 'EN' },
              { value: StartPowerRating.IEC, label: 'IEC' },
              { value: StartPowerRating.DIN, label: 'DIN' }
            ]" />
          <JustInput v-model="form.price" type="number" label="Цена" />
        </div>
        <div class="pt-1">
          <JustImageInput v-model="imageFile" :default-preview-url="imageUrl" id="battery-form-image"
            label="Выбрать изображение" />
        </div>
      </div>

      <p v-if="error" class="text-red-500">{{ error }}</p>
      <div class="flex justify-end gap-2">
        <JustButton v-if="editing" text="Удалить" color="red" @click="deleteBtn" />
        <JustButton :text="'Применить'" type="submit" :loading="loading" />
      </div>
    </form>
  </ModalBase>
</template>
