<script setup lang="ts">
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";

const fileInput = ref<File | null>(null);
const error = ref('');
const successMessage = ref('');
const isSubmitting = ref(false);
const userEmail = ref('');
const fileName = ref('');

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");

  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
  }
})
;

const isDragOver = ref(false);

function onDragOver() {
  isDragOver.value = true;
}

function onDragLeave() {
  isDragOver.value = false;
}

function onDrop(e: DragEvent) {
  isDragOver.value = false;
  error.value = '';

  const file = e.dataTransfer?.files[0];
  if (file) {
    processFile(file);
  }
}

function handleFileChange(e: Event) {
  error.value = '';
  const target = e.target as HTMLInputElement;
  if (target.files && target.files[0]) {
    processFile(target.files[0]);
  }
}

function processFile(file: File) {
  const validTypes = ['image/jpeg', 'image/png'];

  if (!validTypes.includes(file.type)) {
    error.value = 'Formato inválido. Envie apenas arquivos .jpg ou .png';
    fileInput.value = null;
    fileName.value = '';
    return;
  }

  if (file.size > 5 * 1024 * 1024) {
    error.value = 'O arquivo é maior que 5MB. Reduza o tamanho em https://onlinepngtools.com/resize-png';
    fileInput.value = null;
    fileName.value = '';
    return;
  }

  fileInput.value = file;
  fileName.value = file.name;
}

async function handleSubmit() {
  if (!fileInput.value) {
    error.value = 'Selecione um arquivo válido antes de enviar.';
    return;
  }

  isSubmitting.value = true;
  error.value = '';

  const formData = new FormData();
  formData.append('userEmail', userEmail.value);
  formData.append('file', fileInput.value);

  try {
    await axios.post('/api/pre-order/image', formData);
    successMessage.value = 'Pré-pedido enviado com sucesso!';

    fileInput.value = null;
    fileName.value = ''; // <-- Limpa o nome do arquivo
    (document.getElementById('file-upload') as HTMLInputElement).value = '';
  } catch (err) {
    error.value = 'Erro ao enviar o arquivo. Tente novamente mais tarde.';
  } finally {
    isSubmitting.value = false;
  }
}
const videoUrl = `${window.location.origin}/videos/create-image.mp4`;
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

    <h5 class="mb-3">Descritivo da Página de Pré-Pedido via Imagem</h5>
    <p>
      Esta página permite que o usuário envie uma <strong>imagem contendo informações de um pedido</strong> para ser processada automaticamente. É possível fotografar um rascunho de pedido, uma ficha impressa ou qualquer anotação legível.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Upload de imagem:</strong> O usuário pode enviar arquivos nos formatos <code>.jpg</code> ou <code>.png</code>, com limite de 5MB.</li>
      <li><strong>Validação automática:</strong> A aplicação verifica o tipo e o tamanho do arquivo antes do envio.</li>
      <li><strong>Associação automática:</strong> O email do usuário logado é incluído no envio de forma automática.</li>
      <li><strong>Mensagens de erro e sucesso:</strong> Informações visuais ajudam o usuário a entender o status do envio.</li>
    </ul>

    <h6 class="mt-4">Como Funciona o Pré-Pedido por Imagem</h6>
    <p>
      O processo se inicia quando o usuário envia uma imagem por meio de um <strong>POST na Web API</strong>. A imagem é analisada utilizando o serviço <strong>Azure Document Intelligence</strong>, que realiza a extração automática e estruturada dos dados presentes no arquivo.
    </p>
    <p>
      Após a extração, as informações relevantes são organizadas como <strong>pares chave-valor</strong> e salvas em um <strong>banco de dados in-memory</strong>, utilizado temporariamente para fins de demonstração e testes.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade permite uma maneira prática e rápida de registrar pedidos usando imagens, ideal para ambientes informais ou registros físicos que precisam ser digitalizados e estruturados.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://azure.microsoft.com/pt-br/products/ai-services/ai-document-intelligence" target="_blank" rel="noopener">
          Azure AI Document Intelligence – Visão Geral
        </a>
      </li>
    </ul>
  </HelpButton>

  <div class="d-flex justify-content-center align-items-center bg-light px-3" style="min-height: 100vh;">

    <div class="me-4 d-none d-lg-flex align-items-center px-8 bg-purple text-white rounded shadow-sm p-5" style="height: 98%; width: 50%">
      <h1 class="display-5 fw-bold text-start text-white">
        <i class="bi bi-images px-2" style="height: 50px; display: block;"></i><br>
        Fazer<br>
        pré-pedido<br>
        via <u>imagem</u>
      </h1>
    </div>

    <div class="position-relative p-4 rounded shadow-sm bg-white d-flex flex-column justify-content-center align-items-center" style="width: 100%; height: 98%;">

      <div class="w-100" style="max-width: 500px;">
        <div v-if="successMessage" class="alert alert-success mb-4 d-flex align-items-center">
          <i class="bi bi-check-circle-fill me-2 fs-4"></i>
          <div>{{ successMessage }}</div>
        </div>
        <div v-if="error" class="alert alert-danger mb-4">
          <i class="bi bi-x-circle-fill me-2 fs-4"></i>
          {{ error }}<br />
          <a href="https://onlinepngtools.com/resize-png" target="_blank">Clique aqui para redimensionar sua imagem</a>
        </div>

        <!-- Dropzone com clique e drag-and-drop -->
        <div
          class="card shadow-none border border-2 border-dashed position-relative mb-4 border-primary-hover"
          :class="isDragOver ? 'border-primary' : 'border'"
          @dragover.prevent="onDragOver"
          @dragleave.prevent="onDragLeave"
          @drop.prevent="onDrop"
        >
          <!-- LABEL cobre toda a área e ativa o input ao clicar -->
          <label for="file-upload" class="w-100 h-100 d-flex justify-content-center align-items-center flex-column px-5 py-6" role="button" style="cursor: pointer;">
            <input
              type="file"
              id="file-upload"
              class="visually-hidden"
              accept="image/png, image/jpeg"
              @change="handleFileChange"
            />

            <div class="text-center">
              <div class="text-2xl text-muted">
                <i class="bi bi-upload"></i>
              </div>
              <div class="d-flex text-sm mt-3 justify-content-center">
                <p class="fw-semibold mb-0">Clique para enviar ou arraste a imagem aqui</p>
              </div>
              <p class="text-xs text-body-secondary mb-0">
                Apenas PNG ou JPG. Máximo 5MB.
              </p>
              <p v-if="fileName" class="text-sm text-primary mt-2">
                Arquivo selecionado: {{ fileName }}
              </p>
            </div>
          </label>
        </div>


        <button class="btn btn-purple w-100" :disabled="isSubmitting" @click="handleSubmit">
          <i class="bi bi-send me-2" v-if="!isSubmitting"></i>
          {{ isSubmitting ? 'Enviando...' : 'Enviar imagem' }}
        </button>
      </div>

    </div>
  </div>


</template>

<style scoped>
.container {
  max-width: 500px;
}
</style>
