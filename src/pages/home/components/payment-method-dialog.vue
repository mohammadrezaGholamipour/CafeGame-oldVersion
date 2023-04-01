<script setup>
const props = defineProps(["dialog"]);
const emit = defineEmits(["close"]);
//////////////////////////////////////////
const handleCloseDialog = (status) => {
  emit("close", status);
};
</script>
<template>
  <v-dialog v-model="props.dialog" persistent width="600">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <!-- //////////////////////////////////// -->
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-3xl close"
          @click="handleCloseDialog(false)"></i>
        <p class="font-bold">روش پرداخت</p>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalMain">
        salam
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
