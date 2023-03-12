import { createRouter, createWebHistory } from 'vue-router'
import consols from '@/pages/consols/consols.vue'
import moneys from '@/pages/moneys/moneys.vue'
import bills from '@/pages/bills/bills.vue'
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
      meta: {
        menu: true
      }
    },
    {
      path: '/foods',
      name: 'foods',
      component: foods,
      meta: {
        menu: true
      }
    },
    {
      path: '/consols',
      name: 'consols',
      component: consols,
      meta: {
        menu: true
      }
    },
    {
      path: '/bills',
      name: 'bills',
      component: bills,
      meta: {
        menu: true
      }
    },
    {
      path: '/moneys',
      name: 'moneys',
      component: moneys,
      meta: {
        menu: true
      }
    },
    {
      path: '/login',
      name: 'login',
      component: login,
      meta: {
        menu: false
      }
    },
    {
      path: '/register',
      name: 'register',
      component: Register,
      meta: {
        menu: false
      }
    },
  ]
})

export default router
