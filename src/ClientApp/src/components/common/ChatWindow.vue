// src/components/common/ChatWindow.vue
<template>
  <div class="d-flex flex-column p-3" style="height: 95vh;">

    <h4 class="mb-3 item-purple s-h3"><i class="bi bi-robot px-3"></i>{{ title }}</h4>

    <div v-if="isLoading" class="text-muted small mb-2">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Carregando agente ...
    </div>

    <div v-else-if="isWaitingResponse" class="text-muted small mb-2">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Pensando ...
    </div>

    <div class="flex-grow-1 border rounded p-3 mb-3 overflow-auto" style="min-height: 0;">
      <div v-for="(msg, i) in messages" :key="i"
           class="mb-2 p-2 rounded"
           :style="msg.role === 'User' ? userStyle : assistantStyle">
        <div v-html="md.render(msg.content)"></div>
      </div>
    </div>

    <div class="d-flex">
      <input class="form-control me-2" v-model="userInput" @keydown="handleKeyDown" :disabled="isLoading || isWaitingResponse" placeholder="Digite sua mensagem..." />
      <button class="btn btn-outline-secondary me-2" @click="resetThread" :disabled="isLoading">Resetar</button>
      <button class="btn btn-purple" @click="sendMessage" :disabled="isLoading || isWaitingResponse">Enviar</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, defineProps } from 'vue';
import axios from 'axios';
import MarkdownIt from 'markdown-it';

const md = new MarkdownIt();

interface Message {
  role: 'User' | 'Assistant';
  content: string;
}

const props = defineProps<{ agentType: number; title: string; welcomeMessage: string }>();

const isLoading = ref(false);
const isWaitingResponse = ref(false);
const userInput = ref('');
const messages = ref<Message[]>([]);
const agentId = ref('');
const threadId = ref('');

const userStyle = 'max-width: 75%; background-color: #0d6efd; color: white; margin-left: auto;';
const assistantStyle = 'max-width: 75%; background-color: #e6e6e6; color: black;';

async function createAgentAndThread() {
  isLoading.value = true;
  userInput.value = '';
  messages.value = [];

  const agentRes = await axios.post('/api/agents', { Type: props.agentType });
  agentId.value = agentRes.data.id;

  const threadRes = await axios.post('/api/threads');
  threadId.value = threadRes.data.id;

  messages.value.push({ role: 'Assistant', content: props.welcomeMessage });
  isLoading.value = false;
}

async function sendMessage() {
  if (!userInput.value.trim()) return;

  const input = userInput.value;
  messages.value.push({ role: 'User', content: input });
  userInput.value = '';
  isWaitingResponse.value = true;

  const response = await axios.post('/api/agents/run', {
    AgentId: agentId.value,
    ThreadId: threadId.value,
    Message: input
  });

  messages.value.push({ role: 'Assistant', content: response.data.content });
  isWaitingResponse.value = false;
}

async function resetThread() {
  if (isLoading.value) return;
  await createAgentAndThread();
}

function handleKeyDown(event: KeyboardEvent) {
  if (event.key === 'Enter' && !event.shiftKey && !isLoading.value && !isWaitingResponse.value) {
    sendMessage();
  }
}

onMounted(createAgentAndThread);
</script>
