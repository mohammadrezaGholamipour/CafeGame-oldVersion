<script setup>
import { useRoute, useRouter } from "vue-router";
import AuthService from '@/util/AuthService'
import { useStore } from '@/store/index'
import { reactive, watch } from "vue";
/////////////////////////
const props = defineProps(["drawerStatus"]);
const emit = defineEmits(["close"]);
/////////////////////////
const router = useRouter();
const route = useRoute();
const store = useStore()
/////////////////////////
const state = reactive({
  settingList: [
    {
      id: 1,
      name: "صفحه اصلی",
      path: "/",
      icon: "fa-duotone fa-house",
    },
    {
      id: 2,
      name: "مدیریت کنسول ها",
      path: "/consols",
      icon: "fa-brands fa-playstation text-pink-500",
    },
    {
      id: 3,
      name: "مدیریت قیمت زمان بازی",
      path: "/moneys",
      icon: "fa-duotone fa-face-tongue-money text-red-500",
    },
    {
      id: 4,
      name: 'گزارش فاکتور ها',
      path: "/bills",
      icon: "fa-duotone fa-sack-dollar text-green-500",
    },
    {
      id: 5,
      name: "مدیریت بوفه",
      path: "/foods",
      icon: "fa-duotone fa-burger-soda text-red-500",
    },
    {
      id: 6,
      name: "مدیریت اکانت ها",
      path: "",
      icon: "fa-duotone fa-users text-blue-500",
    },
    {
      id: 7,
      name: "برگزاری کاپ",
      path: "",
      icon: "fa-duotone fa-trophy text-yellow-600",
    },
    {
      id: 8,
      name: "خروج از حساب کاربری",
      path: "/login",
      icon: "fa-duotone fa-rectangle-xmark text-red-600",
    },
  ],
});
/////////////////////////
watch(
  () => route.path,
  () => {
    document.getElementById("closeDrawer").click();
  }
);
////////////////////////////
const handleLogOut = (path) => {
  if (path === '/login') {
    AuthService.removeTokenUser()
    AuthService.removeState()
    router.push('/login')
    store.logOut()
  }
}
</script>
<template>
  <div class="offcanvas parent-drawer" id="offcanvasMenuFeatures">
    <div class="flex w-full justify-between p-2.5 items-center" style="border-bottom: dashed 1px black">
      <div class="flex items-center text-white">
        <p class="text-xl ml-1 font-bold">امکانات</p>
        <i class="fa-duotone fa-sliders text-xl"></i>
      </div>
      <i class="fa-duotone fa-rectangle-xmark cursor-pointer text-3xl text-red-500" data-bs-dismiss="offcanvas"
        id="closeDrawer"></i>
    </div>
    <div class="parent-main-menu-setting">
      <div @click="handleLogOut(items.path)" v-show="items.path !== route.path" v-for="items in state.settingList"
        :key="items.id" class="w-full">
        <router-link class="menu-setting-items" :to="items.path">
          <i class="text-xl" :class="items.icon"></i>
          <p class="text-lg mr-2">{{ items.name }}</p>
        </router-link>
      </div>
    </div>
  </div>
</template>
