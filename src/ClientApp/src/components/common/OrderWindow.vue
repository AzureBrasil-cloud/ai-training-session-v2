<script setup lang="ts">
import type { Order } from '@/models/order';
import type { IOffcanvas } from '@/plugins/offcanvas';
import axios from 'axios';
import { computed, inject, watch } from 'vue';

const $offcanvas = inject<IOffcanvas>('$offcanvas');

const model = defineModel<Order>();

defineProps<{
  isEditMode: boolean;
}>();

const emit = defineEmits<{
  (event: 'fetchData'): Promise<void>;
}>();

const extrasOptions = [
  'M&Ms',
  'Leite ninho',
  'Granola',
  'Paçoca'
]

const sizePrices: Record<number, number> = {
  1: 5.00,
  2: 7.50,
  3: 10.00,
}

const toppingPrice = 2.00

const totalPrice = computed(() => {
  if (!model.value) return 0
  const sizeValue = sizePrices[model.value.size] || 0
  const extrasValue = (model.value.extras?.length || 0) * toppingPrice
  return sizeValue + extrasValue
})

async function save() {
  await axios.post(`/api/orders`, model.value);
  await emit("fetchData");
  $offcanvas?.close();
}

</script>

<template>
<Offcanvas name="order" :title="isEditMode ? 'Editar Pedido' : 'Novo Pedido'">
    <div class="row g-5" >

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
          <select class="form-select" v-model.number="model!.size" required>
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
              v-model="model!.extras"
            />
            <label class="form-check-label" :for="extra">{{ extra }}</label>
          </div>
          <small class="form-text text-muted">Selecione zero ou mais adicionais.</small>
        </div>
      </div>

      <!-- Valor total -->
      <div class="col-md-12">
        <div class="alert mt-3" role="alert">
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
