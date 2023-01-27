<script setup>
import ChangeNewFood from "./change-new-food.vue";
import { cafePinia } from "../store/pinia";
import { reactive } from "vue";
/////////////////////////////
const store = cafePinia();
//////////////////////////
const state = reactive({
  headerFoods: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "نام محصول", icon: "fa-duotone fa-plate-utensils" },
    { name: "قیمت محصول", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "تغییرات", icon: "fa-duotone fa-cash-register" },
  ],
});
</script>
<template>
  <div class="ParentFoods">
    <button
      data-bs-target="#tableForm"
      data-bs-toggle="modal"
      class="btn-new-food"
    >
      افزودن خوارکی
    </button>
    <table class="TableFoods">
      <thead class="bg-[#d1d1d180]">
        <tr>
          <td v-for="(items, index) in state.headerFoods" :key="index">
            <div class="TdHeaderFoods">
              <i :class="items.icon" />
              <p class="mr-2">{{ items.name }}</p>
            </div>
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(items, index) in store.foodList" :key="index">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ items.money }}</td>
          <td>
            <div class="inline-flex justify-center items-center">
              <button
                @click="store.handleRemoveFood(items.name)"
                class="BtnRemove"
              >
                <p class="ml-1">حذف</p>
                <i class="fa-duotone fa-trash"></i>
              </button>
              <button
                @click="store.handleChangefoodInfo(items)"
                data-bs-target="#tableForm"
                data-bs-toggle="modal"
                class="BtnChange"
              >
                <p class="ml-1">تغییر</p>
                <i class="fa-duotone fa-file-pen"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <!-- ///////////////////////// -->
    <ChangeNewFood />
  </div>
</template>
