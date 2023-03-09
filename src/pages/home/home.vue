<script setup>
import ConsolePlayStation from "./components/console-playStation.vue";
import { useStore } from '@/store/index'
///////////////////////////////////////
const store = useStore()
////////////////////////
const requestGetBills = () => {
  store.requestGetBills()
};
</script>
<template>
  <div class="ParentHome">
    <ConsolePlayStation @requestGetBills="requestGetBills" :console-list="store.getConsoleList"
      :money-list="store.getMoneyList" :food-list="store.getFoodList" :bill-list="store.getBillList" />
  </div>
</template>
