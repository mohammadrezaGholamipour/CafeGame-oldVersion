<script setup>
import { cafePinia } from "../store/pinia";
///////////////////////////////
const store = cafePinia();
</script>
<template>
  <div v-for="(items, index) in store.listConsole" class="Console" :key="index">
    <!-- //////////////////////////////// -->
    <p class="ConsoleNumber">
      <img
        :src="`src/assets/image/numbers/${items.numberConsole}.png`"
        width="50"
      />
    </p>
    <!-- ////////////////////////////////// -->
    <div class="ConsoleTimer">
      <p class="ml-2 mt-1">
        {{ `${items.time.hours}:${items.time.minutes}:${items.time.seconds}` }}
      </p>
      <transition-scale group>
        <img v-if="items.status" src="../assets/image/timer.gif" width="35" />
        <img v-else src="../assets/image/timer.png" width="35" />
      </transition-scale>
    </div>
    <!-- /////////////////////////////// -->
    <div class="flex flex-col justify-start items-center">
      <transition-scale group>
        <div v-if="!items.status" class="relative">
          <div
            @click="store.handleShowListMoney(index)"
            class="SelectedMoneyConsole"
          >
            <p class="ml-2">
              {{
                items.inputmoney
                  ? Number(items.inputmoney).toLocaleString()
                  : "قیمت"
              }}
            </p>
            <i
              class="fa-duotone transition-all duration-500"
              :class="
                items.showAndHideInputMoney
                  ? 'fa-circle-arrow-down rotate-180'
                  : 'fa-circle-arrow-down'
              "
            ></i>
          </div>
          <transition-expand>
            <ul
              @click="store.handleChangeMoney($event, index)"
              v-if="items.showAndHideInputMoney"
              class="UlMoney"
            >
              <li v-for="(items, index) in store.listMoney" :key="index">
                {{ items }}
              </li>
            </ul>
          </transition-expand>
        </div>

        <!-- /////////////////////////////////// -->
        <p v-if="items.status && items.money" class="ConsoleMoney">
          {{ `${Number(items.money).toLocaleString()} تومان` }}
        </p>
      </transition-scale>
    </div>
    <!-- //////////////////////////////// -->
    <button
      :class="items.status ? 'BtnConsoleFinish' : ' BtnConsoleStart'"
      :data-bs-toggle="items.status ? 'modal' : ''"
      @click="store.handleStatusConsole(index)"
      data-bs-target="#ConsolePay"
    >
      <p>{{ items.status ? "پایان" : "شروع" }}</p>
      <i v-show="!items.status" class="fa-thin fa-circle-play mr-2"></i>
      <i v-show="items.status" class="fa-light fa-circle-xmark mr-2"></i>
    </button>
    <!-- ///////////////////////////////////////// -->
  </div>
</template>
