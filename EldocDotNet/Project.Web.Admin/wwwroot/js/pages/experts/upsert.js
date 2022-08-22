"use strict";
var imageBolb;
var descriptionEditor;
var KTDocsAdd = function () {
    const baseUrl = '/experts';
    var tags;
    function UploadAdapterPlugin(editor) {
        editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
            return new UploadAdapter(loader);
        };
    }

    return {
        init: function () {
            const pmetter = KTPasswordMeter.getInstance(document.querySelector('[data-kt-password-meter="true"]')),
                validatePassword = function () {
                    return pmetter.getScore() >= 80;
                };
            ClassicEditor
                .create(document.querySelector('#Description'), {
                    extraPlugins: [UploadAdapterPlugin],
                    language: 'fa',
                })
                .then(editor => {
                    descriptionEditor = editor;
                    descriptionEditor.model.document.on('change:data', (e) => {
                        form_validation.revalidateField('Description')
                    });
                })
                .catch(error => { console.error(error) });

            const form = document.getElementById("form");
            var form_validation = FormValidation.formValidation(form, {
                fields: {
                    Name: {
                        validators: {
                            notEmpty: {
                                message: "نام کارشناس را وارد نکرده اید"
                            }
                        }
                    },
                    Specialty: {
                        validators: {
                            notEmpty: {
                                message: "تخصص کارشناس را وارد نکرده اید"
                            }
                        }
                    },
                    Description: {
                        validators: {
                            callback: {
                                message: 'توضیحات را وارد نکرده اید',
                                callback: function (input) {
                                    return descriptionEditor.getData() != ''
                                }
                            },
                        }
                    },
                    SessionFee: {
                        validators: {
                            notEmpty: {
                                message: "هزینه جلسه را وارد نکرده اید"
                            }
                        }
                    },
                    MaxActiveSessions: {
                        validators: {
                            notEmpty: {
                                message: "حداکثر جلسات فعال را وارد نکرده اید"
                            },
                            stringLength: {
                                min: 1,
                                max: 30,
                                message: "حداکثر جلسات فعال نمی‌تواند کمتر از 1 باشد"
                            },
                        }
                    },
                    Username: {
                        validators: {
                            notEmpty: {
                                message: "نام کاربری را وارد نکرده اید"
                            }
                        }
                    },
                    Password: {
                        validators: {
                            callback: {
                                message: 'کلمه عبور را وارد نکرده اید',
                                callback: function (input) {
                                    let inputId = e.querySelector('[name="id"]');

                                    return inputId.value === ''
                                }
                            },
                            callback2: {
                                message: "کلمه عبور ایمن نیست",
                                callback2: function (input) {
                                    if (input.value.length > 0) {
                                        return validatePassword();
                                    }
                                }
                            }
                        }
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".form-group",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            });
            const submitButton = document.querySelector('[data-kt-docs-action="submit"]');
            submitButton.addEventListener("click", e => {
                e.preventDefault();
                var fd;
                form_validation.validate().then((function (t) {
                    ("Valid" == t) ?
                        (
                            fd = new FormData(form),
                            fd.set("Description", descriptionEditor.getData()),
                            (imageBolb !== undefined) ? fd.append('Image', imageBolb, imageBolb.type.replace('/', '.')) : '',
                            submitButton.setAttribute("data-kt-indicator", "on"),
                            submitButton.disabled = !0,
                            fetch(`${baseUrl}/upsert`, {
                                method: 'POST',
                                body: fd,
                            })
                                .then(response => {
                                    submitButton.removeAttribute("data-kt-indicator");
                                    submitButton.disabled = !1;
                                    return response.json()
                                })
                                .then(result => {
                                    if (result.status == 1) {
                                        toastr.success(result.message);

                                    } else {
                                        toastr.error(result.message);
                                        result.errors.forEach((item) => {
                                            toastr.warning(item);
                                        });
                                    }
                                })
                                .catch(error => { console.log(error) })
                        )
                        : (toastr.warning("لطفا اطلاعات فرم را تکمیل کنید"), console.log(t))
                }))

            });
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTDocsAdd.init();

    CropperInstance.init('cropper-image', 600, 900, (blob) => {
        imageBolb = blob;
    });
}));