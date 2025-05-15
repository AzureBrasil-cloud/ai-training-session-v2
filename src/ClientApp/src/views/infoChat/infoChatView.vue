<script setup lang="ts">
import ChatWindow from '@/components/common/ChatWindow.vue';
import HelpButton from "@/components/common/HelpButton.vue";
const videoUrl = `${window.location.origin}/videos/video.mp4`;
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
          style="width: 70%;"
      ></video>
    </div>

    <h5 class="mb-3">Descritivo da Página do Assistente Virtual</h5>
    <p>
      Esta página oferece uma interface de chat onde o usuário pode interagir com um <strong>assistente virtual que possui acesso a informações públicas da Contoso Açaí</strong>. O assistente responde dúvidas, fornece orientações e ajuda os usuários a entender melhor os serviços oferecidos pela empresa.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Interface de conversa em tempo real:</strong> O usuário digita mensagens e recebe respostas em linguagem natural do assistente.</li>
      <li><strong>Mensagens com formatação:</strong> O conteúdo pode incluir listas, negrito, links e outros elementos via Markdown.</li>
      <li><strong>Reset de sessão:</strong> A qualquer momento é possível reiniciar a conversa clicando no botão “Resetar”.</li>
    </ul>

    <h6 class="mt-4">Integração com Azure AI Agent Service</h6>
    <p>
      O assistente foi criado utilizando o <strong>Azure AI Agent Service</strong>, um serviço da Microsoft que permite criar agentes conversacionais personalizados com integração nativa a ferramentas de IA.
    </p>
    <p>
      Cada vez que um usuário acessa a página, um novo <strong>agente</strong> e uma nova <strong>thread de conversa</strong> são criados via API. As interações subsequentes utilizam esses identificadores para manter o contexto da conversa.
    </p>

    <h6 class="mt-4">Acesso a Conteúdo Privado com Vector Store</h6>
    <p>
      O conhecimento do assistente é alimentado por <strong>documentos armazenados em um vector store hospedado na Azure</strong>. Essa base vetorial foi criada a partir de arquivos contendo informações da Contoso Açaí e permite que o agente acesse conteúdos de forma semântica.
    </p>

    <p>
      A integração entre o agente e o vector store é realizada utilizando a <strong>ferramenta File Search</strong>, que permite buscar e recuperar conteúdos relevantes com base em embeddings e similaridade de vetores.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade oferece uma experiência inteligente e personalizada de suporte ao cliente, com base em dados reais da empresa. É uma solução moderna para escalar o atendimento com o apoio da inteligência artificial.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
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
  </HelpButton>
  <ChatWindow
    :agentType="1"
    title="Assistente de informações sobre a Contoso Açaí"
    welcomeMessage="👋 Olá! eu sou o assistente virtual da Contoso Açaí. Como posso ajudar você hoje?"
  />
</template>
