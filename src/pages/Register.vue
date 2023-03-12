<script setup>
import { useForm, ErrorMessage, useField } from "vee-validate";
import accountApi from '@/api/account/register'
import { useToast } from "vue-toastification";
import { reactive } from "vue";
import * as yup from "yup";
////////////////////////////
const toast = useToast();
////////////////////////////
const state = reactive({
  schema: yup.object({
    userName: yup
      .string()
      .required("نام و نام خانوادگی را وارد کنید")
      .min(3, "کوتاه میباشد"),
    email: yup
      .string()
      .required("ایمیل خود را وارد کنید")
      .email("ایمیل معتبر نمیباشد"),
    password: yup
      .string()
      .required("رمز خود را وارد کنید")
      .min(4, "حداقل چهار کاراکتر باید باشد"),
    repeatPassword: yup
      .string()
      .required("رمز خود را تکرار کنید")
      .oneOf([yup.ref('password'), null], "پسورد تکرار شده اشتباه است"),
    mobile: yup
      .string()
      .required("تلفن همراه خود را وارد کنید")
      .matches(
        /^(((\+98|0098)-?)|0)9[0-9]{2}-?[0-9]{3}-?[0-9]{4}$/,
        "شماره تلفن صحیح نمیباشد"
      ),
  }),
});

// ////////////////////////////////
const { handleSubmit } = useForm({ validationSchema: state.schema });
///////////////////////////////
const { value: repeatPassword } = useField("repeatPassword");
const { value: userName } = useField("userName");
const { value: password } = useField("password");
const { value: mobile } = useField("mobile");
const { value: email } = useField("email");
///////////////////////////////
function onInvalidSubmit({ errors }) {
  console.log(errors);
  const error = Object.values(errors)
  toast.error(error[0])
}
//////////////////////////////
const onSubmit = handleSubmit((values) => {
  handleAcceptRegister(values);
}, onInvalidSubmit);
///////////////////////////////
const handleAcceptRegister = (values) => {
  console.log(values);
  const userInfo = {
    userName: values.userName.replace(" ", '_'),
    password: values.password,
    email: values.email,
    phoneNumber: values.mobile
  }
  accountApi.new(userInfo)
    .then((response) => { console.log(response) })
    .catch((error) => { console.log(error) })
}
</script>
<template>
  <div class="ParentRegister">
    <div class="Register">
      <img class="Logo" src="../assets/image/logo.png" alt="لوگو" />
      <form @submit.pervent="onSubmit" class="FormInputRegister">
        <!-- //////////////////////// -->
        <div class="flex flex-col items-center justify-center">
          <input v-model="userName" placeholder="نام و نام خانوداگی" class="RegisterInput" type="text" />
          <transition-expand>
            <ErrorMessage v-if="userName" class="ErrorMessage" name="userName" />
          </transition-expand>
        </div>
        <!-- //////////////////////// -->
        <div class="flex flex-col items-center justify-center">
          <input v-model="email" placeholder="ایمیل" class="RegisterInput" type="text" />
          <transition-expand>
            <ErrorMessage v-if="email" class="ErrorMessage" name="email" />
          </transition-expand>
        </div>
        <!-- //////////////////////// -->
        <div class="flex flex-col items-center justify-center">
          <input v-model="password" placeholder="رمز عبور" class="RegisterInput" type="password" />
          <transition-expand>
            <ErrorMessage v-if="password" class="ErrorMessage" name="password" />
          </transition-expand>
        </div>
        <!-- //////////////////////// -->
        <div class="flex flex-col items-center justify-center">
          <input v-model="repeatPassword" placeholder="تکرار رمز عبور" class="RegisterInput" type="password" />
          <transition-expand>
            <ErrorMessage v-if="repeatPassword" class="ErrorMessage" name="repeatPassword" />
          </transition-expand>
        </div>
        <!-- //////////////////////// -->
        <div class="flex flex-col items-center justify-center">
          <input v-model="mobile" placeholder="تلفن همراه" class="RegisterInput" type="text" />
          <transition-expand>
            <ErrorMessage v-if="mobile" class="ErrorMessage" name="mobile" />
          </transition-expand>
        </div>
        <!-- //////////////////////// -->
        <div class="w-full flex justify-center items-center">
          <button class="RegisterBtn">ثبت نام</button>
        </div>
      </form>
    </div>
  </div>
</template>
