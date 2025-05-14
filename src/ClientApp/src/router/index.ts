import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import SignInView from '@/views/auth/SignInView.vue'

const isAuthenticated = () => {
  const loggedUser = sessionStorage.getItem("loggedUser");

  if (loggedUser) {
    const user = JSON.parse(loggedUser);
    return user.hasOwnProperty("email") && user.hasOwnProperty("role");
  }
}

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  linkActiveClass: 'active',
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { requiresAuth: true }
    },
    {
      path: '/orders',
      name: 'orders',
      component: () => import('../views/orders/OrdersView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: "/sign-in",
      name: "signin",
      component: SignInView
    },
    {
      path: '/info-chat',
      name: 'info-chat',
      component: () => import('../views/infoChat/infoChatView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/orders-chat',
      name: 'orders-chat',
      component: () => import('../views/ordersChat/ordersChatView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/send-image-pre-order',
      name: 'send-image-pre-order',
      component: () => import('../views/orders/sendImagePreOrderView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/image-pre-orders',
      name: 'image-pre-orders',
      component: () => import('../views/orders/ImagePreOrderView.vue'),
      meta: { requiresAuth: true }
    }
  ],
})

router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  const userIsAuthenticated = isAuthenticated();

  if (requiresAuth && !userIsAuthenticated) {
    // Se a rota requer autenticação e o usuário não está logado,
    // redireciona para a página de sign-in.
    next({ name: 'signin' });
  } else if (to.name === 'signin' && userIsAuthenticated) {
    // Opcional: se o usuário já está logado e tenta acessar a página de sign-in,
    // redireciona para a página inicial.
    next({ name: 'home' });
  }
  else {
    // Caso contrário, permite a navegação.
    next();
  }
});

export default router
