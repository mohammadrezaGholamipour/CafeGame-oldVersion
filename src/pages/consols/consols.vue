<script setup>
import ConsoleTable from "./components/console-table.vue";
import { useToast } from "vue-toastification";
import { useStore } from '@/store/index'
import console from "@/api/console";
import { reactive } from "vue";
////////////////////////
const toast = useToast();
const store = useStore()
////////////////////////
const state = reactive({
  confirmDialog: {
    text: "دستگاه انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
});
///////////////////////////////
const requestNewConsole = () => {
  if (store.getConsoleList.length < 8) {
    console
      .new({ name: String(Math.random() * 100) })
      .then(() => {
        toast.success("دستگاه با موفقیت اضافه شد");
        store.requestGetConsoles()
      })
      .catch(() => {
        toast.error("دستگاه جدید اضافه نشد");
      });
  }
};
///////////////////////////////
const requestRemoveConsole = (id) => {
  console
    .remove(id)
    .then(() => {
      toast.success("دستگاه با موفقیت حذف شد");
      store.requestGetConsoles()
    })
    .catch(() => {
      toast.error("دستگاه حذف نشد");
    })
    .finally(() => {
      state.confirmDialog.status = false;
      state.confirmDialog.id = "";
    });
};
</script>
<template>
  <div class="parent-consols">
    <ConsoleTable @deleteConsole="requestRemoveConsole" :foodList="store.getFoodList" :billList="store.getBillList" :consoleList="store.getConsoleList"
      @newConsole="requestNewConsole" />
  </div>
</template>
