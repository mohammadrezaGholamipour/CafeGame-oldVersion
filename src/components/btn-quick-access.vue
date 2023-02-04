<script setup>
import { useRouter, useRoute } from "vue-router";
import { useToast } from "vue-toastification";
import { reactive } from "vue";
///////////////////////
const emit = defineEmits(["new"]);
///////////////////////
const router = useRouter();
const route = useRoute();
const toast = useToast();
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
      name: "home",
      iconClass:
        "fa-brands fa-playstation text-2xl text-red-600 p-1 m-1 cursor-pointer",
    },
    {
      id: 3,
      name: "new",
      iconClass:
        "fa-duotone fa-plus text-2xl text-green-400 p-1 px-2 m-1 cursor-pointer",
    },
  ],
});
// /////////////////////
const handleQuickAccess = (id) => {
  switch (id) {
    case 2:
      if (route.path === "/") {
        toast.info("شما در صفحه اصلی قرار دارید");
      } else {
        router.push("/");
      }
      break;
    case 3:
      emit("new");
      break;
  }
};
</script>
<template>
  <div class="parent-quick-access">
    <div class="w-full flex items-center justify-center">
      <i
        v-for="items in state.BtnquickAccess"
        @click="handleQuickAccess(items.id)"
        data-bs-target="#menuSetting"
        data-bs-toggle="modal"
        :class="items.iconClass"
        :key="items.id"
      ></i>
    </div>
  </div>
</template>
