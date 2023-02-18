<script setup>
import TableMoney from "./components/table-moneys.vue";
import { useToast } from "vue-toastification";
import { reactive, onMounted } from "vue";
import money from "@/api/money";
//////////////////////////////////////////
const toast = useToast();
///////////////////////
const state = reactive({
  listMoney: [],
});
///////////////////////
onMounted(() => {
  requestGetMoneys();
});
///////////////////////////////
const requestNewMoney = (newMoney) => {
  money
    .new(newMoney)
    .then(() => {
      toast.success("قیمت جدید با موفقیت اضافه شد");
      requestGetMoneys();
    })
    .catch(() => {
      toast.error("قیمت مورد نظر اضافه نشد");
    });
};
///////////////////////////////
const requestGetMoneys = () => {
  money
    .get()
    .then((response) => {
      state.listMoney = response;
    })
    .catch(() => {
      toast.error("لیست قیمت های ثبت شده دریافت نشد");
    });
};
///////////////////////////////
const requestRemoveMoney = (id) => {
  money
    .remove(id)
    .then(() => {
      requestGetMoneys();
      toast.success("با موفقیت حذف شد");
    })
    .catch(() => {
      toast.error("قیمت مورد نظر حذف نشد");
    });
};
/////////////////////////////////
</script>
<template>
  <div class="parent-management-money">
    <TableMoney
      @removeMoney="requestRemoveMoney"
      :listMoney="state.listMoney"
      @newMoney="requestNewMoney"
    />
  </div>
</template>
