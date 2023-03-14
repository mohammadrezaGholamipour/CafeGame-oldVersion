<script setup>
const props = defineProps(["dialog"]);
const emit = defineEmits(["close"]);
/////////////////////////
const handleCloseModal = () => {
  emit("close");
};
</script>
<template>
  <v-dialog :update:modelValue="handleCloseModal" :modelValue="props.dialog.status" width="600" persistent>
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close" @click="handleCloseModal"></i>
        <p class="font-bold">{{ props.dialog.headerInfo }}</p>
      </div>
      <div class="ModalMain pt-2">
        {{ props.dialog.data }}
      </div>
    </div>
  </v-dialog>
</template>
