import { useToast } from "vue-toastification";
import { defineStore } from 'pinia'
// ///////////////////////////////
const toast = useToast()
// /////////////////////
export const cafePinia = defineStore({
  id: 'cafeGame',
  state: () => ({
    playStationList: [],
    foodList: [],
    moneyList: [],
  }),
  actions: {
    handleChangePlayStationList(playStations) {
      this.playStationList = playStations
    },
    handleChangefoodList(foods) {
      this.foodList = foods
    },
    handleChangemoneyList(moneys) {
      this.moneyList = moneys
    }
  },
  getters: {},
});