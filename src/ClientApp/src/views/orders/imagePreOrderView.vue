<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';

interface PreOrderImage {
  id: string;
  userEmail: string;
  imageName: string;
  imageExtension: string;
  keyValuePairs: string[];
  createdAt: string;
  aiTransformedOrder: string | null;
}

const data = ref<PreOrderImage[]>([]);
const isLoading = ref(false);

async function fetchPreOrders() {
  isLoading.value = true;
  try {
    const response = await axios.get<PreOrderImage[]>('/api/pre-order/image');
    data.value = response.data;
  } catch (error) {
    console.error('Erro ao buscar os pedidos de imagem:', error);
  } finally {
    isLoading.value = false;
  }
}

onMounted(fetchPreOrders);
</script>

<template>
  <div class="p-4">
    <h3 class="mb-4">Lista de pré-pedidos por imagem</h3>

    <div v-if="isLoading" class="text-muted mb-3">
      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
      Carregando pedidos...
    </div>

    <div class="table-responsive">
      <table class="table table-hover">
        <thead class="table-light">
        <tr>
          <th>Email</th>
          <th>Imagem</th>
          <th>Extensão</th>
          <th>Ações</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in data" :key="item.id">
          <td>{{ item.userEmail }}</td>
          <td>{{ item.imageName }}</td>
          <td>{{ item.imageExtension }}</td>
          <td>
            <div class="d-flex gap-2">
              <button class="btn btn-sm btn-outline-primary">Ver</button>
              <button class="btn btn-sm btn-outline-success">Ver com AI</button>
            </div>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
