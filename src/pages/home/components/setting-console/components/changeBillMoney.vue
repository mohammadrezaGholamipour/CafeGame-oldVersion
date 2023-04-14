<script setup>
import { onMounted, reactive, watch } from 'vue';
import { useStore } from '@/store/index'
/////////////////////////
const props = defineProps(["playstation", "oldValue"]);
const emit = defineEmits(["consoleSetting"]);
////////////////////////////////
const store = useStore()
////////////////////////////////
const state = reactive({
  money: {
    name: 'changeBillMoney',
    value: '',
    status: false
  }
})
/////////////////////////
onMounted(() => {
  if (props.oldValue) {
    state.money.value = props.oldValue
  }
})
/////////////////////////
const handleMoneySelect = (money) => {
  state.money.status = false
  state.money.value = money

}
//////////////////////
watch(() => state.money.value, (value) => {
  if (value.rate !== props.playstation.moneySelected.rate) {
    emit('consoleSetting', state.money)
  }
})
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

