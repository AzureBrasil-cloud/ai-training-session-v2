<script setup lang="ts">
// import { onMounted, onBeforeUnmount, ref } from 'vue';
import { useRouter } from 'vue-router'
import SidebarMenu from './SidebarMenu.vue';
import MainContent from './MainContent.vue';
import { useUserStore } from '../../stores/user';
import { useOrganizationStore } from '@/stores/organization';

// const $userStore = useUserStore(),
//   $organizationStore = useOrganizationStore(),
//   router = useRouter(),
//   isLoading = ref(true);
//
// const orgChanged = async (org: Organization) => {
//   await fetchData();
// }

// const fetchData = async () => {
//   isLoading.value = true;
//   const responses = await Promise.all([
//     $userStore.getUser(),
//     $organizationStore.getOrganization(),
//   ]);
//
//   if (responses[1] === null) {
//     router.push({ name: 'setup' });
//     isLoading.value = false;
//     return;
//   }
//
//   isLoading.value = false;
// }

// onMounted(async () => {
//   eventBus.on('OrgChanged', orgChanged)
//   await fetchData();
// });
//
// onBeforeUnmount(() => {
//   eventBus.off('OrgChanged', orgChanged)
// })

</script>

<template>
  <div class="d-flex flex-column flex-lg-row h-lg-100 bg-body-tertiary" data-x-type="layout">
    <SidebarMenu/>
    <MainContent/>
  </div>
</template>

<style scoped></style>
