<script setup>
import MenuSetting from "../components/menu-setting.vue";
import ConsolePs4 from "../components/console-ps4.vue";
import PayModal from "../components/pay-modal.vue";
</script>
<template>
  <div class="ParentHome">
    <img
      src="../assets/image/menu-setting.gif"
      data-bs-target="#menuSetting"
      data-bs-toggle="offcanvas"
      class="cursor-pointer"
      width="30"
    />
    <div class="ParentMain">
      <MenuSetting />
      <ConsolePs4 />
      <PayModal />
    </div>
  </div>
</template>
