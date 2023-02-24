<script setup>
import { reactive } from "vue";
import BillInfoDialog from "./bill-info-dialog.vue";
/////////////////////////////
const props = defineProps(["bills", "foods", "moneys"]);
/////////////////////////////
const state = reactive({
  headerBills: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    {
      name: "اطلاعات زمان شروع",
      icon: "fa-duotone fa-circle-play text-green-500",
    },
    {
      name: "اطلاعات زمان پایان",
      icon: "fa-duotone fa-circle-pause text-red-700",
    },
    {
      name: "قیمت واحد",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
    { name: "خوراکی ها", icon: "fa-duotone fa-burger-soda text-yellow-700" },
    {
      name: "مبلغ پرداخت شده",
      icon: "fa-duotone fa-cash-register text-blue-500",
    },
  ],
  dialog: {
    headerInfo: "",
    status: false,
  },
});
/////////////////////////////////
const handleFindMoney = (hourRateId) => {
  return `${props.moneys
    .find((items) => items.id === hourRateId)
    .rate.toLocaleString()} تومان`;
};
/////////////////////////////////
const handleCloseDialog = () => {
  state.dialog.status = false;
  setTimeout(() => {
    state.dialog.headerInfo = "";
  }, 500);
};
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
          <td>
            <div class="flex justify-center">
              <button
                @click="handleShowDialog(items.startTime, 'data')"
                class="BtnChange mr-0"
              >
                نمایش
              </button>
            </div>
          </td>
          <td>
            <div class="flex justify-center">
              <button
                @click="handleShowDialog(items.endTime, 'data')"
                class="BtnChange mr-0"
              >
                نمایش
              </button>
            </div>
          </td>
          <td>{{ handleFindMoney(items.hourRateId) }}</td>
          <td>{{ items.billFoods[0]?.foodId }}</td>
          <td>{{ items.finalCost.toLocaleString() }} تومان</td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
  <BillInfoDialog @close="handleCloseDialog">salam</BillInfoDialog>
</template>
