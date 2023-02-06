<script setup>
import { useToast } from "vue-toastification";
import { reactive, watch } from "vue";
////////////////////////
const props = defineProps(["consoleList", "moneyList", "foodList", "billList"]);
////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  consoleList: [],
  moneyList: [],
  foodList: [],
  billList: [],
});
/////////////////////
watch(
  () => props.consoleList,
  (value) => {
    state.consoleList = value;
    state.consoleList.forEach((items) => {
      items.showAndHideListMoney = false;
      items.moneySelected = {
        id: 0,
        rate: "",
      };
      items.status = false;
      items.timer = "";
      items.time = {
        hours: "00",
        minutes: "00",
        seconds: "00",
      };
      items.userMoney = "";
    });
  }
);
watch(
  () => props.foodList,
  (value) => {
    state.foodList = value;
  }
);
watch(
  () => props.moneyList,
  (value) => {
    state.moneyList = value;
  }
);
watch(
  () => props.billList,
  (value) => {
    state.billList = value;
  }
);
/////////////////////////
const handleShowAndHideMoneyList = (id) => {
  const playstation = state.consoleList.find((items) => items.id === id);
  playstation.showAndHideListMoney = !playstation.showAndHideListMoney;
};
////////////////////////
const handleMoneySelect = (money, playstation) => {
  playstation.moneySelected = money;
  playstation.showAndHideListMoney = false;
};
////////////////////////////
const handleChangeConsoleStatus = (playstation) => {
  console.log(new Date().toISOString());
  if (playstation.moneySelected.rate) {
    playstation.status = !playstation.status;
  } else {
    toast.error("قیمت واحد را وارد نکرده اید");
  }
  if (playstation.status) {
    playstation.time.hours = "00";
    playstation.time.minutes = "00";
    playstation.time.seconds = "00";
    playstation.money = "";
    handleTimer(playstation, true);
  } else {
    handleTimer(playstation, false);
  }
};
///////////////////////////////////
const handleTimer = (playstation, status) => {
  if (status) {
    playstation.timer = setInterval(() => {
      const moneySelected = playstation.moneySelected.rate;
      const minutes = playstation.time.minutes;
      const hours = playstation.time.hours;
      const minutesMoney = (Number(moneySelected) / 60) * minutes;
      /////////////////////////////////////////////
      playstation.time.seconds++;
      if (playstation.time.seconds == 60) {
        playstation.time.seconds = "00";
        playstation.time.minutes++;
      }
      if (playstation.time.minutes == 60) {
        playstation.time.minutes = "00";
        playstation.time.hours++;
      }
      if (playstation.time.hours > 0) {
        playstation.userMoney = parseInt(
          Math.round(hours * Number(moneySelected) + minutesMoney)
        );
      } else {
        playstation.userMoney = parseInt(Math.round(moneySelected));
      }
    }, 1000);
  } else {
    clearInterval(playstation.timer);
  }
};
</script>
<template>
  <div class="w-full p-3 flex flex-wrap items-center justify-center">
    <div
      v-for="(playstation, index) in state.consoleList"
      :key="playstation.id"
      class="Console"
    >
      <!-- //////////////////////////////// -->
      <p class="ConsoleNumber">
        <img :src="`src/assets/image/numbers/${index + 1}.png`" width="50" />
      </p>
      <!-- ////////////////////////////////// -->
      <div class="ConsoleTimer">
        <p class="ml-2 mt-1">
          {{ playstation.time.hours }}:{{ playstation.time.minutes }}:{{
            playstation.time.seconds
          }}
        </p>
        <transition-scale group>
          <img src="../assets/image/timer.png" width="35" />
        </transition-scale>
      </div>
      <!-- /////////////////////////////// -->
      <div class="flex flex-col justify-start items-center">
        <transition-expand  class="w-full">
          <div v-if="!playstation.status" class="relative flex justify-center">
            <div
              @click="handleShowAndHideMoneyList(playstation.id)"
              class="SelectedMoneyConsole"
            >
              <p class="ml-2 font-bold">
                {{
                  playstation.moneySelected?.rate
                    ? playstation.moneySelected.rate.toLocaleString()
                    : "هزینه بازی"
                }}
              </p>
              <i class="fa-duotone transition-all duration-500"></i>
            </div>
            <transition-expand>
              <ul v-if="playstation.showAndHideListMoney" class="UlMoney">
                <li
                  v-for="money in state.moneyList"
                  @click="handleMoneySelect(money, playstation)"
                  :key="money.id"
                >
                  {{ money.rate.toLocaleString() }}
                </li>
              </ul>
            </transition-expand>
          </div>
           </transition-expand>
          <!-- /////////////////////////////////// -->
          <transition-expand>
          <p v-if="playstation.status" class="ConsoleMoney">
            {{ playstation.userMoney.toLocaleString() }}
          </p>
       </transition-expand>
      </div>
      <!-- //////////////////////////////// -->
      <button
        @click="handleChangeConsoleStatus(playstation)"
        :class="playstation.status ? 'BtnConsoleFinish' : 'BtnConsoleStart'"
      >
        <p>{{ playstation.status ? "پایان" : "شروع" }}</p>
        <i v-if="!playstation.status" class="fa-thin fa-circle-play mr-2"></i>
        <i v-else class="fa-light fa-circle-xmark mr-2"></i>
      </button>
      <!-- ///////////////////////////////////////// -->
    </div>
  </div>
</template>
