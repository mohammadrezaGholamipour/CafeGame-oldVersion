<script setup>
import { filterNumbersWithSep } from "../../../util/filter-numbers";
import { useForm, useField } from "vee-validate";
import { useToast } from "vue-toastification";
import { reactive, watch } from "vue";
import * as yup from "yup";
/////////////////////////
const emit = defineEmits(["money", "close"]);
const props = defineProps(["formModal"]);
/////////////////////////
const toast = useToast();
///////////////////////
const state = reactive({
  schema: yup.object({
    money: yup
      .string()
      .min(4, "قیمت محصول درست نمیباشد")
      .required("لطفا قیمت را وارد کنید")
      .nullable("لطفا قیمت را وارد کنید"),
  }),
});

// ////////////////////////////////
const { handleSubmit } = useForm({ validationSchema: state.schema });
///////////////////////////////
const { value: money } = useField("money");
///////////////////////////////
function onInvalidSubmit({ errors }) {
  toast.error(errors.money);
}
//////////////////////////////
const onSubmit = handleSubmit(() => {
  handleAcceptMoney();
}, onInvalidSubmit);
////////////////////////////////
const handleAcceptMoney = () => {
  const numberMoney = parseInt(money.value.replace(",", ""));
  const newMoney = {
    id: 0,
    rate: numberMoney,
  };
  emit("money", newMoney);
  setTimeout(() => {
    money.value = "";
  }, 300);
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
  setTimeout(() => {
    money.value = "";
  }, 300);
};
</script>
<template>
  <v-dialog
    :update:modelValue="handleCloseModal"
    :modelValue="props.formModal"
    persistent
    width="500"
  >
    <div class="flex w-full flex-col justify-center bg-white rounded-md">
      <div class="ModalHeader">
        <i
          class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
          @click="handleCloseModal"
        ></i>
        <p class="font-bold">قیمت</p>
      </div>
      <div class="ModalMain">
        <div class="flex w-full justify-evenly items-center">
          <input
            placeholder="قیمت مورد نظر را وارد کنید"
            class="food-input"
            v-model="money"
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
