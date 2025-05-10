<script setup lang="ts">
import { ref } from 'vue'

const props = defineProps<{
  tabs: string[]
  initialTab?: string
}>()

const activeTab = ref(props.initialTab ?? props.tabs[0])
</script>

<template>
  <div>
    <div class="flex space-x-2 border-b mb-4">
      <button v-for="tab in tabs" :key="tab" @click="activeTab = tab" :class="[
        'px-4 py-2 text-sm font-medium rounded-t',
        activeTab === tab
          ? 'bg-white border border-b-0 border-gray-300'
          : 'text-gray-500 hover:text-gray-700'
      ]">
        {{ tab }}
      </button>
    </div>
    <div class="p-4 border border-gray-300 rounded-b">
      <slot :active-tab="activeTab" />
    </div>
  </div>
</template>
