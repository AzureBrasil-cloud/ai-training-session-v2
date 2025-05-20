<script setup lang="ts">
import {ADM_CREDENTIALS} from '@/constants/admCredentials';
import {validators} from '@/utils/validators';
import {ref, watch,} from 'vue';
import {useRouter} from 'vue-router';
import HelpButton from "@/components/common/HelpButton.vue";

const router = useRouter();

let email = ref("");
let password = ref("");

let emailError = ref("");
let passwordError = ref("");


const darkTheme = () => {
  document.documentElement.setAttribute('data-bs-theme', 'dark');
  sessionStorage.setItem('theme', 'dark');
}

const handleSubmit = async () => {
  try {
    if (email.value === "") {
      emailError.value = "Email é um campo obrigatório.";
    }

    if (password.value === "") {
      passwordError.value = "Senha é um campo obrigatório.";
    }

    if (email.value === "" || password.value === "") {
      return;
    }

    if (email.value === ADM_CREDENTIALS.email && password.value === ADM_CREDENTIALS.password) {
      darkTheme();
      sessionStorage.setItem("loggedUser", JSON.stringify({
        role: "admin",
        email: ADM_CREDENTIALS.email
      }));
      router.push({name: "home"});
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
      router.push({name: "home"});
    }
  } catch (error) {
    console.error("Erro ao fazer login:", error);
  }
};

const hasErrors = () => {
  return emailError.value || passwordError.value;
};
const videoUrl = `${window.location.origin}/videos/login.mp4`;
const logo = `${window.location.origin}/images/logo-acai.png`;
const logoAzBr = `${window.location.origin}/images/logo-azbr.png`;
</script>

<template>
  <HelpButton>
    <div class="d-flex justify-content-center my-4">
      <video
        ref="player"
        :src="videoUrl"
        controls
        loop
        autoplay
        muted
        playsinline
        style="width: 100%;"
      ></video>
    </div>

    <h2 class="mb-3"><i class="bi bi-person-check-fill px-2"></i> Descritivo da Página de Login</h2>
    <p>
      A página de <strong>Login</strong> permite que os usuários acessem o sistema informando suas
      credenciais (email e senha). Ela fornece autenticação simulada com base em regras definidas no
      frontend, sem conexão com backends reais ou bancos de dados persistentes.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded"><i class="bi bi-list-task px-2"></i>
        Funcionalidades</h5>
      <ul>
        <li><strong>Autenticação de usuários comuns:</strong> Qualquer email válido e senha com no
          mínimo 8 caracteres permite o login como <code>usuário comum</code>.
        </li>
        <li><strong>Autenticação de administrador:</strong> O acesso como <code>administrador</code>
          é
          feito usando:
          <ul>
            <li><strong>Email:</strong> <code>adm@adm.com</code></li>
            <li><strong>Senha:</strong> <code>adm</code></li>
          </ul>
        </li>
        <li><strong>Validação de campos:</strong> O sistema realiza validações locais para garantir
          que o email seja válido e a senha tenha o comprimento mínimo exigido.
        </li>
        <li><strong>Feedback imediato:</strong> Erros de validação são exibidos diretamente abaixo
          dos
          campos de entrada.
        </li>
        <li><strong>Redirecionamento automático:</strong> Após um login bem-sucedido, o usuário é
          redirecionado para a rota <code>home</code>.
        </li>
      </ul>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-asterisk px-2"></i>Processo Técnico</h5>
      O sistema realiza a autenticação diretamente no frontend. As credenciais de administrador
      estão armazenadas como constante no código-fonte (em <code>ADM_CREDENTIALS</code>), enquanto
      as demais validações são feitas com funções auxiliares presentes no arquivo <code>validators.ts</code>.
    </p>

    <p>
      Ao realizar um login com sucesso, os dados do usuário (email e tipo de acesso) são salvos no
      <strong>sessionStorage</strong> com a chave <code>loggedUser</code>, possibilitando o uso da
      sessão em outras páginas do sistema.
    </p>

    <p>
    <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-check-circle px-2"></i> Regras de Validação</h5>
    <ul>
      <li><strong>Email:</strong> Deve ter um formato válido (ex: <code>usuario@email.com</code>).
      </li>
      <li><strong>Senha:</strong> Deve ter no mínimo <strong>8 caracteres</strong>, exceto no caso
        do administrador (<code>adm</code>).
      </li>
    </ul>
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-bullseye px-2"></i>
        Objetivo</h5>
      Esta funcionalidade foi criada para simular o processo de login em um ambiente controlado,
      útil para protótipos, testes ou demonstrações sem dependência de autenticação real.
    </p>

    <p>
      <h5 class="mt-6 mb-3 bg-gray-100 p-2 rounded bck-h"><i class="bi bi-link-45deg px-2"></i>
        Links
        Úteis</h5>
      <ul>
        <li>
          <a href="https://developer.mozilla.org/pt-BR/docs/Web/API/Window/sessionStorage"
             target="_blank" rel="noopener">
            Documentação – sessionStorage
          </a>
        </li>
        <li>
          <a href="https://vuejs.org/guide/essentials/reactivity-fundamentals.html" target="_blank"
             rel="noopener">
            Vue 3 – Fundamentos de Reatividade
          </a>
        </li>
      </ul>
    </p>
  </HelpButton>

  <div
    class="position-relative d-flex justify-content-center px-5 py-5 p-lg-0 bg-body w-100 overflow-hidden"
    data-x-type="page">
    <div
      class="col-lg-5 col-xl-5 p-12 p-xl-10 position-absolute start-0 top-0 min-vh-100 overflow-y-hidden d-none d-lg-flex flex-column border-end-lg"
      style="background-color: #6c2b6d">
      <div class="d-flex justify-content-center align-items-center flex-grow-1">
        <a class="d-block" href="#">
          <img :src="logo" width="500" alt="..."/>
        </a>
      </div>

      <div class="mt-auto mb-8 w-lg-75">
        <h1 class="ls-tight mb-4 text-white">
          Faça seu pedido online!
        </h1>
        <p class="pe-lg-10 text-white">
          E desfrute do melhor açaí da cidade.
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
              <input v-model="email" type="email"
                     :class="['form-control', emailError ? 'is-invalid' : '']" id="email"
                     placeholder="Seu email">
              <span v-if="emailError" class="mt-2 invalid-feedback">{{ emailError }}</span>
            </div>
            <div>
              <div class="d-flex align-items-center mb-2">
                <label class="form-label mb-0" for="password">Senha</label>
              </div>
              <input v-model="password" type="password"
                     :class="['form-control', passwordError ? 'is-invalid' : '']" id="password"
                     placeholder="Sua senha"
                     autocomplete="current-password">
              <span v-if="passwordError" class="mt-2 invalid-feedback">{{ passwordError }}</span>
            </div>
            <div>
              <button @click.prevent="handleSubmit" class="btn btn-purple w-100">
                Entrar
              </button>
            </div>
            <div class="d-flex justify-content-center align-items-center mt-4">
              <img :src="logoAzBr" alt="Logo Azure Brasil" style="max-height: 90px;">
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>

</template>
