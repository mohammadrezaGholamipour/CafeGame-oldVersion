<script setup>
import { useWindowSize } from '@vueuse/core'
import { computed, reactive } from "vue";
////////////////////////////
const props = defineProps(["billFood", "foods"]);
///////////////////////////////
const { width } = useWindowSize()
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
});
///////////////////////////////
const handleTotalFoodMoney = computed(() => {
  const foodMoney = [];
  for (const item of props.billFood) {
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
    <table v-if="width > 650" dir="rtl" class="Table-bills ">
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
        <tr v-for="(items, index) in props.billFood" :key="items.id">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ items.cost?.toLocaleString() }} تومان</td>
          <td>{{ items.count }}</td>
          <td>{{ items.total?.toLocaleString() }} تومان</td>
        </tr>
      </tbody>
      <tfoot v-if="props.billFood.length > 1">
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
    <!-- ///////////////////////////// -->
    <div v-else class="w-full flex flex-col items-center justify-start">
      <div v-for="(items, index) in props.billFood" :key="index" class="parent-mobile-table bg-slate-200">
        <div class="flex flex-col justify-between items-end">
          <!-- ////////////////////////////// -->
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-arrow-down-wide-short" />
            <p class="ml-1">ردیف</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-burger-soda text-yellow-700" />
            <p class="ml-1">نام محصول</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-money-bill-1-wave text-green-500" />
            <p class="ml-1">قیمت واحد</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-cash-register" />
            <p class="ml-1">تعداد</p>
          </div>
          <div class="flex items-center justify-end">
            <i class="fa-duotone fa-cash-register text-blue-500" />
            <p class="ml-1">جمع کل</p>
          </div>
          <!-- ////////////////////////////// -->
        </div>
        <div class="flex flex-col justify-between items-center">
          <p>{{ index + 1 }}</p>
          <p>{{ items.name }}</p>
          <p>{{ items.cost?.toLocaleString() }} تومان</p>
          <p>{{ items.count }}</p>
          <p>{{ items.total?.toLocaleString() }} تومان</p>
        </div>
      </div>
      <div v-if="props.billFood.length > 1"
        class="bg-red-500 flex flex-col items-center rounded-md shadow-md overflow-hidden mt-3 justify-center w-full text-white">
        <div class="flex w-full justify-between items-center p-2">
          <p> {{ handleTotalFoodMoney.toLocaleString() }} تومان</p>
          <p>جمع کل</p>
        </div>

      </div>
    </div>
  </div>
</template>
