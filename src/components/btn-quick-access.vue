<script setup>
import { reactive, onMounted } from "vue";
import { useRouter } from "vue-router";
///////////////////////
onMounted(() => {
  const menuFeatures = document.getElementById("menuFeatures");
  menuFeatures.dataset.bsTarget = "#offcanvasMenuFeatures";
  menuFeatures.dataset.bsToggle = "offcanvas";
});
///////////////////////
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
});
///////////////////////////////
const handleClick = (name) => {
  if (name === "reset") {
    router.push({ name: "home" });
  }
};
</script>
<template>
  <div class="parent-quick-access">
    <div class="w-full flex items-center justify-center">
      <i
        v-for="items in state.BtnquickAccess"
        @click="handleClick(items.name)"
        :class="items.iconClass"
        id="menuFeatures"
        :key="items.id"
      ></i>
    </div>
  </div>
</template>
