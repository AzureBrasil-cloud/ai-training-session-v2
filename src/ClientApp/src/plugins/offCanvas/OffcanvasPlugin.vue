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
const nounSmoothie = `${window.location.origin}/images/noun-smoothie-4106333.svg`;
</script>

<template>
  <div class="offcanvas-backdrop fade" :class="{ show: _show }" v-if="_show" @click="closeOffcanvas"></div>
  <div class="offcanvas offcanvas-end bg-purple" :class="{ show: _show }" v-if="_show">
    <div class="offcanvas-header pb-0">

      <img :src="nounSmoothie" alt="" width="18" class="mb-2">
      <h3 id="offcanvas-title h4" class="mx-3 text-white" v-html="$props.title"></h3>

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
