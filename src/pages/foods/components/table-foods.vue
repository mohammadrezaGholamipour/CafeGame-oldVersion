<script setup>
import confirmDialog from "../../../components/confirm-dialog.vue";
import ChangeNewFood from "./change-new-food.vue";
import { reactive } from "vue";
/////////////////////////////
const emit = defineEmits(["newFood", "updatefood", "removefood"]);
const props = defineProps(["listFood"]);
/////////////////////////////
const state = reactive({
  headerFoods: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "نام محصول", icon: "fa-duotone fa-plate-utensils" },
    { name: "قیمت محصول", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "تغییرات", icon: "fa-duotone fa-cash-register" },
  ],
  foodSelected: {
    data: {},
    status: false,
  },
  confirmDialog: {
    text: "خوراکی انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
});
/////////////////////////////////
const handleChangeOrNewFood = (food) => {
  const foodItem = props.listFood.find(
    (items) => items.id === state.foodSelected.data.id
  );
  if (foodItem) {
    emit("updatefood", state.foodSelected.data.id, food);
  } else {
    emit("newFood", food);
  }
  handleCloseFormDialog();
};
/////////////////////////////////
const handleConfirmDialog = (id) => {
  state.confirmDialog.id = id;
  state.confirmDialog.status = true;
};
/////////////////////////////////
const handleFoodSelected = (items) => {
  state.foodSelected.data = items;
  state.foodSelected.status = true;
};
/////////////////////////////////
const handleCloseFormDialog = () => {
  state.foodSelected.status = false;
  setTimeout(() => {
    state.foodSelected.data = {};
  }, 300);
};
/////////////////////////////////
const handleCloseConfirmDialog = (value) => {
  if (value) {
    emit("removefood", state.confirmDialog.id);
  }
  state.confirmDialog.id = "";
  state.confirmDialog.status = false;
};
</script>
<template>
  <div
    class="overflow-hidden flex items-center flex-col justify-start mt-15 rounded-md w-full"
  >
    <button @click="handleFoodSelected({})" class="btn-new-food">
      <p class="ml-2">افزودن خوراکی</p>
      <i class="fa-duotone fa-plate-utensils"></i>
    </button>
    <table class="TableFoods">
      <thead class="bg-[#d1d1d180]">
        <tr>
          <td v-for="(items, index) in state.headerFoods" :key="index">
            <div class="TdHeaderFoods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
            </div>
          </td>
        </tr>
      </thead>
      <tbody v-if="props.listFood.length">
        <tr v-for="(items, index) in props.listFood" :key="index">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ items.cost?.toLocaleString() }}</td>
          <td>
            <div class="inline-flex justify-center items-center">
              <button @click="handleFoodSelected(items)" class="BtnChange">
                <p class="ml-1">تغییر</p>
                <i class="fa-duotone fa-file-pen"></i>
              </button>
              <button @click="handleConfirmDialog(items.id)" class="BtnRemove">
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
              لیست خوراکی ها خالی میباشد
            </p>
            <i class="fa-duotone fa-utensils-slash text-lg text-red-700"></i>
          </div>
        </td>
      </tr>
    </table>
  </div>
  <!-- ///////////////////////////////// -->
  <ChangeNewFood
    :formModal="state.foodSelected"
    @close="handleCloseFormDialog"
    @food="handleChangeOrNewFood"
  />
  <!-- ///////////////////////////////// -->
  <confirmDialog
    @acceptOrCansel="handleCloseConfirmDialog"
    :confirmDialog="state.confirmDialog"
  />
</template>
