<script setup>
import confirmModal from "./confirm-modal.vue";
import { useToast } from "vue-toastification";
import { cafePinia } from "../store/pinia";
import { reactive, onMounted } from "vue";
import NewMoney from "./new-money.vue";
import money from "../api/money";
///////////////////////
const toast = useToast();
const store = cafePinia();
///////////////////////
const state = reactive({
  tableHeader: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "قیمت به عدد", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "عملیات", icon: "fa-duotone fa-cash-register" },
  ],
  listMoney: [],
  formModal: false,
  confirmModal: {
    text: "قیمت انتخاب شده حذف شود؟",
    status: false,
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
    .then(() => {
      toast.success("قیمت جدید با موفقیت اضافه شد");
      requestGetMoneys();
    })
    .catch(() => {
      toast.error("قیمت مورد نظر اضافه نشد");
    })
    .finally(() => {
      state.formModal = false;
    });
};
///////////////////////////////
const requestGetMoneys = () => {
  money
    .get()
    .then((response) => {
      state.listMoney = response;
      store.handleChangemoneyList(response);
    })
    .catch((error) => {
      console.log(error);
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
    })
    .finally(() => {
      state.confirmModal.id = "";
      state.confirmModal.status = false;
    });
};
/////////////////////////////////
const handleSelectedMoney = (id) => {
  state.confirmModal.id = id;
  state.confirmModal.status = true;
};
/////////////////////////////////
const handleCloseConfirmModal = (value) => {
  if (value) {
    requestRemoveMoney(state.confirmModal.id);
  } else {
    state.confirmModal.id = "";
    state.confirmModal.status = false;
  }
};
</script>
<template>
  <div class="flex w-full flex-col justify-center items-start mt-5">
    <button @click="state.formModal = true" class="btn-new-money">
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
    <NewMoney
      @close="state.formModal = false"
      :formModal="state.formModal"
      @money="requestNewMoney"
    />
    <!-- /////////////////////////// -->
    <confirmModal
      @acceptOrCansel="handleCloseConfirmModal"
      :confirmModal="state.confirmModal"
    />
  </div>
</template>
