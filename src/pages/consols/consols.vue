<script setup>
import ConsoleTable from "./components/console-table.vue";
import { useToast } from "vue-toastification";
import { onMounted, reactive } from "vue";
import console from "../../api/console";
////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  consoleList: [],
  confirmDialog: {
    text: "دستگاه انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
});
///////////////////////////////
onMounted(() => {
  requestGetConsoles();
});
///////////////////////////////
const requestNewConsole = () => {
  if (state.consoleList.length < 8) {
    console
      .new({ name: String(Math.random() * 100) })
      .then(() => {
        toast.success("دستگاه با موفقیت اضافه شد");
        requestGetConsoles();
      })
      .catch(() => {
        toast.error("دستگاه جدید اضافه نشد");
      });
  }
};
///////////////////////////////
const requestGetConsoles = () => {
  console
    .get()
    .then((response) => {
      state.consoleList = response;
    })
    .catch(() => {
      toast.error("خطا در ارتباط با سرور");
    });
};
///////////////////////////////
const requestRemoveConsole = (id) => {
  console
    .remove(id)
    .then(() => {
      toast.success("دستگاه با موفقیت حذف شد");
      requestGetConsoles();
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
  <ConsoleTable
    @deleteConsole="requestRemoveConsole"
    :consoleList="state.confirmDialog"
    @newConsole="requestNewConsole"
  />
</template>
