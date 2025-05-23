<script lang="ts" setup>
import ModalBase from '@/components/ModalBase.vue'
import JustInput from '@/components/common/JustInput.vue'
import JustSelect from '@/components/common/JustSelect.vue'
import JustButton from '@/components/common/JustButton.vue'

import { ref, computed, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { createBattery, updateBattery, deleteBattery } from '@/api/batteries/batteryApi'
import { type BatteryForm, StartPowerRating } from '@/api/batteries/contracts/batteryApiContracts'

const route = useRoute()
const router = useRouter()

const props = defineProps<{
  batteryId?: string
  existingData?: BatteryForm
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
  capacity: 0,
  voltage: 12,
  startPower: 0,
  startPowerRating: StartPowerRating.EN,
  price: 0,
})

const loading = ref(false)
const error = ref<string | null>(null)

const editing = computed(() => !!props.batteryId)

async function submit() {
  loading.value = true
  error.value = null
  try {
    if (props.batteryId) {
      await updateBattery(props.batteryId, form.value)
    } else {
      await createBattery(form.value)
    }
    close(true)
  } catch {
    error.value = 'Ошибка'
  } finally {
    loading.value = false
  }
}

async function deleteBtn() {
  await deleteBattery(props.batteryId!)
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
    capacity: 0,
    voltage: 12,
    startPower: 0,
    startPowerRating: StartPowerRating.EN,
    price: 0,
  }
  emit('close')
}
</script>

<template>
  <ModalBase id="battery" @close="handleClosing">
    <form @submit.prevent="submit" class="space-y-4">
      <h2 class="text-xl font-semibold">{{ editing ? 'Редактировать' : 'Добавить' }} АКБ</h2>

      <JustInput v-model="form.name" id="battery-form-name" label="Название" />
      <JustInput v-model="form.capacity" id="battery-form-capacity" label="Ёмкость" type="number" />
      <JustInput v-model="form.voltage" id="battery-form-voltage" label="Напряжение" type="number" />
      <JustInput v-model="form.startPower" id="battery-form-start-power" label="Пусковой ток" type="number" />


      <JustSelect v-model="form.startPowerRating" id="battery-from-start-power-rating" label="Стандарт пускового тока"
        :options="[
          { value: StartPowerRating.SAE, label: 'SAE' },
          { value: StartPowerRating.EN, label: 'EN' },
          { value: StartPowerRating.IEC, label: 'IEC' },
          { value: StartPowerRating.DIN, label: 'DIN' }
        ]" />

      <JustInput v-model="form.price" type="number" label="Цена" />

      <p v-if="error" class="text-red-500">{{ error }}</p>

      <div class="flex justify-end gap-2">
        <JustButton v-if="editing" text="Удалить" color="red" @click="deleteBtn" />
        <JustButton :text="'Применить'" type="submit" :loading="loading" />
      </div>
    </form>
  </ModalBase>
</template>
