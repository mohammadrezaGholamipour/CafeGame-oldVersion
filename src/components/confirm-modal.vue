<script setup>
const emit = defineEmits(["acceptOrCansel"]);
const props = defineProps(["confirmModal"]);
//////////////////////////////////////////
const handleCloseModal = (value) => {
  emit("acceptOrCansel", value);
};
</script>
<template>
  <v-dialog
    :modelValue="props.confirmModal.status"
    :update:modelValue="handleCloseModal"
    persistent
    width="500"
  >
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalMain">
        <p class="text-xl">{{ props.confirmModal.text }}</p>
      </div>
      <div class="ModalFooter p-2">
        <button @click="handleCloseModal(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleCloseModal(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>تایید</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
