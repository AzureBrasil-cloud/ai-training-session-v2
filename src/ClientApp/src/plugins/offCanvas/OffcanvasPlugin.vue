<script setup lang="ts">
import { inject, computed } from "vue"
import type { IOffcanvas } from ".";

const
  $props = defineProps({
    name: { type: String, default: "" },
    title: { type: String, default: "" }
  }),
  $offcanvas = inject<IOffcanvas>("$offcanvas"),
  _show = computed(() => {
    return $offcanvas?.active() == $props.name
  })

function closeOffcanvas() {
  $offcanvas?.close();
}
</script>

<template>
  <div class="offcanvas-backdrop fade" :class="{ show: _show }" v-if="_show" @click="closeOffcanvas"></div>
  <div class="offcanvas offcanvas-end" :class="{ show: _show }" v-if="_show" style="background: #441945; color: white;">
    <div class="offcanvas-header py-6 border-bottom">
      <h5 id="offcanvas-title h4" class="text-white" v-html="$props.title"></h5>
      <div class="ms-auto text-xxs">
        <button type="button" class="btn-close btn-close-white text-white rounded-circle"
          @click="closeOffcanvas"></button>
      </div>
    </div>
    <div class="offcanvas-body">
      <slot></slot>
    </div>
    <slot name="footer"></slot>
  </div>
</template>
