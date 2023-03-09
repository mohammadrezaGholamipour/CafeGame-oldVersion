<script setup>
import TableFoods from "./components/table-foods.vue";
import { useToast } from "vue-toastification";
import { useStore } from '@/store/index'
import food from "@/api/food";
//////////////////////////////
const toast = useToast();
const store = useStore()
////////////////////////
const requestNewFood = (newFood) => {
  food
    .new(newFood)
    .then(() => {
      toast.success("خوراکی با موفقیت اضافه شد");
      store.requestGetfoods();
    })
    .catch(() => {
      toast.error("خوراکی جدید اضافه نشد");
    });
};
///////////////////////////////
const requestRemovefood = (id) => {
  food
    .remove(id)
    .then(() => {
      toast.success(" خوراکی با موفقیت حذف شد");
      store.requestGetfoods();
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
      store.requestGetfoods();
    })
    .catch(() => {
      toast.error("خوراکی عوض نشد");
    });
};
</script>
<template>
  <div class="ParentFoods">
    <TableFoods @updatefood="requestUpdatefood" @removefood="requestRemovefood" :listFood="store.getFoodList"
      @newFood="requestNewFood" />
  </div>
</template>
