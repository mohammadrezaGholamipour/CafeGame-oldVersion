<script setup>
import PaymentMethodDialog from "./payment-method-dialog.vue";
import FoodStore from "./food-store.vue";
import { reactive, watch } from "vue";
import Faktor from "./faktor.vue";
///////////////////////////////
const emit = defineEmits(["continue", "finish"]);
const props = defineProps(["payModal"]);
//////////////////////////////////////////
const state = reactive({
  paymentMethodModal: false,
  faktorOrFood: false,
  foodSelected: '',
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
        state.foodSelected = '';
        state.faktorOrFood = false;
      }, 200);
    } else {
      if (props.payModal.billPlaystation.billFoods.length) {
        props.payModal.foodList.forEach((food) => {
          for (const foodBill of props.payModal.billPlaystation.billFoods) {
            if (food.id === foodBill.foodId) {
              food.count = foodBill.count
            }
          }
        })
        state.foodSelected = props.payModal.foodList.filter((items) => !!items.count);
      }
    }
  }
);
///////////////////////////////////
const handleBill = (status, paymentMethod) => {
  state.paymentMethodModal = false
  if (status) {
    emit("finish", paymentMethod, state.foodSelected);
  }
};

</script>
<template>
  <v-dialog v-model="props.payModal.status" persistent width="600">
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <!-- //////////////////////////////////// -->
      <div class="ModalHeader">
        <i class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-3xl close" @click="emit('continue')"></i>
        <div class="w-full flex justify-center items-center">
          <img src="@/assets/image/time.svg" width="40" />
          <div class="ml-1 mt-2 flex text-2xl font-bold items-baseline">
            {{ props.payModal.playstation.time?.hours }}:{{
              props.payModal.playstation.time?.minutes
            }}:{{ props.payModal.playstation.time?.seconds }}
          </div>
        </div>
        <transition-slide group>
          <img class="shadow-md rounded-full border-2 border-yellow-700 transition-all cursor-pointer hover:scale-110"
            @click="state.faktorOrFood = !state.faktorOrFood" src="@/assets/image/faktor.jpg" v-if="state.faktorOrFood"
            width="50" />
          <img
            class="shadow-md border-2 border-yellow-700 transition-all bg-yellow-400 rounded-full cursor-pointer hover:scale-110 p-1"
            @click="state.faktorOrFood = !state.faktorOrFood" src="@/assets/image/food.png" width="50" v-else />
        </transition-slide>
      </div>
      <!-- //////////////////////////////////// -->
      <transition-slide>
        <div class="ModalMain">
          <FoodStore @foodSelected="handleFoodSelected" v-show="state.faktorOrFood" :payModal="props.payModal" />
          <Faktor :foodSelected="state.foodSelected" v-show="!state.faktorOrFood" :payModal="props.payModal" />

        </div>
      </transition-slide>
      <!-- //////////////////////////////////// -->
      <div class="ModalFooter p-2">
        <button v-show="!state.faktorOrFood" @click="emit('continue')" class="BtnRemove">
          <i class="fa-duotone fa-chevrons-left mr-2"></i>
          <p>ادامه دادن</p>
        </button>
        <button v-show="!state.faktorOrFood" @click="state.paymentMethodModal = true" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>پایان</p>
        </button>
      </div>
      <!-- //////////////////////////////////// -->
      <PaymentMethodDialog @close="handleBill" :dialog="state.paymentMethodModal" />
      <!-- //////////////////////////////////// -->
    </div>
  </v-dialog>
</template>
