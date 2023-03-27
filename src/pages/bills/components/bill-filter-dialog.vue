<script setup>
import { useStore } from '@/store/index'
import { reactive } from "vue";
//////////////////////////////
const props = defineProps(["dialog"]);
const emit = defineEmits(["acceptOrCansel"]);
/////////////////////////
const store = useStore()
/////////////////////////
const state = reactive({
  playstation: {
    value: '',
    status: false
  },
  startDate: "",
  endDate: "",
});
/////////////////////////
const handleAcceptOrCanselModal = (status) => {
  state.playstation.status = false
  emit("acceptOrCansel", status);
};
/////////////////////////
const consoleSelected = (consoleId) => {
  state.playstation.value = consoleId
  state.playstation.status = false
}
/////////////////////////
</script>
<template>
  <v-dialog :update:modelValue="handleAcceptOrCanselModal" :modelValue="props.dialog.status" height="300" width="600"
    persistent>
    <div class="flex w-full h-full flex-col justify-between bg-white rounded-md">
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleAcceptOrCanselModal(false)"></i>
        <p class="font-bold">فیلتر ها</p>
      </div>
      <!-- /////////////////////// -->
      <div class="flex justify-center items-center pt-2">
        <div class="relative w-full flex justify-center">
          <div @click.self="state.playstation.status = !state.playstation.status" class="SelectedConsoleFilter">
            <i v-if="state.playstation.value" @click="state.playstation.value = ''"
              class="fa-solid fa-circle-xmark text-red-600 cursor-pointer w-[20%]"></i>
            <p @click.self="state.playstation.status = !state.playstation.status" class="font-bold w-[80%]">
              {{
                state.playstation.value ? state.playstation.value : 'شماره دستگاه'
              }}
            </p>

          </div>
          <transition-expand>
            <ul v-if="state.playstation.status" class="UlConsoleFilter">
              <li @click="consoleSelected(console.id)" v-for="console in store.getConsoleList" :key="console.id">
                {{ console.id }} دستگاه شماره
              </li>
            </ul>
          </transition-expand>
        </div>
      </div>
      <!-- ////////////////////// -->
      <div class="ModalFooter p-1">
        <button @click="handleAcceptOrCanselModal(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleAcceptOrCanselModal(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>اعمال کردن</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
