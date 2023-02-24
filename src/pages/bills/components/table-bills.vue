<script setup>
import { reactive } from "vue";
/////////////////////////////
const props = defineProps(["bills"]);
/////////////////////////////
const state = reactive({
  headerBills: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "اطلاعات زمان شروع", icon: "fa-duotone fa-circle-play" },
    { name: "اطلاعات زمان پایان", icon: "fa-duotone fa-circle-pause" },
    { name: "قیمت واحد", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "خوراکی ها", icon: "fa-duotone fa-burger-soda" },
    { name: "مبلغ پرداخت شده", icon: "fa-duotone fa-cash-register" },
  ],
});
/////////////////////////////////
</script>
<template>
  <div
    class="overflow-hidden flex items-center flex-col justify-start mt-15 rounded-md w-full"
  >
    <table class="Table-bills">
      <thead class="bg-[#d1d1d180]">
        <tr>
          <td v-for="(items, index) in state.headerBills" :key="index">
            <div class="td-header-foods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
            </div>
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(items, index) in props.bills" :key="items.id">
          <td>{{ index + 1 }}</td>
          <td>{{ items.startTime }}</td>
          <td>{{ items.endTime }}</td>
          <td>{{ items.hourRateId }}</td>
          <td>{{ items.billFoods[0].foodId }}</td>
          <td>{{ items.finalCost }}</td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
</template>
