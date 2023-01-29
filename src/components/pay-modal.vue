<script setup>
import FoodStore from "./food-store.vue";
import Faktor from "./faktor.vue";
</script>
<template>
  <div class="modal fade ParentModal" data-bs-backdrop="static" id="ConsolePay">
    <div class="modal-dialog ModalDivOne">
      <div class="ModalDivTwo">
        <div class="ModalHeader">
          <transition-slide group>
            <img
              v-if="true"
              class="shadow-md border border-yellow-700 transition-all bg-yellow-500 rounded-full cursor-pointer hover:scale-110 p-1"
              src="../assets/image/food.png"
              width="50"
            />
            <img
              v-else
              class="shadow-md rounded-full border-2 border-yellow-600 transition-all cursor-pointer hover:scale-110"
              src="../assets/image/faktor.jpg"
              width="50"
            />
          </transition-slide>
          <img src="src/assets/image/numbers/one.png" width="50" />
        </div>
        <div class="ModalMain">
          <div class="w-full">
            <transition-slide group>
              <Faktor v-if="true" />
              <FoodStore v-else />
            </transition-slide>
          </div>
        </div>
        <div class="ModalFooter">
          <button v-if="true" data-bs-dismiss="modal" class="BtnAccept">
            <p>اتمام</p>
            <i class="fa-duotone fa-badge-check mr-2"></i>
          </button>
          <button data-bs-dismiss="modal" class="BtnRemove">
            <p>ادامه دادن</p>
            <i class="fa-duotone fa-chevrons-left mr-2"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
