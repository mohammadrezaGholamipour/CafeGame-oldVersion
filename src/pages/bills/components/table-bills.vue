<script setup>
import BillInfoDialog from "./bill-info-dialog.vue";
import BillTime from "./bill-time.vue";
import BillFood from "./bill-food.vue";
import moment from "jalali-moment";
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
      name: "هزینه بازی",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
    { name: "خوراکی ها", icon: "fa-duotone fa-burger-soda text-yellow-700" },
    {
      name: "شماره دستگاه",
      icon: "fa-brands fa-playstation text-blue-500",
    },
    {
      name: "مبلغ پرداخت شده",
      icon: "fa-duotone fa-cash-register text-green-700",
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
//////////////////////////////
const handleShowEndTime = (endTime) => {
  const finishFaktorDay = Number(moment(`${endTime}z`).locale("fa").format("DD"))
  const finishFaktorMonth = Number(moment(`${endTime}z`).locale("fa").format("MM"))
  ///////////////////////////////////////////
  const dayNow = Number(moment().locale("fa").format("DD"))
  const monthNow = Number(moment().locale("fa").format("MM"))
  ///////////////////////////////////////////
  if (monthNow === finishFaktorMonth) {
    switch (dayNow) {
      case finishFaktorDay:
        return 'همین امروز';
      case finishFaktorDay + 1:
        return 'دیروز';
      case finishFaktorDay + 2:
        return 'دو روز پیش';
      case finishFaktorDay + 3:
        return 'سه روز پیش';
      default:
        return moment(`${endTime}z`).locale("fa").format("YY/MM/DD");
    }
  } else {
    return moment(`${endTime}z`).locale("fa").format("YY/MM/DD")
  }


} 
</script>
<template>
  <div class="overflow-y-scroll h-[86vh] flex items-center flex-col justify-start mt-15 rounded-md w-[90vw]">
    <table class="Table-bills">
      <thead>
        <tr>
          <th class="sticky top-0 bg-slate-200" v-for="(items, index) in state.headerBills" :key="index">
            <div class="td-header-foods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
            </div>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="props.bills.length" v-for="(items, index) in props.bills" :key="items.id">
          <td>{{ index + 1 }}</td>
          <td>
            <div class="flex items-center justify-between">
              <p class="ml-2">{{ items.endTime ? handleShowEndTime(items.endTime) : 'هنوز تمام نشده است' }}</p>
              <button @click="
                handleShowDialog(
                  {
                    start: items.startTime,
                    end: items.endTime,
                  },
                  'time'
                )
              " class="BtnChange mr-0">
                نمایش
              </button>

            </div>
          </td>
          <td>{{ handleFindMoney(items.hourRateId) }}</td>
          <td>
            <div class="flex justify-center">
              <button @click="handleShowDialog(items.billFoods, 'food')" v-if="items.billFoods.length"
                class="BtnChange mr-0">
                نمایش
              </button>

              <p v-else>بدون خوراکی</p>
            </div>
          </td>
          <td>{{ items.systemId }}</td>
          <td>
            <p class="font-bold"> {{
              items.finalCost
              ? `${items.finalCost?.toLocaleString()} تومان`
              : "هنوز تمام نشده است"
            }}</p>
          </td>
        </tr>
        <tr v-else>
          <td colspan="6">
            <div class="w-full flex items-center justify-center">
              <p class="text-center p-2 font-bold text-red-500 text-lg">
                لیست تراکنش ها خالی میباشد
              </p>
              <i class="fa-duotone fa-sack-dollar text-lg text-red-700"></i>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
  <BillInfoDialog :dialog="state.dialog" @close="handleCloseDialog">
    <BillTime v-if="state.dialog.component === 'time'" :billTime="state.dialog.data" />
    <BillFood :foods="props.foods" :billFood="state.dialog.data" v-else />
  </BillInfoDialog>
</template>
