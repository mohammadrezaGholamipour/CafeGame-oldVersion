import VueTransitions from '@morev/vue-transitions';
import "vue-toastification/dist/index.css";
import '@morev/vue-transitions/styles';
import Toast from "vue-toastification";
import { createPinia } from 'pinia'
import { createApp } from 'vue'
import router from './router'
import App from './App.vue'
import './css/index.css'
import 'tw-elements';
//////////////////////
const app = createApp(App)
/////////////////////////
app.use(VueTransitions)
app.use(createPinia())
app.use(router)
app.use(Toast, {
  showCloseButtonOnHover: false,
  hideProgressBar: false,
  position: "top-center",
  pauseOnFocusLoss: true,
  closeButton: "button",
  draggablePercent: 0.6,
  closeOnClick: false,
  pauseOnHover: true,
  draggable: true,
  timeout: 5000,
  icon: true,
  rtl: true
})
///////////////
app.mount('#app')
