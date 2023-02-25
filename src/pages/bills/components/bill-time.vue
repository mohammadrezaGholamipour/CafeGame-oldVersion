<script setup>
import moment from "jalali-moment";
import { reactive } from "vue";
/////////////////////////
const props = defineProps(["billTime"]);
/////////////////////////
const state = reactive({
  billTimeTitle: [
    {
      name: "تاریخ شروع",
      icon: "fa-duotone fa-calendar-days text-xl text-green-700",
    },
    { name: "تاریخ پایان", icon: "fa-solid fa-calendar-check text-xl text-red-700" },
    { name: "زمان شروع", icon: "fa-duotone fa-alarm-clock text-xl text-green-700" },
    { name: "زمان پایان", icon: "fa-duotone fa-clock text-xl text-red-700" },
  ],
});
</script>
<template>
  <div class="bill-info-start">
    <div class="flex flex-col items-center justify-center">
      <div
        class="flex w-full items-center mb-3 justify-between"
        v-for="items in state.billTimeTitle"
        :key="items"
      >
        <i :class="items.icon"></i>
        <p class="text-xl ml-2 font-bold">{{ items.name }}</p>
      </div>
    </div>
    <div class="flex text-right flex-col items-center justify-center">
      <div class="flex w-full items-center justify-end mb-3.5">
        <p class="text-xl ">
          {{
            moment(`${props.billTime.start}z`).locale("fa").format("YYYY/M/D")
          }}
        </p>
      </div>
      <div class="flex w-full items-center justify-end mb-3.5">
        <p class="text-xl">
          {{ moment(`${props.billTime.end}z`).locale("fa").format("YYYY/M/D") }}
        </p>
      </div>
      <div class="flex w-full items-center justify-end mb-3.5">
        <p class="text-xl">
          {{
            moment(`${props.billTime.start}z`).locale("fa").format("HH:mm:ss")
          }}
        </p>
      </div>
      <div class="flex w-full items-center justify-end mb-3.5">
        <p class="text-xl">
          {{ moment(`${props.billTime.end}z`).locale("fa").format("HH:mm:ss") }}
        </p>
      </div>
    </div>
  </div>
</template>
