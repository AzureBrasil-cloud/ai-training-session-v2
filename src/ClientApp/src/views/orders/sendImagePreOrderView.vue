<script setup lang="ts">
import {ref, onBeforeMount} from 'vue';
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

    <h2 class="mb-5 mt-8"><i class="bi bi-images px-2"></i> Descritivo da Página de Pré-Pedido via Imagem</h2>
    <p>
      Esta página permite que o usuário envie uma <strong>imagem contendo informações de um
      pedido</strong> para ser processada automaticamente. É possível fotografar um rascunho de
      pedido, uma ficha impressa ou qualquer anotação legível.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-list-task px-2"></i> Funcionalidades</h5>
      <ul>
        <li><strong>Upload de imagem:</strong> O usuário pode enviar arquivos nos formatos <code>.jpg</code>
          ou <code>.png</code>, com limite de 5MB.
        </li>
        <li><strong>Validação automática:</strong> A aplicação verifica o tipo e o tamanho do
          arquivo antes do envio.
        </li>
        <li><strong>Associação automática:</strong> O email do usuário logado é incluído no envio de
          forma automática.
        </li>
        <li><strong>Mensagens de erro e sucesso:</strong> Informações visuais ajudam o usuário a
          entender o status do envio.
        </li>
      </ul>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-asterisk px-2"></i> Como Funciona o Pré-Pedido por Imagem</h5>
      O processo se inicia quando o usuário envia uma imagem por meio de um <strong>POST na Web
      API</strong>. A imagem é analisada utilizando o serviço <strong>Azure Document
      Intelligence</strong>, que realiza a extração automática e estruturada dos dados presentes no
      arquivo.
    </p>
    <p>
      Após a extração, as informações relevantes são organizadas como <strong>pares
      chave-valor</strong> e salvas em um <strong>banco de dados in-memory</strong>, utilizado
      temporariamente para fins de demonstração e testes.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-bullseye px-2"></i> Objetivo</h5>
      Esta funcionalidade permite uma maneira prática e rápida de registrar pedidos usando imagens,
      ideal para ambientes informais ou registros físicos que precisam ser digitalizados e
      estruturados.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-link-45deg px-2"></i> Links Úteis</h5>
      <ul>
        <li>
          <a href="https://azure.microsoft.com/pt-br/products/ai-services/ai-document-intelligence"
             target="_blank" rel="noopener">
            Azure AI Document Intelligence – Visão Geral
          </a>
        </li>
      </ul>
    </p>
  </HelpButton>

  <div
    class="d-flex flex-column flex-lg-row justify-content-center align-items-stretch bg-light px-3 pt-3 pt-lg-0">

    <!-- Título (Mobile) -->
    <div
      class="d-flex d-lg-none align-items-center bg-purple text-white rounded shadow-sm p-4 mb-3 w-100"
      style="max-width: 500px;">
      <h1 class="display-6 fw-bold text-start text-white m-0">
        <i class="bi bi-images me-2"></i>
        Fazer pré-pedido<br>via <u>imagem</u>
      </h1>
    </div>

    <!-- Título (Desktop) -->
    <div
      class="m-lg-2 d-none d-lg-flex align-items-center px-5 py-5 bg-purple text-white rounded shadow-sm"
      style="width: 50%; height: 98vh;">
      <h1 class="display-5 fw-bold text-start text-white">
        <i class="bi bi-images px-2" style="height: 50px; display: block;"></i><br>
        Fazer<br>
        pré-pedido<br>
        via <u>imagem</u>
      </h1>
    </div>

    <!-- Upload -->
    <div
      class="position-relative bg-white rounded shadow-sm d-flex flex-column justify-content-center align-items-center w-100 px-3 py-4 py-lg-5 m-0 m-lg-2"
      style="max-width: 100%;">
      <div class="w-100" style="max-width: 500px;">

        <div v-if="successMessage" class="alert alert-success mb-4 d-flex align-items-center">
          <i class="bi bi-check-circle-fill me-2 fs-4"></i>
          <div>{{ successMessage }}</div>
        </div>
        <div v-if="error" class="alert alert-danger mb-4">
          <i class="bi bi-x-circle-fill me-2 fs-4"></i>
          {{ error }}<br/>
          <a href="https://onlinepngtools.com/resize-png" target="_blank">Clique aqui para
            redimensionar sua imagem</a>
        </div>

        <!-- Dropzone -->
        <div
          class="card shadow-none border border-2 border-dashed position-relative mb-4"
          :class="isDragOver ? 'border-primary' : 'border-primary-hover'"
          @dragover.prevent="onDragOver"
          @dragleave.prevent="onDragLeave"
          @drop.prevent="onDrop"
        >
          <label for="file-upload"
                 class="w-100 h-100 d-flex justify-content-center align-items-center flex-column px-5 py-6"
                 role="button" style="cursor: pointer;">
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
