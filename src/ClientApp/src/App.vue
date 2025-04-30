<script setup lang="ts">
import MainLayout from './components/layout/MainLayout.vue';
</script>

<template>
  <Suspense>
    <MainLayout />
    <template #fallback>
      Loading...
    </template>
  </Suspense>
</template>

<style scoped></style>
