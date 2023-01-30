<script setup>
import MenuSetting from "../components/menu-setting.vue";
import TableFoods from "../components/table-foods.vue";
</script>
<template>
  <div class="ParentFoods">
    <img
      src="../assets/image/menu-setting.gif"
      data-bs-target="#menuSetting"
      data-bs-toggle="offcanvas"
      class="cursor-pointer"
      width="40"
    />
    <TableFoods />
    <MenuSetting />
  </div>
</template>
