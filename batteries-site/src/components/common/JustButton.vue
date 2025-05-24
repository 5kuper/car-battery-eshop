<script lang="ts" setup>
const colorVariants: { [key: string]: string } = {
  blue: 'bg-blue-600 hover:bg-blue-700',
  red: 'bg-red-600 hover:bg-red-700',
  green: 'bg-green-600 hover:bg-green-700',
}

const props = withDefaults(
  defineProps<{
    text: string
    type?: 'button' | 'submit' | 'reset'
    disabled?: boolean
    loading?: boolean,
    color?: string
  }>(), { type: 'button', color: 'blue' })

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
    :class="`px-4 py-2 rounded-xl text-white disabled:opacity-50 transition ${colorVariants[color]}`">
    {{ text }}
  </button>
</template>
