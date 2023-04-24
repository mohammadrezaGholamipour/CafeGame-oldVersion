import { useToast } from "vue-toastification";
import console from "@/api/console";
import { defineStore } from 'pinia'
import moment from "jalali-moment";
import money from "@/api/money";
import food from "@/api/food";
import bill from "@/api/bill";
// //////////////////////////////////
const toast = useToast();
// //////////////////////////////////
export const useStore = defineStore('pinia', {
  state: () => ({
    consoleList: false,
    moneyList: false,
    foodList: false,
    billList: false,
    userInfo: []
  }),
  actions: {
    setUserInfo(userInfo) {
      this.userInfo = userInfo
    },
    requestGetConsoles() {
      console
        .get()
        .then((response) => {
          this.consoleList = response;
          this.consoleList.forEach((items) => {
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
            items.alarm = ''
            items.alarmStatus = false
          });
          if (!this.moneyList) {
            this.requestGetMoneys()
          }
        })
        .catch(() => {
          toast.error("خطا در ارتباط با سرور");
        });
    },
    requestGetMoneys() {
      money
        .get()
        .then((response) => {
          this.moneyList = response;
          if (!this.foodList) {
            this.requestGetfoods()
          }
        })
        .catch(() => {
          toast.error("لیست قیمت ها دریافت نشد");
        });
    },
    requestGetfoods() {
      food
        .get()
        .then((response) => {
          this.foodList = response;
          if (!this.billList) {
            this.requestGetBills()
          }
        })
        .catch(() => {
          toast.error("لیست خوراکی ها دریافت نشد");
        });
    },
    requestGetBills() {
      bill
        .get()
        .then((response) => {
          ////////////////
          let notFinished = []
          let billSort = []
          ///////////////////
          response.forEach(item => {
            let costPlayGame = 0
            let costFood = 0
            ///////////////
            if (item.billFoods.length) {
              for (const foodBill of item.billFoods) {
                for (const food of this.foodList) {
                  if (foodBill.foodId === food.id) {
                    foodBill.total = food.cost * foodBill.count;
                    costPlayGame += foodBill.total;
                    costFood += foodBill.total;
                    foodBill.name = food.name
                    foodBill.cost = food.cost
                  }
                }
              }
              item.costPlayGame = item.finalCost - costPlayGame
              item.costFood = costFood
              // ////////////////////////
            } else {
              item.costPlayGame = item.finalCost
              item.costFood = costFood
            }
            // ////////////////
            if (item.endTime) {
              billSort.push(item)
            } else {
              notFinished.push(item)
            }
          })
          /////////////////
          billSort.sort((a, b) => Number(moment(a.endTime).locale("fa").format("YYMMDD")) - Number(moment(b.endTime).locale("fa").format("YYMMDD")))
          billSort.reverse()
          /////////////////
          this.billList = [...notFinished, ...billSort]
          ////////////////////
        })
        .catch(() => {
          toast.error("لیست فاکتور ها دریافت نشد");
        });
    },
    logOut() {
      this.consoleList = false
      this.moneyList = false
      this.foodList = false
      this.billList = false
      this.userInfo = []
    }

  },
  getters: {
    getConsoleList: (state) => state.consoleList,
    getMoneyList: (state) => state.moneyList,
    getFoodList: (state) => state.foodList,
    getBillList: (state) => state.billList,
    getUserInfo: (state) => state.userInfo,
  },
})