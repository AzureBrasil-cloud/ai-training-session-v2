<script setup lang="ts">
import {useOrganizationStore} from '@/stores/organization';
import UserMenu from './UserMenu.vue';
import AgentsList from '../sidebar/AgentsList.vue';
import RecentChats from '../sidebar/RecentChats.vue';
import {onBeforeMount, ref} from 'vue';

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
            <img :src="logo" alt="..." width="85" />
            <div class="d-grid flex-grow-1 ls-tight text-sm">
              <span class="item-purple fw-semibold ">Contoso Açaí</span>
              <span class="text-truncate text-xs text-body-secondary mt-n1">Web app</span>
            </div>
          </div>
      </div>

      <div class="px-4 py-2 flex-fill overflow-y-auto scrollbar">
        <div v-if="userRole === 'user'" class="vstack gap-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-3">
      <span class="d-block text-sm item-purple fw-semibold bg-light px-3 py-2 rounded-3 w-100">
        USUÁRIO
      </span>
            </div>

            <ul class="navbar-nav navbar-vertical-nav d-flex flex-column mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-3"></i>
                  <span>Meus pedidos</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/send-image-pre-order" class="dropdown-item">
                  <i class="bi bi-images px-3"></i>
                  <span>Pré-pedido (imagem)</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/send-audio-pre-order" class="dropdown-item">
                  <i class="bi bi-music-note-list px-3"></i>
                  <span>Pré-pedido (áudio)</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/send-review" class="dropdown-item">
                  <i class="bi bi-card-checklist px-3"></i>
                  <span>Fazer avaliação</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/info-chat" class="dropdown-item">
                  <i class="bi bi-robot px-3"></i>
                  <span>Assistente de informações</span>
                </RouterLink>
              </li>
            </ul>
          </div>
        </div>


        <div v-else-if="userRole === 'admin'" class="vstack gap-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-3">
      <span class="d-block text-sm item-purple fw-semibold bg-light px-3 py-2 rounded-3 w-100">
        ADMIN
      </span>
            </div>

            <ul class="navbar-nav navbar-vertical-nav d-flex flex-column mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-3"></i>
                  <span>Todos os pedidos</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/image-pre-orders" class="dropdown-item">
                  <i class="bi bi-images px-4"></i>
                  <span>Pré-pedidos (imagem)</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/audio-pre-orders" class="dropdown-item">
                  <i class="bi bi-music-note-list px-3"></i>
                  <span>Pré-pedidos (áudio)</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/reviews" class="dropdown-item">
                  <i class="bi bi-card-checklist px-3"></i>
                  <span>Avaliações</span>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/orders-chat" class="dropdown-item">
                  <i class="bi bi-robot px-3"></i>
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
