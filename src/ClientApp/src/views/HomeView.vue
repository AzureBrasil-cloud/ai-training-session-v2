<script setup lang="ts">
import {onMounted, ref} from "vue";
import {useRouter} from "vue-router";

let userRole = ref("");
const router = useRouter();

onMounted(async () => {
  const loggedUser = sessionStorage.getItem("loggedUser");
  userRole.value = loggedUser ? JSON.parse(loggedUser)?.role : "";
});
</script>

<template>

  <div class="flex-fill h-100 d-flex flex-column">
    <header class="px-4 py-3">
      <div class="d-flex align-items-center justify-content-between flex-wrap gap-4 ">
        <div>
          <h6 style="color: gray">
            AzureBrasil.cloud apresenta Contoso Açaí
          </h6>
        </div>
      </div>
    </header>
    <div
      class="flex-fill overflow-y-lg-auto scrollbar bg-body rounded-top-3 rounded-top-lg-4 border-top"
      data-x-inject="page">

      <main class="h-100">
        <div class="container-fluid h-100">
          <div class="row g-0 align-items-center justify-content-center" style="height: 100%;">
            <div class="col-12 d-flex flex-column flex-md-row h-100">

              <div class="w-100 w-md-50 d-flex flex-column justify-content-center align-items-start px-5 py-5 py-md-0 mb-4 mb-md-0 end-border">
                <h1 class="home-text-light text-start fw-bold lh-1 display-6 display-md-3 display-lg-1">
                  Seja muito <br> bem-vindo(a) ao <u>Contoso Açai</u>
                </h1>
              </div>

              <div class="w-100 w-md-50 d-flex flex-column justify-content-center px-5">
                <div v-if="userRole === 'user'">
                  <div>
                    <div class="row g-3 s-0 ps-sm-4">
                      <div class="col-12">
                        <RouterLink to="/orders"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-chat-square-dots fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Meus pedidos</span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/send-image-pre-order"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-images fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">
                          Pré-pedido <span class="badge bg-white item-purple ms-2 fs-6 fs-md-5">Imagem</span>
                        </span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/send-audio-pre-order"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-music-note-list fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">
                          Pré-pedido <span class="badge bg-white item-purple ms-2 fs-6 fs-md-5">Áudio</span>
                        </span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/send-review"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-card-checklist fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Fazer avaliação</span>
                        </RouterLink>
                      </div>

                      <div class="col-12 mb-5 mb-md-0">
                        <RouterLink to="/info-chat"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-robot fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Assistente de informações</span>
                        </RouterLink>
                      </div>
                    </div>
                  </div>
                </div>

                <div v-else-if="userRole === 'admin'">

                  <div>
                    <div class="row g-3 ps-0 ps-sm-4">
                      <div class="col-12">
                        <RouterLink to="/orders"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-chat-square-dots fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Meus pedidos</span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/image-pre-orders"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-images fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">
                          Pré-pedido <span class="badge bg-white item-purple ms-2 fs-6 fs-md-5">Imagem</span>
                        </span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/audio-pre-orders"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-music-note-list fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">
                          Pré-pedido <span class="badge bg-white item-purple ms-2 fs-6 fs-md-5">Áudio</span>
                        </span>
                        </RouterLink>
                      </div>

                      <div class="col-12">
                        <RouterLink to="/reviews"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-card-checklist fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Fazer avaliação</span>
                        </RouterLink>
                      </div>

                      <div class="col-12 mb-5 mb-md-0">
                        <RouterLink to="/orders-chat"
                                    class="btn btn-purple w-100 py-4 d-flex align-items-center justify-content-center gap-3 flex-wrap text-center"
                                    style="min-height: 90px;">
                          <i class="bi bi-robot fs-1 fs-md-1"></i>
                          <span class="fs-4 fs-md-2">Assistente de informações</span>
                        </RouterLink>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>

</template>

<style scoped>
.text-left {
  text-align: left;
}

.text-right {
  text-align: right;
}
</style>
