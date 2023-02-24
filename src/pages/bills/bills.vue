<script setup>
import TableBills from "./components/table-bills.vue";
import { useToast } from "vue-toastification";
import { reactive, onMounted } from "vue";
import billApi from "@/api/bill";
////////////////////////////////
const toast = useToast();
//////////////////////////////
const state = reactive({
  billList: [],
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
    })
    .catch(() => {
      toast.error("خطا در ارتباط با سرور");
    });
};
</script>
<template>
  <div class="Parent-bills">
    <TableBills :bills="state.billList" />
  </div>
</template>
