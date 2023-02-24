<script setup>
/////////////////////////
const emit = defineEmits(["close"]);
const props = defineProps(["InfoModal"]);
/////////////////////////

const handleCloseModal = () => {
  emit("close");
};
</script>
<template>
  <v-dialog
    :modelValue="props.InfoModal?.status"
    update:modelValue="handleCloseModal"
    width="500"
    persistent
  >
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalHeader">
        <i
          class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleCloseModal"
        ></i>
        <p class="font-bold">{{ props.InfoModal.headerInfo }}</p>
      </div>
      <div class="ModalMain">
        <slot />
      </div>
      <div class="ModalFooter">
        <button @click="handleCloseModal" class="BtnRemove">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>بازگشت</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
