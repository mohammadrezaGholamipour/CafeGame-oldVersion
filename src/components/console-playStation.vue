<script setup>
import { cafePinia } from "../store/pinia";
import { reactive, watch } from "vue";
////////////////////////
const store = cafePinia();
////////////////////////
const state = reactive({
  playstationList: [],
  moneyList: [],
  foodList: [],
});
/////////////////////
watch(
  () => store.playStationList,
  (value) => {
    state.playstationList = value;
    state.playstationList.forEach((items) => {
      items.showAndHideListMoney = false;
      items.moneySelected = {
        id: 0,
        rate: "",
      };
      items.status = false;
    });
  }
);
watch(
  () => store.foodList,
  (value) => {
    state.foodList = value;
  }
);
watch(
  () => store.moneyList,
  (value) => {
    state.moneyList = value;
  }
);
/////////////////////////
const handleShowAndHideMoneyList = (id) => {
  const playstation = state.playstationList.find((items) => items.id === id);
  playstation.showAndHideListMoney = !playstation.showAndHideListMoney;
};
////////////////////////
const handleMoneySelect = (money, playstation) => {
  playstation.moneySelected = money;
  playstation.showAndHideListMoney = false;
};
////////////////////////////
const handleChangeConsoleStatus = (playstation) => {
  playstation.status = !playstation.status;
};
</script>
<template>
  <div class="w-full p-3 flex flex-wrap items-center justify-center">
    <div
      v-for="(console, index) in state.playstationList"
      :key="console.id"
      class="Console"
    >
      <!-- //////////////////////////////// -->
      <p class="ConsoleNumber">
        <img :src="`src/assets/image/numbers/${index + 1}.png`" width="50" />
      </p>
      <!-- ////////////////////////////////// -->
      <div class="ConsoleTimer">
        <p class="ml-2 mt-1">15:17:00</p>
        <transition-scale group>
          <img src="../assets/image/timer.png" width="35" />
        </transition-scale>
      </div>
      <!-- /////////////////////////////// -->
      <div class="flex flex-col justify-start items-center">
        <transition-scale group class="w-full">
          <div v-if="!console.status" class="relative flex justify-center">
            <div
              @click="handleShowAndHideMoneyList(console.id)"
              class="SelectedMoneyConsole"
            >
              <p class="ml-2 font-bold">
                {{
                  console.moneySelected?.rate
                    ? console.moneySelected.rate.toLocaleString()
                    : "هزینه بازی"
                }}
              </p>
              <i class="fa-duotone transition-all duration-500"></i>
            </div>
            <transition-expand>
              <ul v-if="console.showAndHideListMoney" class="UlMoney">
                <li
                  v-for="money in state.moneyList"
                  @click="handleMoneySelect(money, console)"
                  :key="money.id"
                >
                  {{ money.rate.toLocaleString() }}
                </li>
              </ul>
            </transition-expand>
          </div>
          <!-- /////////////////////////////////// -->
          <p v-else class="ConsoleMoney">10,000 تومن</p>
        </transition-scale>
      </div>
      <!-- //////////////////////////////// -->
      <button
        @click="handleChangeConsoleStatus(console)"
        class="BtnConsoleStart"
      >
        <p>شروع</p>
        <i class="fa-thin fa-circle-play mr-2"></i>
      </button>
      <!-- ///////////////////////////////////////// -->
    </div>
  </div>
</template>
