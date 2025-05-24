<script lang="ts" setup>
import { ref } from 'vue'

const file = defineModel<File | null>()

const props = defineProps<{
  id?: string
  defaultPreviewUrl?: string
  label?: string
  error?: string
  disabled?: boolean
}>()

const previewUrl = ref<string | null>(props.defaultPreviewUrl || null)

function handleFileChange(event: Event) {
  const target = event.target as HTMLInputElement
  const newFile = target.files?.[0] || null
  file.value = newFile
  previewUrl.value = newFile ? URL.createObjectURL(newFile) : props.defaultPreviewUrl || null
}
</script>

<template>
  <div class="flex flex-col space-y-1">
    <label :for="id"
      class="cursor-pointer px-4 py-2 max-w-max rounded-xl bg-gray-500 hover:bg-gray-600 transition text-white text-sm">
      {{ label }}
    </label>

    <input :id="id" type="file" accept="image/*" :disabled="disabled" @change="handleFileChange" class="hidden" />
    <img v-if="previewUrl" :src="previewUrl" alt="Preview" class="mt-2 w-full max-h-40 object-contain rounded" />

    <p v-if="error" class="text-sm text-red-500">{{ error }}</p>
  </div>
</template>
