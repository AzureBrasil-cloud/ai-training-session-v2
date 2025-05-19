<script setup lang="ts">
import { inject, onBeforeMount, onMounted, ref } from "vue";
import axios from 'axios';
import type {Order} from "@/models/order.ts";
import HelpButton from "@/components/common/HelpButton.vue";
import OrderWindow from "@/components/common/OrderWindow.vue";
import { auth } from "@/utils/auth";
import type { IOffcanvas } from "@/plugins/offcanvas";

const $offcanvas = inject<IOffcanvas>('$offcanvas');
const _data = ref<Order[]>();
const isEditMode = ref(false);
const selected = ref<string | null>(null);
const form = ref<Order>();

const userEmail = ref("");
const userRole = ref("");

const userIsAdmin = auth.userIsAdmin();

const handleOpenOrderWindow = () => {
  isEditMode.value = false;
  selected.value = null;
  form.value = {
    id: '',
    createdAt: null,
    totalValue: null,
    userEmail: userEmail.value,
    size: 1,
    extras: []
  };
  $offcanvas?.show("order");
}

function getSizeLabel(size: number): string {
  switch (size) {
    case 1: return 'Pequeno'
    case 2: return 'Médio'
    case 3: return 'Grande'
    default: return 'Desconhecido'
  }
}

async function fetchData(page: number = 1) {
  const url = userRole.value === "admin"
    ? "/api/orders"
    : `/api/orders?userEmail=${userEmail.value}`

  const response = await axios.get<Order[]>(url);
  _data.value = response.data;
}

onBeforeMount(() => {
  const loggedUser = JSON.parse(sessionStorage.getItem("loggedUser") || "{}");

  if (loggedUser.hasOwnProperty("email")) {
    userEmail.value = loggedUser.email;
    userRole.value = loggedUser.role;
  }
});

onMounted(async () => {
  await fetchData();
});

const videoUrl = `${window.location.origin}/videos/list-create-order.mp4`;
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
          style="width: 100%; height: auto;"
      ></video>
    </div>

    <h5 class="mb-3">Descritivo da Página de Pedidos de Açaí</h5>
    <p>
      Esta página permite <strong>visualizar e criar pedidos de açaí</strong>, com funcionalidades diferentes para administradores e usuários comuns. A interface oferece uma tabela com os pedidos existentes e um formulário intuitivo para criar novos pedidos diretamente na aplicação.
    </p>

    <h6 class="mt-4">Funcionalidades</h6>
    <ul>
      <li><strong>Listagem de pedidos:</strong> Exibe todos os pedidos registrados, com informações como data, usuário, valor, tamanho e adicionais escolhidos.</li>
      <li><strong>Permissões por perfil:</strong>
        <ul>
          <li><strong>Administradores:</strong> Conseguem visualizar todos os pedidos do sistema.</li>
          <li><strong>Usuários:</strong> Visualizam apenas os pedidos feitos com seu próprio e-mail.</li>
        </ul>
      </li>
      <li><strong>Criação de pedidos:</strong> Usuários logados podem clicar em <code>Criar</code> e realizar um novo pedido de forma guiada.</li>
    </ul>

    <h6 class="mt-4">Como funciona a Criação de Pedidos</h6>
    <p>
      Ao iniciar um novo pedido, o usuário deve escolher o <strong>tamanho do açaí</strong> (Pequeno, Médio ou Grande) e pode selecionar <strong>opcionalmente adicionais</strong> como M&Ms, Leite ninho, Granola ou Paçoca.
    </p>
    <p>
      Os preços são calculados dinamicamente:
    </p>
    <ul>
      <li><strong>Pequeno:</strong> R$5,00</li>
      <li><strong>Médio:</strong> R$7,50</li>
      <li><strong>Grande:</strong> R$10,00</li>
      <li><strong>Adicional:</strong> R$2,00 por item</li>
    </ul>
    <p> O valor total é exibido em tempo real antes da confirmação. Após salvar, o pedido é enviado para a API e aparece na tabela.
    </p>

    <h6 class="mt-4">Objetivo</h6>
    <p>
      Esta funcionalidade oferece uma forma rápida, prática e visual de realizar pedidos, mantendo controle centralizado e visibilidade tanto para clientes quanto para administradores do sistema.
    </p>
  </HelpButton>

  <div class="row g-3 px-4 py-4 justify-content-between">
    <div class="col">
      <h4 class="fw-semibold mb-1 item-purple s-h3">
        <i class="bi bi-chat-square-dots px-2"></i>Lista de pedidos de açaí
      </h4>
    </div>
    <div v-if="!userIsAdmin" class="col-12 col-sm-auto">
      <div class="hstack gap-2">
        <button type="button" class="btn btn-purple w-100 w-sm-auto" @click="handleOpenOrderWindow">
          Criar <i class="bi bi-plus-square p-1"></i>
        </button>
      </div>
    </div>
  </div>

  <hr class="mt-6 mb-0" />

  <div class="table-responsive" style="overflow-x: auto;">
    <table class="table table-hover table-nowrap w-100">
      <thead class="table-light text-start">
      <tr>
        <th scope="col" class="colum-title" style="width: 15%;">Data</th>
        <th scope="col" class="colum-title" style="width: 20%;">Usuário</th>
        <th scope="col" class="colum-title" style="width: 15%;">Valor</th>
        <th scope="col" class="colum-title" style="width: 15%;">Tamanho</th>
        <th scope="col" class="colum-title" style="width: 45%;">Extras</th>
      </tr>
      </thead>
      <tbody>
      <tr class="text-start" v-for="a in _data" :key="a.id">
        <td><span v-date="a.createdAt"></span></td>
        <td><span v-text="a.userEmail"></span></td>
        <td><span v-money="a.totalValue"></span></td>
        <td>{{ getSizeLabel(a.size) }}</td>
        <td>
          <ul class="mb-0 ps-3">
            <li
              v-for="extra in a.extras"
              :key="extra"
              class="text-white px-3 m-1 rounded-pill d-inline-block"
              style="background-color: #531A54;"
            >
              {{ extra }}
            </li>
          </ul>
        </td>
      </tr>
      </tbody>
    </table>
  </div>

  <OrderWindow
    :is-edit-mode="isEditMode"
    v-model="form"
    @fetch-data="fetchData"
  />

</template>
