<script setup lang="ts">
import type { Order } from '@/models/order';
import type { PreOrderAudio } from '@/models/preOrderAudio';
import type { IOffcanvas } from '@/plugins/offcanvas';
import { preOrder } from '@/utils/preOrder';
import axios from 'axios';
import { computed, inject, ref, watchEffect } from 'vue';

const preOrderAudioRef = ref<string>("");

const $offcanvas = inject<IOffcanvas>('$offcanvas');

const model = defineModel<Order>();

console.log("model", model);

const props = defineProps<{
  isEditMode: boolean;
  preOrderAudio?: PreOrderAudio;
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

watchEffect(() => {
  preOrderAudioRef.value = props.preOrderAudio?.content ?? "";
})

const sizeAcai = `${window.location.origin}/images/size-acai.svg`;
</script>

<template>
<Offcanvas name="order" :title="isEditMode ? 'Editar Pedido' : 'Novo Pedido'">
    <div class="row g-3">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title item-purple">Pedido solicitado</h5>
            <p class="card-subtitle text-body-secondary text-sm mb-4">
              Informações do pedido: <br />
              {{ preOrderAudioRef }}
            </p>
          </div>
        </div>
      </div>

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

      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title pb-2 item-purple"><i class="bi bi-arrows-vertical"></i>Tamanho</h5>
            <select class="form-select form-select-sm" v-model.number="model!.size" required>
              <option disabled value="">Selecione o tamanho</option>
              <option :value="1">Pequeno</option>
              <option :value="2">Médio</option>
              <option :value="3">Grande</option>
            </select>
          </div>
        </div>
      </div>

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
                  v-model="model!.extras"
                  :checked="model?.extras?.includes(extra.toLowerCase())"
                />
                <label class="form-check-label" :for="extra">{{ extra }}</label>
              </div>
            </div>
            <small class="form-text text-muted bg-white rounded-2 p-2">Selecione zero ou mais
              adicionais.</small>
          </div>
        </div>
      </div>

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
