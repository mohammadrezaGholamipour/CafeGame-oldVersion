<script setup>
import { cafePinia } from "../store/pinia";
import { computed } from "vue";
///////////////////////////////
const store = cafePinia();
// /////////////////////////
const handleTotalFoods = computed(() => {
  let foodsMoney = [];
  store.console.foods.forEach((items) => {
    foodsMoney.push(items.total);
  });
  const total = foodsMoney.reduce((money, items) => {
    return money + items;
  });
  return total;
});
</script>
<template>
  <div class="flex items-center justify-center">
    <div
      v-if="!store.console.foods?.length"
      class="flex flex-col items-center justify-center"
    >
      <div class="ConsoleTimer">
        <p class="ml-2 mt-2 flex items-baseline">
          زمان بازی شده :
          {{
            `${store.console?.time?.hours}:${store.console?.time?.minutes}:${store.console?.time?.seconds}`
          }}
        </p>
        <img src="../assets/image/time.svg" width="40" />
      </div>
      <div class="flex items-center">
        <p class="consolePay">
          {{ Number(store.console.money).toLocaleString() }} تومان
        </p>
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
          <tr
            v-for="(items, index) in store.console.foods"
            class="faktor-main"
            :key="index"
          >
            <td>{{ index + 1 }}</td>
            <td>{{ items.name }}</td>
            <td>{{ Number(items.money).toLocaleString() }}</td>
            <td>{{ items.count }}</td>
            <td>{{ items.total.toLocaleString() }}</td>
          </tr>
        </tbody>
        <tfoot class="faktor-footer">
          <tr>
            <td class="text-right" colspan="3">هزینه خوارکی ها</td>
            <td class="text-left" colspan="2">
              {{ Number(handleTotalFoods).toLocaleString() }} تومان
            </td>
          </tr>
          <tr>
            <td class="text-right" colspan="3">هزینه بازی</td>
            <td class="text-left" colspan="2">
              {{ Number(store.console.money).toLocaleString() }} تومان
            </td>
          </tr>
          <tr>
            <td class="text-right" colspan="3">جمع کل</td>
            <td class="text-left" colspan="2">
              {{
                (
                  Number(handleTotalFoods) + Number(store.console.money)
                ).toLocaleString()
              }}
              تومان
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>
