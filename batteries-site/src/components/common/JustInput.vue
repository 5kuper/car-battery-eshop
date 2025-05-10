<script lang="ts" setup>
const modelValue = defineModel<string>()

defineProps<{
  id?: string
  type?: string
  label?: string
  placeholder?: string
  error?: string
  disabled?: boolean
}>()
</script>

<template>
  <div class="flex flex-col space-y-1">
    <label v-if="label" :for="id" class="text-sm font-medium text-gray-700">{{ label }}</label>
    <input :id="id" :type="type" :placeholder="placeholder" v-model="modelValue" :disabled="disabled"
      class="px-4 py-2 rounded-xl border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100" />
    <p v-if="error" class="text-sm text-red-500">{{ error }}</p>
  </div>
</template>
