<script setup>
import { filterNumbersWithSep, filterNumbers } from "@/util/filter-numbers";
import { onMounted, reactive, watch } from 'vue';
import { useToast } from "vue-toastification";
////////////////////////
const props = defineProps(["playstation", "oldValue"]);
const emit = defineEmits(["consoleSetting"]);
////////////////////
const toast = useToast();
////////////////////
onMounted(() => {
  if (props.oldValue) {
    if (props.oldValue.type === 'time') {
      state.time.hours = props.oldValue.value.hours
      state.time.minutes = props.oldValue.value.minutes
    } else {
      state.money = props.oldValue.value
    }
  }
})
////////////////////
const state = reactive({
  tabAlarm: 'time',
  money: '',
  timer: '',
  time: {
    hours: '',
    minutes: '',
  }

})
//////////////////////
watch(
  () => state.money,
  (value) => {
    clearTimeout(state.timer)
    state.money = filterNumbersWithSep(value);
    ////////////////
    if (state.money) {
      state.timer = setTimeout(() => {
        if (Number(state.money.replace(',', '')) <= props.playstation.userMoney) {
          toast.error(`مبلغ وارد شده باید بیشتر از ${props.playstation.userMoney.toLocaleString()} باشد`)
        } else {
          const alarmBill = {
            name: 'alarmBill',
            value: state.money ? {
              value: state.money.replace(',', ''),
              type: 'money'
            } : ''
          }
          //////////////////
          emit('consoleSetting', alarmBill)
        }
      }, 1000);
    }

  }
);
//////////////////////
watch(() => state.time.hours, (value) => {
  state.time.hours = filterNumbers(value)
})
//////////////////////
watch(() => state.time.minutes, (value) => {
  state.time.minutes = filterNumbers(value)
})
//////////////////////
watch(state.time, () => {
  clearTimeout(state.timer)
  if (state.time.hours && state.time.minutes) {
    state.timer = setTimeout(() => {
      if (Number(props.playstation?.time?.hours) > Number(state.time.hours)) {
        toast.error(`ساعت وارد شده باید حداقل ${props.playstation?.time?.hours} باشد`)
      } else if (Number(props.playstation?.time?.hours) === Number(state.time.hours)) {
        if (Number(props.playstation?.time?.minutes) >= Number(state.time.minutes)) {
          toast.error(`دقیقه وارد شده باید بیشتر از ${props.playstation?.time?.minutes} باشد`)
        }
        else {
          const alarmBill = {
            name: 'alarmBill',
            value: {
              value: state.time,
              type: 'time'
            }
          }
          //////////////////
          emit('consoleSetting', alarmBill)
        }
      }
      else {
        const alarmBill = {
          name: 'alarmBill',
          value: {
            value: state.time,
            type: 'time'
          }
        }
        //////////////////
        emit('consoleSetting', alarmBill)
      }
    }, 900);
  } else {
    emit('consoleSetting', { name: 'alarmBill', value: '' })
  }

})
</script>
<template>
  <div class="flex flex-col w-full overflow-visible items-center">
    <v-tabs align-tabs="center" class="w-full " v-model="state.tabAlarm" bg-color="primary">
      <v-tab value="time">براساس زمان</v-tab>
      <v-tab value="money">هزینه بازی</v-tab>
    </v-tabs>
    <v-window class="overflow-visible w-full" v-model="state.tabAlarm">
      <v-window-item value="time">
        <div class="flex flex-wrap mt-3 justify-center items-center">
          <p class="text-center font-bold" v-if="props.playstation?.time?.hours">حداقل باید {{
            props.playstation?.time?.hours }} ساعت وارد کنید</p>
          <div class="flex flex-wrap justify-center items-center">
            <input placeholder="ساعت" class="food-input text-center" maxlength="2" v-model="state.time.hours"
              type="text" />
            <input placeholder="دقیقه" class="food-input text-center" maxlength="2" v-model="state.time.minutes"
              type="text" />
          </div>
        </div>
      </v-window-item>
      <v-window-item class="overflow-visible w-full" value="money">
        <div class="flex mt-3 font-bold flex-col items-center justify-center">
          <p class="text-center">مبلغ وارد شده باید بیشتر از {{ props.playstation?.userMoney?.toLocaleString() }} تومان
            باشد</p>
          <input placeholder="قیمت مورد نظر را وارد کنید" class="food-input text-center" v-model="state.money"
            type="text" />
        </div>
      </v-window-item>
    </v-window>
  </div>
</template>

