<script setup>
import PersianNumberToString from "persian-number-tostring";
import confirmDialog from "@/components/confirm-dialog.vue";
import { useWindowSize } from '@vueuse/core'
import NewMoney from "./new-money.vue";
import { reactive } from "vue";
///////////////////////
const emit = defineEmits(["newMoney", "removeMoney"]);
const props = defineProps(["listMoney"]);
///////////////////////
const { width } = useWindowSize()
///////////////////////
const state = reactive({
  tableHeader: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "قیمت به عدد", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "قیمت به حروف", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "عملیات", icon: "fa-duotone fa-cash-register" },
  ],
  formModal: false,
  confirmDialog: {
    text: "قیمت انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
});
/////////////////////////////
const handleSelectedMoney = (id) => {
  state.confirmDialog.id = id;
  state.confirmDialog.status = true;
};
/////////////////////////////////
const handleNewMoney = (newMoney) => {
  emit("newMoney", newMoney);
  state.formModal = false;
};
/////////////////////////////////
const handleCloseconfirmDialog = (value) => {
  if (value) {
    emit("removeMoney", state.confirmDialog.id);
  }
  state.confirmDialog.id = "";
  state.confirmDialog.status = false;
};
</script>
<template>
  <div class="overflow-y-scroll h-[86vh] flex items-center flex-col justify-start mt-15 rounded-md w-[90vw]">
    <div class="w-full flex items-center justify-start">
      <button @click="state.formModal = true" class="btn-new-money">
        <p class="ml-2">قیمت جدید</p>
        <i class="fa-duotone fa-money-bill-1-wave"></i>
      </button>
    </div>
    <table v-if="width > 550" class="table-money">
      <thead>
        <tr>
          <th class="sticky top-0 p-3 bg-slate-200 z-50" v-for="(items, index) in state.tableHeader" :key="index">
            <div class="flex text-lg font-bold items-center justify-center">
              <p class="ml-2">{{ items.name }}</p>
              <i :class="items.icon"></i>
            </div>
          </th>
        </tr>
      </thead>
      <tbody v-if="props.listMoney.length">
        <tr v-for="(items, index) in props.listMoney">
          <td class="td-money">{{ index + 1 }}</td>
          <td class="td-money">{{ items.rate.toLocaleString() }}</td>
          <td class="td-money">
            {{ PersianNumberToString(items.rate) }} تومان
          </td>
          <td class="td-money">
            <div class="inline-flex justify-center items-center">
              <button @click="handleSelectedMoney(items.id)" class="BtnRemove">
                <p class="ml-1">حذف</p>
                <i class="fa-duotone fa-trash"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
      <tr v-else>
        <td colspan="4">
          <div class="w-full flex items-center justify-center">
            <p class="text-center p-4 font-bold text-red-500 text-lg">
              لیست قیمت ها خالی میباشد
            </p>
            <i class="fa-duotone fa-sack-dollar text-lg text-red-700"></i>
          </div>
        </td>
      </tr>
    </table>
    <!-- /////////////////////////// -->
    <div v-else class="w-full flex flex-col items-center justify-start">
      <div v-for="(items, index) in props.listMoney" :key="index" class="parent-mobile-table min-w-[300px] flex-row">
        <div class="flex flex-col justify-between items-start">
          <!-- ////////////////////////////// -->
          <div class="flex items-center justify-end py-1">
            <p class="ml-1">ردیف</p>
            <i class="fa-duotone fa-arrow-down-wide-short" />
          </div>
          <div class="flex items-center justify-end py-1">
            <p class="ml-1">قیمت به عدد</p>
            <i class="fa-duotone fa-money-bill-1-wave text-red-500" />
          </div>
          <div class="flex items-center justify-end py-1">
            <p class="ml-1">عملیات</p>
            <i class="fa-duotone fa-cash-register text-yellow-500" />
          </div>
          <!-- ////////////////////////////// -->
        </div>
        <div class="flex flex-col justify-between items-center">
          <p class="py-1">{{ index + 1 }}</p>
          <p>{{ items.rate.toLocaleString() }}</p>
          <p>
          <div class="inline-flex justify-center items-center">
            <button @click="handleSelectedMoney(items.id)" class="BtnRemove">
              <p class="ml-1">حذف</p>
              <i class="fa-duotone fa-trash"></i>
            </button>
          </div>
          </p>
        </div>
      </div>
    </div>
    <!-- /////////////////////////// -->
    <NewMoney @close="state.formModal = false" :formModal="state.formModal" @money="handleNewMoney" />
    <!-- /////////////////////////// -->
    <confirmDialog @acceptOrCansel="handleCloseconfirmDialog" :confirmDialog="state.confirmDialog" />
  </div>
</template>
