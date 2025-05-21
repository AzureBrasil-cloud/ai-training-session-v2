<script setup lang="ts">
import { EXTRA_OPTIONS, SIZE_PRICES, TOPPING_PRICE } from '@/constants/order';
import type { Order } from '@/models/order';
import type { PreOrderAudio } from '@/models/preOrderAudio';
import type { IOffcanvas } from '@/plugins/offcanvas';
import { auth } from '@/utils/auth';
import { capitalize } from '@/utils/capitalize';
import axios from 'axios';
import { computed, inject, onBeforeMount, ref, watchEffect } from 'vue';

const preOrderAudioRef = ref<string>("");

const $offcanvas = inject<IOffcanvas>('$offcanvas');

const model = defineModel<Order>();

const isAdmin = auth.userIsAdmin();

const selectedSize = ref(1);

const props = defineProps<{
  isEditMode: boolean;
  preOrderAudio?: PreOrderAudio;
}>();

const emit = defineEmits<{
  (event: 'fetchData'): Promise<void>;
}>();

const totalPrice = computed(() => {
  if (!model.value) return 0
  const sizeValue = SIZE_PRICES[model.value.size] || 0
  const extrasValue = (model.value.extras?.length || 0) * TOPPING_PRICE
  return sizeValue + extrasValue
})

async function save() {
  await axios.post(`/api/orders`, model.value);
  await emit("fetchData");
  $offcanvas?.close();
  selectedSize.value = 1;
}

watchEffect(() => {
  preOrderAudioRef.value = props.preOrderAudio?.content ?? "";
})

const handleSizeClick = (event: MouseEvent) => {
  const target = event.currentTarget as HTMLElement;
  const sizeValue = target.getAttribute('data-size-value');
  if (sizeValue) {
    selectedSize.value = parseInt(sizeValue);
    model!.value!.size = selectedSize.value;
  }
};

const handleSelectChange = (event: any) => {
  const selectedValue = event.target.value;
  if (selectedValue) {
    selectedSize.value = parseInt(selectedValue);
    model!.value!.size = selectedSize.value;
  }
}

watchEffect(() => {
  if (model.value) {
    selectedSize.value = model.value.size;
  }
});

const handleCloseOffcanvas = () => {
  $offcanvas?.close();
  selectedSize.value = 1;
}

const sizeAcai = `${window.location.origin}/images/size-acai-white.svg`;
</script>

<template>
<Offcanvas name="order" :title="isEditMode ? 'Editar Pedido' : 'Novo Pedido'">
    <div class="row g-3">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title fw-bold"><i class="bi bi-box-seam fs-md-1"></i> Pedido solicitado</h5>
            <p class="card-subtitle text-body-secondary text-sm mb-4">
              <span class="fw-bold">Informações do pedido:</span> <br />
              <hr class="mt-3 mb-3 mb-0" />
              {{ preOrderAudioRef }}
            </p>
          </div>
        </div>
      </div>

      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title fw-bold"><i class="bi bi-currency-dollar"></i>Valores
            </h5>
            <p class="card-subtitle text-body-secondary text-sm mb-4 fw-bold">
              Escolha o tamanho e os adicionais
            </p>
            <div class="list-group list-group-borderless gap-2 list-group-flush">
              <div data-size-value="1" @click="handleSizeClick" class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div :class="['icon icon-shape text-lg text-primary', selectedSize === 1 ? 'border-purple bg-body-custom' : 'bg-body-secondary']">
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
              <div data-size-value="2" @click="handleSizeClick" class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div :class="['icon icon-shape text-lg text-primary', selectedSize === 2 ? 'border-purple bg-body-custom' : 'bg-body-secondary']">
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
              <div data-size-value="3" @click="handleSizeClick" class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div :class="['icon icon-shape text-lg text-primary', selectedSize === 3 ? 'border-purple bg-body-custom' : 'bg-body-secondary']">
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

              <hr />

              <div class="list-group-item">
                <div class="d-flex align-items-center">
                  <div class="me-4">
                    <div class="icon icon-shape text-lg bg-body-secondary text-primary">
                      <i class="bi bi-plus-circle-dotted text-white"></i>
                    </div>
                  </div>
                  <div class="flex-fill">
                    <span class="d-block h6 fw-semibold mb-1 stretched-link">Cada
                      adicional</span>
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
            <h5 class="card-title pb-2 fw-bold"><i class="bi bi-arrows-vertical"></i>Tamanho</h5>
            <select @change="handleSelectChange" class="form-select form-select-sm" required>
              <option disabled hidden value="">Selecione o tamanho</option>
              <option :selected="selectedSize === 1" :value="1">Pequeno</option>
              <option :selected="selectedSize === 2" :value="2">Médio</option>
              <option :selected="selectedSize === 3" :value="3">Grande</option>
            </select>
          </div>
        </div>
      </div>

      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <div class="mb-4">
              <h5 class="card-title pb-2 fw-bold"><i
                class="bi bi-plus-circle-dotted"></i> Adicionais</h5>
              <div class="form-check d-inline-block me-3" v-for="extra in EXTRA_OPTIONS"
                   :key="extra">

                <input
                  class="form-check-input"
                  type="checkbox"
                  :id="extra"
                  :value="extra"
                  v-model="model!.extras"
                  :checked="model?.extras?.includes(extra)"
                />
                <label class="form-check-label" :for="extra">{{ capitalize(extra) }}</label>
              </div>
            </div>
            <small :class="['form-text', isAdmin ? 'bg-body-secondary text-white' : 'bg-gray-100 text-black', 'rounded-2', 'p-2', 'px-4']">Selecione zero ou mais
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
          </div>
        </div>
      </div>

    </div>

    <template #footer>
      <div class="d-flex align-items-center bg-body-secondary justify-content-between justify-content-end gap-2 py-4 px-8 border-top">
        <button type="button" class="btn btn-sm btn-neutral" @click="handleCloseOffcanvas">Cancelar
        </button>
        <button type="button" class="btn btn-sm btn-purple" @click="save">
          Salvar
        </button>
      </div>
    </template>
  </Offcanvas>
</template>
