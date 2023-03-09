<script setup>
import { useToast } from "vue-toastification";
import PayModal from "./pay-modal.vue";
import { reactive, watch } from "vue";
import billApi from "@/api/bill";
////////////////////////
const props = defineProps(["consoleList", "moneyList", "foodList", "billList"]);
const emit = defineEmits(["requestGetBills"]);
////////////////////////
const toast = useToast();
////////////////////////
const state = reactive({
  consoleList: [],
  moneyList: [],
  foodList: [],
  billList: [],
  payModal: {
    playstation: {},
    billPlaystation: {},
    foodList: [],
    status: false,
  },
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
    const billsNotFinished = value.filter((items) => !items.endTime);
    if (billsNotFinished.length) {
      handleBillNotFinished(billsNotFinished);
    }
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
  if (playstation.status) {
    const bill = state.billList.find(
      (items) => items.systemId === playstation.id && !items.endTime
    );
    state.payModal.playstation = playstation;
    state.payModal.foodList = state.foodList;
    state.payModal.billPlaystation = bill;
    state.payModal.status = true;
  } else if (playstation.moneySelected.rate) {
    requestStartBill(
      playstation.id,
      playstation.moneySelected.id,
      new Date().toISOString()
    );
  } else {
    toast.error("لطفا قیمت واحد دستگاه را انتخاب کنید");
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
    playstation.status = false;
    clearInterval(playstation.timer);
    playstation.time.hours = "00";
    playstation.time.minutes = "00";
    playstation.time.seconds = "00";
    playstation.moneySelected = {};
    playstation.userMoney = "";
    playstation.timer = "";
  }
};
//////////////////////////////
const requestStartBill = (systemId, rateId, startInfo) => {
  billApi
    .new(systemId, rateId, startInfo)
    .then(() => {
      emit("requestGetBills");
    })
    .catch(() => {
      toast.error("شروع انجام نشد");
    });
};
//////////////////////////////
const requestFinishBill = (playstation, finishInfo) => {
  const bill = state.billList.find(
    (items) => items.systemId === playstation.id && !items.endTime
  );
  billApi
    .close(bill.id, finishInfo)
    .then(() => {
      emit("requestGetBills");
      handleTimer(playstation, false);
      state.payModal.status = false;
      setTimeout(() => {
        state.payModal.playstation = {};
        state.payModal.billPlaystation = {};
      }, 200);
    })
    .catch(() => {
      toast.error("پایان انجام نشد");
    });
};
//////////////////////////////
const handleBillNotFinished = (billList) => {
  /////////////////////////////////////////////
  for (const bill of billList) {
    const playstation = state.consoleList.find(
      (console) => console.id === bill.systemId
    );
    if (!playstation.timer) {
      const money = state.moneyList.find(
        (items) => items.id === bill.hourRateId
      );
      /////////////////////////////////////////////
      const startTime = new Date(`${bill.startTime}Z`);
      let delta = Math.abs(new Date().getTime() - startTime.getTime()) / 1000;
      let days = Math.floor(delta / 86400);
      delta -= days * 86400;
      let hours = Math.floor(delta / 3600) % 24;
      delta -= hours * 3600;
      let minutes = Math.floor(delta / 60) % 60;
      delta -= minutes * 60;
      let seconds = Math.floor(delta % 60);
      playstation.time = {
        hours,
        minutes,
        seconds,
      };
      /////////////////////////////////////
      playstation.moneySelected = money;
      playstation.status = true;
      handleTimer(playstation, true);
    }
  }
};
//////////////////////////////
const handleContinuePlaystation = () => {
  state.payModal.status = false;
  setTimeout(() => {
    state.payModal.playstation = {};
  }, 200);
};
///////////////////////////
const handleFinishPlaystation = (foodSelected) => {
  const billId = state.payModal.billPlaystation.id;
  const food = [];
  if (foodSelected.length) {
    for (const item of foodSelected) {
      food.push({
        foodId: item.id,
        count: item.count,
      });
    }
    requestSetFood(billId, food);
  } else {
    requestFinishBill(state.payModal.playstation, new Date().toISOString());
  }
};
///////////////////////////
const requestSetFood = (billId, food) => {
  billApi
    .setFood(billId, food)
    .then(() => {
      requestFinishBill(state.payModal.playstation, new Date().toISOString());
    })
    .catch(() => {
      toast.error("خوراکی ها در این فاکتور ثبت نشد");
    });
};
</script>
<template>
  <div class="w-full h-full p-3 flex flex-wrap items-center justify-center">
    <div v-for="(playstation, index) in state.consoleList" :key="playstation.id" class="Console">
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
          <img src="@/assets/image/timer.png" v-if="!playstation.status" width="35" />
          <img v-else src="@/assets/image/timer.gif" width="35" />
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
          <div @click="handleShowAndHideMoneyList(playstation.id)" class="SelectedMoneyConsole">
            <p class="font-bold">
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
              <li @click="handleMoneySelect(money, playstation)" v-for="money in state.moneyList" :key="money.id">
                {{ money.rate.toLocaleString() }}
              </li>
            </ul>
          </transition-expand>
        </div>
      </div>
      <!-- //////////////////////////////// -->
      <button :class="playstation.status ? 'BtnConsoleFinish' : 'BtnConsoleStart'"
        @click="handleChangeConsoleStatus(playstation)">
        <p>{{ playstation.status ? "پایان" : "شروع" }}</p>
        <i v-if="!playstation.status" class="fa-thin fa-circle-play mr-2"></i>
        <i v-else class="fa-light fa-circle-xmark mr-2"></i>
      </button>
      <!-- ///////////////////////////////////////// -->
    </div>
    <!-- ////////////////////////////////////// -->
    <PayModal @continue="handleContinuePlaystation" @finish="handleFinishPlaystation" :payModal="state.payModal" />
    <!-- ////////////////////////////////////// -->
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
