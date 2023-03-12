<script setup>
import BtnQuickAccess from "@/components/btn-quick-access.vue";
import MenuFeatures from "@/components/menu-features.vue";
import { onMounted, watch, reactive } from "vue";
import Loading from "./components/loading.vue";
import { useStore } from '@/store/index'
///////////////////////////////////////
const store = useStore()
////////////////////////
const state = reactive({
  loading: false
})
////////////////////////
onMounted(() => {
  store.requestGetConsoles()
})
////////////////////////
// watch(() => store.getBillList, (value) => {
//   if (value) {
//     setTimeout(() => {
//       state.loading = false
//     }, 3000);
//   }
// })
</script>
<template >
  <div v-show="!state.loading">
    <router-view v-slot="{ Component, route }">
      <transition-fade appear group :duration="1000">
        <BtnQuickAccess v-show="route.meta.menu" />
        <component :is="Component" />
        <MenuFeatures />
      </transition-fade>
    </router-view>
  </div>
  <transition-scale appear v-if="state.loading">
    <Loading />
  </transition-scale>
</template>
