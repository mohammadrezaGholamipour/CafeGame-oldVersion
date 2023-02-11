<script setup>
import ConfirmModal from "../components/confirm-modal.vue";
import { useToast } from "vue-toastification";
import { onMounted, reactive } from "vue";
import console from "../api/console";
////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  manageConsoleList: [],
  confirmModal: {
    text: "دستگاه انتخاب شده حذف شود؟",
    status: false,
    id: "",
  },
});
///////////////////////////////
onMounted(() => {
  requestGetConsoles();
});
///////////////////////////////
const requestNewConsole = () => {
  if (state.manageConsoleList.length < 8) {
    console
      .new({ name: String(Math.random() * 100) })
      .then(() => {
        toast.success("دستگاه با موفقیت اضافه شد");
        requestGetConsoles();
      })
      .catch(() => {
        toast.error("دستگاه جدید اضافه نشد");
      });
  }
};
///////////////////////////////
const requestGetConsoles = () => {
  console
    .get()
    .then((response) => {
      state.manageConsoleList = response;
    })
    .catch(() => {
      toast.error("خطا در ارتباط با سرور");
    });
};
///////////////////////////////
const requestRemoveConsole = (id) => {
  console
    .remove(id)
    .then(() => {
      toast.success("دستگاه با موفقیت حذف شد");
      requestGetConsoles();
    })
    .catch(() => {
      toast.error("دستگاه حذف نشد");
    })
    .finally(() => {
      state.confirmModal.status = false;
      state.confirmModal.id = "";
    });
};
///////////////////////////////
const handleSelectConsole = (items) => {
  state.confirmModal.id = items.id;
  state.confirmModal.status = true;
};
///////////////////////////////
const handleCloseConfirmModal = (value) => {
  if (value) {
    requestRemoveConsole(state.confirmModal.id);
  } else {
    state.confirmModal.status = false;
    state.confirmModal.id = "";
  }
};
</script>
<template>
  <div class="parent-consols">
    <!-- //////////////////////////// -->
    <div class="flex items-center">
      <div class="new-console" @click="requestNewConsole">
        <p class="text-white ml-2">اضافه کردن دستگاه</p>
        <i class="fa-duotone fa-gamepad-modern text-white text-2xl"></i>
      </div>
    </div>
    <!-- //////////////////////////// -->
    <transition-slide group>
      <div
        class="w-full h-full flex flex-wrap justify-center items-center mt-4"
      >
        <div
          v-for="(items, index) in state.manageConsoleList"
          class="manage-consols"
          :key="index"
        >
          <img src="../assets/image/logoConsole.png" width="150" />
          <div class="flex mt-2 w-full justify-center items-center p-2">
            <i
              class="fa-duotone fa-circle-question console-icon text-gray-200"
            ></i>
            <img
              :src="`src/assets/image/numbers/${index + 1}.png`"
              class="bg-white rounded-full outline-none"
              width="50"
            />
            <i
              class="fa-duotone fa-trash-can-list console-icon text-gray-200"
              @click="handleSelectConsole(items)"
            ></i>
          </div>
        </div>
      </div>
    </transition-slide>
    <!-- //////////////////////////// -->
    <ConfirmModal
      @acceptOrCansel="handleCloseConfirmModal"
      :confirmModal="state.confirmModal"
    />
    <!-- //////////////////////////// -->
  </div>
</template>
