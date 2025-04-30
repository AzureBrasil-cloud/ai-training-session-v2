<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue';
import { RouterLink } from 'vue-router'
import type { Thread } from '@/models/thread';
import type { PaginatedResult } from '@/models/paginatedResult';
import axios from 'axios';
import eventBus from '@/services/eventBus';

const threads = ref<PaginatedResult<Thread[]>>();

const fetchThreads = async () => {
  try {
    const response = await axios.get('/api/threads');
    threads.value = response.data;
  } catch (error) {
    console.error('Error fetching threads:', error);
  }
};

onMounted(async () => {
  await fetchThreads();
  eventBus.on('ThreadChanged', fetchThreads)
});

onBeforeUnmount(() => {
  eventBus.off('ThreadChanged', fetchThreads)
})
</script>

<template>
  <div>
    <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
      <span class="d-block text-xs text-body-secondary fw-semibold me-auto">Conversas</span>
    </div>
    <ul class="navbar-nav navbar-vertical-nav gap-0.5 mx-lg-n2">
      <li class="nav-item" v-for="t in threads?.data" :key="t.id">
        <RouterLink :to="{ name: 'thread-chat', params: { id: t.id } }" class="nav-link">
          <span>{{ t.firstMessage }}</span>
        </RouterLink>
      </li>
    </ul>
  </div>
</template>

<style scoped></style>
