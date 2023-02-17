<script setup>
import { filterNumbersWithSep } from "../../../util/filter-numbers";
import { useForm, useField } from "vee-validate";
import { useToast } from "vue-toastification";
import { reactive, watch } from "vue";
import * as yup from "yup";
/////////////////////////
const emit = defineEmits(["food", "close"]);
const props = defineProps(["formModal"]);
/////////////////////////
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
//////////////////////////////////
watch(
  () => props.formModal.data,
  (value) => {
    name.value = value.name;
    money.value = value.cost;
  }
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
  const numberMoney = parseInt(money.value.replace(",", ""));
  let food = {};
  ///////////////////////
  if (props.formModal.data.id) {
    if (
      props.formModal.data.name !== name.value ||
      props.formModal.data.cost !== numberMoney
    ) {
      food = {
        id: 0,
        name: name.value,
        cost: numberMoney,
      };
      emit("food", food);
    } else {
      handleCloseModal();
    }
  } else {
    food = {
      id: 0,
      name: name.value,
      cost: numberMoney,
    };
    emit("food", food);
  }
};
///////////////////////
watch(
  () => money.value,
  (value) => {
    money.value = filterNumbersWithSep(value);
  }
);
const handleCloseModal = () => {
  emit("close");
};
</script>
<template>
  <v-dialog
    :modelValue="props.formModal?.status"
    update:modelValue="handleCloseModal"
    persistent
    width="500"
  >
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalHeader">
        <i
          class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleCloseModal"
        ></i>
        <p class="font-bold">خوراکی</p>
      </div>
      <div class="ModalMain">
        <div class="flex w-full justify-evenly items-center">
          <input
            placeholder="قیمت محصول"
            class="food-input"
            v-model="money"
            type="text"
            dir="rtl"
          />
          <input
            placeholder="نام محصول"
            class="food-input"
            v-model="name"
            type="text"
            dir="rtl"
          />
        </div>
      </div>
      <div class="ModalFooter">
        <button @click="onSubmit" class="BtnAccept">
          <i class="fa-duotone fa-badge-check mr-2"></i>
          <p>ثبت</p>
        </button>
      </div>
    </div>
  </v-dialog>
</template>
