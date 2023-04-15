<script setup>
import { computed, onMounted, reactive, watch } from "vue";
import { filterNumbers } from "@/util/filter-numbers";
import { useToast } from "vue-toastification";
///////////////////
const props = defineProps(["playstation", "oldValue"]);
const emit = defineEmits(["consoleSetting"]);
////////////////////
const toast = useToast();
////////////////////
const state = reactive({
  time: {
    name: 'editBill',
    value: '',
    editBillType: 'افزودن'
  }
})
////////////////////
onMounted(() => {
  if (props.oldValue) {
    state.time.value = props.oldValue
    if (String(props.oldValue)[0] === "-") {
      state.time.editBillType = "افزودن"
    } else {
      state.time.editBillType = "کم کردن"
    }
  }
})
////////////////////
const editeBill = computed(() => {
  ////////////////////
  const data = {
    name: 'editBill',
    value: state.time.value,
    editBillType: state.time.editBillType
  }
  ///////////////////
  if (data.value) {
    if (data.editBillType === "افزودن") {
      data.value = -data.value
    } else {
      data.value = Number(data.value)
    }
  }
  ///////////////////
  return data
})
////////////////////
watch(
  () => state.time.value,
  (value) => {
    state.time.value = filterNumbers(value);
    ///////////////////////////
    const minutes = (props.playstation.time.hours * 60) + props.playstation.time.minutes;
    if (state.time.editBillType === "کم کردن" && Number(state.time.value) > minutes) {
      toast.error(`حداکثر میتوانید ${minutes} دقیقه کم کنید`)
      state.time.value = ''
    } else {
      emit('consoleSetting', editeBill.value)
    }
    ///////////////////////////
  }
);
////////////////////
watch(
  () => state.time.editBillType,
  () => {
    const minutes = (props.playstation.time.hours * 60) + props.playstation.time.minutes;
    if (state.time.editBillType === "کم کردن" && Number(state.time.value) > minutes) {
      toast.error(`حداکثر میتوانید ${minutes} دقیقه کم کنید`)
      state.time.value = ''
    } else {
      emit('consoleSetting', editeBill.value)
    }
    ///////////////////////////
  }
);
</script>
<template>
  <div class="flex flex-col w-full justify-center items-center">
    <!-- ////////////////////// -->
    <input placeholder="برحسب دقیقه" class="food-input text-center" v-model="state.time.value" type="text" />
    <!-- //////////////////////// -->
    <v-radio-group v-model="state.time.editBillType" class="flex w-full justify-center" inline>
      <v-radio label="افزودن" value="افزودن" class="text-green-700 font-bold"></v-radio>
      <v-radio :disabled="!props.playstation?.time?.minutes" label="کم کردن" value="کم کردن"
        class="text-red-700 font-bold"></v-radio>
    </v-radio-group>
    <!-- ///////////////////////// -->
  </div>
</template>

