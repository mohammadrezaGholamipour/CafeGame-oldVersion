<script setup>
import { reactive, onMounted } from "vue";
///////////////////////
onMounted(() => {
  const menuFeatures = document.getElementById("menuFeatures");
  menuFeatures.dataset.bsTarget = "#offcanvasMenuFeatures";
  menuFeatures.dataset.bsToggle = "offcanvas";
});
/////////////////////
const state = reactive({
  BtnquickAccess: [
    {
      id: 1,
      name: "menuFeatures",
      iconClass:
        "fa-duotone fa-bars text-2xl text-blue-600 p-1 m-1 cursor-pointer",
    },
    {
      id: 2,
      name: "home",
      iconClass:
        "fa-brands fa-playstation text-2xl text-red-600 p-1 m-1 cursor-pointer",
    },
  ],
});
///////////////////////////////
const handleClick = (name) => {
  if (name === "home") {
    window.location = '/'

  }
};
</script>
<template>
  <div class="parent-quick-access">
    <div class="w-full flex items-center justify-center">
      <i v-for="items in state.BtnquickAccess" @click="handleClick(items.name)" :class="items.iconClass" id="menuFeatures"
        :key="items.id"></i>
    </div>
  </div>
</template>
