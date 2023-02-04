<script setup>
import ConsolePlayStation from "../components/console-playStation.vue";
import { useToast } from "vue-toastification";
import { cafePinia } from "../store/pinia";
import console from "../api/console";
import money from "../api/money";
import { onMounted } from "vue";
import food from "../api/food";
////////////////////////
const store = cafePinia();
const toast = useToast();
////////////////////////
const requestGetConsoles = () => {
  console
    .get()
    .then((response) => {
      store.handleChangePlayStationList(response);
    })
    .catch(() => {
      toast.error("لیست دستگاه ها دریافت نشد");
    });
};
////////////////////////
const requestGetMoneys = () => {
  money
    .get()
    .then((response) => {
      store.handleChangemoneyList(response);
    })
    .catch(() => {
      toast.error("لیست قیمت ها دریافت نشد");
    });
};
////////////////////////
const requestGetfoods = () => {
  food
    .get()
    .then((response) => {
      store.handleChangefoodList(response);
    })
    .catch(() => {
      toast.error("لیست خوراکی ها دریافت نشد");
    });
};
////////////////////////
onMounted(() => {
  requestGetConsoles();
  requestGetMoneys();
  requestGetfoods();
});
</script>
<template>
  <div class="ParentHome">
    <ConsolePlayStation />
  </div>
</template>
