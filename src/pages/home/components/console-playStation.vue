<script setup>
import SettingConsoleDialog from './setting-console/setting-console-dialog.vue'
import ConfirmDialog from '@/components/confirm-dialog.vue';
import { reactive, watch, onMounted } from "vue";
import { useToast } from "vue-toastification";
import { useRouter } from "vue-router";
import PayModal from "./pay-modal.vue";
import billApi from "@/api/bill";
////////////////////////
const props = defineProps(["consoleList", "moneyList", "foodList", "billList"]);
const emit = defineEmits(["requestGetBills"]);
////////////////////////
const router = useRouter()
const toast = useToast();
////////////////////////
const state = reactive({
  payModal: {
    playstation: {},
    billPlaystation: {},
    foodList: [],
    status: false,
  },
  settingDialog: {
    playstation: {},
    status: false
  },
  confirmDialog: {
    text: "فاکتور مورد نظر حذف شود؟",
    playstation: {},
    status: false
  }
});
/////////////////////
onMounted(() => {
  if (props.billList.length) {
    const billsNotFinished = props.billList.filter((items) => !items.endTime);
    if (billsNotFinished.length) {
      handleBillNotFinished(billsNotFinished);
    }
  }
})
/////////////////////
watch(
  () => props.billList,
  () => {
    if (props.billList.length) {
      const billsNotFinished = props.billList.filter((items) => !items.endTime);
      if (billsNotFinished.length) {
        handleBillNotFinished(billsNotFinished);
      }
    }
  }
);
/////////////////////////
const handleShowAndHideMoneyList = (id) => {
  if (props.moneyList.length) {
    const playstation = props.consoleList.find((items) => items.id === id);
    playstation.showAndHideListMoney = !playstation.showAndHideListMoney;
    const home = document.getElementsByClassName('ParentHome');
    const backGround = document.createElement('div');
    backGround.className = "background-select-money"
    backGround.onclick = handleHidenBackGroundSelectMoney
    home[0].appendChild(backGround)
  } else {
    router.push('/moneys')
  }
};
////////////////////////
const handleMoneySelect = (money, playstation) => {
  playstation.moneySelected = money;
  playstation.showAndHideListMoney = false;
  const backGround = document.getElementsByClassName('background-select-money');
  backGround[0].remove()
};
////////////////////////////
const handleHidenBackGroundSelectMoney = () => {
  const backGround = document.getElementsByClassName('background-select-money');
  backGround[0].remove()
  props.consoleList.forEach((item) => {
    item.showAndHideListMoney = false
  })
}
////////////////////////////
const handleChangeConsoleStatus = (playstation) => {
  if (playstation.status) {
    const bill = props.billList.find(
      (items) => items.systemId === playstation.id && !items.endTime
    );
    state.payModal.playstation = playstation;
    state.payModal.foodList = props.foodList;
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
  const bill = props.billList.find(
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
    const playstation = props.consoleList.find(
      (console) => console.id === bill.systemId
    );
    handleShowBillFoodMoney(playstation, bill.billFoods)
    if (!playstation.timer) {
      const money = props.moneyList.find(
        (items) => items.id === bill.hourRateId
      );
      /////////////////////////////////////////////
      const startTime = new Date(bill.startTime);
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
const handleFinishPlaystation = (paymentMethod, foodSelected) => {
  requestBillPayment(paymentMethod, foodSelected)
};
///////////////////////////
const requestBillPayment = (paymentMethod, foodSelected) => {
  const billId = state.payModal.billPlaystation.id;
  billApi.paymentMethod(billId, paymentMethod)
    .then(() => {
      const food = [];
      if (foodSelected.length) {
        for (const item of foodSelected) {
          food.push({
            foodId: item.id,
            count: item.count,
          });
        }
        requestSetFoodAndFinishBill(billId, food);
      } else {
        requestFinishBill(state.payModal.playstation, new Date().toISOString());
      }
    })
    .catch(() => { toast.error('خطا در سرور لطفا مجددا امتحان کنید') })
}
///////////////////////////
const getImageUrl = (imagePath) => {
  return new URL(imagePath, import.meta.url).href
}
///////////////////////////
const requestSetFoodAndFinishBill = (billId, food) => {
  billApi
    .setFood(billId, food)
    .then(() => {
      requestFinishBill(state.payModal.playstation, new Date().toISOString());
    })
    .catch(() => {
      toast.error("خوراکی ها در این فاکتور ثبت نشد");
    });
};
///////////////////////////
const requestSetFood = (billId, food) => {
  billApi
    .setFood(billId, food)
    .then(() => { emit("requestGetBills") })
    .catch(() => {
      toast.error("خوراکی ها در این فاکتور ثبت نشد");
    });
};
///////////////////////////
const changeMoney = (billId, money) => {
  billApi.changeMoney(billId, money.id)
    .then(() => {
      emit("requestGetBills")
      state.settingDialog.playstation.moneySelected = money

    })
    .catch(() => { toast.error('قیمت واحد فاکتور عوض نشد') })
}
///////////////////////////
const handleShowSettingDialog = (playstation) => {
  const billId = props.billList.find((item) => item.systemId === playstation.id && !item.endTime).id
  const billFoods = props.billList.find((item) => item.systemId === playstation.id && !item.endTime).billFoods
  state.settingDialog.playstation = playstation
  state.settingDialog.playstation.billId = billId
  state.settingDialog.playstation.billFoods = billFoods
  state.settingDialog.status = true
}
/////////////////////////
const handleCloseSettingConsoleDialog = (status, consoleSetting) => {
  if (status && consoleSetting.length) {
    for (const item of consoleSetting) {
      switch (item.name) {
        case 'food':
          const food = [];
          if (item.value.length) {
            for (const foodItem of item.value) {
              if (foodItem.count) {
                food.push({
                  foodId: foodItem.id,
                  count: foodItem.count,
                });
              }
            }
          }
          requestSetFood(state.settingDialog.playstation.billId, food)
          break;
        case 'changeBillMoney':
          changeMoney(state.settingDialog.playstation.billId, item.value)
          break;
      }
    }

    state.settingDialog.status = false
  } else {
    state.settingDialog.status = false
    state.settingDialog.playstation = {}
  }
}
//////////////////////////
const handleRemoveBill = (playstation) => {
  state.confirmDialog.status = true
  state.confirmDialog.playstation = playstation
}
//////////////////////
const requestRemoveBill = (status) => {
  state.confirmDialog.status = false
  if (status) {
    const billId = props.billList.find((item) => item.systemId === state.confirmDialog.playstation.id && !item.endTime).id
    billApi.remove(billId)
      .then(() => {
        state.confirmDialog.playstation.status = false
        state.confirmDialog.playstation.showAndHideListMoney = false;
        state.confirmDialog.playstation.moneySelected = {};
        clearInterval(state.confirmDialog.playstation.timer)
        state.confirmDialog.playstation.timer = "";
        state.confirmDialog.playstation.time = {
          hours: "00",
          minutes: "00",
          seconds: "00",
        };
        state.confirmDialog.playstation.userMoney = "";
        emit('requestGetBills')
      })
      .catch(() => {
        toast.error('فاکتور مورد نظر حذف نشد')
      })
  }
}
const handleShowBillFoodMoney = (playstation, billFoods) => {
  if (billFoods.length) {
    /////////////////////
    let billFoodList = [];
    for (const food of props.foodList) {
      for (const billFood of billFoods) {
        if (food.id === billFood.foodId) {
          billFood.name = food.name;
          billFood.cost = food.cost;
          billFood.total = billFood.cost * billFood.count;
          billFoodList.push(billFood);
        }
      }
    }
    ///////////////////
    const foodMoney = [];
    for (const item of billFoodList) {
      foodMoney.push(item.total);
    }
    const totalFoodMoney = foodMoney.reduce((total, item) => {
      return total + item;
    });
    ///////////////////
    playstation.foodMoney = totalFoodMoney.toLocaleString()
  } else {
    playstation.foodMoney = ''
  }
}
</script>
<template>
  <div class="w-full h-full mt-10 p-3 flex flex-wrap items-center justify-center">
    <div v-for="(playstation, index) in props.consoleList" :key="playstation.id" class="Console">
      <!-- //////////////////////////////// -->
      <p class="ConsoleNumber">
        <img :src="getImageUrl(`/assets/${index + 1}.png`)" width="50" />
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
      <transition-fade>
        <i v-if="playstation.status" @click="handleShowSettingDialog(playstation)"
          class="fa-duotone fa-sliders fa-beat setting-console text-blue-700"></i>
      </transition-fade>
      <!-- /////////////////////////////// -->
      <transition-fade>
        <i v-if="playstation.status" @click="handleRemoveBill(playstation)"
          class="fa-duotone fa-trash fa-beat remove-bill text-red-700"></i>
      </transition-fade>
      <!-- /////////////////////////////// -->
      <div class="flex w-full flex-col justify-start items-center">
        <div v-if="playstation.status" class="ConsoleMoney text-2xl">
          <transition-scale>
            <div v-if="playstation.userMoney" class="flex items-center">
              <p> {{ playstation.userMoney.toLocaleString() }}</p>
              <i class="fa-duotone fa-money-bill-1-wave text-green-500 mr-2"></i>
            </div>
          </transition-scale>
          <!-- ///////////////////// -->
          <transition-scale>
            <div v-if="playstation?.foodMoney?.length" class="flex items-center">
              <p>{{ playstation.foodMoney }}</p>
              <i class="fa-duotone fa-burger-soda text-yellow-700 mr-2"></i>
            </div>
          </transition-scale>
        </div>
        <!-- /////////////////////////// -->
        <div class="relative w-full flex justify-center" v-else>
          <div @click="handleShowAndHideMoneyList(playstation.id)" class="SelectedMoneyConsole">
            <p class="font-bold">
              {{
                playstation.moneySelected?.rate
                ? playstation.moneySelected.rate.toLocaleString()
                : props.moneyList.length ? "هزینه بازی" : 'افزودن قیمت'
              }}
            </p>
            <i class="fa-duotone transition-all duration-500"></i>
          </div>
          <transition-expand>
            <ul v-if="playstation.showAndHideListMoney" class="UlMoney">
              <li @click="handleMoneySelect(money, playstation)" v-for="money in props.moneyList" :key="money.id">
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
    <SettingConsoleDialog @close="handleCloseSettingConsoleDialog" :settingDialog="state.settingDialog" />
    <!-- ////////////////////////////////////// -->
    <ConfirmDialog @acceptOrCansel="requestRemoveBill" :confirmDialog="state.confirmDialog" />
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
