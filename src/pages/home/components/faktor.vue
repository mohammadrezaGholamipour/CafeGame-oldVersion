<script setup>
import { useWindowSize } from '@vueuse/core'
import { reactive, computed } from "vue";
///////////////////////////////////////////
const props = defineProps(["payModal", "foodSelected"]);
///////////////////////////////////////////
const { width } = useWindowSize()
///////////////////////////////////////////
const state = reactive({
  headerFoods: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "نام محصول", icon: "fa-duotone fa-plate-utensils" },
    { name: "قیمت محصول", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "تعداد", icon: "fa-duotone fa-cash-register" },
    { name: "جمع", icon: "fa-duotone fa-receipt" },
  ],
});
///////////////////////////////////////////
const handleTotalFoodMoney = computed(() => {
  const foodMoney = [];
  for (const item of props.foodSelected) {
    foodMoney.push(item.count * item.cost);
  }
  const totalFoodMoney = foodMoney.reduce((total, item) => {
    return total + item;
  });
  return totalFoodMoney;
});
</script>
<template>
  <div class="flex w-full justify-center items-center">
    <div v-if="!props.foodSelected.length" class="flex items-center">
      <img src="@/assets/image/money.svg" width="50" />
      <p class="text-3xl ml-1">هزارتومان</p>
      <p class="text-3xl ml-1 mt-1">
        {{ props.payModal.playstation.userMoney?.toLocaleString() }}
      </p>
    </div>
    <!-- /////////////////////////////// -->
    <table v-else-if="width > 600" dir="rtl" class="TableFoods">
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
      <tbody>
        <tr v-for="(items, index) in props.foodSelected" :key="index">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ items.cost?.toLocaleString() }}</td>
          <td>{{ items.count }}</td>
          <td>{{ (items.count * items.cost).toLocaleString() }}</td>
        </tr>
      </tbody>
      <tfoot class="bg-red-500 text-white">
        <tr>
          <td class="p-2" colspan="4">
            <p class="flex w-full justify-center">جمع هزینه خوراکی</p>
          </td>
          <td class="p-2">
            {{ handleTotalFoodMoney.toLocaleString() }}
          </td>
        </tr>
        <tr>
          <td class="p-2" colspan="4">
            <p class="flex w-full justify-center">هزینه زمان بازی شده</p>
          </td>
          <td class="p-2">
            {{ props.payModal.playstation.userMoney?.toLocaleString() }}
          </td>
        </tr>
        <tr class="bg-blue-600">
          <td class="p-2" colspan="4">
            <p class="flex w-full justify-center">جمع کل</p>
          </td>
          <td class="p-2">
            {{
              (
                handleTotalFoodMoney + props.payModal.playstation.userMoney
              ).toLocaleString()
            }}
          </td>
        </tr>
      </tfoot>
    </table>
    <!-- //////////////////////////// -->
    <div v-else class="w-full flex flex-col items-center justify-start">
      <div v-for="(items, index) in props.foodSelected" :key="index" class="parent-mobile-table">
        <div class="flex flex-col justify-between items-end">
          <!-- ////////////////////////////// -->
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-arrow-down-wide-short" />
            <p class="ml-1">ردیف</p>
          </div>
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
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-receipt" />
            <p class="ml-1">جمع</p>
          </div>
          <!-- ////////////////////////////// -->
        </div>
        <div class="flex flex-col justify-between items-center">
          <p>{{ index + 1 }}</p>
          <p>{{ items.name }}</p>
          <p>{{ items.cost?.toLocaleString() }}</p>
          <p>{{ items.count }}</p>
          <p>{{ (items.count * items.cost).toLocaleString() }}</p>
        </div>
      </div>
      <div class="bg-red-500 flex flex-col items-center rounded-md shadow-md overflow-hidden mt-3 justify-center w-full text-white">
        <div class="flex w-full justify-between items-center p-2">
          <p>{{ handleTotalFoodMoney.toLocaleString() }}</p>
          <p>جمع هزینه خوراکی</p>
        </div>
        <div class="flex w-full justify-between items-center p-2">
          <p> {{ props.payModal.playstation.userMoney?.toLocaleString() }}</p>
          <p>هزینه بازی شده</p>
        </div>
        <div class="flex w-full justify-between items-center bg-blue-600 p-2">
          <p> {{ (handleTotalFoodMoney + props.payModal.playstation.userMoney).toLocaleString() }}</p>
          <p>جمع کل</p>
        </div>
      </div>
    </div>
  </div>
</template>
