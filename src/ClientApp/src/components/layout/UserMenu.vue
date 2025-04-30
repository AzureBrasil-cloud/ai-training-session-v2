<script setup lang="ts">
import { RouterLink } from 'vue-router'
import { useUserStore } from '@/stores/user';

const $store = useUserStore();
</script>

<template>
  <div class="d-flex flex-column gap-2 px-4 pb-4">
    <div class="dropend mx-n2">
      <button type="button"
        class="w-100 px-2 py-2 text-start border-0 bg-transparent shadow-none bg-accent-hover rounded d-flex gap-3 align-items-center"
        data-bs-toggle="dropdown">
        <div class="avatar">
          <img src="https://assets.webpixels.io/img/memoji/memoji-2.svg" alt="..." />
        </div>
        <div class="flex-fill">
          <div class="d-flex align-items-center gap-2">
            <span class="text-sm text-heading fw-semibold">{{ $store.user?.name }}</span>
          </div>
          <span class="d-block text-xs text-muted">{{ $store.user?.email }}</span>
        </div>
      </button>


      <div class="dropdown-menu ">
        <div class="dropdown-header">
          <span class="d-block text-xs text-muted mb-1">Signed in as:</span>
          <span class="d-block text-heading fw-semibold">{{ $store.user?.name }}</span>
        </div>
        <div class="dropdown-divider"></div>
        <a class="dropdown-item" href="#">
          <i class="bi bi-house me-3"></i>Home
        </a>
        <a class="dropdown-item" href="#">
          <i class="bi bi-pencil me-3"></i>Edit profile
        </a>
        <div class="dropdown-divider"></div>
        <RouterLink to="/settings/projects" class="dropdown-item">
          <i class="bi bi-gear me-3"></i>
          <span>Settings</span>
        </RouterLink>
        <a class="dropdown-item" href="#">
          <i class="bi bi-image me-3"></i>Media
        </a>
        <div class="dropdown-divider"></div>
        <a class="dropdown-item" href="/MicrosoftIdentity/Account/SignIn">
          <i class="bi bi-chevron-expand me-3"></i>Trocar Conta
        </a>
        <a class="dropdown-item" href="/MicrosoftIdentity/Account/SignOut">
          <i class="bi bi-person me-3"></i>Sair
        </a>
      </div>

    </div>
  </div>
</template>

<style scoped></style>
