<script setup>
import { reactive } from "vue";
/////////////////////////
const state = reactive({
  settingList: [
    {
      id: 1,
      name: "لیست خوراکی ها",
      path: "/foods",
      icon: "fa-duotone fa-burger-soda text-red-500",
    },
    {
      id: 2,
      name: "گزارش پرداختی ها",
      path: "",
      icon: "fa-duotone fa-sack-dollar text-green-500",
    },
    {
      id: 3,
      name: "مدیریت اکانت ها",
      path: "",
      icon: "fa-duotone fa-clipboard-list text-blue-500",
    },
  ],
});
</script>
<template>
  <div class="offcanvas menu-setting" id="menuSetting">
    <div
      class="flex w-full justify-between p-2 items-center"
      style="border-bottom: dashed 1px black"
    >
      <p class="text-xl font-bold">تنظیمات</p>
      <i
        class="fa-duotone fa-rectangle-xmark cursor-pointer text-3xl text-red-500"
        data-bs-dismiss="offcanvas"
      ></i>
    </div>
    <div class="parent-main-menu-setting">
      <div class="w-full" v-for="items in state.settingList" :key="items.id">
        <router-link class="menu-setting-items" :to="items.path">
          <i class="text-xl" :class="items.icon"></i>
          <p class="text-lg mr-2">{{ items.name }}</p>
          </router-link>
      </div>
    </div>
  </div>
</template>
