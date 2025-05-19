<script setup lang="ts">
const emit = defineEmits(['close'])
</script>

<template>
  <div class="modal d-block fade show" tabindex="-1" style="background-color: rgba(0,0,0,0.5);">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Ajuda</h5>
          <button type="button" class="btn-close" @click="emit('close')"></button>
        </div>
        <div class="modal-body">
          <slot />
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" @click="emit('close')">Fechar</button>
        </div>
      </div>
    </div>
  </div>
</template>
