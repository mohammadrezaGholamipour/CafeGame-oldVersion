<script setup>
import FoodStore from "./food-store.vue";
import { reactive, watch } from "vue";
import Faktor from "./faktor.vue";
///////////////////////////////
const emit = defineEmits(["continue", "finish"]);
const props = defineProps(["payModal"]);
//////////////////////////////////////////
const state = reactive({
  faktorOrFood: false,
  foodSelected: [],
});
///////////////////////////////////
const handleFoodSelected = (food) => {
  state.foodSelected = food;
};
///////////////////////////////////
watch(
  () => props.payModal.status,
  (value) => {
    if (!value) {
      setTimeout(() => {
        state.foodSelected = [];
        state.faktorOrFood = false;
      }, 200);
    }
  }
);
///////////////////////////////////
const handleFinishBill = () => {
  emit("finish", state.foodSelected);
};
</script>
<template>
  <v-dialog v-model="props.payModal.status" persistent width="600">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <!-- //////////////////////////////////// -->
      <div class="ModalHeader">
        <i
          class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-3xl close"
          @click="emit('continue')"
        ></i>
        <div class="w-full flex justify-center items-center">
          <img src="../assets/image/time.svg" width="40" />
          <div class="ml-1 mt-2 flex text-2xl font-bold items-baseline">
            {{ props.payModal.playstation.time?.hours }}:{{
              props.payModal.playstation.time?.minutes
            }}:{{ props.payModal.playstation.time?.seconds }}
          </div>
        </div>
        <transition-slide group>
          <img
            class="shadow-md rounded-full border-2 border-yellow-700 transition-all cursor-pointer hover:scale-110"
            @click="state.faktorOrFood = !state.faktorOrFood"
            src="../assets/image/faktor.jpg"
            v-if="state.faktorOrFood"
            width="50"
          />
          <img
            class="shadow-md border-2 border-yellow-700 transition-all bg-yellow-400 rounded-full cursor-pointer hover:scale-110 p-1"
            @click="state.faktorOrFood = !state.faktorOrFood"
            src="../assets/image/food.png"
            width="50"
            v-else
          />
        </transition-slide>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalMain">
        <transition-expand class="w-full">
          <FoodStore
            @foodSelected="handleFoodSelected"
            v-show="state.faktorOrFood"
            :payModal="props.payModal"
          />
        </transition-expand>
        <transition-expand class="w-full">
          <Faktor
            :foodSelected="state.foodSelected"
            v-show="!state.faktorOrFood"
            :payModal="props.payModal"
          />
        </transition-expand>
      </div>
      <!-- //////////////////////////////////// -->
      <div class="ModalFooter p-2">
        <button @click="emit('continue')" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>ادامه دادن</p>
        </button>
        <button
          v-show="!state.faktorOrFood"
          @click="handleFinishBill"
          class="BtnAccept"
        >
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>پایان</p>
        </button>
      </div>
      <!-- //////////////////////////////////// -->
    </div>
  </v-dialog>
</template>
