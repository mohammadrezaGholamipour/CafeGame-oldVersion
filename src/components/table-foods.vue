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
  mainFoods: [
    { name: "اب کوچک", money: "4000", count: 0, total: "" },
    { name: "چیپس", money: "12000", count: 0, total: "" },
    { name: "نوشابه", money: "8000", count: 0, total: "" },
    { name: "پفک", money: "10000", count: 0, total: "" },
    { name: "کلوچه", money: "5000", count: 0, total: "" },
  ],
  removeFood: false,
});
const handleRemoveFood = (food) => {
  state.mainFoods = state.mainFoods.filter((items) => items.name !== food);
};
</script>
<template>
  <div class="ParentFoods">
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
        <tr v-for="(items, index) in state.mainFoods" :key="index">
          <td>{{ index + 1 }}</td>
          <td>{{ items.name }}</td>
          <td>{{ Number(items.money).toLocaleString() }}</td>
          <td>
            <div class="inline-flex justify-center items-center">
              <button class="BtnRemove" @click="handleRemoveFood(items.name)">
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
