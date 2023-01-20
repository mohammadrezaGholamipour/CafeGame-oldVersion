<script setup>
import { cafePinia } from "../store/pinia";
///////////////////////////////////////////
const store = cafePinia();
///////////////////////////////////////////
</script>
<template>
  <div class="Parent-food-store">
    <div class="flex flex-col">
      <p class="text-xl font-bold">
        نام محصول <i class="fa-duotone fa-burger-soda"></i>
      </p>
      <p
        class="mt-3 p-1 font-bold"
        v-for="(items, index) in store.foodList"
        :key="index"
      >
        {{ items.name }}
      </p>
    </div>
    <div class="flex flex-col">
      <p class="text-xl font-bold">تعداد <i class="fa-duotone fa-tally"></i></p>
      <div
        v-for="(items, index) in store.foodList"
        class="mt-3 flex items-center"
        :key="index"
      >
        <i
          class="fa-duotone fa-circle-plus text-green-600 text-xl cursor-pointer"
          @click="store.handlePlusFood(index)"
        ></i>
        <p class="mx-3 font-bold p-1 pt-1.5 px-3 shadow-md flex items-center bg-yellow-500 rounded-full text-white">{{ items.count }}</p>
        <i
          class="fa-duotone fa-circle-minus text-red-500 text-xl cursor-pointer"
          @click="store.handleMinusFood(index)"
        ></i>
      </div>
    </div>
  </div>
</template>
