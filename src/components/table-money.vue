<script setup>
import { reactive, onMounted } from "vue";
import money from "../api/money";
import ChangeNewMoney from "./change-new-money.vue";
import ConfirmRemove from "./confirm-modal.vue";
///////////////////////
const state = reactive({
  tableHeader: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "قیمت به عدد", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "قیمت به حروف", icon: "fa-duotone fa-comments-dollar" },
    { name: "عملیات", icon: "fa-duotone fa-cash-register" },
  ],
  listMoney: [],
  modalData: {
    text: "قیمت انتخاب شده حذف شود؟",
    id: "",
  },
});
/////////////////////////////
onMounted(() => {
  requestGetMoneys();
});
///////////////////////////////
const requestNewMoney = (newMoney) => {
  money
    .new(newMoney)
    .then((response) => {
      requestGetMoneys();
    })
    .catch((error) => {
      console.log(error);
    });
};
///////////////////////////////
const requestGetMoneys = () => {
  money
    .get()
    .then((response) => {
      state.listMoney = response;
    })
    .catch((error) => {
      console.log(error);
    });
};
///////////////////////////////
const requestRemoveMoney = () => {
  money
    .remove(state.modalData.id)
    .then((response) => {
      requestGetMoneys();
    })
    .catch((error) => {
      console.log(error);
    });
};
/////////////////////////////////
</script>
<template>
  <div class="flex w-full flex-col justify-center items-start mt-5">
    <button
      data-bs-target="#tableForm"
      class="btn-new-money"
      data-bs-toggle="modal"
    >
      قیمت جدید
    </button>
    <table class="table-money">
      <thead>
        <tr>
          <td
            v-for="(items, index) in state.tableHeader"
            :key="index"
            class="p-3"
          >
            <div class="flex text-lg font-bold items-center justify-center">
              <p class="ml-2">{{ items.name }}</p>
              <i :class="items.icon"></i>
            </div>
          </td>
        </tr>
      </thead>
      <tbody v-if="state.listMoney.length">
        <tr v-for="(items, index) in state.listMoney">
          <td class="td-money">{{ index + 1 }}</td>
          <td class="td-money">{{ items.rate.toLocaleString() }}</td>
          <td class="td-money">{{ items.rate }}</td>
          <td class="td-money">
            <div class="inline-flex justify-center items-center">
              <button
                @click="state.modalData.id = items.id"
                data-bs-target="#confirmRemoveConsole"
                data-bs-toggle="modal"
                class="BtnRemove"
              >
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
            <p class="text-center p-4 font-bold text-red-500 text-xl">
              لیست قیمت ها خالی میباشد
            </p>
            <i class="fa-duotone fa-sack-dollar text-xl text-red-700"></i>
          </div>
        </td>
      </tr>
    </table>
    <!-- /////////////////////////// -->
    <ChangeNewMoney @money="requestNewMoney" />
    <!-- /////////////////////////// -->
    <ConfirmRemove @accept="requestRemoveMoney()" :modal="state.modalData" />
  </div>
</template>
