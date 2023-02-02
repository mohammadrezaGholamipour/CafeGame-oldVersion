import { createRouter, createWebHistory } from 'vue-router'
import Register from '../pages/Register.vue'
import console from '../pages/consols.vue'
import money from '../pages/money.vue'
import Foods from '../pages/Foods.vue'
import login from '../pages/login.vue'
import Home from '../pages/Home.vue'

//////////////////////////////////
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home,
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
    {
      path: '/foods',
      name: 'Foods',
      component: Foods,
    },
    {
      path: '/consols',
      name: 'consols',
      component: console,
    },
    {
      path: '/money',
      name: 'money',
      component: money,
    },
  ]
})

export default router
