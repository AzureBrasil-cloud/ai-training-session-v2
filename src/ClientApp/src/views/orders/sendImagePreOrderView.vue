<script setup lang="ts">
import { ref } from 'vue';
import axios from 'axios';
import { useUserStore } from '@/stores/user';

const userStore = useUserStore();
const fileInput = ref<File | null>(null);
const error = ref('');
const isSubmitting = ref(false);

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
  formData.append('userEmail', userStore.email);
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
</script>

<template>
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
