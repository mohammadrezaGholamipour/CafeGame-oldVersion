<script setup>
import BillFilterDialog from "./bill-filter-dialog.vue";
import { computed, reactive, onMounted } from "vue";
import BillInfoDialog from "./bill-info-dialog.vue";
import BillsReport from "./billsReport.vue";
import { useWindowSize } from '@vueuse/core'
import BillTime from "./bill-time.vue";
import BillFood from "./bill-food.vue";
import moment from "jalali-moment";
import billApi from '@/api/bill'
/////////////////////////////
const props = defineProps(["bills", "foods", "moneys", "consoles"]);
/////////////////////////////
const { width } = useWindowSize()
/////////////////////////////
const state = reactive({
  billList: [],
  headerBills: [
    { name: "ردیف", icon: "fa-duotone fa-arrow-down-wide-short" },
    {
      name: "اطلاعات زمان شروع و پایان",
      icon: "fa-duotone fa-hourglass-start text-red-500",
    },
    {
      name: "مبلغ واحد",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
    { name: "خوراکی ها", icon: "fa-duotone fa-burger-soda text-yellow-700" },
    { name: "هزینه بازی شده", icon: "fa-duotone fa-coin text-green-600" },
    {
      name: "شماره دستگاه",
      icon: "fa-brands fa-playstation text-blue-500",
    },
    {
      name: "روش پرداخت",
      icon: "fa-duotone fa-cash-register text-red-700",
    },
    {
      name: "مبلغ پرداخت شده",
      icon: "fa-duotone fa-money-bill-1-wave text-green-500",
    },
  ],
  dialog: {
    headerInfo: "",
    data: {},
    status: false,
    component: "",
  },
  filterDialog: {
    status: false,
    filterStatus: false
  },
  costList: {
    costPlayGame: 0,
    totalBills: 0,
    totalCost: 0,
    costFood: 0,
    time: 0,
  },
  showAndHideDatePicker: false
});
/////////////////////////////////
onMounted(() => {
  state.costList.time = moment().locale("fa").format("YYYY/MM/DD")
  /////////////
  if (props.bills.length) {
    state.billList = props.bills
    for (const bill of state.billList) {
      if (bill.endTime) {
        if (moment(bill.endTime).locale("fa").format("YYYY/MM/DD") === state.costList.time) {
          state.costList.totalCost += (bill.costFood + bill.costPlayGame)
          state.costList.costPlayGame += bill.costPlayGame
          state.costList.costFood += bill.costFood
          state.costList.totalBills++
        }
      }
    }
  }
});
/////////////////////////////////
const changeDateBillsReport = (date) => {
  state.costList.time = date
  state.costList.costPlayGame = 0
  state.costList.costFood = 0
  state.costList.totalBills = 0
  state.costList.totalCost = 0
  /////////////////////////
  if (props.bills.length) {
    for (const bill of state.billList) {
      if (bill.endTime) {
        if (moment(bill.endTime).locale("fa").format("YYYY/MM/DD") === state.costList.time) {
          state.costList.totalCost += (bill.costFood + bill.costPlayGame)
          state.costList.costPlayGame += bill.costPlayGame
          state.costList.costFood += bill.costFood
          state.costList.totalBills++
        }
      }
    }
  }
}
/////////////////////////////////
const handleFindMoney = (hourRateId) => {
  return `${props.moneys
    .find((items) => items.id === hourRateId)
    ?.rate?.toLocaleString()} تومان`;
};
/////////////////////////////////
const handleShowDialog = (data, type) => {
  state.dialog.data = data;
  state.dialog.component = type;
  //////////////////////////
  if (type === "time") {
    state.dialog.headerInfo = "اطلاعات زمان شروع و پایان فاکتور";
  } else if (type === "food") {
    state.dialog.headerInfo = "لیست خوراکی های ثبت شده";
  } else {
    state.dialog.headerInfo = "آمار فاکتور های ثبت شده";
  }
  state.dialog.status = true;
};
/////////////////////////////////
const handleCloseDialog = () => {
  state.dialog.status = false;
};
//////////////////////////////
const handleShowEndTime = (endTime) => {
  const finishFaktorDay = Number(moment(endTime).locale("fa").format("DD"))
  const finishFaktorMonth = Number(moment(endTime).locale("fa").format("MM"))
  ///////////////////////////////////////////
  const dayNow = Number(moment().locale("fa").format("DD"))
  const monthNow = Number(moment().locale("fa").format("MM"))
  ///////////////////////////////////////////
  if (monthNow === finishFaktorMonth) {
    switch (dayNow) {
      case finishFaktorDay:
        return 'همین امروز';
      case finishFaktorDay + 1:
        return 'دیروز';
      case finishFaktorDay + 2:
        return 'دو روز پیش';
      case finishFaktorDay + 3:
        return 'سه روز پیش';
      default:
        return moment(endTime).locale("fa").format("YY/MM/DD");
    }
  } else {
    return moment(endTime).locale("fa").format("YY/MM/DD")
  }


}
//////////////////////
const handleFilter = () => {
  if (state.filterDialog.filterStatus) {
    state.filterDialog.filterStatus = !state.filterDialog.filterStatus
    state.billList = props.bills
  } else {
    state.filterDialog.status = true
  }
}
////////////////////
const styleBtnSearch = computed(() => {
  if (state.filterDialog.filterStatus) {
    return '--fa-primary-color: #775a5a; --fa-secondary-color: #d93636;'
  } else { return '--fa-primary-color: #5e91d4; --fa-secondary-color: #367cba;' }
})
////////////////////
const handleAcceptOrCanselFilter = (status, filterData) => {
  state.filterDialog.status = false
  if (status) {
    requestGetBillFilter(filterData)
  } else {
    state.filterDialog.filterStatus = false
  }
}
////////////////////
const handleFindConsole = (systemId) => {
  const console = props.consoles.find((item) => item.id === systemId)
  return console.name
}
////////////////////
const requestGetBillFilter = (filterData) => {
  const url = `bill?systemId=${filterData.playstation ? filterData.playstation : ''}&startDate=${filterData.startDate ? filterData.startDate.replace(':', '%3A') : ''}&endDate=${filterData.endDate ? filterData.endDate.replace(':', '%3A') : ''}`;
  ////////////////////
  billApi.getFilter(url)
    .then((response) => {
      ////////////////
      let notFinished = []
      let billSort = []
      ///////////////////
      response.forEach(item => {
        if (item.endTime) {
          billSort.push(item)
        } else {
          notFinished.push(item)
        }
      })
      /////////////////
      billSort.sort((a, b) => Number(moment(a.endTime).locale("fa").format("DD")) - Number(moment(b.endTime).locale("fa").format("DD")))
      billSort.reverse()
      /////////////////
      state.billList = [...notFinished, ...billSort]
      ////////////////////
      state.filterDialog.filterStatus = true
    }).catch((error) => {
      console.log(error);
    })
}
//////////////////////////////
const handlePaymentMethod = (paymentMethod, finalCost) => {
  if (finalCost) {
    switch (paymentMethod) {
      case 0:
        return 'نقد';
      case 1:
        return 'کارت';
      case 2:
        return 'اکانت';
    }
  } else {
    return '-'
  }

}
//////////////////////////////
</script>
<template>
  <div class="overflow-y-scroll h-[86vh] flex items-center flex-col justify-start mt-15 rounded-md w-[90vw]">
    <table v-if="width > 1022" class="Table-bills">
      <thead>
        <tr>
          <th class="sticky top-0 bg-slate-200" v-for="(items, index) in state.headerBills" :key="index">
            <div class="td-header-foods">
              <p class="ml-2 font-bold">{{ items.name }}</p>
              <i :class="items.icon" />
              <i @click="handleFilter" v-if="items.name === 'ردیف'"
                class="fa-duotone fa-magnifying-glass fa-beat-fade mr-2 cursor-pointer text-xl"
                :style="styleBtnSearch"></i>
            </div>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="state.billList.length" v-for="(items, index) in state.billList" :key="items.id">
          <td>{{ index + 1 }}</td>
          <td>
            <div class="flex items-center justify-between">
              <p class="ml-2">{{ items.endTime ? handleShowEndTime(items.endTime) : ' تمام نشده است' }}</p>
              <button @click="handleShowDialog(
                {
                  start: items.startTime,
                  end: items.endTime,
                },
                'time'
              )
                " class="BtnChange mr-0">
                نمایش
              </button>

            </div>
          </td>
          <td>{{ handleFindMoney(items.hourRateId) }}</td>
          <td>
            <div class="flex justify-center">
              <button @click="handleShowDialog(items.billFoods, 'food')" v-if="items.billFoods.length"
                class="BtnChange mr-0">
                نمایش
              </button>
              <p v-else>بدون خوراکی</p>
            </div>
          </td>
          <td>{{ items.costPlayGame ? `${items.costPlayGame.toLocaleString()} تومان` : '-' }}</td>
          <td>{{ handleFindConsole(items.systemId) }}</td>
          <td>{{ handlePaymentMethod(items.paymentMethod, items.finalCost) }}</td>
          <td>
            <p class="font-bold"> {{
              items.finalCost
              ? `${items.finalCost?.toLocaleString()} تومان`
              : "-"
            }}</p>
          </td>
        </tr>
        <tr v-else>
          <td colspan="8">
            <div class="w-full flex items-center justify-center">
              <p class="text-center p-2 font-bold text-red-500 text-lg">
                لیست تراکنش ها خالی میباشد
              </p>
              <i class="fa-duotone fa-sack-dollar text-lg text-red-700"></i>
            </div>
          </td>
        </tr>
      </tbody>
      <tfoot v-if="state.billList.length">
        <tr class="sticky bottom-0 text-center text-lg text-white">
          <td @click="state.showAndHideDatePicker = true" class="p-2 bg-slate-500">{{ state.costList.time }}</td>
          <td class="bg-yellow-500">تعداد فاکتور : {{ state.costList.totalBills }}</td>
          <td colspan="2" class="bg-red-600">خوراکی ها : {{ state.costList.costFood.toLocaleString() }}</td>
          <td colspan="2" class="bg-blue-500">بازی شده : {{ state.costList.costPlayGame.toLocaleString() }}</td>
          <td colspan="2" class="bg-green-600">جمع کل : {{ state.costList.totalCost.toLocaleString() }}</td>
        </tr>
      </tfoot>
    </table>
    <!-- ///////////////////// -->
    <div v-else class="w-full flex flex-col items-center justify-start">
      <div class="flex justify-center items-center">
        <button @click="handleFilter"
          class="border-2 border-solid border-yellow-500 shadow-lg font-bold text-white p-2 rounded-md">فیلتر ها <i
            class="fa-duotone fa-magnifying-glass fa-beat-fade mr-2 cursor-pointer text-xl"
            :style="styleBtnSearch"></i></button>
        <button @click="handleShowDialog(state.costList, 'billsReport')"
          class="border-2 border-solid border-red-500 mr-2 shadow-lg font-bold text-white p-2 rounded-md">آمار
          فاکتورها <i class="fa-duotone fa-cash-register fa-beat-fade text-red-700 text-xl" /></button>
      </div>
      <div v-for="(items, index) in state.billList" :key="index" class="parent-mobile-table min-w-[300px] flex-row">
        <div class="flex flex-col justify-between items-start">
          <!-- ////////////////////////////// -->
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">ردیف</p>
            <i class="fa-duotone fa-arrow-down-wide-short" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">اطلاعات زمان</p>
            <i class="fa-duotone fa-hourglass-start text-red-500" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">مبلغ واحد</p>
            <i class="fa-duotone fa-money-bill-1-wave text-green-500" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">خوراکی ها</p>
            <i class="fa-duotone fa-burger-soda text-red-700" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">هزینه بازی شده</p>
            <i class="fa-duotone fa-coin text-green-600" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">شماره دستگاه</p>
            <i class="fa-brands fa-playstation text-blue-500" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">روش پرداخت</p>
            <i class="fa-duotone fa-cash-register text-red-700" />
          </div>
          <div class="flex items-center justify-end py-2">
            <p class="ml-1">پرداخت شده</p>
            <i class="fa-duotone fa-money-bill-1-wave text-green-500" />
          </div>
          <!-- ////////////////////////////// -->
        </div>
        <div class="flex flex-col justify-between items-center">
          <p class="py-2">{{ index + 1 }}</p>
          <p>
          <div class="flex items-center justify-between py-2">
            <p class="ml-2">{{ items.endTime ? handleShowEndTime(items.endTime) : 'تمام نشده است' }}</p>
            <p class="text-red-500 font-bold"
              @click="handleShowDialog({ start: items.startTime, end: items.endTime, }, 'time')">
              نمایش
            </p>
          </div>
          </p>
          <p class="py-2">{{ handleFindMoney(items.hourRateId) }}</p>
          <p>
          <div class="flex justify-center py-2">
            <p @click="handleShowDialog(items.billFoods, 'food')" v-if="items.billFoods.length"
              class="text-red-500 font-bold">
              نمایش
            </p>

            <p v-else>بدون خوراکی</p>
          </div>
          </p>
          <p class="py-2">{{ items.costPlayGame ? `${items.costPlayGame.toLocaleString()} تومان` : '-' }}</p>
          <p class="py-2">{{ handleFindConsole(items.systemId) }}</p>
          <p class="py-2">{{ handlePaymentMethod(items.paymentMethod, items.finalCost) }}</p>
          <p class="font-bold py-2"> {{
            items.finalCost
            ? `${items.finalCost?.toLocaleString()} تومان`
            : "-"
          }}</p>
        </div>
      </div>
    </div>
  </div>
  <!-- ///////////////////////////////// -->
  <BillInfoDialog :dialog="state.dialog" @close="handleCloseDialog">
    <BillTime v-if="state.dialog.component === 'time'" :billTime="state.dialog.data" />
    <BillFood :foods="props.foods" :billFood="state.dialog.data" v-if="state.dialog.component === 'food'" />
    <BillsReport @changeDateBillsReport="changeDateBillsReport" :billsReport="state.dialog.data"
      v-if="state.dialog.component === 'billsReport'" />
  </BillInfoDialog>
  <!-- ///////////////////////////////// -->
  <BillFilterDialog :dialog="state.filterDialog" @acceptOrCansel="handleAcceptOrCanselFilter" />
  <date-picker @change="changeDateBillsReport($event.format('jYYYY/jMM/jDD'))" :show="state.showAndHideDatePicker"
    format="jYYYY/jMM/jDD" simple id="dataBillsReport" @close="state.showAndHideDatePicker = false" />
</template>
<style>
#dataBillsReport .vpd-input-group {
  display: none;
}
</style>

