<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';

interface Review {
  id: string;
  userEmail: string;
  content: string;
  classification: number;
  classificationName: string;
  createdAt: string;
}

const data = ref<Review[]>([]);
const isLoading = ref(false);

async function fetchReviews() {
  isLoading.value = true;
  try {
    const response = await axios.get<Review[]>('/api/reviews');
    data.value = response.data;
  } catch (error) {
    console.error('Erro ao buscar os reviews:', error);
  } finally {
    isLoading.value = false;
  }
}

onMounted(fetchReviews);
</script>

<template>
  <div class="p-4">
    <h3 class="mb-4">Lista de reviews</h3>

    <div v-if="isLoading" class="text-muted mb-3">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Carregando reviews...
    </div>

    <div class="table-responsive">
      <table class="table table-hover">
        <thead class="table-light">
        <tr>
          <th>Email</th>
          <th>Conteúdo</th>
          <th>Classificação</th>
          <th>Nome da Classificação</th>
          <th>Data</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in data" :key="item.id">
          <td>{{ item.userEmail }}</td>
          <td>{{ item.content }}</td>
          <td>{{ item.classification }}</td>
          <td>{{ item.classificationName }}</td>
          <td><span v-date="item.createdAt"></span></td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
