<script setup>
import { filterNumbersWithSep } from "../util/filter-numbers";
import { useForm, useField } from "vee-validate";
import { useToast } from "vue-toastification";
import { cafePinia } from "../store/pinia";
import { reactive, watch } from "vue";
import * as yup from "yup";
/////////////////////////
const emit = defineEmits(["food"]);
const props = defineProps(["food"]);
/////////////////////////
const store = cafePinia();
const toast = useToast();
///////////////////////
const state = reactive({
  schema: yup.object({
    name: yup
      .string()
      .min(2, "لطفا نام محصول را کامل وارد کنید")
      .required("لطفا نام محصول را وارد کنید")
      .nullable("لطفا نام محصول را وارد کنید"),
    money: yup
      .string()
      .min(4, "قیمت محصول درست نمیباشد")
      .required("لطفا قیمت را وارد کنید")
      .nullable("لطفا قیمت را وارد کنید"),
  }),
});
// ////////////////////////////////
watch(
  () => props.food,
  (value) => {
    name.value = value.name;
    money.value = value.cost;
  },
  { deep: true }
);
// ////////////////////////////////
const { handleSubmit } = useForm({ validationSchema: state.schema });
///////////////////////////////
const { value: name } = useField("name");
const { value: money } = useField("money");
///////////////////////////////
function onInvalidSubmit({ errors }) {
  toast.error(errors.name ? errors.name : errors.money);
}
//////////////////////////////
const onSubmit = handleSubmit(() => {
  handleAcceptFood();
}, onInvalidSubmit);
////////////////////////////////
const handleAcceptFood = () => {
  const closeModal = document.getElementsByClassName("close");
  const numberMoney = money.value;

  let food = {};
  ///////////////////////

  if (props.food?.id) {
    console.log("بوده");
    food = {
      id: 0,
      name: name.value,
      cost: parseInt(numberMoney.replace(",", "")),
    };
  } else {
    console.log("جدیده");
    food = {
      id: 0,
      name: name.value,
      cost: parseInt(numberMoney.replace(",", "")),
    };
  }
  emit("food", food);
  closeModal[0].click();
};
// /////////////////////
watch(
  () => money.value,
  (value) => {
    money.value = filterNumbersWithSep(value);
  }
);
</script>
<template>
  <div class="modal fade ParentModal" data-bs-backdrop="static" id="tableForm">
    <div class="modal-dialog modal-dialog-centered ModalDivOne">
      <div class="ModalDivTwo">
        <div class="ModalHeader">
          <p class="font-bold">افزون خوراکی</p>
          <i
            class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
            data-bs-dismiss="modal"
          ></i>
        </div>
        <div class="ModalMain">
          <div class="flex w-full justify-evenly items-center">
            <input
              placeholder="نام محصول"
              class="food-input"
              v-model="name"
              type="text"
            />
            <input
              placeholder="قیمت محصول"
              class="food-input"
              v-model="money"
              type="text"
            />
          </div>
        </div>
        <div class="ModalFooter">
          <button @click="onSubmit" class="BtnAccept">
            <p>ثبت</p>
            <i class="fa-duotone fa-badge-check mr-2"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
