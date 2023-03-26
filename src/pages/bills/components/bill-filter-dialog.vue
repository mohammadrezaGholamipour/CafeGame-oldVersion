<script setup>
const props = defineProps(["dialog"]);
const emit = defineEmits(["acceptOrCansel"]);
/////////////////////////

const handleAcceptOrCanselModal = (status) => {
  emit("acceptOrCansel", status);
};
</script>
<template>
  <v-dialog :update:modelValue="handleAcceptOrCanselModal" :modelValue="props.dialog.status" width="600" persistent>
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleAcceptOrCanselModal(false)"></i>
        <p class="font-bold">فیلتر ها</p>
      </div>
      <!-- /////////////////////// -->
      <div class="ModalMain pt-2">
        فیتلر هستم
      </div>
      <!-- ////////////////////// -->
      <div class="ModalFooter p-1">
        <button @click="handleAcceptOrCanselModal(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleAcceptOrCanselModal(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>تایید</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
