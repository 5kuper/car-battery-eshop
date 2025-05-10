<script lang="ts" setup>
const props = defineProps<{
  text: string
  type?: 'button' | 'submit' | 'reset'
  disabled?: boolean
  loading?: boolean
}>()

const emit = defineEmits<{
  (e: 'click', event: MouseEvent): void
}>()

function handleClick(event: MouseEvent) {
  if (!props.disabled) {
    emit('click', event)
  }
}
</script>

<template>
  <button :type="type" :disabled="disabled || loading" @click="handleClick"
    class="px-4 py-2 rounded-xl text-white bg-blue-600 hover:bg-blue-700 disabled:opacity-50 transition">
    <span v-if="!loading">{{ text }}</span>
    <span v-else>Loading...</span>
  </button>
</template>
