<script setup lang="ts">
const modelValue = defineModel()

defineProps<{
  id?: string
  options: { value: string | number; label: string }[]
  label?: string
  error?: string
  disabled?: boolean
}>()
</script>

<template>
  <div class="flex flex-col space-y-1">
    <label v-if="label" :for="id" class="text-sm font-medium text-gray-700">{{ label }}</label>
    <select :id="id" v-model="modelValue" :disabled="disabled"
      class="px-4 py-2 rounded-xl border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100">
      <option v-for="option in options" :key="option.value" :value="option.value">
        {{ option.label }}
      </option>
    </select>
    <p v-if="error" class="text-sm text-red-500">{{ error }}</p>
  </div>
</template>
