<script setup>
import TableBills from "./components/table-bills.vue";
import { useStore } from '@/store/index'
//////////////////////////////////
const store = useStore()
</script>
<template>
  <div class="Parent-bills">
    <TableBills :moneys="store.getMoneyList" :consoles="store.getConsoleList" :foods="store.getFoodList"
      :bills="store.getBillList" />
  </div>
</template>
