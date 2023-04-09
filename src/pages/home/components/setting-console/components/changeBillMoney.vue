<script setup>
import { useStore } from '@/store/index'
import { reactive } from 'vue';
/////////////////////////
const props = defineProps(["playstation"]);
////////////////////////////////
const store = useStore()
////////////////////////////////
const state = reactive({
  money: {
    value: '',
    status: false
  }
})
/////////////////////////
const handleMoneySelect = (money) => {
  state.money.value = money
  state.money.status = false
}
</script>
<template>
  <div class="flex w-full justify-center items-center">
    <p class="font-bold flex justify-center items-center flex-row-reverse text-center">قیمت واحد ثبت شده در فاکتور
    <div class="relative w-ful flex justify-center mr-2 ">
      <div @click="state.money.status = !state.money.status" class="SelectedMoneyConsole">
        <p class="font-bold">
          {{
            state.money.value ? state.money.value.rate?.toLocaleString() :
            props.playstation.moneySelected?.rate.toLocaleString()
          }}
        </p>
        <i class="fa-duotone transition-all duration-500"></i>
      </div>
      <transition-expand>
        <ul v-if="state.money.status" class="UlMoney">
          <li @click="handleMoneySelect(money)" v-for="money in store.getMoneyList" :key="money.id">
            {{ money.rate.toLocaleString() }}
          </li>
        </ul>
      </transition-expand>
    </div>
    </p>
    <!-- /////////////////////////// -->

  </div>
</template>

