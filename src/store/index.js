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
    consoleList: [],
    moneyList: [],
    foodList: [],
    billList: [],
  }),
  actions: {
    requestGetConsoles() {
      console
        .get()
        .then((response) => {
          this.consoleList = response;
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
        })
        .catch(() => {
          toast.error("لیست خوراکی ها دریافت نشد");
        });
    },
    requestGetBills() {
      bill
        .get()
        .then((response) => {
          this.billList = response;
        })
        .catch(() => {
          toast.error("لیست فاکتور ها دریافت نشد");
        });
    },
  },
  getters: {
    getConsoleList: (state) => state.consoleList,
    getMoneyList: (state) => state.moneyList,
    getFoodList: (state) => state.foodList,
    getBillList: (state) => state.billList,
  },
})