<script setup>
import MenuFeatures from "../components/menu-features.vue";
import TableMoney from "../components/table-money.vue";
// ////////////////////////////////////////
</script>
<template>
  <div class="parent-management-money">
    <!-- ///////////////////////////////// -->
    <img
      src="../assets/image/menu-setting.gif"
      data-bs-target="#menuSetting"
      data-bs-toggle="offcanvas"
      class="cursor-pointer"
      width="40"
    />
    <!-- /////////////////////////////// -->
    <TableMoney />
    <MenuFeatures />
  </div>
</template>
