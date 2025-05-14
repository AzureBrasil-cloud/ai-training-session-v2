<script setup lang="ts">
import {useOrganizationStore} from '@/stores/organization';
import UserMenu from './UserMenu.vue';
import AgentsList from '../sidebar/AgentsList.vue';
import RecentChats from '../sidebar/RecentChats.vue';

// const $orgStore = useOrganizationStore();
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
            <img src="https://assets.webpixels.io/img/logos/clever-dark-sm.svg"
                 class="img-dark avatar avatar-sm"
                 alt="..."/>
            <img src="https://assets.webpixels.io/img/logos/clever-light-sm.svg"
                 class="img-light avatar avatar-sm"
                 alt="..."/>
            <div class="d-grid flex-grow-1 ls-tight text-sm">

              <span class="text-truncate text-heading fw-semibold">Contoso Açaí</span>
              <span class="text-truncate text-xs text-body-secondary mt-n1">Web app</span>
            </div>
          </div>
      </div>
      <div class="px-4 py-2 flex-fill overflow-y-auto scrollbar">
        <div class="vstack gap-5 py-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
              <span class="d-block text-xs text-body-secondary fw-semibold me-auto">Usuário</span>
            </div>
            <ul class="navbar-nav navbar-vertical-nav gap-0.5 mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Meus pedidos</span>
                </RouterLink>
                <RouterLink to="/send-image-pre-order" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Pré-pedido via imagem</span>
                </RouterLink>
                <RouterLink to="/info-chat" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Assistente de informações</span>
                </RouterLink>
              </li>
            </ul>
          </div>
        </div>
        <div class="vstack gap-5">
          <div>
            <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
              <span class="d-block text-xs text-body-secondary fw-semibold me-auto">Admin</span>
            </div>
            <ul class="navbar-nav navbar-vertical-nav gap-0.5 mx-lg-n2">
              <li class="nav-item">
                <RouterLink to="/orders" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
                  <span>Todos os pedidos</span>
                </RouterLink>
                <RouterLink to="/orders-chat" class="dropdown-item">
                  <i class="bi bi-chat-square-dots px-4"></i>
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
