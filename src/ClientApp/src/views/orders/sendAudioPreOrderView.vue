<script setup lang="ts">
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';

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
</script>

<template>
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
