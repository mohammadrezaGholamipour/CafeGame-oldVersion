<template>
  <div
    class="modal fade ParentModal"
    data-bs-backdrop="static"
    id="tableForm"
  >
    <div class="modal-dialog ModalDivOne">
      <div class="ModalDivTwo">
        <div class="ModalHeader">
          <p class="font-bold">افزون خوراکی</p>
          <i
            class="fa-solid fa-circle-xmark text-red-600 cursor-pointer text-2xl"
            data-bs-dismiss="modal"
          ></i>
        </div>
        <div class="ModalMain">main</div>
        <div class="ModalFooter">
          <button class="BtnAccept">
            <p>ثبت</p>
            <i class="fa-duotone fa-badge-check mr-2"></i>
          </button>
          <button class="BtnRemove" data-bs-dismiss="modal">
            <p>بازگشت</p>
            <i class="fa-duotone fa-chevrons-left mr-2"></i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
