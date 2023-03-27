<script setup>
import { useStore } from '@/store/index'
import { reactive, watch } from "vue";
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
watch(() => props.dialog.status, () => {
  state.playstation.value = ''
  state.startDate = ''
  state.endDate = ''
})
/////////////////////////
const handleAcceptOrCanselModal = (status) => {
  const filterData = {
    playstation: state.playstation.value,
    startDate: state.startDate,
    endDate: state.endDate,
  }
  state.playstation.status = false
  ///////////////////////
  emit("acceptOrCansel", status, filterData);
};
/////////////////////////
const consoleSelected = (consoleId) => {
  state.playstation.value = consoleId
  state.playstation.status = false
}
</script>
<template>
  <v-dialog :update:modelValue="handleAcceptOrCanselModal" :modelValue="props.dialog.status" height="310" width="600"
    persistent>
    <div class="flex w-full h-full flex-col justify-between bg-white rounded-md">
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleAcceptOrCanselModal(false)"></i>
        <p class="font-bold">فیلتر ها</p>
      </div>
      <!-- /////////////////////// -->
      <div class="flex flex-col-reverse justify-center items-center p-2">
        <!-- ////////////////////////// -->
        <div class="relative w-full flex mt-5 justify-center">
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
        <!-- /////////////////// -->
        <div class="flex flex-row-reverse justify-evenly flex-wrap w-full items-center">
          <DatePicker format="YYYY-MM-DD HH:mm:ss" display-format="jDD/jMMMM/jYYYY" class="m-1" dir="rtl"
            v-model="state.startDate" simple placeholder="تاریخ شروع" />
          <DatePicker format="YYYY-MM-DD HH:mm:ss" display-format="jDD/jMMMM/jYYYY" class="m-1" dir="rtl"
            v-model="state.endDate" simple placeholder="تاریخ پایان" />
        </div>
        <!-- /////////////////// -->
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
<style>
.vpd-input-group {
  height: 40px;
}

.vpd-icon-btn {
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
}

.form-control {
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
  padding-left: 0px !important;
  width: 150px !important;
}

.vpd-wrapper {
  border-radius: 5px;
}

.vpd-content {
  border-radius: 15px;
}

.vpd-actions {
  text-align: center;
}

.vpd-column-content {
  display: flex;
  flex-direction: column;
}

.vpd-simple-content {
  border-bottom: solid 1px rgb(73, 73, 73);
}

.vpd-column-header {
  border-bottom: dashed 1px rgb(131, 131, 131);
}

.vpd-addon-list-item.vpd-selected {
  border-radius: 5px;
  font-weight: 800;
  background-color: #af3434;
  color: white !important;
}
</style>
