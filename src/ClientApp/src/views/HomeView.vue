<script setup lang="ts">
import {onBeforeMount, onMounted, ref} from "vue";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import markdownit from 'markdown-it';
import axios from "axios";
import {useRouter} from "vue-router";

const connection = new HubConnectionBuilder().withUrl('/responsesChatHub').withAutomaticReconnect().build();
const message = ref<string>('');
const messages = ref<{ id: string, message: string }[]>([]);
const md = new markdownit({
  html: true,
  linkify: true,
  typographer: true,
  breaks: true,
});

let userRole = ref("");
const router = useRouter();

onBeforeMount(() => {
  const loggedUser = sessionStorage.getItem("loggedUser");

  userRole.value = loggedUser ? JSON.parse(loggedUser)?.role : "";
});

const file = ref<File | null>(null);
const fileContent = ref<string | ArrayBuffer | null>(null);
const fileUploading = ref(false);
const uploadedFileId = ref<string>('');

const renderMarkdown = (text: string) => {
  return md.render(text);
};

bindConnectionMessages(connection);

function bindConnectionMessages(connection: HubConnection) {
  connection.on('newMessageWithId', (id, message) => {
    const theMessage = messages.value.find(m => m.id === id);
    if (theMessage) {
      theMessage.message += message;
    } else {
      messages.value.push({
        id,
        message,
      });
    }
  });
}

function sendMessage() {
  if (message.value) {
    messages.value.push({
      id: '',
      message: message.value,
    });

    connection.send("Chat", message.value, uploadedFileId.value, file.value?.type || '');
    message.value = '';
    uploadedFileId.value = '';
  }
}

function handleFile(event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    file.value = input.files[0];
    const reader = new FileReader();
    reader.onload = (e) => {
      fileContent.value = e.target?.result || null;
      if (fileContent.value) {
        uploadFile();
      }
    };
    reader.readAsDataURL(file.value);
  }
}

async function uploadFile() {
  if (!fileContent.value) return;

  fileUploading.value = true;
  try {
    const response = await axios.post('/api/files/upload', {
      fileName: file.value?.name,
      fileType: file.value?.type,
      fileSize: file.value?.size,
      fileContent: fileContent.value,
    });
    uploadedFileId.value = response.data.id;

    console.log('File uploaded successfully:', response.data);
  } catch (error) {
    console.error('Error uploading file:', error);
  } finally {
    fileUploading.value = false;
  }
}

function resetChat() {
  messages.value = [];
  message.value = '';
  file.value = null;
  fileContent.value = null;
  uploadedFileId.value = '';
  connection.send("Reset");
}

onMounted(async () => {
  try {
    await connection.start();
    console.log("SignalR connection started");
  } catch (err) {
    console.error("Error while starting connection: " + err);
  }
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
                  <div v-if="messages.length === 0">
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

                  <div v-if="messages.length === 0">
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

                <div v-else class="overflow-auto" style="max-height: 98vh">
                  <div
                    class="vstack gap-4 bg-body border px-10 py-6 mb-2 shadow"
                    :class="{ 'opacity-75': m.id === '', 'text-right': m.id === '', 'text-left': m.id !== '', 'rounded-pill': m.id === '', 'rounded': m.id !== '' }"
                    v-for="m in messages"
                    :key="m.id"
                  >
                    <div class="article text-sm mt-4">
                      <p v-html="renderMarkdown(m.message)"></p>
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
