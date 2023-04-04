<script setup>
const emit = defineEmits(["acceptOrCansel"]);
const props = defineProps(["confirmDialog"]);
//////////////////////////////////////////
const handleCloseDialog = (status) => {
  emit("acceptOrCansel", status);
};
</script>
<template>
  <v-dialog :modelValue="props.confirmDialog.status" :update:modelValue="handleCloseDialog" persistent width="333">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalMain">
        <p class="text-xl text-center">{{ props.confirmDialog.text }}</p>
      </div>
      <div class="ModalFooter p-1">
        <button @click="handleCloseDialog(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleCloseDialog(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>تایید</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
