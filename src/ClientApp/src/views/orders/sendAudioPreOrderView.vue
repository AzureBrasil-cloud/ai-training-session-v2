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
const isDragOver = ref(false);

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");
  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
  }
});

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
  if (file) processFile(file);
}

function handleFileChange(e: Event) {
  error.value = '';
  const target = e.target as HTMLInputElement;
  if (target.files && target.files[0]) {
    processFile(target.files[0]);
  }
}

function processFile(file: File) {
  if (file.type !== 'audio/mpeg') {
    error.value = 'Formato inválido. Envie apenas arquivos .mp3';
    fileInput.value = null;
    fileName.value = '';
    return;
  }

  if (file.size > 3 * 1024 * 1024) {
    error.value = 'O arquivo é maior que 3MB. Por favor, reduza o tamanho.';
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
    await axios.post('/api/pre-order/audio', formData);
    successMessage.value = 'Pré-pedido de áudio enviado com sucesso!';
    fileInput.value = null;
    fileName.value = '';
    (document.getElementById('file-upload') as HTMLInputElement).value = '';
  } catch (err) {
    error.value = 'Erro ao enviar o arquivo. Tente novamente mais tarde.';
  } finally {
    isSubmitting.value = false;
  }
}

const videoUrl = `${window.location.origin}/videos/create-audio.mp4`;
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

    <h2 class="mb-5 mt-8"><i class="bi bi-music-note-list px-2"></i>Descritivo da Página de Pré-Pedido via Áudio</h2>
    <p>
      Esta página permite que usuários realizem um <strong>pré-pedido de açaí por meio do envio de
      um arquivo de áudio no formato MP3</strong>. A proposta é simplificar o processo de pedido
      utilizando a voz.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-list-task px-2"></i>
        Funcionalidades</h5>
      <ul>
        <li><strong>Envio de áudio:</strong> O usuário pode selecionar e enviar um arquivo de áudio
          <code>.mp3</code> contendo seu pedido falado.
        </li>
        <li><strong>Validação de formato:</strong> Apenas arquivos com a extensão <code>.mp3</code>
          são aceitos. Outros formatos são bloqueados com aviso ao usuário.
        </li>
        <li><strong>Associação automática:</strong> O e-mail do usuário logado é recuperado
          automaticamente e associado ao pré-pedido.
        </li>
        <li><strong>Feedback visual:</strong> Exibe mensagens de erro e sucesso durante o processo
          de
          envio.
        </li>
      </ul>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-asterisk px-2"></i>O que
        acontece após o envio</h5>
      Após o envio, o arquivo de áudio é processado pela API de backend. O sistema utiliza o serviço
      <strong>Azure AI Speech</strong> para transcrever automaticamente o conteúdo falado em texto.
      <p>
        O conteúdo textual resultante é salvo junto ao pré-pedido no banco de dados
        <strong>in-memory</strong>, ficando disponível para análises e ações futuras por parte dos
        administradores.
      </p>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-bullseye px-2"></i> Objetivo</h5>
      Esta funcionalidade promove acessibilidade e praticidade, permitindo que usuários façam
      pedidos por voz em vez de preencher formulários tradicionais.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-link-45deg px-2"></i> Links
        Úteis</h5>
      <ul>
        <li>
          <a href="https://learn.microsoft.com/pt-br/azure/ai-services/speech-service/overview"
             target="_blank" rel="noopener">
            Azure AI Speech – Visão Geral
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
        <i class="bi bi-music-note-list me-2"></i>
        Fazer pré-pedido<br>via <u>áudio</u>
      </h1>
    </div>

    <!-- Título (Desktop) -->
    <div
      class="m-lg-2 d-none d-lg-flex align-items-center px-5 py-5 bg-purple text-white rounded shadow-sm"
      style="width: 50%; height: 98vh;">
      <h1 class="display-5 fw-bold text-start text-white">
        <i class="bi bi-music-note-list px-2" style="height: 50px; display: block;"></i>
        <br>
        Fazer<br>
        pré-pedido<br>
        via <u>áudio</u>
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
          {{ error }}
        </div>

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
              accept="audio/mpeg"
              @change="handleFileChange"
            />
            <div class="text-center">
              <div class="text-2xl text-muted">
                <i class="bi bi-mic-fill"></i>
              </div>
              <div class="d-flex text-sm mt-3 justify-content-center">
                <p class="fw-semibold mb-0">Clique para enviar ou arraste o áudio aqui</p>
              </div>
              <p class="text-xs text-body-secondary mb-0">
                Apenas arquivos MP3. Máximo 3MB.
              </p>
              <p v-if="fileName" class="text-sm text-primary mt-2">
                Arquivo selecionado: {{ fileName }}
              </p>
            </div>
          </label>
        </div>

        <button class="btn btn-purple w-100" :disabled="isSubmitting" @click="handleSubmit">
          <i class="bi bi-send me-2" v-if="!isSubmitting"></i>
          {{ isSubmitting ? 'Enviando...' : 'Enviar áudio' }}
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
