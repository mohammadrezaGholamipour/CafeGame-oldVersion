<script setup>
import { reactive, watch } from 'vue';
// //////////////////////
const emit = defineEmits(["changeDateBillsReport"]);
const props = defineProps(["billsReport"]);
/////////////////////////
const state = reactive({
  time: '',
  showAndHideDatePicker: false
})
///////////////////////////
watch(() => state.time, (value) => {
  if (props.billsReport.time !== value) {
    emit('changeDateBillsReport', value)
  }
})
</script>
<template>
  <div class="flex w-full items-center justify-between flex-row-reverse p-3 bg-white">
    <div class="flex flex-col justify-between items-end">
      <!-- ////////////////////////////// -->
      <div class="flex items-center py-2">
        <p class="mr-1">تاریخ</p>
        <i class="fa-duotone fa-hourglass-start text-red-500" @click="state.showAndHideDatePicker = true" />
      </div>
      <div class="flex items-center py-2">
        <p class="mr-1">تعداد فاکتور</p>
        <i class="fa-duotone fa-arrow-down-wide-short" />
      </div>
      <div class="flex items-center py-2">
        <p class="mr-1">هزینه خوراکی ها</p>
        <i class="fa-duotone fa-burger-soda text-red-700" />
      </div>
      <div class="flex items-center py-2">
        <p class="mr-1">هزینه بازی شده</p>
        <i class="fa-brands fa-playstation text-blue-500" />
      </div>
      <div class="flex items-center py-2">
        <p class="mr-1">جمع کل</p>
        <i class="fa-duotone fa-money-bill-1-wave text-green-500" />
      </div>
      <!-- ////////////////////////////// -->
    </div>
    <div class="flex flex-col font-bold justify-between items-center">
      <p class="py-2">{{ props.billsReport.time }}<date-picker v-model="state.time" :show="state.showAndHideDatePicker"
          format="jYYYY/jMM/jDD" simple id="dataBillsReport" @close="state.showAndHideDatePicker = false" /></p>
      <p class="py-2">{{ props.billsReport.totalBills }}</p>
      <p class="py-2">{{ props.billsReport.costFood.toLocaleString() }}</p>
      <p class="py-2">{{ props.billsReport.costPlayGame.toLocaleString() }}</p>
      <p class="py-2">{{ props.billsReport.totalCost.toLocaleString() }}</p>
    </div>
  </div>
</template>
<style>
#dataBillsReport .vpd-input-group {
  display: none;
}
</style>

