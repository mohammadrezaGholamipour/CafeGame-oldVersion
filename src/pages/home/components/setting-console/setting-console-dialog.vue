<script setup>
import ChangeBillMoney from './components/changeBillMoney.vue';
import AlarmBill from './components/alarmBill.vue';
import EditBill from './components/editBill.vue';
import Food from './components/food.vue';
import { reactive, watch } from 'vue';
/////////////////////////////
const props = defineProps(["settingDialog"]);
const emit = defineEmits(["close"]);
//////////////////////////////////////////
watch(() => props.settingDialog.status, (value) => {
  if (!value) {
    state.settingData.forEach((item) => item.value = '')
  }
})
//////////////////////////////////////////
const state = reactive({
  tabSettingList: [
    { name: 'افزودن خوارکی', icon: '', value: 1 },
    { name: 'عوض کردن مبلغ واحد', icon: '', value: 2 },
    { name: 'گزاشتن یاد اور', icon: '', value: 3 },
    { name: 'تغییر در فاکتور', icon: '', value: 4 },
  ],
  settingData: [
    { name: 'food', value: '' },
    { name: 'changeBillMoney', value: '' },
    { name: 'alarmBill', value: '' },
    { name: 'editBill', value: '' },
  ],
  tabSetting: 1,
})
//////////////////////////////////////////
const handleFindComponent = (tabSetting) => {
  switch (tabSetting) {
    case 1:
      return Food;
    case 2:
      return ChangeBillMoney;
    case 3:
      return AlarmBill;
    case 4:
      return EditBill;
  }
}
//////////////////////////////////////////
const handleCloseDialog = (status) => {
  if (status) {
    const consoleSetting = state.settingData.filter((item) => item.value)
    emit('close', status, consoleSetting)
  } else {
    emit('close', status)
  }

}
//////////////////////////////////////////
const handleConsoleSetting = (data) => {
  const consoleSetting = state.settingData.find((item) => item.name === data.name)
  consoleSetting.value = data.value
}
///////////////////////
</script>
<template>
  <v-dialog v-model="props.settingDialog.status" persistent width="592">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <!-- //////////////////////////////////// -->
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-3xl close"
          @click="handleCloseDialog(false)"></i>
        <p class="font-bold">تنظیمات</p>
      </div>
      <!-- //////////////////////////////////// -->
      <div class=" ModalMain overflow-visible  justify-center items-center" style="padding: 0px;">
        <div class="flex w-full items-center justify-center">
          <v-tabs class="" v-model="state.tabSetting" bg-color="red">
            <v-tab v-for="item in state.tabSettingList" :key="item.value" :value="item.value">{{ item.name }}</v-tab>
          </v-tabs>
        </div>
        <v-window :class="state.tabSetting !== 2 ? 'overflow-y-scroll' : 'overflow-visible'"
          class="w-full flex my-2 justify-center items-center" v-model="state.tabSetting">
          <v-window-item :class="state.tabSetting !== 2 ? 'overflow-y-scroll' : 'overflow-visible'"
            :value="state.tabSetting">
            <transition-scale group>
              <component @consoleSetting="handleConsoleSetting" :playstation="props.settingDialog.playstation"
                :is=handleFindComponent(state.tabSetting)>
              </component>
            </transition-scale>
          </v-window-item>
        </v-window>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalFooter p-2">
        <button @click="handleCloseDialog(false)" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>بازگشت</p>
        </button>
        <button @click="handleCloseDialog(true)" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>اعمال کردن</p>
        </button>
      </div>
      <!-- //////////////////////////////////// -->
    </div>
  </v-dialog>
</template>
<style>
.v-window__container {
  width: 100%;
  max-height: 250px;
}
</style>
