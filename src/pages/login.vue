<script setup>
import { useToast } from "vue-toastification";
import { reactive } from "vue";
////////////////////////////
const toast = useToast();
const state = reactive({
  loginInput: [
    {
      placeholder: "نام کاربری",
      name: "userName",
      validate: false,
      type: "text",
      message: "",
      value: "",
    },
    {
      placeholder: "رمز عبور",
      name: "passWord",
      type: "password",
      validate: false,
      message: "",
      value: "",
    },
  ],
});
//////////////////////////////////////
const handleValidateInput = (name) => {
  const input = state.loginInput.filter((items) => items.name === name);
  if (input[0].value.length > 0) {
    if (input[0].name === "userName") {
      if (isNaN(input[0].value)) {
        if (input[0].value.length > 5) {
          input[0].validate = true;
          input[0].message = "";
        } else {
          input[0].validate = false;
          input[0].message = "باید بیشتر از 5 رقم باشد";
        }
      } else {
        input[0].validate = false;
        input[0].message = "نام کاربری نامعتبر میباشد";
      }
    } else {
      if (input[0].value.length > 5) {
        input[0].validate = true;
        input[0].message = "";
      } else {
        input[0].validate = false;
        input[0].message = "باید بیشتر از 5 رقم باشد";
      }
    }
  } else {
    input[0].message = "";
  }
};
///////////////////////////
const handleLogin = () => {
  const inputValue = state.loginInput.every((items) => items.value);
  if (inputValue) {
    const validate = state.loginInput.every((items) => items.validate);
    if (validate) {
      toast.success("لاگین");
    } else {
      toast.error("اطلاعات صحیح نمیباشد");
    }
  } else {
    toast.error("لطفا اطلاعات خود را وارد کنید");
  }
};
</script>
<template>
  <div class="ParentLogin">
    <div class="login">
      <img class="Logo" src="../assets/image/logo.png" alt="لوگو" />
      <template v-for="items in state.loginInput">
        <input
          @blur.trim="handleValidateInput(items.name)"
          :placeholder="items.placeholder"
          v-model="items.value"
          class="LoginInput"
          :type="items.type"
          maxlength="15"
        />
        <transition-expand>
          <p class="ErrorMessage" v-if="items.message">
            {{ items.message }}
          </p>
        </transition-expand>
      </template>
      <button @click="handleLogin" class="loginBtn">وارد شدن</button>
    </div>
  </div>
</template>
