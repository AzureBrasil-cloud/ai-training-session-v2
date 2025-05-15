<script setup lang="ts">
  import { ADM_CREDENTIALS } from '@/constants/admCredentials';
  import { validators } from '@/utils/validators';
  import { ref, watch,  } from 'vue';
  import { useRouter } from 'vue-router';

  const router = useRouter();

  let email = ref("");
  let password = ref("");

  let emailError = ref("");
  let passwordError = ref("");

  watch(email, (newValue) => {
    console.log("Email:", newValue);
  });

  const handleSubmit = async () => {
    try {
      if (email.value === ADM_CREDENTIALS.email && password.value === ADM_CREDENTIALS.password) {
        sessionStorage.setItem("loggedUser", JSON.stringify({
          role: "admin",
          email: ADM_CREDENTIALS.email
        }));
        router.push({ name: "home" });
      } else {
        if (!validators.isValidEmail(email.value)) {
          emailError.value = "Email inválido.";
        }

        if (!validators.isValidPassword(password.value)) {
          passwordError.value = "A senha precisa ter pelo menos 8 caracteres.";
        }

        if (hasErrors()) {
          return;
        }

        sessionStorage.setItem("loggedUser", JSON.stringify({
          role: "user",
          email: email.value
        }));
        router.push({ name: "home" });
      }
    } catch (error) {
      console.error("Erro ao fazer login:", error);
    }
  };

  const hasErrors = () => {
    return emailError.value || passwordError.value;
  };

</script>

<template>

  <div class="position-relative d-flex justify-content-center px-5 py-5 p-lg-0 bg-body" data-x-type="page">
    <div
      class="col-lg-5 col-xl-5 p-12 p-xl-10 position-absolute start-0 top-0 min-vh-100 overflow-y-hidden d-none d-lg-flex flex-column bg-body-secondary border-end-lg">

      <a class="d-block" href="#">
        <img src="/logo-acai.png" width="150"
          alt="..." />
      </a>

      <div class="mt-auto mb-8 w-lg-75">
        <h1 class="ls-tight mb-4 item-purple">
          Faça seu pedido online!
        </h1>
        <p class="text-body-secondary pe-lg-10">
          E desfrute do melhor açai da cidade
        </p>
      </div>

      <div
        class="w-rem-56 h-rem-56 bg-white bg-opacity-10 rounded-circle position-absolute bottom-0 end-0 me-10 transform translate-y-50">
      </div>
    </div>
    <div
      class="col-12 col-md-9 col-lg-7 offset-xl-7 offset-lg-5 vh-lg-100 d-flex flex-column justify-content-center py-lg-16 px-lg-20 position-relative">
      <div class="row">
        <div class="col-lg-10 col-md-9 col-xl-8 col-xxl-7 mx-auto ms-xl-0">
          <div class="mb-12">
            <h1 class="h2 ls-tight fw-bolder item-purple">
              Entre na sua conta
            </h1>
            <p class="text-sm mt-2 text-body-secondary">
              Insira suas credenciais abaixo.
            </p>
          </div>


          <form class="vstack gap-5">
            <div>
              <label class="form-label" for="email">Email</label>
              <input v-model="email" type="email" :class="['form-control', emailError ? 'is-invalid' : '']" id="email" placeholder="Seu email">
              <span v-if="emailError" class="mt-2 invalid-feedback">{{ emailError }}</span>
            </div>
            <div>
              <div class="d-flex align-items-center mb-2">
                <label class="form-label mb-0" for="password">Senha</label>
              </div>
              <input v-model="password" type="password" :class="['form-control', passwordError ? 'is-invalid' : '']" id="password" placeholder="Sua senha"
                autocomplete="current-password">
                <span v-if="passwordError" class="mt-2 invalid-feedback">{{ passwordError }}</span>
            </div>
            <div>
              <button @click="handleSubmit" class="btn btn-purple w-100">
                Entrar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>

</template>
