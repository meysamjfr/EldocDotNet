"use strict";
var KTUsersAddUser = function () {
    const t = document.getElementById("kt_modal_add_user"),
        e = t.querySelector("#kt_modal_add_user_form"),
        boomodal = new bootstrap.Modal(t);
    return {
        init: function () {
            (() => {
                var o = FormValidation.formValidation(e, {
                    fields: {
                        user_name: {
                            validators: {
                                notEmpty: {
                                    message: "Full name is required"
                                }
                            }
                        },
                        user_email: {
                            validators: {
                                notEmpty: {
                                    message: "Valid email address is required"
                                }
                            }
                        }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row",
                            eleInvalidClass: "",
                            eleValidClass: ""
                        })
                    }
                });
                const i = t.querySelector('[data-kt-users-modal-action="submit"]');
                i.addEventListener("click", (t => {
                    t.preventDefault(), o && o.validate().then((function (t) {
                        console.log("validated!"), "Valid" == t ? (i.setAttribute("data-kt-indicator", "on"), i.disabled = !0, setTimeout((function () {
                            i.removeAttribute("data-kt-indicator"), i.disabled = !1, Swal.fire({
                                text: "فرم با موفقیت ارسال شد!",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "باشه فهمیدم!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then((function (t) {
                                t.isConfirmed && boomodal.hide()
                            }))
                        }), 2e3)) : Swal.fire({
                            text: "متأسفیم ، به نظر می رسد برخی خطاها شناسایی شده است ، لطفاً دوباره امتحان کنید.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "باشه فهمیدم!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    }))
                })), t.querySelector('[data-kt-users-modal-action="cancel"]').addEventListener("click", (t => {
                    t.preventDefault(), Swal.fire({
                        text: "آیا مطمئن هستید که می خواهید لغو کنید؟?",
                        icon: "warning",
                        showCancelButton: !0,
                        buttonsStyling: !1,
                        confirmButtonText: "بله ، آن را لغو کنید!",
                        cancelButtonText: "خیر",
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: "btn btn-active-light"
                        }
                    }).then((function (t) {
                        t.value ? (e.reset(), boomodal.hide()) : "cancel" === t.dismiss && Swal.fire({
                            text: "فرم شما لغو نشده است !.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "باشه فهمیدم!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    }))
                })), t.querySelector('[data-kt-users-modal-action="close"]').addEventListener("click", (t => {
                    t.preventDefault(), Swal.fire({
                        text: "آیا مطمئن هستید که می خواهید لغو کنید؟?",
                        icon: "warning",
                        showCancelButton: !0,
                        buttonsStyling: !1,
                        confirmButtonText: "بله ، آن را لغو کنید!",
                        cancelButtonText: "خیر",
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: "btn btn-active-light"
                        }
                    }).then((function (t) {
                        t.value ? (e.reset(), boomodal.hide()) : "cancel" === t.dismiss && Swal.fire({
                            text: "فرم شما لغو نشده است !.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "باشه فهمیدم!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    }))
                }))
            })()
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTUsersAddUser.init()
}));