<script setup>
import { reactive } from "vue";
import BillInfoDialog from "./bill-info-dialog.vue";
import BillInfoStart from "./bill-time.vue";
/////////////////////////////
const props = defineProps(["bills", "foods", "moneys"]);
/////////////////////////////
const state = reactive({
  headerBills: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    {
      name: "اطلاعات زمان شروع و پایان",
      icon: "fa-duotone fa-hourglass-start text-red-500 ",
    },
    {
      name: "قیمت واحد",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
    { name: "خوراکی ها", icon: "fa-duotone fa-burger-soda text-yellow-700" },
    {
      name: "شماره دستگاه",
      icon: "fa-duotone fa-cash-register text-blue-500",
    },
    {
      name: "مبلغ پرداخت شده",
      icon: "fa-duotone fa-cash-register text-blue-500",
    },
  ],
  dialog: {
    headerInfo: "",
    data: {},
    status: false,
  },
});
/////////////////////////////////
const handleFindMoney = (hourRateId) => {
  return `${props.moneys
    .find((items) => items.id === hourRateId)
    ?.rate?.toLocaleString()} تومان`;
};
/////////////////////////////////
const handleShowDialog = (billStartInfo, billEndInfo) => {
  const BillTime = { start: billStartInfo, end: billEndInfo };
  state.dialog.headerInfo = "اطلاعات زمان شروع و پایان فاکتور";
  state.dialog.data = BillTime;
  state.dialog.status = true;
};
/////////////////////////////////
const handleCloseDialog = () => {
  state.dialog.status = false;
  setTimeout(() => {
    state.dialog.headerInfo = "";
    state.dialog.data = {};
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
                @click="handleShowDialog(items.startTime, items.endTime)"
                class="BtnChange mr-0"
              >
                نمایش
              </button>
            </div>
          </td>
          <td>{{ handleFindMoney(items.hourRateId) }}</td>
          <td>{{ items.billFoods[0]?.foodId }}</td>
          <td>{{ items.systemId }}</td>
          <td>{{ items.finalCost.toLocaleString() }} تومان</td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
  <BillInfoDialog :dialog="state.dialog" @close="handleCloseDialog">
    <BillInfoStart :billTime="state.dialog.data" />
  </BillInfoDialog>
</template>
