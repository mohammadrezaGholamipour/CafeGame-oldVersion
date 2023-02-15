<script setup>
import MenuFeatures from "./menu-features.vue";
import { useRoute, useRouter } from "vue-router";
import { reactive, watch } from "vue";
///////////////////////
const route = useRoute();
const router = useRouter();
/////////////////////
const state = reactive({
  BtnquickAccess: [
    {
      id: 1,
      name: "menuFeatures",
      iconClass:
        "fa-duotone fa-ellipsis-stroke text-2xl text-yellow-500 p-1 m-1 cursor-pointer",
    },
    {
      id: 2,
      name: "reset",
      iconClass:
        "fa-brands fa-playstation text-2xl text-red-600 p-1 m-1 cursor-pointer",
    },
  ],
  drawerStatus: false,
});
///////////////////////
watch(route, () => {
  state.drawerStatus = false;
});
///////////////////////
const handleClick = (name) => {
  if (name === "menuFeatures") {
    state.drawerStatus = true;
  } else {
    window.location.reload();
    router.path("/");
  }
};
////////////////////////
const handleCloseDrawer = () => {
  state.drawerStatus = false;
};
</script>
<template>
  <div class="parent-quick-access">
    <div class="w-full flex items-center justify-center">
      <i
        v-for="items in state.BtnquickAccess"
        @click="handleClick(items.name)"
        :class="items.iconClass"
        :key="items.id"
      ></i>
    </div>
    <MenuFeatures
      :drawerStatus="state.drawerStatus"
      @close="handleCloseDrawer"
    />
  </div>
</template>
