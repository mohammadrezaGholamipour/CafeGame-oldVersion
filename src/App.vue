<script setup>
import BtnQuickAccess from "@/components/btn-quick-access.vue";
import MenuFeatures from "@/components/menu-features.vue";
import { onMounted, watch, reactive } from "vue";
import { useStore } from '@/store/index'
///////////////////////////////////////
const store = useStore()
////////////////////////
const state = reactive({
  loading: true
})
////////////////////////
onMounted(() => {
  store.requestGetConsoles()
})
watch(() => store.getConsoleList, () => {
  store.requestGetMoneys()
})
watch(() => store.getMoneyList, () => {
  store.requestGetfoods()
})
watch(() => store.getFoodList, () => {
  store.requestGetBills()
  state.loading = false
})
</script>
<template>
  <router-view v-slot="{ Component }">
    <transition-scale group :duration="1000">
      <BtnQuickAccess />
      <component v-show="!state.loading" :is="Component" />
      <MenuFeatures />
    </transition-scale>
  </router-view>
</template>
