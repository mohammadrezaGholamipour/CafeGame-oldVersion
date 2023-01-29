<script setup>
import ChangeNewFood from "./change-new-food.vue";
import { reactive } from "vue";
/////////////////////////////
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
        <tr>
          <td>1</td>
          <td>چیپس</td>
          <td>12,000</td>
          <td>
            <div class="inline-flex justify-center items-center">
              <button class="BtnRemove">
                <p class="ml-1">حذف</p>
                <i class="fa-duotone fa-trash"></i>
              </button>
              <button
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
