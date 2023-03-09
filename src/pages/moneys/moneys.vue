<script setup>
import TableMoney from "./components/table-moneys.vue";
import { useToast } from "vue-toastification";
import { useStore } from '../../store/index'
import money from "@/api/money";
//////////////////////////////////////////
const toast = useToast();
const store = useStore()
///////////////////////////////
const requestNewMoney = (newMoney) => {
  money
    .new(newMoney)
    .then(() => {
      toast.success("قیمت جدید با موفقیت اضافه شد");
      store.requestGetMoneys()
    })
    .catch(() => {
      toast.error("قیمت مورد نظر اضافه نشد");
    });
};
///////////////////////////////
const requestRemoveMoney = (id) => {
  money
    .remove(id)
    .then(() => {
      store.requestGetMoneys()
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
    <TableMoney @removeMoney="requestRemoveMoney" :listMoney="store.getMoneyList" @newMoney="requestNewMoney" />
  </div>
</template>
