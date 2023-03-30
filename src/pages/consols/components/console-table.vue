<script setup>
import confirmDialog from "@/components/confirm-dialog.vue";
import ConsoleBills from "./console-bills.vue";
import { reactive } from "vue";
////////////////////////
const props = defineProps(["consoleList", "billList", "foodList"]);
const emit = defineEmits(["newConsole", "deleteConsole"]);
////////////////////////
const state = reactive({
  totalTime: {
    hours: '',
    minutes: '',
    seconds: ''
  },
  totalMoney: '',
  confirmDialog: {
    text: "دستگاه انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
  dialog: {
    headerInfo: "",
    data: {},
    status: false,
    component: "",
  },
});
///////////////////////////////////
const handleCloseConfirmDialog = (value) => {
  if (value) {
    emit("deleteConsole", state.confirmDialog.id);
  }
  state.confirmDialog.status = false;
  state.confirmDialog.id = "";
};
///////////////////////////////////
const handleShowConfirmDialog = (items) => {
  state.confirmDialog.id = items.id;
  state.confirmDialog.status = true;
};
///////////////////////////////////
const handleShowDialog = (consoleSelected) => {
  state.totalMoney = 0
  state.totalTime.hours = 0
  state.totalTime.minutes = 0
  state.totalTime.seconds = 0
  //////////////////////////
  const consoleBills = props.billList.filter((items) => items.systemId === consoleSelected.id && items.endTime)
  for (const bill of consoleBills) {
    //////////////////////////
    const startTime = new Date(`${bill.startTime}Z`);
    const endTime = new Date(`${bill.endTime}Z`);
    let delta = Math.abs(endTime.getTime() - startTime.getTime()) / 1000;
    let days = Math.floor(delta / 86400);
    delta -= days * 86400;
    let hours = Math.floor(delta / 3600) % 24;
    delta -= hours * 3600;
    let minutes = Math.floor(delta / 60) % 60;
    delta -= minutes * 60;
    let seconds = Math.floor(delta % 60);
    bill.time = {
      hours,
      minutes,
      seconds,
    };
    //////////////////////////
    state.totalTime.hours += bill.time.hours
    state.totalTime.minutes += bill.time.minutes
    state.totalTime.seconds += bill.time.seconds
    //////////////////////////
    if (bill.billFoods.length) {
      bill.moneyPlayGame = bill.finalCost
      for (const foodBill of bill.billFoods) {
        for (const food of props.foodList) {
          if (foodBill.foodId === food.id) {
            bill.moneyPlayGame -= food.cost * foodBill.count
          }
        }
      }
      state.totalMoney += bill.moneyPlayGame
    } else {
      state.totalMoney += bill.finalCost
    }

    //////////////////////////
  }
  state.dialog.headerInfo = "فاکتور های ثبت شده در این دستگاه";
  state.dialog.data = {
    totalMoney: state.totalMoney,
    totalTime: state.totalTime
  };
  state.dialog.status = true;
}
///////////////////////////////////
const handleCloseDialog = () => {
  state.dialog.status = false;
  setTimeout(() => {
    state.dialog.headerInfo = "";
    state.dialog.data = {};
  }, 200);
};
///////////////////////////////////
const getImageUrl = (imagePath) => {
  return new URL(imagePath, import.meta.url).href
}
///////////////////////////////////
const handleNewConsole = () => {
  emit("newConsole");
};
</script>
<template>
  <div class="flex flex-col w-full justify-center items-center">
    <!-- //////////////////////////// -->
    <div class="new-console" @click="handleNewConsole">
      <p class="ml-2 text-white">افزودن دستگاه</p>
      <i class="fa-duotone fa-gamepad-modern text-white text-2xl"></i>
    </div>
    <!-- //////////////////////////// -->
    <transition-slide group>
      <div class="w-full h-full flex flex-wrap justify-center items-center">
        <div v-for="(items, index) in props.consoleList" class="manage-consols" :key="index">
          <img src="@/assets/image/logoConsole.png" width="150" />
          <div class="flex mt-2 w-full justify-center items-center p-2">
            <i @click="handleShowDialog(items)" class="fa-duotone fa-circle-question console-icon text-gray-200"></i>
            <img :src="getImageUrl(`/assets/${index + 1}.png`)" class="bg-white rounded-full outline-none" width="50" />
            <i class="fa-duotone fa-trash-can-list console-icon text-gray-200"
              @click="handleShowConfirmDialog(items)"></i>
          </div>
        </div>
      </div>
    </transition-slide>
  </div>
  <!-- //////////////////////////// -->
  <ConsoleBills :dialog="state.dialog" @close="handleCloseDialog" />
  <!-- //////////////////////////// -->
  <confirmDialog @acceptOrCansel="handleCloseConfirmDialog" :confirmDialog="state.confirmDialog" />
</template>
