<script setup>
import ChangeNewFood from "./change-new-food.vue";
import { useToast } from "vue-toastification";
import food from "../api/food";
import { reactive, onMounted } from "vue";
/////////////////////////////
const toast = useToast();
/////////////////////////////
const state = reactive({
  headerFoods: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    { name: "نام محصول", icon: "fa-duotone fa-plate-utensils" },
    { name: "قیمت محصول", icon: "fa-duotone fa-money-bill-1-wave" },
    { name: "تغییرات", icon: "fa-duotone fa-cash-register" },
  ],
  listFood: [],
  foodSelected: {},
});
/////////////////////////////
onMounted(() => {
  requestGetfoods();
});
///////////////////////////////
const requestNewFood = (newFood) => {
  food
    .new(newFood)
    .then((response) => {
      toast.success("خوراکی با موفقیت اضافه شد");
      state.foodSelected = {};
      requestGetfoods();
    })
    .catch(() => {
      toast.error("خوراکی جدید اضافه نشد");
    });
};
///////////////////////////////
const requestGetfoods = () => {
  food
    .get()
    .then((response) => {
      state.listFood = response;
    })
    .catch(() => {
      toast.error("خطا در ارتباط با سرور");
    });
};
///////////////////////////////
const requestRemovefood = (id) => {
  food
    .remove(id)
    .then(() => {
      toast.success(" خوراکی با موفقیت حذف شد");
      requestGetfoods();
    })
    .catch((error) => {
      toast.error("خوراکی حذف نشد");
    });
};
///////////////////////////////
const requestUpdatefood = (foodData) => {
  food
    .update(state.foodSelected.id, foodData)
    .then(() => {
      toast.warning(" خوراکی با موفقیت عوض شد");
      requestGetfoods();
    })
    .catch((error) => {
      toast.error("خوراکی عوض نشد");
    });
};
/////////////////////////////////
const handleFood = (food) => {
  const foodItem = state.listFood.find(
    (items) => items.id === state.foodSelected.id
  );
  if (foodItem) {
    requestUpdatefood(food);
  } else {
    requestNewFood(food);
  }
};
/////////////////////////////////
const handleFoodSelected = (items) => {
  state.foodSelected = items;
};
</script>
<template>
  <div class="w-full flex flex-col items-start justify-center">
    <button
      @click="handleFoodSelected({})"
      data-bs-target="#tableForm"
      data-bs-toggle="modal"
      class="btn-new-food"
    >
      افزودن خوارکی
    </button>
    <div class="overflow-y-scroll rounded-md w-full" style="height: 76vh">
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
        <tbody class="test">
          <tr v-for="(items, index) in state.listFood" :key="index">
            <td>{{ index + 1 }}</td>
            <td>{{ items.name }}</td>
            <td>{{ items.cost?.toLocaleString() }}</td>
            <td>
              <div class="inline-flex justify-center items-center">
                <button @click="requestRemovefood(items.id)" class="BtnRemove">
                  <p class="ml-1">حذف</p>
                  <i class="fa-duotone fa-trash"></i>
                </button>
                <button
                  @click="handleFoodSelected(items)"
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
    </div>
    <!-- ///////////////////////// -->
    <ChangeNewFood :food="state.foodSelected" @food="handleFood" />
  </div>
</template>
