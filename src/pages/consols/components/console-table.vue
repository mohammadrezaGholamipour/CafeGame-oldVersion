<script setup>
import confirmDialog from "@/components/confirm-dialog.vue";
import { reactive } from "vue";
import ConsoleBills from "./console-bills.vue";
////////////////////////
const emit = defineEmits(["newConsole", "deleteConsole"]);
const props = defineProps(["consoleList"]);
////////////////////////
const state = reactive({
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
  state.dialog.headerInfo = "فاکتور های ثبت شده در این دستگاه";
  state.dialog.data = consoleSelected;
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
            <img :src="`src/assets/image/numbers/${index + 1}.png`" class="bg-white rounded-full outline-none"
              width="50" />
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
