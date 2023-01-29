import { useToast } from "vue-toastification";
import { defineStore } from 'pinia'
// ///////////////////////////////
const toast = useToast()
// /////////////////////
export const cafePinia = defineStore({
  id: 'cafeGame',
  state: () => ({}),
  actions: {},
  getters: {},
});