<script setup>
import { useToast } from "vue-toastification";
import { cafePinia } from "../store/pinia";
import { reactive, watch } from "vue";
// ///////////////////////////////
const store = cafePinia();
const toast = useToast();
///////////////////////
const state = reactive({
  food: { id: "", name: "", money: "", count: 0, total: "" },
});
///////////////////////
watch(
  () => store.food,
  (value) => {
    state.food.name = value.name;
    state.food.money = value.money;
    state.food.id = value.id;
  }
);
///////////////////////
const handleAcceptFood = () => {
  const closeModal = document.getElementsByClassName("close");
  const name = state.food.name;
  const money = state.food.money;
  if (store.food.money === money && store.food.name === name) {
    closeModal[0].click();
  } else {
    store.handleAcceptFood({
      id: state.food.id,
      name: state.food.name,
      money: state.food.money,
      count: 0,
      total: "",
    });
    closeModal[0].click();
  }
};
</script>
<template>
  <div class="modal fade ParentModal" data-bs-backdrop="static" id="tableForm">
    <div class="modal-dialog ModalDivOne">
      <div class="ModalDivTwo">
        <div class="ModalHeader">
          <p class="font-bold">افزون خوراکی</p>
          <i
            class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
            @click="store.handleCloseFoodModal()"
            data-bs-dismiss="modal"
          ></i>
        </div>
        <div class="ModalMain">
          <div class="flex w-full justify-evenly items-center">
            <input
              v-model="state.food.name"
              placeholder="نام محصول"
              class="food-input"
              type="text"
            />
            <input
              v-model="state.food.money"
              placeholder="قیمت محصول"
              class="food-input"
              type="text"
            />
          </div>
        </div>
        <div class="ModalFooter">
          <button @click="handleAcceptFood()" class="BtnAccept">
            <p>ثبت</p>
            <i class="fa-duotone fa-badge-check mr-2"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
