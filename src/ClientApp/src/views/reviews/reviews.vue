<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';
import HelpButton from '@/components/common/HelpButton.vue'

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

    <h5 class="mb-3">Descritivo da Página de Avaliações</h5>
    <p>
      A página de <strong>Avaliações</strong> tem como objetivo listar e apresentar todas as avaliações feitas pelos usuários do sistema. Essas avaliações são coletadas com o intuito de fornecer feedback qualitativo e quantitativo sobre os serviços ou produtos oferecidos.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Listagem das avaliações:</strong> Exibe todas as avaliações em formato de tabela.</li>
      <li>
        <strong>Campos apresentados:</strong>
        <ul>
          <li><strong>Email do usuário:</strong> Identifica quem deixou a avaliação.</li>
          <li><strong>Conteúdo da avaliação:</strong> Comentário escrito pelo usuário.</li>
          <li><strong>Classificação numérica:</strong> Nota de 0 a 5 atribuída automaticamente com base na análise do conteúdo.</li>
          <li><strong>Nome da classificação:</strong> Descrição textual da nota (ex: "Bom", "Ruim").</li>
          <li><strong>Data da avaliação:</strong> Indica quando a avaliação foi registrada.</li>
        </ul>
      </li>
      <li><strong>Ajuda contextual:</strong> Um botão de ajuda (“?”) no canto superior direito abre este conteúdo explicativo.</li>
    </ul>

    <h6 class="mt-4">Classificação das Avaliações</h6>
    <p>
      Quando o usuário envia uma avaliação via POST, o sistema utiliza o Azure OpenAI (via Chat Completions API) para interpretar o conteúdo da mensagem e classificá-la automaticamente em uma das categorias pré-definidas.
    </p>
    <p>
      Esse processo de categorização é feito por um modelo de linguagem treinado para compreender sentimentos e contextos, garantindo uma classificação mais precisa com base em linguagem natural.
    </p>

    <h6 class="mt-4">Processamento e Armazenamento</h6>
    <p>
      Após a categorização, tanto o conteúdo da avaliação quanto a categoria atribuída são armazenados em um banco de dados em memória (<em>in-memory</em>), utilizado exclusivamente para fins temporários e demonstrativos.
    </p>
    <p>
      Esta página de Avaliações tem como única responsabilidade exibir os dados armazenados, sem permitir edição ou reclassificação. Todas as informações apresentadas já foram previamente processadas no momento do envio da avaliação.
    </p>

    <h6 class="mt-4">Tipos de Classificação</h6>
    <p>As classificações possíveis são:</p>
    <ul>
      <li><strong>1 – Muito Ruim</strong> (<em>VeryBad</em>): experiência extremamente negativa.</li>
      <li><strong>2 – Ruim</strong> (<em>Bad</em>): abaixo do esperado.</li>
      <li><strong>3 – Neutra</strong> (<em>Neutral</em>): sem destaques positivos ou negativos.</li>
      <li><strong>4 – Boa</strong> (<em>Good</em>): experiência positiva.</li>
      <li><strong>5 – Muito Boa</strong> (<em>VeryGood</em>): experiência excelente.</li>
      <li><strong>6 – Desconhecida</strong> (<em>Unknown</em>): classificação ausente ou inválida.</li>
    </ul>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      A funcionalidade permite que administradores acompanhem a percepção dos usuários sobre o serviço, identifiquem oportunidades de melhoria e reconheçam pontos fortes, promovendo um ciclo contínuo de aperfeiçoamento baseado em feedback real.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/pt-br/azure/ai-services/openai/overview" target="_blank" rel="noopener">
          Azure OpenAI – Visão Geral
        </a>
      </li>
      <li>
        <a href="https://learn.microsoft.com/pt-br/dotnet/ai/conceptual/zero-shot-learning#few-shot-learning" target="_blank" rel="noopener">
          Few-Shot Learning – Documentação .NET + IA
        </a>
      </li>
    </ul>
  </HelpButton>

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
