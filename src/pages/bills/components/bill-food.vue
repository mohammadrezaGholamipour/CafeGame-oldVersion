<script setup>
import { computed, reactive } from "vue";
////////////////////////////
const props = defineProps(["billFood", "foods"]);
///////////////////////////////
const state = reactive({
  headerBillFood: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    {
      name: "نام محصول",
      icon: "fa-duotone fa-burger-soda text-yellow-700",
    },
    {
      name: "قیمت واحد",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
    { name: "تعداد", icon: "fa-duotone fa-cash-register" },
    {
      name: "جمع کل",
      icon: "fa-duotone fa-cash-register text-blue-500",
    },
  ],
  foodList: [],
});
///////////////////////////////
const handleFood = computed(() => {
  state.foodList = [];
  for (const food of props.foods) {
    for (const billFood of props.billFood) {
      if (food.id === billFood.foodId) {
        billFood.name = food.name;
        billFood.cost = food.cost;
        billFood.total = billFood.cost * billFood.count;
        state.foodList.push(billFood);
      }
    }
  }
  return state.foodList;
});
////////////////////////////
const handleTotalFoodMoney = computed(() => {
  const foodMoney = [];
  for (const item of state.foodList) {
    foodMoney.push(item.total);
  }
  const totalFoodMoney = foodMoney.reduce((total, item) => {
    return total + item;
  });
  return totalFoodMoney;
});
</script>
<template>
  <div class="w-full overflow-y-scroll max-h-[500px] rounded-md">
    <table dir="rtl" class="Table-bills ">
      <thead class="bg-[#d1d1d180]">
        <tr>
          <td v-for="(items, index) in state.headerBillFood" :key="index">
            <div class="td-header-foods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
            </div>
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(items, index) in handleFood.reverse()" :key="items.id">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ items.cost.toLocaleString() }} تومان</td>
          <td>{{ items.count }}</td>
          <td>{{ items.total.toLocaleString() }} تومان</td>
        </tr>
      </tbody>
      <tfoot v-if="handleFood.length > 1">
        <tr class="bg-red-500 text-white">
          <td class="p-2" colspan="4">
            <p class="text-center text-xl">جمع کل</p>
          </td>
          <td>
            <p class="text-center text-lg">
              {{ handleTotalFoodMoney.toLocaleString() }} تومان
            </p>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>
