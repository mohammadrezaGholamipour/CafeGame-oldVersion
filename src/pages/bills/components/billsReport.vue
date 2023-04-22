<script setup>
const props = defineProps(["billsReport"]);
</script>
<template>
  <div class="flex w-full items-center justify-between flex-row-reverse p-3 bg-white">
    <div class="flex flex-col justify-between items-end">
      <!-- ////////////////////////////// -->
      <div class="flex items-center py-2">
        <p class="mr-1">تاریخ</p>
        <i class="fa-duotone fa-hourglass-start text-red-500" />
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
      <p class="py-2">{{ props.billsReport.time }}</p>
      <p class="py-2">{{ props.billsReport.totalBills }}</p>
      <p class="py-2">{{ props.billsReport.costFood.toLocaleString() }}</p>
      <p class="py-2">{{ props.billsReport.costPlayGame.toLocaleString() }}</p>
      <p class="py-2">{{ props.billsReport.totalCost.toLocaleString() }}</p>
    </div>
  </div>
</template>

