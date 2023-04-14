<script setup>
import { useWindowSize } from '@vueuse/core'
import { useRouter } from "vue-router";
import { reactive, watch } from "vue";
//////////////////////////
const emit = defineEmits(["foodSelected"]);
const props = defineProps(["payModal"]);
//////////////////////////
const { width } = useWindowSize()
const router = useRouter()
//////////////////////////
const state = reactive({
  headerFoods: [
    { name: "نام محصول", icon: "fa-duotone fa-plate-utensils" },
    { name: "قیمت محصول", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "تعداد", icon: "fa-duotone fa-cash-register" },
  ],
});
//////////////////////////
const handleCount = (foodId, type) => {
  const food = props.payModal.foodList.find((items) => items.id === foodId);
  if (type === "Decrease") {
    if (food.count) {
      food.count--;
    }
  } else {
    food.count++;
  }
};
//////////////////////////
watch(
  () => props.payModal.foodList,
  (value) => {
    const selected = value.some((items) => items.count);
    if (selected) {
      const foodIsCountTrue = value.filter((items) => !!items.count);
      emit("foodSelected", foodIsCountTrue);
    } else {
      emit("foodSelected", []);
    }
  },
  { deep: true }
);
</script>
<template>
  <div class="Parent-food-store">
    <table v-if="width > 500" dir="rtl" class="TableFoods ">
      <thead class="bg-[#d1d1d180]">
        <tr>
          <td v-for="(items, index) in state.headerFoods" :key="index">
            <div class="TdHeaderFoods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
            </div>
          </td>
        </tr>
      </thead>
      <tbody v-if="props.payModal.foodList.length" class="test">
        <tr v-for="(items, index) in props.payModal.foodList" :key="index">
          <td>{{ items.name }}</td>
          <td>{{ items.cost?.toLocaleString() }}</td>
          <td>
            <div class="inline-flex justify-center items-center">
              <i class="fa-duotone fa-circle-minus text-red-500 cursor-pointer"
                @click="handleCount(items.id, 'Decrease')"></i>
              <p class="mx-3 font-bold">{{ items.count }}</p>
              <i class="fa-duotone fa-circle-plus text-green-600 cursor-pointer"
                @click="handleCount(items.id, 'Increase')"></i>
            </div>
          </td>
        </tr>
      </tbody>
      <tr v-else>
        <td colspan="4">
          <div class="w-full flex items-center justify-center cursor-pointer">
            <p @click="router.push('/foods')" class="text-center  p-4 font-bold text-red-500 text-lg">
              لیست خوراکی ها خالی میباشد (برای افزودن کلیک کنید)
            </p>
            <i class="fa-duotone fa-utensils-slash text-lg text-red-700"></i>
          </div>
        </td>
      </tr>
    </table>
    <div v-if="width <= 500" class="w-full flex flex-col items-center justify-start">
      <div v-if="props.payModal.foodList.length" v-for="(items, index) in props.payModal.foodList" :key="index"
        class="parent-mobile-table">
        <div class="flex flex-col justify-between items-end">
          <!-- ////////////////////////////// -->
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-plate-utensils" />
            <p class="ml-1">نام محصول</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-money-bill-1-wave" />
            <p class="ml-1">قیمت محصول</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-cash-register" />
            <p class="ml-1">تعداد</p>
          </div>
          <!-- ////////////////////////////// -->
        </div>
        <div class="flex flex-col justify-between items-center">
          <p>{{ items.name }}</p>
          <p>{{ items.cost?.toLocaleString() }}</p>
          <div class="inline-flex justify-center items-center">
            <i class="fa-duotone fa-circle-minus text-red-500 cursor-pointer"
              @click="handleCount(items.id, 'Decrease')"></i>
            <p class="mx-3 font-bold">{{ items.count }}</p>
            <i class="fa-duotone fa-circle-plus text-green-600 cursor-pointer"
              @click="handleCount(items.id, 'Increase')"></i>
          </div>
        </div>
      </div>
      <div v-else class="w-full flex items-center justify-center cursor-pointer">
        <p @click="router.push('/foods')" class="text-center  p-4 font-bold text-red-500 text-lg">
          لیست خوراکی ها خالی میباشد (برای افزودن کلیک کنید)
        </p>
        <i class="fa-duotone fa-utensils-slash text-lg text-red-700"></i>
      </div>
    </div>
  </div>
</template>
