<template>
  <div class="flex items-center justify-center">
    <div v-if="true" class="flex flex-col items-center justify-center">
      <div class="ConsoleTimer">
        <p class="ml-2 mt-2 flex items-baseline">زمان بازی شده :2:57:32</p>
        <img src="../assets/image/time.svg" width="40" />
      </div>
      <div class="flex items-center">
        <p class="consolePay">20,0000 تومان</p>
        <img src="../assets/image/money.svg" width="50" />
      </div>
    </div>
    <div class="w-full" v-else>
      <table class="table-faktor">
        <thead>
          <tr class="faktor-header">
            <td>
              <div class="faktor-header-td">
                <p>ردیف</p>
                <i class="fa-solid fa-arrow-down-wide-short mr-1"></i>
              </div>
            </td>
            <td>
              <div class="faktor-header-td">
                <p style="white-space: nowrap">نام محصول</p>
                <i class="fa-duotone fa-burger-soda mr-1"></i>
              </div>
            </td>
            <td>
              <div class="faktor-header-td">
                <p style="white-space: nowrap">قیمت واحد</p>
                <i class="fa-duotone fa-money-bill mr-1"></i>
              </div>
            </td>
            <td>
              <div class="faktor-header-td">
                <p>تعداد</p>
                <i class="fa-duotone fa-tally mr-1"></i>
              </div>
            </td>
            <td>
              <div class="faktor-header-td">
                <p style="white-space: nowrap">جمع کل</p>
                <i class="fa-duotone fa-credit-card mr-1"></i>
              </div>
            </td>
          </tr>
        </thead>
        <tbody>
          <tr class="faktor-main">
            <td>1</td>
            <td>چیپس</td>
            <td>20,0000</td>
            <td>2</td>
            <td>40,0000</td>
          </tr>
        </tbody>
        <tfoot class="faktor-footer">
          <tr>
            <td class="text-right" colspan="3">هزینه خوارکی ها</td>
            <td class="text-left" colspan="2">55,000 تومان</td>
          </tr>
          <tr>
            <td class="text-right" colspan="3">هزینه بازی</td>
            <td class="text-left" colspan="2">63,000 تومان</td>
          </tr>
          <tr>
            <td class="text-right" colspan="3">جمع کل</td>
            <td class="text-left" colspan="2">57,0000 تومان</td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>
