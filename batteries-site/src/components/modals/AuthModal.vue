<script setup lang="ts">
import ModalBase from '@/components/ModalSheet.vue';

import JustTabs from '../common/JustTabs.vue';
import JustInput from '../common/JustInput.vue';
import JustButton from '../common/JustButton.vue';

import { reactive } from 'vue';
import { useAuthStore } from '@/stores/authStore';

const authStore = useAuthStore();

const loginForm = reactive({
  username: '',
  password: '',
})

const registerForm = reactive({
  username: '',
  password: '',
  confirmPwd: '',
})

function clear() {
  loginForm.username = ''
  loginForm.password = ''
  registerForm.username = ''
  registerForm.password = ''
  registerForm.confirmPwd = ''
}

async function login() {
  await authStore.login(loginForm.username, loginForm.password)
}

async function register() {
  await authStore.register(loginForm.username, loginForm.password)
}

function logout() {
  authStore.logout()
}
</script>

<template>
  <ModalBase id="auth" @close="clear">
    <div v-if="!authStore.isAuthenticated">
      <JustTabs :tabs="['Вход', 'Регистрация']">
        <template #default="{ activeTab }">
          <div v-if="activeTab === 'Вход'" class="space-y-3">
            <JustInput v-model="loginForm.username" label="Имя пользователя" />
            <JustInput v-model="loginForm.password" label="Пароль" type="password" />
            <JustButton text="Войти" @click="login" />
          </div>
          <div v-if="activeTab === 'Регистрация'" class="space-y-3">
            <JustInput v-model="registerForm.username" label="Имя пользователя" />
            <JustInput v-model="registerForm.password" label="Придумайте пароль" type="password" />
            <JustInput v-model="registerForm.confirmPwd" label="Подтвердите пароль" type="password" />
            <JustButton text="Зарегистрироваться" @click="register" />
          </div>
        </template>
      </JustTabs>
    </div>
    <div v-else class="space-y-2">
      <h1>Вы вошли в аккаунт.</h1>
      <JustButton text="Выйти" @click="logout" />
    </div>
  </ModalBase>
</template>
