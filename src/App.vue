<script setup>
import BtnQuickAccess from "@/components/btn-quick-access.vue";
import MenuFeatures from "@/components/menu-features.vue";
import { onMounted, watch, computed } from "vue";
import Loading from "./components/loading.vue";
import AuthService from "./util/AuthService";
import { useStore } from '@/store/index'
import { useRoute } from "vue-router";
///////////////////////////////////////
const store = useStore()
const route = useRoute()
////////////////////////
onMounted(() => {
  if (AuthService.getTokenUser()) {
    store.requestGetConsoles()
  }
})
////////////////////////
watch(() => store.getUserInfo, (value) => {
  if (value.token) {
    store.requestGetConsoles()
  }
})
////////////////////////
const handleLoading = computed(() => {
  if (route.path !== '/login' && route.path !== '/register') {
    if (store.getConsoleList && store.getMoneyList && store.getFoodList && store.getBillList) {
      return false
    } else {
      return true
    }
  } else {
    return false
  }
})
</script>
<template>
  <div v-if="!handleLoading">
    <router-view v-slot="{ Component, route }">
      <transition-fade appear group :duration="1000">
        <BtnQuickAccess v-show="route.meta.menu" />
        <component :is="Component" />
        <MenuFeatures />
      </transition-fade>
    </router-view>
  </div>
  <transition-scale appear v-if="handleLoading">
    <Loading />
  </transition-scale>
</template>
