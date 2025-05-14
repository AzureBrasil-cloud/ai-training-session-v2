<script setup lang="ts">
import { ref, onBeforeMount } from 'vue';
import axios from 'axios';

const userEmail = ref('');
const reviewText = ref('');
const isSubmitting = ref(false);
const error = ref('');
const success = ref(false);

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");
  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
  }
});

async function submitReview() {
  if (!reviewText.value.trim()) {
    error.value = 'Por favor, escreva seu review antes de enviar.';
    return;
  }

  isSubmitting.value = true;
  error.value = '';
  success.value = false;

  try {
    await axios.post('/api/reviews', {
      UserEmail: userEmail.value,
      Content: reviewText.value
    });
    success.value = true;
    reviewText.value = '';
  } catch (err) {
    error.value = 'Erro ao enviar o review. Tente novamente mais tarde.';
  } finally {
    isSubmitting.value = false;
  }
}
</script>

<template>
  <div class="container py-4">
    <h3 class="mb-4">Enviar review sobre o Açaí</h3>

    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-if="success" class="alert alert-success">Review enviado com sucesso!</div>

    <div class="mb-3">
      <label for="review-text" class="form-label">Seu review</label>
      <textarea id="review-text" class="form-control" rows="5" v-model="reviewText" placeholder="Escreva aqui o que achou do açaí..." :disabled="isSubmitting"></textarea>
    </div>

    <button class="btn btn-primary" @click="submitReview" :disabled="isSubmitting">
      {{ isSubmitting ? 'Enviando...' : 'Enviar Review' }}
    </button>
  </div>
</template>

<style scoped>
.container {
  max-width: 600px;
}
</style>
