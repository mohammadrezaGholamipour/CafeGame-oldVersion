import { createRouter, createWebHistory } from 'vue-router'
import consols from '@/pages/consols/consols.vue'
import moneys from '@/pages/moneys/moneys.vue'
import foods from '@/pages/foods/foods.vue'
import Register from '@/pages/Register.vue'
import home from '@/pages/home/home.vue'
import login from '@/pages/login.vue'
//////////////////////////////////
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: home,
    },
    {
      path: '/foods',
      name: 'foods',
      component: foods,
    },
    {
      path: '/consols',
      name: 'consols',
      component: consols,
    },
    {
      path: '/moneys',
      name: 'moneys',
      component: moneys,
    },
    {
      path: '/login',
      name: 'Login',
      component: login,
    },
    {
      path: '/register',
      name: 'Register',
      component: Register,
    },
  ]
})

export default router
