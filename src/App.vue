<script setup>
import BtnQuickAccess from "./components/btn-quick-access.vue";
import MenuFeatures from "./components/menu-features.vue";
import { useStore } from './store/index'
///////////////////////////////////////
const store = useStore()
</script>
<template>
  <router-view v-slot="{ Component }">
    <transition-scale group :duration="1000">
      <BtnQuickAccess />
      <component :is="Component" />
      <MenuFeatures />
    </transition-scale>
  </router-view>
</template>
