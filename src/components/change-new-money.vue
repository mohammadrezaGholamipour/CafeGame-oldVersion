<script setup>
import { filterNumbersWithSep } from "../util/filter-numbers";
import { useForm, useField } from "vee-validate";
import { useToast } from "vue-toastification";
import { cafePinia } from "../store/pinia";
import { reactive, watch } from "vue";
import * as yup from "yup";
/////////////////////////
const emit = defineEmits(["money"]);
/////////////////////////
const store = cafePinia();
const toast = useToast();
///////////////////////
const state = reactive({
  schema: yup.object({
    money: yup
      .string()
      .min(4, "قیمت  درست نمیباشد")
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
  const closeModal = document.getElementsByClassName("close");
  const numberMoney = parseInt(money.value.replace(",", ""));
  let newMoney = {
    rate: numberMoney,
  };
  ///////////////////////
  emit("money", newMoney);
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
    <div class="modal-dialog ModalDivOne">
      <div class="ModalDivTwo">
        <div class="ModalHeader">
          <p class="font-bold">افزون قیمت</p>
          <i
            class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl close"
            data-bs-dismiss="modal"
          ></i>
        </div>
        <div class="ModalMain">
          <div class="flex w-full justify-center items-center">
            <input
              placeholder="قیمت"
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
