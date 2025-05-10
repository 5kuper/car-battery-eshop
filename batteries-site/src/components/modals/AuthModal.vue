<script setup lang="ts">
import ModalBase from '@/components/ModalBase.vue';

import JustTabs from '../common/JustTabs.vue';
import JustInput from '../common/JustInput.vue';
import JustButton from '../common/JustButton.vue';

import { reactive, ref } from 'vue';
import { login, register } from '@/api/batteries/authApi';

const loginForm = reactive({
  username: '',
  password: '',
})

const registerForm = reactive({
  username: '',
  password: '',
  confirmPwd: '',
})

const isAuthenticated = ref(!!localStorage.getItem('token'))

async function loginBtn() {
  const result = await login({ username: loginForm.username, password: loginForm.password })
  localStorage.setItem('token', result.token)
  isAuthenticated.value = true
}

async function registerBtn() {
  const result = await register({ username: registerForm.username, password: registerForm.password })
  localStorage.setItem('token', result.token)
  isAuthenticated.value = true
}

function logout() {
  localStorage.removeItem('token')
  isAuthenticated.value = false
}

function clear() {
  loginForm.username = ''
  loginForm.password = ''
  registerForm.username = ''
  registerForm.password = ''
  registerForm.confirmPwd = ''
}
</script>

<template>
  <ModalBase id="auth" @close="clear">
    <div v-if="!isAuthenticated">
      <JustTabs :tabs="['Вход', 'Регистрация']">
        <template #default="{ activeTab }">
          <div v-if="activeTab === 'Вход'" class="space-y-3">
            <JustInput v-model="loginForm.username" label="Имя пользователя" />
            <JustInput v-model="loginForm.password" label="Пароль" type="password" />
            <JustButton text="Войти" @click="loginBtn" />
          </div>
          <div v-if="activeTab === 'Регистрация'" class="space-y-3">
            <JustInput v-model="registerForm.username" label="Имя пользователя" />
            <JustInput v-model="registerForm.password" label="Придумайте пароль" type="password" />
            <JustInput v-model="registerForm.confirmPwd" label="Подтвердите пароль" type="password" />
            <JustButton text="Зарегистрироваться" @click="registerBtn" />
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
