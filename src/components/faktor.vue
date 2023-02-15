<script setup>
import { reactive, computed } from "vue";
///////////////////////////////////////////
const props = defineProps(["payModal", "foodSelected"]);
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
  <div class="flex w-full justify-center items-center p-2">
    <div v-if="!props.foodSelected.length" class="flex items-center">
      <img src="../assets/image/money.svg" width="50" />
      <p class="text-3xl ml-1">هزارتومان</p>
      <p class="text-3xl ml-1 mt-1">
        {{ props.payModal.playstation.userMoney?.toLocaleString() }}
      </p>
    </div>
    <table v-else dir="rtl" class="TableFoods">
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
  </div>
</template>
