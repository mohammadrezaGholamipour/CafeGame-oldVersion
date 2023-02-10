<script setup>
import { useToast } from "vue-toastification";
import { reactive, watch } from "vue";
import moment from "jalali-moment";
import billApi from "../api/bill";
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
    const billNotFinished = value.filter((items) => !items.finishTime);

    handleBillNotFinished(billNotFinished);
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
  if (playstation.moneySelected.rate) {
    playstation.status = !playstation.status;
  } else {
    toast.error("قیمت واحد را وارد نکرده اید");
  }
  if (playstation.status) {
    requestStartBill(
      playstation.id,
      playstation.moneySelected.id,
      new Date().toISOString()
    );
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
    playstation.time.hours = "00";
    playstation.time.minutes = "00";
    playstation.time.seconds = "00";
    playstation.moneySelected = {};
    playstation.userMoney = "";
  }
};
//////////////////////////////
const requestStartBill = (systemId, rateId, startInfo) => {
  billApi
    .new(systemId, rateId, startInfo)
    .then((response) => {
      console.log(response);
    })
    .catch((error) => {
      console.log(error);
    });
};
//////////////////////////////
const handleBillNotFinished = (billList) => {
  for (const bill of billList) {
    const playstation = state.consoleList.find(
      (console) => console.id === bill.systemId
    );
    const money = state.moneyList.find((money) => money.id === bill.hourRateId);
    /////////////////////////////////////////////
    const timeStart = moment(`${bill.startTime}z`)
      .locale("fa")
      .format("HH:mm:ss");
    const hoursStart = timeStart.slice(0, 2);
    const minutesStart = timeStart.slice(3, 5);
    const secondsStart = timeStart.slice(6, 8);
    /////////////////////////////////////////////
    const timeNow = moment().local("fa").format("HH:mm:ss");
    const hoursNow = timeNow.slice(0, 2);
    const minutesNow = timeNow.slice(3, 5);
    const secondsNow = timeNow.slice(6, 8);
    /////////////////////////////////////////////
    playstation.time = {
      hours:
        hoursNow > hoursStart ? hoursNow - hoursStart : hoursStart - hoursNow,
      minutes:
        minutesNow > minutesStart
          ? minutesNow - minutesStart
          : minutesStart - minutesNow,
      seconds:
        secondsNow > secondsStart
          ? secondsNow - secondsStart
          : secondsStart - secondsNow,
    };
    playstation.moneySelected = money;
    playstation.status = true;
    handleTimer(playstation, true);
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
          <img
            src="../assets/image/timer.png"
            v-if="!playstation.status"
            width="35"
          />
          <img v-else src="../assets/image/timer.gif" width="35" />
        </transition-scale>
      </div>
      <!-- /////////////////////////////// -->
      <div class="flex w-full flex-col justify-start items-center">
        <div v-if="playstation.status" class="ConsoleMoney">
          {{
            playstation.userMoney ? playstation.userMoney.toLocaleString() : ""
          }}
        </div>
        <!-- /////////////////////////// -->
        <div class="relative w-full flex justify-center" v-else>
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
                @click="handleMoneySelect(money, playstation)"
                v-for="money in state.moneyList"
                :key="money.id"
              >
                {{ money.rate.toLocaleString() }}
              </li>
            </ul>
          </transition-expand>
        </div>
      </div>
      <!-- //////////////////////////////// -->
      <button
        :class="playstation.status ? 'BtnConsoleFinish' : 'BtnConsoleStart'"
        @click="handleChangeConsoleStatus(playstation)"
      >
        <p>{{ playstation.status ? "پایان" : "شروع" }}</p>
        <i v-if="!playstation.status" class="fa-thin fa-circle-play mr-2"></i>
        <i v-else class="fa-light fa-circle-xmark mr-2"></i>
      </button>
      <!-- ///////////////////////////////////////// -->
    </div>
  </div>
</template>
<style>
.bounce-enter-active {
  animation: bounce-in 0.5s;
}
.bounce-leave-active {
  animation: bounce-in 0.5s reverse;
}
@keyframes bounce-in {
  0% {
    transform: scale(0);
  }
  50% {
    transform: scale(1.25);
  }
  100% {
    transform: scale(1);
  }
}
</style>
