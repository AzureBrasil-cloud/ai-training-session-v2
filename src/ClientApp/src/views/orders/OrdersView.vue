<script setup lang="ts">
import { inject, onBeforeMount, onMounted, ref } from "vue";
import axios from 'axios';
import type { IOffcanvas } from '@/plugins/offcanvas';
import type {Order} from "@/models/order.ts";
import { computed } from 'vue'

const $offcanvas = inject<IOffcanvas>('$offcanvas');
const _data = ref<Order[]>();
const isEditMode = ref(false);
const selected = ref<string | null>(null);
const form = ref<Order>();

let userEmail = ref("");
let userRole = ref("");

const sizePrices: Record<number, number> = {
  1: 5.00,
  2: 7.50,
  3: 10.00,
}

const toppingPrice = 2.00

const totalPrice = computed(() => {
  if (!form.value) return 0
  const sizeValue = sizePrices[form.value.size] || 0
  const extrasValue = (form.value.extras?.length || 0) * toppingPrice
  return sizeValue + extrasValue
})

const extrasOptions = [
  'M&Ms',
  'Leite ninho',
  'Granola',
  'Paçoca'
]

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

async function openCreate() {
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
  $offcanvas?.show('order');
}

async function save() {
  await axios.post<Model>(`/api/orders`, form.value);
  await fetchData();
  $offcanvas?.close();
}
const sizeAcai = `${window.location.origin}/images/size-acai.svg`;
</script>

<template>
  <div class="row g-6 p-4 align-items-end justify-content-between">
    <div class="col">
      <h4 class="fw-semibold mb-1 item-purple">Pedidos de açaí</h4>
    </div>
    <div class="col-12 col-sm-auto">
      <div class="hstack gap-2">
        <button type="button" class="btn btn-sm btn-purple" @click="openCreate">
          Criar <i class="bi bi-plus-square p-1"></i>
        </button>
      </div>
    </div>
  </div>

  <hr class="mt-6 mb-0" />

  <div class="table-responsive">
    <table class="table table-hover table-nowrap">
      <thead class="table-light text-start">
      <tr>
        <th scope="col" style="width: 15%;">Data</th>
        <th scope="col" style="width: 20%;">Usuário</th>
        <th scope="col" style="width: 15%;">Valor</th>
        <th scope="col" style="width: 15%;">Tamanho</th>
        <th scope="col" style="width: 45%;">Extras</th>
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

  <Offcanvas name="order" :title="isEditMode ? 'Editar Pedido' : 'Novo Pedido'">
    <div class="row g-3">
      <!-- Valores informativos -->
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title item-purple"><i class="bi bi-currency-dollar"></i>Valores
            </h5>
            <p class="card-subtitle text-body-secondary text-sm mb-4">
              Escolha o tamanho e os adicionais
            </p>
            <div class="list-group list-group-borderless gap-2 list-group-flush">
              <div class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div class="icon icon-shape text-lg bg-body-secondary text-primary">
                      <img :src="sizeAcai" alt="" width="12">
                    </div>
                  </div>
                  <div class="flex-fill">
                    <a href="#" class="d-block h6 fw-semibold mb-1 stretched-link">Pequeno</a>
                    <p class="text-xs text-muted">
                      300 ml
                    </p>
                  </div>
                  <div class="ms-auto text-end">
                    <span class="text-sm text-muted"><strong>$5.00</strong></span>
                  </div>
                </div>
              </div>
              <div class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div class="icon icon-shape text-lg bg-body-secondary text-primary">
                      <img :src="sizeAcai" alt="" width="15">
                    </div>
                  </div>
                  <div class="flex-fill">
                    <a href="#" class="d-block h6 fw-semibold mb-1 stretched-link">Médio</a>
                    <p class="text-xs text-muted">
                      500 ml
                    </p>
                  </div>
                  <div class="ms-auto text-end">
                    <span class="text-sm text-muted"><strong>$7.50</strong></span>
                  </div>
                </div>
              </div>
              <div class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div class="icon icon-shape text-lg bg-body-secondary text-primary">
                      <img :src="sizeAcai" alt="" width="18">
                    </div>
                  </div>
                  <div class="flex-fill">
                    <a href="#" class="d-block h6 fw-semibold mb-1 stretched-link">Grande</a>
                    <p class="text-xs text-muted">
                      800 ml
                    </p>
                  </div>
                  <div class="ms-auto text-end">
                    <span class="text-sm text-muted"><strong>$10.00</strong></span>
                  </div>
                </div>
              </div>
              <div class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div class="icon icon-shape text-lg bg-body-secondary text-primary">
                      <i class="bi bi-plus-circle-dotted item-purple"></i>
                    </div>
                  </div>
                  <div class="flex-fill">
                    <a href="#" class="d-block h6 fw-semibold mb-1 stretched-link">Cada
                      adicional</a>
                  </div>
                  <div class="ms-auto text-end">
                    <span class="text-sm text-muted"><strong>$2.00</strong></span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>


      <!-- Seleção de tamanho -->
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title pb-2 item-purple"><i class="bi bi-arrows-vertical"></i>Tamanho
            </h5>
            <select class="form-select form-select-sm" v-model.number="form!.size" required>
              <option disabled value="">Selecione o tamanho</option>
              <option :value="1">Pequeno</option>
              <option :value="2">Médio</option>
              <option :value="3">Grande</option>
            </select>
          </div>
        </div>
      </div>

      <!-- Seleção de adicionais -->
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <div class="mb-4">
              <h5 class="card-title pb-2 item-purple"><i
                class="bi bi-plus-circle-dotted"></i> Adicionais</h5>
              <div class="form-check d-inline-block me-3" v-for="extra in extrasOptions"
                   :key="extra">
                <input
                  class="form-check-input"
                  type="checkbox"
                  :id="extra"
                  :value="extra"
                  v-model="form!.extras"
                />
                <label class="form-check-label" :for="extra">{{ extra }}</label>
              </div>
            </div>
            <small class="form-text text-muted bg-white rounded-2 p-2">Selecione zero ou mais
              adicionais.</small>
          </div>
        </div>
      </div>

      <!-- Valor total -->
      <div class="col-md-12">
        <div class="d-flex justify-content-between text-white bg-white bg-opacity-10 rounded-3 p-3">
          <div>
            <span class="text-white text-base fw-semibold">Valor do pedido:</span>
          </div>
          <div class="d-flex align-items-end">
            <div class="me-1 display-6">
              <span>${{ totalPrice.toFixed(2) }}</span>
            </div>
            <div class="text-muted text-base fw-semibold">
              <span class="text-white">/ reais</span>
            </div>
          </div>
        </div>
      </div>

    </div>

    <!-- Rodapé -->
    <template #footer>
      <div class="d-flex align-items-center bg-body-secondary justify-content-between justify-content-end gap-2 py-4 px-8 border-top">
        <button type="button" class="btn btn-sm btn-neutral" @click="$offcanvas?.close()">Cancelar
        </button>
        <button type="button" class="btn btn-sm btn-purple" @click="save">
          Salvar
        </button>
      </div>
    </template>
  </Offcanvas>

</template>
