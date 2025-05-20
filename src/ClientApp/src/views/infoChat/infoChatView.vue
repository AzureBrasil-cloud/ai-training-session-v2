<script setup lang="ts">
import ChatWindow from '@/components/common/ChatWindow.vue';
import HelpButton from "@/components/common/HelpButton.vue";
const videoUrl = `${window.location.origin}/videos/doc-agent.mp4`;
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

    <h2 class="mb-5 mt-8"><i class="bi bi-robot px-3"></i>Descritivo da Página do Assistente Virtual</h2>
    <p>
      Esta página oferece uma interface de chat onde o usuário pode interagir com um <strong>assistente virtual que possui acesso a informações públicas da Contoso Açaí</strong>. O assistente responde dúvidas, fornece orientações e ajuda os usuários a entender melhor os serviços oferecidos pela empresa.
    </p>

    <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-list-task px-2"></i> Funcionalidades</h5>
    <ul>
      <li><strong>Interface de conversa em tempo real:</strong> O usuário digita mensagens e recebe respostas em linguagem natural do assistente.</li>
      <li><strong>Mensagens com formatação:</strong> O conteúdo pode incluir listas, negrito, links e outros elementos via Markdown.</li>
      <li><strong>Reset de sessão:</strong> A qualquer momento é possível reiniciar a conversa clicando no botão “Resetar”.</li>
    </ul>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-diagram-3-fill px-2"></i> Integração com Azure AI Agent Service</h5>
      O assistente foi criado utilizando o <strong>Azure AI Agent Service</strong>, um serviço da Microsoft que permite criar agentes conversacionais personalizados com integração nativa a ferramentas de IA.
    </p>
    <p>
      Cada vez que um usuário acessa a página, um novo <strong>agente</strong> e uma nova <strong>thread de conversa</strong> são criados via API. As interações subsequentes utilizam esses identificadores para manter o contexto da conversa.
    </p>

    <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-asterisk px-2"></i> Acesso a Conteúdo Privado com Vector Store</h5>
    <p>
      O conhecimento do assistente é alimentado por <strong>documentos armazenados em um vector store hospedado na Azure</strong>. Essa base vetorial foi criada a partir de arquivos contendo informações da Contoso Açaí e permite que o agente acesse conteúdos de forma semântica.
    </p>

    <p>
      A integração entre o agente e o vector store é realizada utilizando a <strong>ferramenta File Search</strong>, que permite buscar e recuperar conteúdos relevantes com base em embeddings e similaridade de vetores.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-bullseye px-2"></i> Objetivo</h5>
      Esta funcionalidade oferece uma experiência inteligente e personalizada de suporte ao cliente, com base em dados reais da empresa. É uma solução moderna para escalar o atendimento com o apoio da inteligência artificial.
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
        <a href="https://learn.microsoft.com/en-us/azure/ai-services/agents/how-to/tools/file-search?tabs=python&pivots=overview#vector-stores" target="_blank" rel="noopener">
          Azure Agents – Vector Stores
        </a>
      </li>
      <li>
        <a href="https://learn.microsoft.com/en-us/azure/ai-services/agents/how-to/tools/file-search?tabs=python&pivots=overview" target="_blank" rel="noopener">
          Azure Agents – File Search
        </a>
      </li>
    </ul>
    </p>
  </HelpButton>
  <ChatWindow
    :agentType="1"
    title="Assistente de informações sobre a Contoso Açaí"
    welcomeMessage="👋 Olá! eu sou o assistente virtual da Contoso Açaí. Como posso ajudar você hoje?"
  />
</template>
