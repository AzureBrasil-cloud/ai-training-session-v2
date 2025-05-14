import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import SignInView from '@/views/auth/SignInView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  linkActiveClass: 'active',
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/orders',
      name: 'orders',
      component: () => import('../views/orders/OrdersView.vue'),
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
    },
    {
      path: '/orders-chat',
      name: 'orders-chat',
      component: () => import('../views/ordersChat/ordersChatView.vue'),
    },
    {
      path: '/send-image-pre-order',
      name: 'send-image-pre-order',
      component: () => import('../views/orders/sendImagePreOrderView.vue'),
    }
  ],
})

export default router
