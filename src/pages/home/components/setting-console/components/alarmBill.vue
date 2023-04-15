<script setup>
import { filterNumbersWithSep } from "@/util/filter-numbers";
import { onMounted, reactive, watch } from 'vue';
////////////////////////
const props = defineProps(["playstation", "oldValue"]);
const emit = defineEmits(["consoleSetting"]);
////////////////////
onMounted(() => {
  console.log(props.playstation);
})
////////////////////
const state = reactive({
  tabAlarm: 'time',
  alarmBill: {
    name: 'alarmBill',
    value: ''
  }
})
//////////////////////
watch(
  () => state.alarmBill.value,
  (value) => {
    console.log(state.alarmBill.value);
    if (state.alarmBill.value.length > 3) {
      state.alarmBill.value = filterNumbersWithSep(value); 
      emit('consoleSetting', state.alarmBill)
    }
  }
);
</script>
<template>
  <div class="flex flex-col w-full overflow-visible items-center">
    <v-tabs align-tabs="center" class="w-full " v-model="state.tabAlarm" bg-color="primary">
      <v-tab value="time">براساس زمان</v-tab>
      <v-tab value="money">هزینه بازی</v-tab>
    </v-tabs>
    <v-window class="overflow-visible" v-model="state.tabAlarm">
      <v-window-item value="time">time</v-window-item>
      <v-window-item class="overflow-visible" value="money">
        <input placeholder="قیمت مورد نظر را وارد کنید" class="food-input text-center" v-model="state.alarmBill.value"
          type="text" />
      </v-window-item>
    </v-window>
  </div>
</template>

