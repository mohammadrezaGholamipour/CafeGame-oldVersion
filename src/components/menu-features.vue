<script setup>
import { useRoute } from "vue-router";
import { reactive } from "vue";
/////////////////////////
const props = defineProps(["drawerStatus"]);
const emit = defineEmits(["close"]);
/////////////////////////
const route = useRoute();
/////////////////////////
const state = reactive({
  settingList: [
    {
      id: 1,
      name: "مدیریت کنسول ها",
      path: "/consols",
      icon: "fa-brands fa-playstation text-pink-500",
    },
    {
      id: 2,
      name: "مدیریت قیمت زمان بازی",
      path: "/money",
      icon: "fa-duotone fa-face-tongue-money text-red-500",
    },
    {
      id: 3,
      name: "گزارش تراکنش ها",
      path: "",
      icon: "fa-duotone fa-sack-dollar text-green-500",
    },
    {
      id: 4,
      name: "مدیریت بوفه",
      path: "/foods",
      icon: "fa-duotone fa-burger-soda text-red-500",
    },
    {
      id: 5,
      name: "مدیریت اکانت ها",
      path: "",
      icon: "fa-duotone fa-users text-blue-500",
    },
    {
      id: 6,
      name: "برگزاری کاپ",
      path: "",
      icon: "fa-duotone fa-trophy text-yellow-600",
    },
  ],
});
</script>
<template>
  <v-layout>
    <v-navigation-drawer
      @update:modelValue="emit('close')"
      class="bg-slate-300 rounded-l-xl"
      :model-value="props.drawerStatus"
      location="right"
      temporary
    >
      <div
        class="flex w-full justify-between p-2.5 items-center"
        style="border-bottom: dashed 1px black"
      >
        <div class="flex items-center">
          <p class="text-xl ml-1 font-bold">امکانات</p>
          <i class="fa-duotone fa-sliders text-xl"></i>
        </div>
        <i
          class="fa-duotone fa-rectangle-xmark cursor-pointer text-3xl text-red-500"
          @click="emit('close')"
        ></i>
      </div>
      <div class="parent-main-menu-setting">
        <div
          v-show="items.path !== route.path"
          v-for="items in state.settingList"
          :key="items.id"
          class="w-full"
        >
          <router-link class="menu-setting-items" :to="items.path">
            <i class="text-xl" :class="items.icon"></i>
            <p class="text-lg mr-2">{{ items.name }}</p>
          </router-link>
        </div>
      </div>
    </v-navigation-drawer>
  </v-layout>
</template>
