<script setup>
import ConsolePlayStation from "../components/console-playStation.vue";
import { useToast } from "vue-toastification";
import { onMounted, reactive } from "vue";
import console from "../api/console";
import money from "../api/money";
import food from "../api/food";
import bill from "../api/bill";
/////////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  consoleList: [],
  moneyList: [],
  foodList: [],
  billList: [],
});
////////////////////////
const requestGetConsoles = () => {
  console
    .get()
    .then((response) => {
      state.consoleList = response;
      requestGetMoneys();
    })
    .catch(() => {
      toast.error("لیست دستگاه ها دریافت نشد");
    });
};
////////////////////////
const requestGetMoneys = () => {
  money
    .get()
    .then((response) => {
      state.moneyList = response;
      requestGetfoods();
    })
    .catch(() => {
      toast.error("لیست قیمت ها دریافت نشد");
    });
};
////////////////////////
const requestGetfoods = () => {
  food
    .get()
    .then((response) => {
      state.foodList = response;
      requestGetBills();
    })
    .catch(() => {
      toast.error("لیست خوراکی ها دریافت نشد");
    });
};
////////////////////////
const requestGetBills = () => {
  bill
    .get()
    .then((response) => {
      state.billList = response;
    })
    .catch(() => {
      toast.error("لیست فاکتور ها دریافت نشد");
    });
};
onMounted(() => {
  requestGetConsoles();
});
</script>
<template>
  <div class="ParentHome">
    <ConsolePlayStation
      @requestGetBills="requestGetBills"
      :console-list="state.consoleList"
      :money-list="state.moneyList"
      :food-list="state.foodList"
      :bill-list="state.billList"
    />
  </div>
</template>
