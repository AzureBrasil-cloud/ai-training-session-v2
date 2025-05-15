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
})
;

function handleFileChange(e: Event) {
  error.value = '';
  const target = e.target as HTMLInputElement;
  if (target.files && target.files[0]) {
    const file = target.files[0];
    const validTypes = ['image/jpeg', 'image/png'];

    if (!validTypes.includes(file.type)) {
      error.value = 'Formato inválido. Envie apenas arquivos .jpg ou .png';
      fileInput.value = null;
      return;
    }

    if (file.size > 3 * 1024 * 1024) {
      error.value = 'O arquivo é maior que 3MB. Reduza o tamanho em https://www2.lunapic.com/';
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
    await axios.post('/api/pre-order/image', formData);
    alert('Pré-pedido enviado com sucesso!');
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

    <h5 class="mb-3">Descritivo da Página de Pré-Pedido via Imagem</h5>
    <p>
      Esta página permite que o usuário envie uma <strong>imagem contendo informações de um pedido</strong> para ser processada automaticamente. É possível fotografar um rascunho de pedido, uma ficha impressa ou qualquer anotação legível.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Upload de imagem:</strong> O usuário pode enviar arquivos nos formatos <code>.jpg</code> ou <code>.png</code>, com limite de 3MB.</li>
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

  <div class="container py-4">
    <h3 class="mb-4">Fazer pré-pedido via imagem</h3>

    <div class="mb-3">
      <label for="file-upload" class="form-label">Selecionar imagem (.jpg ou .png, máx 3MB)</label>
      <input type="file" class="form-control" id="file-upload" accept="image/png, image/jpeg" @change="handleFileChange">
    </div>

    <div v-if="error" class="alert alert-danger">
      {{ error }}<br />
      <a href="https://www2.lunapic.com/" target="_blank">Clique aqui para redimensionar sua imagem</a>
    </div>

    <button class="btn btn-primary" :disabled="isSubmitting" @click="handleSubmit">
      {{ isSubmitting ? 'Enviando...' : 'Enviar imagem' }}
    </button>
  </div>
</template>

<style scoped>
.container {
  max-width: 500px;
}
</style>
