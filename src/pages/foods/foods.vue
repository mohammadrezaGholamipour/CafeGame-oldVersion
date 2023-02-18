<script setup>
import TableFoods from "./components/table-foods.vue";
import { useToast } from "vue-toastification";
import { onMounted } from "vue";
import { reactive } from "vue";
import food from "@/api/food";
//////////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  listFood: [],
});
////////////////////////
onMounted(() => {
  requestGetfoods();
});
///////////////////////////////
const requestNewFood = (newFood) => {
  food
    .new(newFood)
    .then(() => {
      toast.success("خوراکی با موفقیت اضافه شد");
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
    .catch(() => {
      toast.error("خوراکی حذف نشد");
    });
};
///////////////////////////////
const requestUpdatefood = (foodId, foodData) => {
  food
    .update(foodId, foodData)
    .then(() => {
      toast.success(" خوراکی با موفقیت عوض شد");
      requestGetfoods();
    })
    .catch(() => {
      toast.error("خوراکی عوض نشد");
    });
};
</script>
<template>
  <div class="ParentFoods">
    <TableFoods
      @updatefood="requestUpdatefood"
      @removefood="requestRemovefood"
      :listFood="state.listFood"
      @newFood="requestNewFood"
    />
  </div>
</template>
