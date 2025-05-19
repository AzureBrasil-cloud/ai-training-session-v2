<script setup lang="ts">
import { ref, onMounted, inject, onBeforeMount } from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";
import type { IOffcanvas } from '@/plugins/offcanvas';
import type { Order } from '@/models/order';
import type { PreOrderAudio } from '@/models/preOrderAudio';
import AudioOrderWindowPreOrder from '@/components/common/AudioOrderWindowPreOrder.vue';

const data = ref<PreOrderAudio[]>([]);
const isLoading = ref(false);
const $offcanvas = inject<IOffcanvas>('$offcanvas');
const isEditMode = ref(false);
const selected = ref<string | null>(null);
const form = ref<Order>();
const orderData = ref<PreOrderAudio>();

const videoUrl = `${window.location.origin}/videos/pre-order-audio.mp4`;

onMounted(fetchPreOrders);

async function fetchPreOrders() {
  isLoading.value = true;
  try {
    const response = await axios.get<PreOrderAudio[]>('/api/pre-order/audio');
    data.value = response.data;
  } catch (error) {
    console.error('Erro ao buscar os pedidos de áudio:', error);
  } finally {
    isLoading.value = false;
  }
}

const handleOpenOrderWindow = async (id: string, aiTransformation: boolean) => {
  let url = `/api/pre-order/audio/${id}`;

  if (aiTransformation) {
    url += '?applyAiTransformation=true';
  }

  const response = await axios.get<PreOrderAudio>(url);
  orderData.value = response.data;

  if (aiTransformation) {
    form.value = {
      id: response.data.id,
      createdAt: new Date(response.data.createdAt),
      totalValue: null,
      userEmail: response.data.userEmail,
      size: response.data.aiTransformedOrder?.size ?? 1,
      extras: response.data.aiTransformedOrder?.extras.map(e => e.toLowerCase()) ?? []
    };
  }
  else {
    form.value = {
      id: response.data.id,
      createdAt: new Date(response.data.createdAt),
      totalValue: null,
      userEmail: response.data.userEmail,
      size: 1,
      extras: []
    };
  }

  isEditMode.value = false;
  selected.value = null;
  $offcanvas?.show("order");
}

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

    <h5 class="mb-3">Descritivo da Página de Pré-Pedidos por Áudio</h5>
    <p>
      Esta página apresenta a <strong>lista de pré-pedidos realizados por usuários a partir de áudios enviados pelos usuários</strong>. Cada entrada representa um pedido potencial capturado por voz e transcrito automaticamente.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Listagem dos pré-pedidos:</strong> Exibe os pré-pedidos enviados por áudio em formato de tabela.</li>
      <li><strong>Campos exibidos:</strong>
        <ul>
          <li><strong>Email do usuário:</strong> Identificação de quem enviou o pedido.</li>
          <li><strong>Nome do áudio:</strong> Nome do arquivo .mp3 enviado.</li>
          <li><strong>Extensão:</strong> Formato do arquivo (<code>.mp3</code>).</li>
        </ul>
      </li>
      <li><strong>Ações disponíveis:</strong>
        <ul>
          <li><strong>Ver:</strong> Permite ao administrador visualizar o conteúdo transcrito e criar manualmente o pedido.</li>
          <li><strong>Ver com AI:</strong> Utiliza a IA da Azure OpenAI para preencher automaticamente os dados estruturados do pedido com base no áudio.</li>
        </ul>
      </li>
    </ul>

    <h6 class="mt-4">Processo de Geração do Pré-Pedido</h6>
    <p>
      O pré-pedido é criado quando um usuário envia um <strong>arquivo de áudio (MP3)</strong> para a Web API. Esse áudio é processado pelo serviço <strong>Azure AI Speech</strong>, que realiza a transcrição do conteúdo falado em texto.
    </p>

    <h6 class="mt-4">Criação de Pedido com Ajuda de IA</h6>
    <p>
      Quando o administrador clica em <strong>"Ver com AI"</strong>, o sistema utiliza o <strong>SDK da Azure OpenAI</strong> em conjunto com a funcionalidade de <strong>Structured Output</strong>. Essa abordagem permite extrair informações estruturadas do conteúdo transcrito, como tamanhos e complementos, etc., automatizando o preenchimento do pedido.
    </p>
    <p>
      Isso reduz erros e acelera o processo de atendimento, oferecendo uma experiência assistida por IA para os operadores do sistema.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      A funcionalidade permite capturar pedidos por voz e dar suporte aos administradores na transformação desses pedidos em ações concretas, seja por preenchimento manual ou automatizado com IA.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/pt-br/azure/ai-services/speech-service/overview" target="_blank" rel="noopener">
          Azure AI Speech – Visão Geral
        </a>
      </li>
      <li>
        <a href="https://learn.microsoft.com/en-us/azure/ai-services/openai/how-to/structured-outputs?tabs=python-secure%2Cdotnet-entra-id&pivots=programming-language-csharp" target="_blank" rel="noopener">
          Azure OpenAI – Structured Outputs
        </a>
      </li>
    </ul>
  </HelpButton>
  <div class="p-4">
    <h3 class="mb-4">Lista de pré-pedidos por áudio</h3>

    <div v-if="isLoading" class="text-muted mb-3">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Carregando pedidos...
    </div>

    <div class="table-responsive">
      <table class="table table-hover">
        <thead class="table-light">
        <tr>
          <th>Email</th>
          <th>Áudio</th>
          <th>Extensão</th>
          <th>Ações</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in data" :key="item.id">
          <td>{{ item.userEmail }}</td>
          <td>{{ item.audioName }}</td>
          <td>{{ item.audioExtension }}</td>
          <td>
            <div class="d-flex gap-2">
              <button class="btn btn-sm btn-primary" @click="() => handleOpenOrderWindow(item.id, false)">Ver</button>
              <button class="btn btn-sm btn-success" @click="() => handleOpenOrderWindow(item.id, true)">Ver com AI</button>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>

  <AudioOrderWindowPreOrder
    :is-edit-mode="isEditMode"
    v-model="form"
    :pre-order-audio="orderData"
  />
</template>
