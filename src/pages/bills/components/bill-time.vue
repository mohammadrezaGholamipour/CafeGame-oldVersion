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
    {
      name: "تاریخ پایان",
      icon: "fa-duotone fa-calendar-days text-xl text-red-700",
    },
    {
      name: "زمان شروع",
      icon: "fa-duotone fa-alarm-clock text-xl text-green-700",
    },
    { name: "زمان پایان", icon: "fa-duotone fa-alarm-clock text-xl text-red-700" },
  ],
});
</script>
<template>
  <div class="bill-info-start">
    <div class="flex flex-col items-center justify-center">
      <div class="flex w-full items-center  justify-end" v-for="items in state.billTimeTitle" :key="items">
        <p class="text-xl mr-2 my-1">{{ items.name }}</p>
      </div>
    </div>
    <div class="flex  flex-col items-center justify-center">
      <div class="flex w-full items-center justify-center my-1">
        <p class="text-xl">
          {{
            moment(props.billTime.start).locale("fa").format("YYYY/M/D")
          }}
        </p>
      </div>
      <div class="flex w-full items-center justify-center my-1">
        <p class="text-xl">
          {{
            props.billTime.end ? moment(props.billTime.end).locale("fa").format("YYYY/M/D") : 'تمام نشده است'
          }}
        </p>
      </div>
      <div class="flex w-full items-center justify-center my-1">
        <p class="text-xl">
          {{
            moment(props.billTime.start).locale("fa").format("HH:mm:ss")
          }}
        </p>
      </div>
      <div class="flex w-full items-center justify-center my-1">
        <p class="text-xl">
          {{ props.billTime.end ? moment(props.billTime.end).locale("fa").format("HH:mm:ss") : 'تمام نشده است'
          }}
        </p>
      </div>
    </div>
  </div>
</template>
