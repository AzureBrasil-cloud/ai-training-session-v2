<script setup lang="ts">
import {useOrganizationStore} from '@/stores/organization';
import UserMenu from './UserMenu.vue';
import AgentsList from '../sidebar/AgentsList.vue';
import RecentChats from '../sidebar/RecentChats.vue';
import { onBeforeMount, ref } from 'vue';

// const $orgStore = useOrganizationStore();

let userRole = ref("");

onBeforeMount(() => {
  const loggedUser = sessionStorage.getItem("loggedUser");

  userRole.value = loggedUser ? JSON.parse(loggedUser)?.role : "";
});

const logo = `${window.location.origin}/images/logo-acai.png`;
</script>

<template>
  <div class="offcanvas-lg offcanvas-start w-rem-80 w-lg-auto border-end-lg" data-bs-scroll="true"
       tabindex="-1"
       id="sidebarOffcanvas" aria-labelledby="sidebarOffcanvasLabel">
    <nav
      class="sidebar w-100 w-rem-lg-64 d-flex flex-column flex-shrink-0 position-relative z-2 h-100">

      <div class="dropdown-center px-4 py-2 mx-n2 position-relative">
          <div
            class="w-100 px-2 py-2 text-start border-0 bg-transparent shadow-none bg-accent-hover rounded d-flex gap-3 align-items-center"
            data-bs-toggle="dropdown">
            <img :src="logo" class="img-dark" alt="..." width="85" />
            <div class="d-grid flex-grow-1 ls-tight text-sm">
              <span class="item-purple fw-semibold ">Contoso Açaí</span>
              <span class="text-truncate text-xs text-body-secondary mt-n1">Web app</span>
            </div>
          </div>
      </div>

      <div class="px-4 py-2 flex-fill overflow-y-auto scrollbar">
        <div v-if="userRole === 'user'" class="vstack gap-5 py-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
              <span class="d-block text-sm item-purple fw-semibold me-auto">USUÁRIO</span>
            </div>
            <ul class="navbar-nav navbar-vertical-nav gap-1 mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Meus pedidos</span>
                </RouterLink>
                <RouterLink to="/send-image-pre-order" class="dropdown-item">
                  <i class="bi bi-images px-4"></i>
                  <span>Pré-pedido (imagem) </span>
                </RouterLink>
                <RouterLink to="/send-audio-pre-order" class="dropdown-item">
                  <i class="bi bi-music-note-list px-4"></i>
                  <span>Pré-pedido (áudio) </span>
                </RouterLink>
                <RouterLink to="/send-review" class="dropdown-item">
                  <i class="bi bi-card-checklist px-4"></i>
                  <span>Fazer avaliação</span>
                </RouterLink>
                <RouterLink to="/info-chat" class="dropdown-item">
                  <i class="bi bi-robot px-4"></i>
                  <span>Assistente de informações</span>
                </RouterLink>
              </li>
            </ul>
          </div>
        </div>

        <div v-else-if="userRole === 'admin'" class="vstack gap-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
              <span class="d-block text-sm item-purple fw-semibold me-auto">ADMIN</span>
            </div>
            <ul class="navbar-nav navbar-vertical-nav gap-0.5 mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Todos os pedidos</span>
                </RouterLink>
                <RouterLink to="/image-pre-orders" class="dropdown-item">
                  <i class="bi bi-images px-4"></i>
                  <span>Pré-pedidos (imagem)</span>
                </RouterLink>
                <RouterLink to="/audio-pre-orders" class="dropdown-item">
                  <i class="bi bi-music-note-list px-4"></i>
                  <span>Pré-pedidos (áudio)</span>
                </RouterLink>
                <RouterLink to="/reviews" class="dropdown-item">
                  <i class="bi bi-card-checklist px-4"></i>
                  <span>Avaliações</span>
                </RouterLink>
                <RouterLink to="/orders-chat" class="dropdown-item">
                  <i class="bi bi-robot px-4"></i>
                  <span>Assistente de pedidos</span>
                </RouterLink>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <UserMenu/>
    </nav>
  </div>
</template>

<style scoped></style>
