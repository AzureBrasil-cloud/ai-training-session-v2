<script setup lang="ts">
import { onMounted, ref } from "vue";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import markdownit from 'markdown-it'
import axios from "axios";

const connection = new HubConnectionBuilder().withUrl('/responsesChatHub').withAutomaticReconnect().build();
const message = ref<string>('');
const messages = ref<{ id: string, message: string }[]>([]);
const md = new markdownit({
  html: true,
  linkify: true,
  typographer: true,
  breaks: true,
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
          <h1 class="ls-tight h3 ">
            HOME PAGE
          </h1>
        </div>
        <div class="hstack gap-2 justify-content-end">
          <!-- <button type="button" class="btn btn-sm btn-neutral" @click="resetChat">
            <i class="bi bi-plus-lg me-1"></i>
            New chat
          </button> -->
        </div>
      </div>
    </header>
    <div class="flex-fill overflow-y-lg-auto scrollbar bg-body rounded-top-3 rounded-top-lg-4 border-top"
      data-x-inject="page">
      <main class=" h-100">
        <div class="row g-0 justify-content-center h-100">
          <div class="col-xl-10 col-xxl-9 h-100 d-flex flex-column">
            <div class="mt-16 mb-16 px-6 px-lg-8 text-center">
              <div v-if="messages.length === 0">
                <h1 class="ls-tight h1 ">
                  Hello
                </h1>

                <p class="px-lg-32 mt-4 text-body-secondary">
                  Ask anything, explore possibilities, and get instant insights all in one prompt.
                </p>
                <!-- <div class="row row-cols-sm-2 g-5 mt-10 w-xxl-75 mx-auto">
                  <div class="col">
                    <div class="card shadow-2-hover">
                      <div class="p-3 d-flex align-items-center gap-4">
                        <div class="icon icon-shape bg-purple-100 text-purple-500 text-lg rounded">
                          <i class="bi bi-justify-left"></i>
                        </div>
                        <div class="text-heading fw-semibold me-auto">
                          Write a text
                        </div>
                        <button type="button"
                          class="btn btn-xs btn-square btn-neutral rounded-circle border border-dashed shadow-none stretched-link">
                          <i class="bi bi-plus-lg"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="col">
                    <div class="card shadow-2-hover">
                      <div class="p-3 d-flex align-items-center gap-4">
                        <div class="icon icon-shape bg-orange-100 text-orange-500 text-lg rounded">
                          <i class="bi bi-brilliance"></i>
                        </div>
                        <div class="text-heading fw-semibold me-auto">
                          Generate an image
                        </div>
                        <button type="button"
                          class="btn btn-xs btn-square btn-neutral rounded-circle border border-dashed shadow-none stretched-link">
                          <i class="bi bi-plus-lg"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="col">
                    <div class="card shadow-2-hover">
                      <div class="p-3 d-flex align-items-center gap-4">
                        <div class="icon icon-shape bg-yellow-100 text-yellow-500 text-lg rounded">
                          <i class="bi bi-camera-video"></i>
                        </div>
                        <div class="text-heading fw-semibold me-auto">
                          Create video
                        </div>
                        <button type="button"
                          class="btn btn-xs btn-square btn-neutral rounded-circle border border-dashed shadow-none stretched-link">
                          <i class="bi bi-plus-lg"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="col">
                    <div class="card shadow-2-hover">
                      <div class="p-3 d-flex align-items-center gap-4">
                        <div class="icon icon-shape bg-green-100 text-green-500 text-lg rounded">
                          <i class="bi bi-signpost-2"></i>
                        </div>
                        <div class="text-heading fw-semibold me-auto">
                          Write a plan
                        </div>
                        <button type="button"
                          class="btn btn-xs btn-square btn-neutral rounded-circle border border-dashed shadow-none stretched-link">
                          <i class="bi bi-plus-lg"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div> -->
              </div>
              <div class="vstack gap-4 bg-body border px-10 py-6 mb-2 shadow"
                :class="{ 'opacity-75': m.id === '', 'text-right': m.id === '', 'text-left': m.id !== '', 'rounded-pill': m.id === '', 'rounded': m.id !== '' }"
                v-else v-for="m in messages" :key="m.id">
                <div class="article text-sm mt-4">
                  <p v-html="renderMarkdown(m.message)"></p>
                </div>
              </div>
            </div>
            <!-- <div class="mt-auto px-6 px-lg-8 py-6"> -->
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
