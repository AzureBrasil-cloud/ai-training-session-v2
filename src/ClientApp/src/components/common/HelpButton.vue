<script setup lang="ts">
import { ref } from 'vue'
import HelpModal from "@/components/common/HelpModal.vue";

const showModal = ref(false)
defineExpose({ showModal })
</script>

<template>
  <button
    class="btn btn-sm btn-light position-fixed top-0 end-0 m-3 z-3"
    @click="showModal = true"
    aria-label="Ajuda"
  >
    ?
  </button>

  <HelpModal v-if="showModal" @close="showModal = false">
    <slot />
  </HelpModal>
</template>

<style scoped>
.z-3 {
  z-index: 1055;
}
</style>
