<script lang="ts" setup>
import { computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const route = useRoute()
const router = useRouter()

const props = defineProps<{
  id: string
}>()

const emit = defineEmits<{
  (e: 'close'): void
}>()

const show = computed(() => route.query.modal === props.id)

onMounted(() => {
  window.addEventListener('keydown', (event: KeyboardEvent) => {
    if (show.value && event.key === 'Escape') {
      close();
    }
  })
})

const close = () => {
  router.replace({ query: { ...route.query, modal: undefined } })
  emit('close')
};
</script>

<template>
  <transition name="modal-fade">
    <div v-if="show" class="modal-overlay" @click.self="close">
      <div class="modal-content">
        <button class="close-button" @click="close">×</button>
        <slot />
      </div>
    </div>
  </transition>
</template>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 8px;
  position: relative;
  max-width: 500px;
  width: 100%;
}

.close-button {
  position: absolute;
  top: 5px;
  right: 20px;
  background: none;
  border: none;
  font-size: 30px;
  cursor: pointer;
}

.modal-fade-enter-active,
.modal-fade-leave-active {
  transition: opacity 0.3s;
}

.modal-fade-enter-from,
.modal-fade-leave-to {
  opacity: 0;
}
</style>
