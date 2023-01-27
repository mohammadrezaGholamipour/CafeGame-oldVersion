import { useToast } from "vue-toastification";
import { defineStore } from 'pinia'
// ///////////////////////////////
const toast = useToast()
// /////////////////////
export const cafePinia = defineStore({
  id: 'cafeGame',
  state: () => ({
    listConsole: [
      {
        id: 1,
        numberConsole: "one",
        timer: "",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 2,
        numberConsole: "two",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 3,
        numberConsole: "three",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 4,
        numberConsole: "four",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 5,
        numberConsole: "five",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 6,
        numberConsole: "six",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 7,
        numberConsole: "seven",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
      {
        id: 8,
        numberConsole: "eigth",
        time: {
          hours: "00",
          minutes: "00",
          seconds: "00",
        },
        timer: "",
        inputmoney: "",
        money: "",
        status: false,
        showAndHideInputMoney: false,
        foods: []
      },
    ],
    foodList: [
      { id: 1, name: "اب کوچک", money: "4,000", count: 0, total: '' },
      { id: 2, name: "چیپس", money: "12,000", count: 0, total: '' },
      { id: 3, name: "نوشابه", money: "8,000", count: 0, total: '' },
      { id: 4, name: "پفک", money: "10,000", count: 0, total: '' },
      { id: 5, name: "کلوچه", money: "5,000", count: 0, total: '' },
    ],
    listMoney: ["12,000", "15,000", "20,000", "25,000", "30,000"],
    storeOrFaktor: true,
    console: {},
    food: {},
  }),
  actions: {
    handleStatusConsole(index) {
      if (this.listConsole[index].inputmoney) {
        this.listConsole[index].status = !this.listConsole[index].status;
      } else {
        toast.error("قیمت واحد را وارد نکرده اید");
      }
      if (this.listConsole[index].status) {
        this.listConsole[index].time.hours = "00";
        this.listConsole[index].time.minutes = "00";
        this.listConsole[index].time.seconds = "00";
        this.listConsole[index].money = "";
        this.handleTimer(index, true);
      } else {
        this.handleTimer(index, false);
        this.console = this.listConsole[index]
      }
    },
    // //////////////////////////////////
    handleTimer(index, status) {
      if (status) {
        this.listConsole[index].timer = setInterval(() => {
          const inputMoney = this.listConsole[index].inputmoney;
          const minutes = this.listConsole[index].time.minutes;
          const hours = this.listConsole[index].time.hours;
          const minutesMoney = (Number(inputMoney) / 60) * minutes;
          /////////////////////////////////////////////
          this.listConsole[index].time.seconds++;
          if (this.listConsole[index].time.seconds == 60) {
            this.listConsole[index].time.seconds = "00";
            this.listConsole[index].time.minutes++;
          }
          if (this.listConsole[index].time.minutes == 60) {
            this.listConsole[index].time.minutes = "00";
            this.listConsole[index].time.hours++;
          }
          if (this.listConsole[index].time.hours > 0) {
            this.listConsole[index].money = parseInt(
              Math.round(hours * inputMoney + minutesMoney)
            )
          } else {
            this.listConsole[index].money = parseInt(
              Math.round(this.listConsole[index].inputmoney)
            )
          }
        }, 1000);
      } else {
        clearInterval(this.listConsole[index].timer);
      }
    },
    // //////////////////////////////////
    handleChangeMoney(event, index) {
      let money = event.target.innerHTML;
      this.listConsole[index].inputmoney = Number(money.replace(",", ""));
      this.listConsole[index].showAndHideInputMoney =
        !this.listConsole[index].showAndHideInputMoney;
      this.listConsole[index].money = "";
      this.listConsole[index].time.hours = "00";
      this.listConsole[index].time.minutes = "00";
      this.listConsole[index].time.seconds = "00";
    },
    // /////////////////////////////////
    handleShowListMoney(index) {
      this.listConsole[index].showAndHideInputMoney =
        !this.listConsole[index].showAndHideInputMoney;
    },
    // /////////////////////////////////
    handleStoreOrFaktor() {
      this.storeOrFaktor = !this.storeOrFaktor
      if (this.storeOrFaktor) {
        this.foodList.forEach((items) => {
          if (items.count) {
            items.total = Number(items.money) * items.count
          }
        })
        const foods = this.foodList.filter((items) => items.count)
        this.console.foods = foods
      }
    },
    // /////////////////////////////////
    handleContinue() {
      const consoleIndex = this.listConsole.findIndex((items) => items.id === this.console.id)
      this.listConsole[consoleIndex].status = true
      this.handleTimer(consoleIndex, true)
      this.storeOrFaktor = true
      setTimeout(() => {
        this.foodList.forEach((items) => {
          items.count = 0
        })
        this.listConsole[consoleIndex].foods = []
        this.console = {}
      }, 300);

    },
    // /////////////////////////////////
    handlePlusFood(index) {
      this.foodList[index].count++
    },
    handleMinusFood(index) {
      if (this.foodList[index].count > 0) {
        this.foodList[index].count--
      }
    },
    // /////////////////////////////////
    handleChangefoodInfo(food) {
      this.food = food
    },
    ////////////////////////////////
    handleCloseFoodModal() {
      setTimeout(() => {
        this.food = {}
      }, 500);
    },
    ////////////////////////////////
    handleRemoveFood(name) {
      this.foodList = this.foodList.filter((items) => items.name !== name)
    },
    // /////////////////////////////////
    handleAcceptFood(food) {
      const foodItem = this.foodList.find((items) => items.id === food.id)
      if (foodItem) {
        foodItem.name = food.name
        foodItem.money = food.money
      } else {
        this.foodList.push(food)
      }
    },
    // /////////////////////////////////
    handleAcceptPay() {
      const consoleIndex = this.listConsole.findIndex((items) => items.id === this.console.id)
      this.listConsole[consoleIndex].inputmoney = ''
      this.listConsole[consoleIndex].time.hours = '00'
      this.listConsole[consoleIndex].time.minutes = '00'
      this.listConsole[consoleIndex].time.seconds = '00'
      setTimeout(() => {
        this.foodList.forEach((items) => {
          items.count = 0
          items.total = ''
        })
        this.listConsole[consoleIndex].foods = []
        this.listConsole[consoleIndex].money = ''

        this.console = {}
      }, 500);
      this.storeOrFaktor = true
    }
    // /////////////////////////////////
  },
  getters: {},
});