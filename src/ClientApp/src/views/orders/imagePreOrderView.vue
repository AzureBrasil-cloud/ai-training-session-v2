<script setup lang="ts">
import { ref, onMounted, inject } from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";
import type { IOffcanvas } from '@/plugins/offcanvas';
import type { Order } from '@/models/order';
import type { PreOrderImage } from '@/models/preOrderImage';
import ImageOrderWindowPreOrder from '@/components/common/ImageOrderWindowPreOrder.vue';

const videoUrl = `${window.location.origin}/videos/pre-order-image.mp4`;

const data = ref<PreOrderImage[]>([]);
const isLoading = ref(false);
const $offcanvas = inject<IOffcanvas>('$offcanvas');
const isEditMode = ref(false);
const selected = ref<string | null>(null);
const form = ref<Order>();
const orderData = ref<PreOrderImage>();

onMounted(fetchPreOrders);

async function fetchPreOrders() {
  isLoading.value = true;
  try {
    const response = await axios.get<PreOrderImage[]>('/api/pre-order/image');
    data.value = response.data;
  } catch (error) {
    console.error('Erro ao buscar os pedidos de imagem:', error);
  } finally {
    isLoading.value = false;
  }
}

const handleOpenOrderWindow = async (id: string, aiTransformation: boolean) => {
  let url = `/api/pre-order/image/${id}`;

  if (aiTransformation) {
    url += '?applyAiTransformation=true';
  }

  const response = await axios.get<PreOrderImage>(url);
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

    <h5 class="mb-3">Descritivo da Página de Pré-Pedidos por Imagem</h5>
    <p>
      Esta página apresenta a <strong>lista de pré-pedidos realizados a partir de imagens enviadas pelos usuários</strong>. Cada entrada mostra dados como email do remetente, nome da imagem, extensão do arquivo e ações que podem ser realizadas pelo administrador.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Listagem de pré-pedidos:</strong> Exibe todos os arquivos de imagem submetidos para análise.</li>
      <li><strong>Campos exibidos:</strong>
        <ul>
          <li><strong>Email do usuário:</strong> Identifica o remetente da imagem.</li>
          <li><strong>Nome da imagem:</strong> Nome do arquivo enviado.</li>
          <li><strong>Extensão:</strong> Tipo da imagem (ex: <code>.png</code>, <code>.jpg</code>).</li>
        </ul>
      </li>
      <li><strong>Ações disponíveis:</strong>
        <ul>
          <li><strong>Ver:</strong> Permite ao administrador visualizar os dados extraídos da imagem e montar um pedido manualmente.</li>
          <li><strong>Ver com AI:</strong> Ativa o processamento com inteligência artificial para gerar o pedido automaticamente com base nas informações extraídas.</li>
        </ul>
      </li>
    </ul>

    <h6 class="mt-4">Como Funciona o Pré-Pedido por Imagem</h6>
    <p>
      O processo se inicia quando o usuário envia uma imagem por meio de um POST na Web API. A imagem é analisada utilizando o serviço <strong>Azure Document Intelligence</strong>, que realiza a extração automática e estruturada dos dados presentes no arquivo.
    </p>
    <p>
      Após a extração, as informações relevantes são organizadas como pares chave-valor e salvas em um banco de dados <strong>in-memory</strong>, utilizado temporariamente para fins de demonstração e testes.
    </p>

    <h6 class="mt-4">Criação de Pedidos com AI</h6>
    <p>
      O administrador pode optar por:
    </p>
    <ul>
      <li><strong>Ver:</strong> Acessar manualmente os dados extraídos e registrar o pedido com base nas informações fornecidas.</li>
      <li><strong>Ver com AI:</strong> Utilizar IA para interpretar os dados extraídos e preencher automaticamente os campos do pedido, facilitando o processo e evitando erros manuais.</li>
    </ul>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade visa facilitar o registro de pedidos a partir de documentos físicos, imagens escaneadas ou fotos de anotações, automatizando a leitura e estruturação das informações com apoio da inteligência artificial.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://azure.microsoft.com/pt-br/products/ai-services/ai-document-intelligence" target="_blank" rel="noopener">
          Azure Document Intelligence – Visão Geral
        </a>
      </li>
    </ul>
  </HelpButton>

  <div class="p-4">

    <div class="col">
      <h4 class="fw-semibold mb-1 item-purple s-h3">
        <i class="bi bi-images px-2"></i>Lista de pré-pedidos por imagem
      </h4>
    </div>

    <div v-if="isLoading" class="text-muted mb-3">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Carregando pedidos...
    </div>

    <div class="table-responsive">
      <table class="table table-hover">
        <thead class="table-light">
        <tr>
          <th>Email</th>
          <th>Imagem</th>
          <th>Extensão</th>
          <th>Ações</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in data" :key="item.id">
          <td>{{ item.userEmail }}</td>
          <td>{{ item.imageName }}</td>
          <td>{{ item.imageExtension }}</td>
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

  <ImageOrderWindowPreOrder
    :is-edit-mode="isEditMode"
    v-model="form"
    :pre-order-image="orderData"
  />
</template>
