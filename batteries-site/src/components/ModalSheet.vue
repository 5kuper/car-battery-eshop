<script setup lang="ts">
import { ref, computed, watch, onMounted, onUnmounted, nextTick } from 'vue'
import { useRouter, useRoute } from 'vue-router'

type Mode = 'modal' | 'sheet'

const props = defineProps<{
  id: string,
  mode?: Mode
}>()

const emit = defineEmits<{
  (e: 'close'): void
  (e: 'is-sheet', value: boolean): void
}>()

const route = useRoute()
const router = useRouter()

const show = computed(() => route.query.modal === props.id)

const isMobile = ref(false)
function useMediaQuery(q: string) {
  let mql: MediaQueryList | null = null
  const apply = () => (isMobile.value = !!mql?.matches)
  if (typeof window !== 'undefined') {
    mql = window.matchMedia(q)
    apply()
    mql.addEventListener('change', apply)
  }
  return () => mql?.removeEventListener('change', apply)
}
const stopMQ = useMediaQuery('(max-width: 768px)')

const effectiveMode = computed<Mode>(() => props.mode ?? (isMobile.value ? 'sheet' : 'modal'))
watch(effectiveMode, (val) => emit('is-sheet', val === 'sheet'), { immediate: true })

const panel = ref<HTMLElement | null>(null)
let lastFocused: HTMLElement | null = null

function close() {
  router.replace({ query: { ...route.query, modal: undefined } })
}

function onKey(e: KeyboardEvent) {
  if (show.value && e.key === 'Escape') {
    e.stopPropagation()
    close()
  }
}

onMounted(() => window.addEventListener('keydown', onKey))
onUnmounted(() => {
  window.removeEventListener('keydown', onKey)
  stopMQ?.()
  if (typeof document !== 'undefined') document.body.classList.remove('no-scroll')
})

watch(show, async (v) => {
  if (typeof document === 'undefined') return
  const b = document.body
  if (v) {
    lastFocused = document.activeElement as HTMLElement | null
    b.classList.add('no-scroll')
    await nextTick()
    panel.value?.focus()
  } else {
    b.classList.remove('no-scroll')
    if (lastFocused && document.contains(lastFocused)) lastFocused.focus()
    lastFocused = null
  }
})

/* --- Sheet swipe --- */

let startY = 0, dy = 0
let allowDrag = false

function inTopZone(e: TouchEvent) {
  const el = panel.value
  if (!el) return false
  const r = el.getBoundingClientRect()
  const y = e.touches[0].clientY
  return y - r.top <= 24
}

function ts(e: TouchEvent) {
  if (effectiveMode.value !== 'sheet') return
  const target = e.target as HTMLElement
  allowDrag = !!target.closest('.grip') || inTopZone(e)
  startY = e.touches[0].clientY
  dy = 0
  const el = panel.value
  if (el) el.style.transform = 'translateY(0)'
  lastFocused = document.activeElement as HTMLElement | null
}

function tm(e: TouchEvent) {
  if (effectiveMode.value !== 'sheet' || !allowDrag) return
  const el = panel.value
  if (!el) return

  dy = e.touches[0].clientY - startY

  const atTop = el.scrollTop <= 0
  if (!atTop) return

  if (dy > 0) {
    if (e.cancelable) e.preventDefault()
    el.style.transform = `translateY(${dy}px)`
  }
}

function te() {
  if (effectiveMode.value !== 'sheet' || !allowDrag) return
  const el = panel.value
  if (!el) return

  const closeIt = dy > 100
  el.style.transition = 'transform 160ms ease'
  el.style.transform = closeIt ? 'translateY(100%)' : 'translateY(0)'
  el.addEventListener('transitionend', () => {
    el.style.transition = ''
    if (closeIt) close()
  }, { once: true })

  dy = 0
  allowDrag = false
}

function tc() {
  const el = panel.value
  if (el) {
    el.style.transition = ''
    el.style.transform = 'translateY(0)'
  }
  dy = 0
  allowDrag = false
}
</script>

<template>
  <transition name="fade" @after-leave="emit('close')">
    <div v-if="show" class="overlay" @click.self="close" aria-hidden="false">
      <section ref="panel" class="panel" :class="effectiveMode === 'sheet' ? 'panel-sheet' : 'panel-modal'"
        role="dialog" aria-modal="true" :aria-labelledby="`${props.id}-title`" tabindex="-1" @touchstart="ts"
        @touchmove="tm" @touchend="te" @touchcancel="tc">
        <div v-if="effectiveMode === 'sheet'" class="grip" aria-hidden="true"></div>
        <button v-if="effectiveMode === 'modal'" class="close" @click="close">×</button>
        <slot />
      </section>
    </div>
  </transition>
</template>

<style scoped>
:global(html) {
  scrollbar-gutter: stable;
}

:global(body.no-scroll) {
  overflow: hidden;
}

.overlay {
  position: fixed;
  inset: 0;
  z-index: 1000;
  display: flex;
  background: rgba(0, 0, 0, .5);
  overscroll-behavior: contain;
}

.panel {
  position: relative;
  background: #fff;
  color: #111;
  outline: none;
  will-change: transform;
}

.panel-modal {
  margin: auto;
  width: min(500px, 92vw);
  max-height: 90vh;
  overflow: auto;
  border-radius: 12px;
  padding: 20px;
}

.panel-sheet {
  margin-top: auto;
  width: 100%;
  max-height: 85vh;
  overflow: auto;
  border-radius: 16px 16px 0 0;
  padding: 16px 20px 20px;
  transform: translateY(0);
  touch-action: pan-y;
  overscroll-behavior: contain;
  -webkit-overflow-scrolling: touch;
}

.grip {
  position: relative;
  width: 48px;
  height: 5px;
  border-radius: 999px;
  background: #e5e7eb;
  margin: 8px auto 6px;
  touch-action: none;
  -webkit-user-select: none;
  user-select: none;
  pointer-events: auto;
}

.grip::before {
  content: "";
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  top: -12px;
  width: 120px;
  height: 32px;
  border-radius: 16px;
}

.close {
  position: absolute;
  top: 8px;
  right: 16px;
  background: none;
  border: 0;
  font-size: 28px;
  cursor: pointer;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity .2s
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0
}

.sheet-enter-active {
  transition: transform .22s ease, opacity .22s
}

.sheet-leave-active {
  transition: transform .18s ease, opacity .18s
}

.sheet-enter-from,
.sheet-leave-to {
  transform: translateY(20px);
  opacity: 0;
}
</style>
