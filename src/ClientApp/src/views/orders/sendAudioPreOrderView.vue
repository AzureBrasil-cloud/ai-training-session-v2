<script setup lang="ts">
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";

const fileInput = ref<File | null>(null);
const error = ref('');
const isSubmitting = ref(false);
const userEmail = ref('');

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");
  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
  }
});

function handleFileChange(e: Event) {
  error.value = '';
  const target = e.target as HTMLInputElement;
  if (target.files && target.files[0]) {
    const file = target.files[0];

    if (file.type !== 'audio/mpeg') {
      error.value = 'Formato inválido. Envie apenas arquivos .mp3';
      fileInput.value = null;
      return;
    }

    fileInput.value = file;
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
    (document.getElementById('file-upload') as HTMLInputElement).value = '';
  } catch (err) {
    error.value = 'Erro ao enviar o arquivo. Tente novamente mais tarde.';
  } finally {
    isSubmitting.value = false;
  }
}
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

    <h5 class="mb-3">Descritivo da Página de Pré-Pedido via Áudio</h5>
    <p>
      Esta página permite que usuários realizem um <strong>pré-pedido de açaí por meio do envio de um arquivo de áudio no formato MP3</strong>. A proposta é simplificar o processo de pedido utilizando a voz.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Envio de áudio:</strong> O usuário pode selecionar e enviar um arquivo de áudio <code>.mp3</code> contendo seu pedido falado.</li>
      <li><strong>Validação de formato:</strong> Apenas arquivos com a extensão <code>.mp3</code> são aceitos. Outros formatos são bloqueados com aviso ao usuário.</li>
      <li><strong>Associação automática:</strong> O e-mail do usuário logado é recuperado automaticamente e associado ao pré-pedido.</li>
      <li><strong>Feedback visual:</strong> Exibe mensagens de erro e sucesso durante o processo de envio.</li>
    </ul>

    <h6 class="mt-4">O que acontece após o envio</h6>
    <p>
      Após o envio, o arquivo de áudio é processado pela API de backend. O sistema utiliza o serviço <strong>Azure AI Speech</strong> para transcrever automaticamente o conteúdo falado em texto.
    </p>
    <p>
      O conteúdo textual resultante é salvo junto ao pré-pedido no banco de dados <strong>in-memory</strong>, ficando disponível para análises e ações futuras por parte dos administradores.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade promove acessibilidade e praticidade, permitindo que usuários façam pedidos por voz em vez de preencher formulários tradicionais.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/pt-br/azure/ai-services/speech-service/overview" target="_blank" rel="noopener">
          Azure AI Speech – Visão Geral
        </a>
      </li>
    </ul>
  </HelpButton>
  <div class="container py-4">
    <h3 class="mb-4">Fazer pré-pedido via áudio</h3>

    <div class="mb-3">
      <label for="file-upload" class="form-label">Selecionar áudio (.mp3)</label>
      <input type="file" class="form-control" id="file-upload" accept="audio/mpeg" @change="handleFileChange">
    </div>

    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <button class="btn btn-primary" :disabled="isSubmitting" @click="handleSubmit">
      {{ isSubmitting ? 'Enviando...' : 'Enviar áudio' }}
    </button>
  </div>
</template>

<style scoped>
.container {
  max-width: 500px;
}
</style>
