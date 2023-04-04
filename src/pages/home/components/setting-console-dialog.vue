<script setup>
import { reactive } from 'vue';
/////////////////////////////
const props = defineProps(["settingDialog"]);
const emit = defineEmits(["close"]);
//////////////////////////////////////////
const state = reactive({
  tabSettingList: [
    { name: 'افزودن خوارکی', icon: '', value: 1 },
    { name: 'عوض کردن مبلغ واحد', icon: '', value: 2 },
    { name: 'گزاشتن یاد اور', icon: '', value: 3 },
    { name: 'تغییر در فاکتور', icon: '', value: 4 },
  ],
  tabSetting: 'two'
})
//////////////////////////////////////////
const handleCloseDialog = (status) => {
  emit('close', status)
}
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
      <div class="ModalMain p-0 justify-center items-center">
        <div class="flex w-full items-center justify-center">
          <v-tabs class="" v-model="state.tabSetting" bg-color="red">
            <v-tab v-for="item in state.tabSettingList" :key="item.value" :value="item.value">{{ item.name }}</v-tab>
          </v-tabs>
        </div>
        <v-window class="w-full bg-black flex mt-2 justify-center items-center" v-model="state.tabSetting">
          <v-window-item :value="1">
            One
          </v-window-item>
          <v-window-item :value="2">
            Two
          </v-window-item>
          <v-window-item :value="3">
            Three
          </v-window-item>
          <v-window-item :value="4">
            Three
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
