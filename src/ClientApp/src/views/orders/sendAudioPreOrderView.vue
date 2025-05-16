<script setup lang="ts">
import {ref, onBeforeMount} from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";

const fileInput = ref<File | null>(null);
const error = ref('');
const isSubmitting = ref(false);
const userEmail = ref('');
const fileName = ref('');

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");
  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
  }
});

function handleFileChange(e: Event) {
  error.value = '';
  fileName.value = ''; // limpa nome anterior
  const target = e.target as HTMLInputElement;
  if (target.files && target.files[0]) {
    const file = target.files[0];

    if (file.type !== 'audio/mpeg') {
      error.value = 'Formato inválido. Envie apenas arquivos .mp3';
      fileInput.value = null;
      return;
    }

    fileInput.value = file;
    fileName.value = file.name; // define nome do arquivo
  }
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
    alert('Pré-pedido de áudio enviado com sucesso!');
    fileInput.value = null;
    fileName.value = ''; // limpa o nome do arquivo
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
        style="width: 70%;"
      ></video>
    </div>

    <h5 class="mb-3">Descritivo da Página de Pré-Pedido via Áudio</h5>
    <p>
      Esta página permite que usuários realizem um <strong>pré-pedido de açaí por meio do envio de
      um arquivo de áudio no formato MP3</strong>. A proposta é simplificar o processo de pedido
      utilizando a voz.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
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
      <li><strong>Feedback visual:</strong> Exibe mensagens de erro e sucesso durante o processo de
        envio.
      </li>
    </ul>

    <h6 class="mt-4">O que acontece após o envio</h6>
    <p>
      Após o envio, o arquivo de áudio é processado pela API de backend. O sistema utiliza o serviço
      <strong>Azure AI Speech</strong> para transcrever automaticamente o conteúdo falado em texto.
    </p>
    <p>
      O conteúdo textual resultante é salvo junto ao pré-pedido no banco de dados
      <strong>in-memory</strong>, ficando disponível para análises e ações futuras por parte dos
      administradores.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade promove acessibilidade e praticidade, permitindo que usuários façam
      pedidos por voz em vez de preencher formulários tradicionais.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/pt-br/azure/ai-services/speech-service/overview"
           target="_blank" rel="noopener">
          Azure AI Speech – Visão Geral
        </a>
      </li>
    </ul>
  </HelpButton>


  <div class="d-flex justify-content-center align-items-center bg-light px-3"
       style="min-height: 100vh;">

    <div class="me-4 d-none d-lg-flex align-items-center px-8 bg-purple text-white rounded shadow-sm p-5" style="max-width: 400px; min-height: 400px; width: 500px">
      <h1 class="display-5 fw-bold text-start text-white">
        <i class="bi bi-music-note-list px-2" style="height: 50px; display: block;"></i>
        <br>
        Fazer<br>
        pré-pedido<br>
        via <u>áudio</u>
      </h1>
    </div>

    <div
      class="position-relative p-4 rounded shadow-sm bg-white d-flex flex-column justify-content-center align-items-center"
      style="max-width: 600px; width: 100%; min-height: 400px;">

      <div class="w-100" style="max-width: 500px;">
        <div
          class="card shadow-none border border-2 border-dashed border-primary-hover position-relative mb-4">
          <div class="d-flex justify-content-center px-5 py-6">
            <label for="file-upload" class="stretched-link" role="button">
              <input type="file" class="visually-hidden" id="file-upload" accept="audio/mpeg"
                     @change="handleFileChange">
            </label>
            <div class="text-center">
              <div class="text-2xl text-muted">
                <i class="bi bi-mic-fill"></i>
              </div>
              <div class="d-flex text-sm mt-3 justify-content-center">
                <p class="fw-semibold">Clique para enviar ou arraste o áudio aqui</p>
              </div>
              <p class="text-xs text-body-secondary">
                Apenas arquivos MP3. Máximo 3MB.
              </p>
            </div>
          </div>

          <div v-if="fileName" class="text-center pb-3">
            <i class="bi bi-file-earmark-music me-1"></i> {{ fileName }}
          </div>
        </div>

        <div v-if="error" class="alert alert-danger mb-4">
          {{ error }}
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
