<script setup lang="ts">
import {ref, onBeforeMount} from 'vue';
import axios from 'axios';
import HelpButton from "@/components/common/HelpButton.vue";

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
    error.value = 'Erro ao enviar a avaliação. Tente novamente mais tarde.';
  } finally {
    isSubmitting.value = false;
  }
}

const videoUrl = `${window.location.origin}/videos/create-review.mp4`;
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

    <h5 class="mb-3">Descritivo da Página de Envio de Avaliações</h5>
    <p>
      Esta página permite que os usuários do sistema enviem avaliações sobre os pedidos de <strong>açaí</strong>
      realizados. A interface é simples e direta, composta por um campo de texto onde o usuário pode
      descrever sua experiência e um botão para enviar a avaliação.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Envio de avaliações:</strong> O usuário pode compartilhar sua opinião sobre o
        pedido realizado.
      </li>
      <li><strong>Feedback visual:</strong> A aplicação exibe mensagens de sucesso ou erro durante o
        envio da avaliação.
      </li>
      <li><strong>Identificação automática:</strong> O email do usuário logado é preenchido
        automaticamente com base nas informações da sessão.
      </li>
    </ul>

    <h6 class="mt-4">Processo de Classificação Técnica</h6>
    <p>
      Ao submeter uma avaliação, o conteúdo textual enviado é analisado por um modelo de linguagem
      baseado no <strong>Azure OpenAI</strong>, utilizando a API de <em>Chat Completions</em>. Com o
      auxílio de técnicas de <strong>prompt engineering</strong>, a mensagem é interpretada e
      classificada automaticamente.
    </p>
    <p>
      Essa classificação segue a enumeração abaixo:
    </p>
    <ul>
      <li><strong>1 – Muito Ruim</strong> (<em>VeryBad</em>): experiência extremamente negativa.
      </li>
      <li><strong>2 – Ruim</strong> (<em>Bad</em>): abaixo do esperado.</li>
      <li><strong>3 – Neutra</strong> (<em>Neutral</em>): sem destaques positivos ou negativos.</li>
      <li><strong>4 – Boa</strong> (<em>Good</em>): experiência positiva.</li>
      <li><strong>5 – Muito Boa</strong> (<em>VeryGood</em>): experiência excelente.</li>
      <li><strong>6 – Desconhecida</strong> (<em>Unknown</em>): classificação ausente ou inválida.
      </li>
    </ul>

    <h6 class="mt-4">Armazenamento Temporário</h6>
    <p>
      Após a classificação, tanto o conteúdo da avaliação quanto a categoria atribuída são salvos em
      um <strong>banco de dados in-memory</strong>, utilizado para fins demonstrativos e não
      persistentes.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      O objetivo principal desta funcionalidade é capturar o sentimento dos usuários em relação aos
      pedidos realizados, fornecendo subsídios para melhorias no atendimento e na qualidade do
      produto.
    </p>

    <h6 class="mt-4">Links Úteis</h6>
    <ul>
      <li>
        <a href="https://learn.microsoft.com/pt-br/azure/ai-services/openai/overview"
           target="_blank" rel="noopener">
          Azure OpenAI – Visão Geral
        </a>
      </li>
      <li>
        <a
          href="https://learn.microsoft.com/pt-br/dotnet/ai/conceptual/zero-shot-learning#few-shot-learning"
          target="_blank" rel="noopener">
          Few-Shot Learning – Documentação .NET + IA
        </a>
      </li>
    </ul>
  </HelpButton>


  <div class="d-flex justify-content-center align-items-center bg-light px-3" style="min-height: 100vh;">

    <div class="me-4 d-none d-lg-flex align-items-center px-8 bg-purple text-white rounded shadow-sm p-5" style="height: 98%; width: 50%">
      <h1 class="display-5 fw-bold text-start text-white">
        <i class="bi bi-card-checklist px-2" style="height: 50px; display: block;"></i><br>
        Enviar a<br>
        <u>avaliação</u> <br>
        sobre o açaí<br>
      </h1>
    </div>

    <div class="position-relative p-4 rounded shadow-sm bg-white d-flex flex-column justify-content-center align-items-center" style="width: 100%; height: 98%;">

      <div class="w-100" style="max-width: 500px;">

        <div v-if="error" class="alert alert-danger mb-4"><i class="bi bi-x-circle-fill me-2 fs-4"></i>{{ error }}</div>
        <div v-if="success" class="alert alert-success mb-4"><i class="bi bi-check-circle-fill me-2 fs-4"></i>Avaliação enviada com sucesso!</div>

        <div class="mb-4">
          <label for="avaliacao-text" class="form-label fw-semibold"><i class="bi bi-pen px-3"></i>Sua avaliação</label>
          <textarea
            id="avaliacao-text"
            class="form-control"
            rows="5"
            v-model="reviewText"
            placeholder="Escreva aqui o que achou do açaí..."
            :disabled="isSubmitting"
          ></textarea>
        </div>

        <button class="btn btn-purple w-100" @click="submitReview" :disabled="isSubmitting">
          <i class="bi bi-send me-2" v-if="!isSubmitting"></i>
          {{ isSubmitting ? 'Enviando...' : 'Enviar Review' }}
        </button>
      </div>
    </div>
  </div>

</template>

<style scoped>
.container {
  max-width: 600px;
}
</style>
