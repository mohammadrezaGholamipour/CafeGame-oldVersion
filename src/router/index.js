import { createRouter, createWebHistory } from 'vue-router'
import consols from '../pages/consols/consols.vue'
import foods from '../pages/foods/foods.vue'
import Register from '../pages/Register.vue'
import money from '../pages/money.vue'
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
      name: 'foods',
      component: foods,
    },
    {
      path: '/consols',
      name: 'consols',
      component: consols,
    },
    {
      path: '/money',
      name: 'money',
      component: money,
    },
  ]
})

export default router
