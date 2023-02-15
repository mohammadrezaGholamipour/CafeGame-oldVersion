<script setup>
import BtnQuickAccess from "./components/btn-quick-access.vue";
</script>
<template>
  <router-view v-slot="{ Component }">
    <transition-scale group :duration="1000">
      <BtnQuickAccess />
      <component :is="Component" />
    </transition-scale>
  </router-view>
</template>
