<script setup>
import { filterNumbersWithSep } from "@/util/filter-numbers";
import { onMounted, reactive, watch } from "vue";
///////////////////
const props = defineProps(["playstation", "oldValue"]);
const emit = defineEmits(["consoleSetting"]);
////////////////////
const state = reactive({
  time: {
    name: 'editBill',
    value: '',
  }
})
////////////////////
onMounted(() => {
  state.time.value = props.oldValue
})
////////////////////
watch(
  () => state.time.value,
  (value) => {
    state.time.value = -filterNumbersWithSep(value);
    emit('consoleSetting', state.time)
  }
);
</script>
<template>
  <div class="flex w-full justify-center items-center">
    <input placeholder="دقیق مورد نظر را وارد کنید" class="food-input" v-model="state.time.value" type="text" dir="rtl" />
  </div>
</template>

