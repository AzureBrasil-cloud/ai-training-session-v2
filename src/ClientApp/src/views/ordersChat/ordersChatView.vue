<script setup lang="ts">
import ChatWindow from '@/components/common/ChatWindow.vue';
</script>

<template>
  <ChatWindow
    :agentType="2"
    title="Assistente de informação sobre os pedidos dos clientes"
    welcomeMessage="👋 Olá! eu sou o assistente virtual de pedidos. Como posso ajudar você hoje?"
  />
</template>
