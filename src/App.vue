<template>
  <router-view v-slot="{ Component }">
    <transition-scale group :duration="1000">
      <component :is="Component" />
    </transition-scale>
  </router-view>
</template>
