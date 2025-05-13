<script setup lang="ts">
import { inject, onMounted, ref } from "vue";
import axios from 'axios';
import type { IOffcanvas } from '@/plugins/offcanvas';
import type {Order} from "@/models/order.ts";
import { computed } from 'vue'

const projects = ref<{ id: string; name: string }[]>([]);
const $offcanvas = inject<IOffcanvas>('$offcanvas');
const _data = ref<Order[]>();
const isEditMode = ref(false);
const selected = ref<string | null>(null);
const form = ref<Order>();

//todo: get from user store
const email = "user@user.com";

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
  const response = await axios.get<Order[]>(`/api/orders?userEmail=${email}`);
  _data.value = response.data;
}

onMounted(async () => {
  await fetchData();
});

async function openCreate() {
  debugger;
  isEditMode.value = false;
  selected.value = null;
  form.value = {
    id: '',
    createdAt: null,
    totalValue: null,
    userEmail: email,
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
</script>

<template>
  <div class="row g-6 align-items-end justify-content-between">
    <div class="col">
      <h4 class="fw-semibold mb-1">Modelos</h4>
    </div>
    <div class="col-12 col-sm-auto">
      <div class="hstack gap-2">
        <button type="button" class="btn btn-sm btn-primary" @click="openCreate">
          Criar
        </button>
      </div>
    </div>
  </div>

  <hr class="mt-6 mb-0" />

  <div class="table-responsive">
    <table class="table table-hover table-nowrap">
      <thead class="table-light text-start">
      <tr>
        <th scope="col">Data</th>
        <th scope="col">Valor</th>
        <th scope="col">Tamanho</th>
        <th scope="col">Extras</th>
      </tr>
      </thead>
      <tbody>
      <tr class="text-start" v-for="a in _data" :key="a.id">
        <td><span v-date="a.createdAt"></span></td>
        <td><span v-money="a.totalValue"></span></td>
        <td>{{ getSizeLabel(a.size) }}</td>
        <td>
          <ul class="mb-0 ps-3">
            <li v-for="extra in a.extras" :key="extra">{{ extra }}</li>
          </ul>
        </td>
      </tr>
      </tbody>
    </table>
  </div>

  <Offcanvas name="order" :title="isEditMode ? 'Editar Pedido' : 'Novo Pedido'">
    <div class="row g-5">

      <!-- Valores informativos -->
      <div class="col-md-12">
        <div class="alert alert-light border mb-4">
          <strong>Valores:</strong>
          <ul class="mb-0 ps-3">
            <li>Pequeno: <strong>$5.00</strong></li>
            <li>Médio: <strong>$7.50</strong></li>
            <li>Grande: <strong>$10.00</strong></li>
            <li>Cada adicional: <strong>$2.00</strong></li>
          </ul>
        </div>
      </div>

      <!-- Seleção de tamanho -->
      <div class="col-md-12">
        <div>
          <label class="form-label">Tamanho</label>
          <select class="form-select" v-model.number="form!.size" required>
            <option disabled value="">Selecione o tamanho</option>
            <option :value="1">Pequeno</option>
            <option :value="2">Médio</option>
            <option :value="3">Grande</option>
          </select>
        </div>
      </div>

      <!-- Seleção de adicionais -->
      <div class="col-md-12">
        <div>
          <label class="form-label">Adicionais</label>
          <div class="form-check" v-for="extra in extrasOptions" :key="extra">
            <input
              class="form-check-input"
              type="checkbox"
              :id="extra"
              :value="extra"
              v-model="form!.extras"
            />
            <label class="form-check-label" :for="extra">{{ extra }}</label>
          </div>
          <small class="form-text text-muted">Selecione zero ou mais adicionais.</small>
        </div>
      </div>

      <!-- Valor total -->
      <div class="col-md-12">
        <div class="alert alert-secondary mt-3" role="alert">
          Valor estimado do pedido: <strong>${{ totalPrice.toFixed(2) }}</strong>
        </div>
      </div>

    </div>

    <!-- Rodapé -->
    <template #footer>
      <div class="d-flex align-items-center justify-content-end gap-2 py-4 px-8 bg-body-tertiary border-top">
        <button type="button" class="btn btn-sm btn-neutral" @click="$offcanvas?.close()">Fechar</button>
        <button type="button" class="btn btn-sm btn-primary" @click="save">
          Salvar
        </button>
      </div>
    </template>
  </Offcanvas>

</template>
