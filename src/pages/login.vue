<script setup>
import { useForm, ErrorMessage, useField } from "vee-validate";
import { useToast } from "vue-toastification";
import AuthService from "../util/AuthService";
import accountApi from '@/api/account/login'
import { useStore } from '@/store/index'
import { useRouter } from "vue-router";
import { reactive } from "vue";
import * as yup from "yup";
///////////////////////////
const router = useRouter()
const toast = useToast();
const store = useStore()
///////////////////////////
const state = reactive({
  timer: false,
  schema: yup.object({
    email: yup
      .string()
      .required("ایمیل خود را وارد کنید")
      .email("ایمیل معتبر نمیباشد"),
    password: yup
      .string()
      .required("رمز خود را وارد کنید")
      .min(4, "حداقل چهار کاراکتر باید باشد"),
  }),
});

// ////////////////////////////////
const { handleSubmit } = useForm({ validationSchema: state.schema });
///////////////////////////////
const { value: email } = useField("email");
const { value: password } = useField("password");
///////////////////////////////
function onInvalidSubmit({ errors }) {
  const error = Object.values(errors)
  toast.error(error[0])
}
//////////////////////////////
const onSubmit = () => {
  clearTimeout(state.timer)
  state.timer = setTimeout(
    handleSubmit((values) => {
      handleAcceptLogin(values);
    }, onInvalidSubmit), 200);
}
///////////////////////////////
const handleAcceptLogin = (values) => {
  const userlogin = {
    email: values.email,
    password: values.password,
  }
  accountApi.login(userlogin)
    .then((response) => {
      AuthService.setTokenUser(response.token)
      AuthService.setState(response.userState)
      store.setUserInfo(response)
      router.push('/')
    })
    .catch((error) => { console.log(error) })

}
</script>
<template>
  <div class="parent-login">
    <div class="login">
      <img class="Logo" src="../assets/image/logo.png" alt="لوگو" />
      <!-- //////////////////////// -->
      <div class="flex flex-col items-center justify-center">
        <input v-model="email" placeholder="ایمیل" class="LoginInput" type="text" />
        <transition-expand>
          <ErrorMessage v-if="email" class="ErrorMessage" name="email" />
        </transition-expand>
      </div>
      <!-- //////////////////////// -->
      <div class="flex flex-col items-center justify-center">
        <input v-model="password" placeholder="رمزعبور" class="LoginInput" type="password" />
        <transition-expand>
          <ErrorMessage v-if="password" class="ErrorMessage" name="password" />
        </transition-expand>
      </div>
      <!-- //////////////////////// -->
      <button @click="onSubmit" class="loginBtn">وارد شدن</button>
        <p class="mt-3 text-white cursor-pointer" @click="router.push('/register')">ثبت نام نکرده اید؟</p>
    </div>
  </div>
</template>
