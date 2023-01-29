<script setup>
import { reactive } from "vue";

const state = reactive({
  manageConsoleList: [
    { id: 1, type: "ps4", image: "one" },
    { id: 2, type: "ps2", image: "two" },
    { id: 3, type: "ps5", image: "three" },
    { id: 4, type: "ps1", image: "four" },
    { id: 5, type: "ps4", image: "five" },
    { id: 6, type: "ps3", image: "six" },
    { id: 4, type: "ps4", image: "seven" },
    { id: 5, type: "ps5", image: "eigth" },
  ],
});
</script>
<template>
  <div class="parent-consols">
    <div class="new-console">
      <p class="text-white ml-2">اضافه کردن دستگاه</p>
      <i class="fa-duotone fa-gamepad-modern text-white text-2xl"></i>
    </div>
    <div class="w-full h-full flex flex-wrap justify-center items-center mt-4">
      <div
        v-for="(items, index) in state.manageConsoleList"
        class="manage-consols"
        :key="index"
      >
        <img
          :src="`src/assets/image/numbers/${items.image}.png`"
          class="number-manage-console"
          width="50"
        />
        <div class="flex w-full justify-center items-center p-2">
          <i class="fa-duotone fa-pen-circle console-icon text-yellow-600"></i>
          <i
            class="fa-duotone fa-circle-question console-icon text-blue-600"
          ></i>
          <i class="fa-duotone fa-trash-can-list console-icon text-red-600"></i>
        </div>
      </div>
    </div>
  </div>
</template>
