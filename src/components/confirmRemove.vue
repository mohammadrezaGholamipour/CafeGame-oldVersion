<script setup>
const emit = defineEmits(["accept"]);
const props = defineProps(["modal"]);
const handleAcceptModal = () => {
  emit("accept", props.modal.id);
};
</script>
<template>
  <div
    class="modal fade ParentModal"
    data-bs-backdrop="static"
    id="confirmRemoveConsole"
  >
    <div class="modal-dialog modal-dialog-centered ModalDivOne w-96">
      <div class="ModalDivTwo">
        <div class="ModalMain">
          <p class="text-xl">{{ props.modal.text }}</p>
        </div>
        <div class="ModalFooter p-2">
          <button
            @click="handleAcceptModal"
            data-bs-dismiss="modal"
            class="BtnAccept"
          >
            <p>تایید</p>
            <i class="fa-duotone fa-badge-check mr-2"></i>
          </button>
          <button data-bs-dismiss="modal" class="BtnRemove">
            <p>بازگشت</p>
            <i class="fa-duotone fa-chevrons-left mr-2"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
