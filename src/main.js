import VueTransitions from '@morev/vue-transitions';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import "vue-toastification/dist/index.css";
import { createVuetify } from 'vuetify'
import '@morev/vue-transitions/styles';
import Toast from "vue-toastification";
import { createPinia } from 'pinia'
import moment from 'jalali-moment'
import './util/convert-numbers';
import { createApp } from 'vue';
import router from './router';
import App from './App.vue';
import './css/index.css';
import 'vuetify/styles';
import 'tw-elements';
//////////////////////
const vuetify = createVuetify({
  components,
  directives,
})
//////////////////////
const app = createApp(App)
/////////////////////////
app.use(VueTransitions)
app.use(createPinia())
app.use(moment)
app.use(vuetify)
app.use(router)
app.use(Toast, {
  transition: "Vue-Toastification__slideBlurred",
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
