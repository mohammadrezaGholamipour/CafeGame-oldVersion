<script setup>
import { reactive } from 'vue';
/////////////////////////////
const props = defineProps(["dialog"]);
const emit = defineEmits(["close"]);
//////////////////////////////////////////
const state = reactive({
  paymentMethods: [
    { name: 'نقدی', value: 0, color: 'text-green-600 font-bold text-xl mx-1' },
    { name: 'کارت', value: 1, color: 'text-yellow-600 font-bold text-xl mx-1' },
    { name: 'اکانت', value: 2, color: 'text-red-600 font-bold text-xl mx-1' },
  ],
  paymentSelected: 0
})
//////////////////////////////////////////
const handleCloseDialog = (status) => {
  emit("close", status, state.paymentSelected);
};
</script>
<template>
  <v-dialog v-model="props.dialog" persistent width="400">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <!-- //////////////////////////////////// -->
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-3xl close"
          @click="handleCloseDialog(false)"></i>
        <p class="font-bold">روش پرداخت</p>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalMain justify-center items-center">
        <v-radio-group v-model="state.paymentSelected" class="flex w-full justify-center" inline>
          <v-radio v-for="item in state.paymentMethods" :key="item.value" :label="item.name" :value="item.value"
            :class="item.color"></v-radio>

        </v-radio-group>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalFooter p-2">
        <button @click="handleCloseDialog(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleCloseDialog(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>تایید</p>
        </button>
      </div>
      <!-- //////////////////////////////////// -->
    </div>
  </v-dialog>
</template>
