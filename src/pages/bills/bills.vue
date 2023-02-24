<script setup>
import TableBills from "./components/table-bills.vue";
import { useToast } from "vue-toastification";
import { reactive, onMounted } from "vue";
import moneyApi from "@/api/money";
import billApi from "@/api/bill";
import foodApi from "@/api/food";
////////////////////////////////
const toast = useToast();
//////////////////////////////
const state = reactive({
  moneyList: [],
  billList: [],
  foodList: [],
});
//////////////////////////////////
onMounted(() => {
  requestGetBills();
});
//////////////////////////////////
const requestGetBills = () => {
  billApi
    .get()
    .then((response) => {
      state.billList = response;
      requestGetFoods();
      requestGetMoneys();
    })
    .catch(() => {
      toast.error("خطا در ارتباط با سرور");
    });
};
//////////////////////////////////
const requestGetMoneys = () => {
  moneyApi
    .get()
    .then((response) => {
      state.moneyList = response;
    })
    .catch(() => {
      toast.error("لیست قیمت ها دریافت نشد");
    });
};
//////////////////////////////////
const requestGetFoods = () => {
  foodApi
    .get()
    .then((response) => {
      state.foodList = response;
    })
    .catch(() => {
      toast.error("لیست خوارکی ها دریافت نشد");
    });
};
</script>
<template>
  <div class="Parent-bills">
    <TableBills
      :moneys="state.moneyList"
      :foods="state.foodList"
      :bills="state.billList"
    />
  </div>
</template>
