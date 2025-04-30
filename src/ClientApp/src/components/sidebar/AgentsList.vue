<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import type { Agent } from '@/models/agent';
import axios from 'axios';
import type { PaginatedResult } from '@/models/paginatedResult';
import service from '@/services/thread';
import type { Thread } from '@/models/thread';

const agents = ref<PaginatedResult<Agent[]>>();
const router = useRouter();

const fetchAgents = async () => {
  try {
    const response = await axios.get('/api/agents');
    agents.value = response.data;
  } catch (error) {
    console.error('Error fetching agents:', error);
  }
};

const createNewThread = async (agent: Agent) => {
  const thread = await service.create({
    projectId: agent.projectId,
    agentId: agent.id,
  } as Thread);
  if (thread) {
    router.push({ name: 'thread-chat', params: { id: thread.id } });
  }
};

onMounted(async () => {
  await fetchAgents();
});
</script>

<template>
  <div>
    <div class="d-flex align-items-center px-3 px-lg-0 mb-1">
      <span class="d-block text-xs text-body-secondary fw-semibold me-auto">Agentes</span>
    </div>
    <ul class="navbar-nav navbar-vertical-nav gap-0.5 mx-lg-n2">
      <li class="nav-item" v-for="agent in agents?.data" :key="agent.id">
        <a href="#" class="nav-link" @click="createNewThread(agent)">
          <i class="bi bi-chat-square-dots"></i>
          <span>{{ agent.name }}</span>
        </a>
      </li>
    </ul>
  </div>
</template>

<style scoped></style>
