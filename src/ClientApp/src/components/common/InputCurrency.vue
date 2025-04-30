<script setup lang="ts">
import { ref, watch } from 'vue';

const model = defineModel({ default: 0 });

const formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
});
const currencyValue = ref(formatter.format(model.value));

function parseCurrency(value: string): number {
  // Remove anything that's not a digit, minus sign, or decimal point.
  const numeric = Number(value.replace(/[^0-9.-]+/g, ''));
  return isNaN(numeric) ? 0 : numeric;
}

watch(currencyValue, (newValue) => {
  const theValue = parseCurrency(newValue);
  currencyValue.value = formatter.format(theValue);
  model.value = parseCurrency(currencyValue.value);
});

watch(model, (newValue) => {
  currencyValue.value = formatter.format(newValue);
});
</script>

<template>
  <input type="text" class="form-control" required v-model.lazy="currencyValue" />
</template>
