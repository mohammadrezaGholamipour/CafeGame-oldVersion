import { useToast } from "vue-toastification";
import console from "@/api/console";
import { defineStore } from 'pinia'
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
          this.billList = response.reverse();
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