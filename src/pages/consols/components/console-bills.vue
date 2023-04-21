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
        <div class="flex flex-col p-2 justify-center items-center">
          <p class="bg-blue-500 p-2 text-lg rounded-md mb-2 text-white shadow-md">زمان بازی شده با دستگاه</p>
          <p class="text-xl font-bold mt-3"> {{ props.dialog.data.totalTime.hours }} : {{
            props.dialog.data.totalTime.minutes }} : {{
    props.dialog.data.totalTime.seconds }} </p>
        </div>
        <div class="flex flex-col p-2 justify-center items-center">
          <p class="bg-yellow-500 p-2 text-lg rounded-md mb-2 text-white shadow-md">هزینه بازی شده با دستگاه</p>
          <div class="flex items-center text-xl font-bold mt-3">
            <p class="mr-2">تومان</p>
            <p> {{ props.dialog.data.costPlayGame.toLocaleString() }}</p>
          </div>
        </div>
      </div>
    </div>
  </v-dialog>
</template>
