<script setup lang="ts">
import ChatWindow from '@/components/common/ChatWindow.vue';
import HelpButton from "@/components/common/HelpButton.vue";
const videoUrl = `${window.location.origin}/videos/order-agent.mp4`;
</script>

<template>
  <HelpButton>
    <div class="d-flex justify-content-center my-4">
      <video
          ref="player"
          :src="videoUrl"
          controls
          loop
          autoplay
          muted
          playsinline
          style="width: 100%;"
      ></video>
    </div>

    <h2 class="mb-5 mt-8"><i class="bi bi-robot px-2"></i> Descritivo da Página do Assistente de Pedidos</h2>
    <p>
      Esta página apresenta um <strong>assistente virtual especializado em informações sobre pedidos</strong>. Ele é voltado para administradores e gestores, permitindo consultar dados qualitativos e quantitativos a respeito das compras realizadas pelos clientes da Contoso Açaí.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-list-task px-2"></i> Funcionalidades</h5>
    <ul>
      <li><strong>Consultas sobre pedidos:</strong> O assistente pode responder perguntas sobre quantidade de pedidos, valores médios, produtos mais vendidos, entre outras métricas.</li>
      <li><strong>Análise de comportamento:</strong> Possibilita entender tendências e comportamentos dos clientes com base no histórico de compras.</li>
      <li><strong>Ferramenta de envio de e-mails:</strong> O agente pode enviar e-mails, com base em solicitações feitas pelos administradores via chat.</li>
      <li><strong>Interface conversacional:</strong> Toda a interação acontece via chat com mensagens em linguagem natural, otimizando a experiência do administrador.</li>
    </ul>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-asterisk px-2"></i> Acesso às Informações</h5>
      Diferente do agente público, este assistente <strong>não acessa documentos em vector store</strong>. Todas as informações que ele utiliza estão <strong>incorporadas diretamente nas instruções do agente</strong> e incluem o histórico de pedidos registrados no sistema.
    </p>

    <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-envelope-arrow-up px-2"></i> Envio de E-mails com Function Calling</h5>
    <p>
      O assistente possui integração com uma ferramenta de envio de e-mails, implementada usando <strong>Function Calling</strong> do Azure AI Agent Service no lado do cliente. Isso permite que o agente ative funções personalizadas, como disparar e-mails para clientes ou membros da equipe.
    </p>
    <p>
      O mecanismo permite controle sobre os parâmetros enviados, garantindo flexibilidade e proteção na execução de tarefas sensíveis.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-bullseye px-2"></i> Objetivo</h5>
      O objetivo deste agente é <strong>auxiliar gestores e administradores</strong> na tomada de decisões baseadas em dados reais, sem a necessidade de acessar painéis complexos ou bancos de dados diretamente.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-link-45deg px-2"></i> Links Úteis</h5>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/en-us/azure/ai-services/agents/overview" target="_blank" rel="noopener">
          Azure AI Agent Service – Visão Geral
        </a>
      </li>
      <li>
        <a href="https://learn.microsoft.com/en-us/azure/ai-services/agents/how-to/tools/function-calling?tabs=python&pivots=overview" target="_blank" rel="noopener">
          Function Calling – Documentação Oficial
        </a>
      </li>
    </ul>
    </p>
  </HelpButton>

  <ChatWindow
    :agentType="2"
    title="Assistente de informação sobre os pedidos dos clientes"
    welcomeMessage="👋 Olá! eu sou o assistente virtual de pedidos. Como posso ajudar você hoje?"
  />
</template>
