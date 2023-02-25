<script setup>
import BillInfoDialog from "./bill-info-dialog.vue";
import BillTime from "./bill-time.vue";
import BillFood from "./bill-food.vue";
import { reactive } from "vue";

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
    component: "",
  },
});
/////////////////////////////////
const handleFindMoney = (hourRateId) => {
  return `${props.moneys
    .find((items) => items.id === hourRateId)
    ?.rate?.toLocaleString()} تومان`;
};
/////////////////////////////////
const handleShowDialog = (billSelected, type) => {
  if (type === "time") {
    state.dialog.headerInfo = "اطلاعات زمان شروع و پایان فاکتور";
    state.dialog.data = billSelected;
    state.dialog.component = type;
  } else {
    state.dialog.headerInfo = "لیست خوراکی های ثبت شده";
    state.dialog.data = billSelected;
    state.dialog.component = type;
  }
  state.dialog.status = true;
};
/////////////////////////////////
const handleCloseDialog = () => {
  state.dialog.status = false;
  setTimeout(() => {
    state.dialog.headerInfo = "";
    state.dialog.data = {};
    state.dialog.component = "";
  }, 200);
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
                @click="
                  handleShowDialog(
                    {
                      start: items.startTime,
                      end: items.endTime,
                    },
                    'time'
                  )
                "
                class="BtnChange mr-0"
              >
                نمایش
              </button>
            </div>
          </td>
          <td>{{ handleFindMoney(items.hourRateId) }}</td>
          <td>
            <div class="flex justify-center">
              <button
                @click="handleShowDialog(items.billFoods, 'food')"
                v-if="items.billFoods.length"
                class="BtnChange mr-0"
              >
                نمایش
              </button>
              <p v-else>بدون خوراکی</p>
            </div>
          </td>
          <td>{{ items.systemId }}</td>
          <td>{{ items.finalCost.toLocaleString() }} تومان</td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
  <BillInfoDialog :dialog="state.dialog" @close="handleCloseDialog">
    <BillTime
      v-if="state.dialog.component === 'time'"
      :billTime="state.dialog.data"
    />
    <BillFood :billFood="state.dialog.data" v-else />
  </BillInfoDialog>
</template>
