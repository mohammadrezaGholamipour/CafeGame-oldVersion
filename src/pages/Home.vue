<script setup>
import BtnQuickAccess from "../components/btn-quick-access.vue";
import MenuFeatures from "../components/menu-features.vue";
import ConsolePs4 from "../components/console-ps4.vue";
import PayModal from "../components/pay-modal.vue";
</script>
<template>
  <div class="ParentHome">
    <BtnQuickAccess />
    <div class="ParentMain">
      <MenuFeatures />
      <ConsolePs4 />
      <PayModal />
    </div>
  </div>
</template>
