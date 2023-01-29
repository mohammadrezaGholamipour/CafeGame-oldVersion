<template>
  <div class="Console">
    <!-- //////////////////////////////// -->
    <p class="ConsoleNumber">
      <img :src="`src/assets/image/numbers/one.png`" width="50" />
    </p>
    <!-- ////////////////////////////////// -->
    <div class="ConsoleTimer">
      <p class="ml-2 mt-1">15:17:00</p>
      <transition-scale group>
        <img src="../assets/image/timer.png" width="35" />
      </transition-scale>
    </div>
    <!-- /////////////////////////////// -->
    <div class="flex flex-col justify-start items-center">
      <transition-scale group>
        <div class="relative">
          <div class="SelectedMoneyConsole">
            <p class="ml-2">10,000</p>
            <i class="fa-duotone transition-all duration-500"></i>
          </div>
          <transition-expand>
            <ul class="UlMoney">
              <li>2000</li>
            </ul>
          </transition-expand>
        </div>

        <!-- /////////////////////////////////// -->
        <p class="ConsoleMoney">10,000 تومن</p>
      </transition-scale>
    </div>
    <!-- //////////////////////////////// -->
    <button class="BtnConsoleStart">
      <p>شروع</p>
      <i class="fa-thin fa-circle-play mr-2"></i>
    </button>
    <!-- ///////////////////////////////////////// -->
  </div>
</template>
