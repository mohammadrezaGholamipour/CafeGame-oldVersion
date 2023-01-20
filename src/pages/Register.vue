<script setup>
import { Field, useForm, ErrorMessage } from "vee-validate";
import { reactive } from "vue";
import * as yup from "yup";
////////////////////////////
const state = reactive({
  registerInput: [
    {
      placeholder: "نام",
      name: "name",
      type: "text",
      rules: yup
        .string()
        .required("نام خود را وارد کنید")
        .min(3, "کوتاه میباشد"),
      value: "",
    },
    {
      placeholder: "نام خانوادگی",
      name: "lastName",
      type: "text",
      rules: yup
        .string()
        .required("نام خانوادگی خود را وارد کنید")
        .min(3, "کوتاه میباشد"),
      value: "",
    },
    {
      placeholder: "نام کاربری",
      name: "userName",
      type: "text",
      rules: yup
        .string()
        .required("نام کاربری خود را وارد کنید")
        .min(8, "حداقل 8 کاراکتر"),
      value: "",
    },
    {
      placeholder: "شماره تلفن همراه",
      name: "mobile",
      type: "text",
      rules: yup
        .string()
        .required("تلفن همراه خود را وارد کنید")
        .min(11, "شماره تلفن صحیح نمیباشد")
        .max(11, "شماره تلفن صحیح نمیباشد"),
      value: "",
    },
    {
      placeholder: "رمز عبور",
      name: "password",
      type: "password",
      rules: yup
        .string()
        .required("رمز خود را وارد کنید")
        .min(4, "حداقل چهار کاراکتر باید باشد"),
      value: "",
    },
    {
      placeholder: "تکرار رمز عبور",
      name: "repeatPassword",
      type: "password",
      rules: yup
        .string()
        .required("رمز خود را تکرار کنید")
        .min(4, "حداقل چهار کاراکتر باید باشد"),
      value: "",
    },
  ],
});
const { handleSubmit, setFieldError } = useForm({
  validationSchema: state.schema,
});
const handelRegisterFrom = handleSubmit((value) => {
  if (value.password === value.repeatPassword) {
    console.log("halle");
  } else {
    setFieldError("repeatPassword", "رمز تکرار شده اشتباه است");
  }
});
</script>
<template>
  <div class="ParentRegister">
    <div class="Register">
      <img class="Logo" src="../assets/image/logo.png" alt="لوگو" />
      <form @submit.pervent="handelRegisterFrom" class="FormInputRegister">
        <template v-for="items in state.registerInput" :key="items.name">
          <div class="flex flex-col items-center justify-center">
            <Field
              :placeholder="items.placeholder"
              :validateOnBlur="false"
              class="RegisterInput"
              :rules="items.rules"
              :name="items.name"
              :type="items.type"
            />
            <transition-expand>
              <ErrorMessage class="ErrorMessage" :name="items.name" />
            </transition-expand>
          </div>
        </template>
        <!-- //////////////////////// -->
        <div class="w-full flex justify-center items-center">
          <button class="RegisterBtn">ثبت نام</button>
        </div>
      </form>
    </div>
  </div>
</template>
