<script setup lang="ts">
import { computed } from 'vue';
import type { PaginatedResult } from '@/models/paginatedResult';

const _model = defineModel<PaginatedResult>({ default: {} as PaginatedResult }),
  _totalPages = computed(() => _model.value.totalPages >= 10 ? 10 : _model.value.totalPages);

const $emit = defineEmits(["pageChange"]);

function pageChange(page: number) {
  $emit('pageChange', page);
}
</script>

<template>
  <div class="d-flex align-items-center justify-content-between">
    <ul class="nav">
      <li class="nav-item">
        <button class="nav-link" :class="{ disabled: !_model?.hasPreviousPage }"
          @click="pageChange(_model?.pageIndex - 1)">
          <i class="bi bi-arrow-left me-2"></i>Anterior
        </button>
      </li>
    </ul>
    <nav aria-label="Page navigation">
      <ul class="nav nav-pills">
        <li class="nav-item" v-for="i in _totalPages" :key="i">
          <button class="nav-link" :class="{ active: i === _model?.pageIndex, disabled: i === _model?.pageIndex }"
            @click="pageChange(i)">
            {{ i }}
          </button>
        </li>
      </ul>
    </nav>
    <ul class="nav">
      <li class="nav-item">
        <button class="nav-link" :class="{ disabled: !_model?.hasNextPage }" @click="pageChange(_model?.pageIndex + 1)">
          Próxima<i class="bi bi-arrow-right ms-2"></i>
        </button>
      </li>
    </ul>
  </div>
</template>
