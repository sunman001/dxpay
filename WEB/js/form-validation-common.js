var validate;

jQuery.validator.setDefaults({
    debug: true,
    invalidHandler: function (event, validator) {
        // 'this' refers to the form
        //var errors = validator.numberOfInvalids();
        //if (errors) {
        //    var message = "有" + errors + "个表单项未通过验证!";
        //    $(".form-error-summary").remove();
        //    $(validator.currentForm).find('button[type=submit]').attr('disabled', 'disabled');
        //    $(validator.currentForm).prepend(
        //        '<div class="form-error-summary error">' + message + "</div>");
        //} else {
        //    $("div.error").hide();
        //    $(validator.currentForm).find('button[type=submit]').attr('disabled', true);
        //}
    },
    showErrors: function (errorMap, errorList) {
        var validator = this;
        // 'this' refers to the form
        var errors = errorList.length;
        if (errors > 0) {
            var message = "有表单项验证未通过,请完善";
            $(".form-error-summary").remove();
            $(validator.currentForm).prepend('<div class="form-error-summary error">' + message + "</div>");
        } else {
            $("div.error").hide();
        }
        this.defaultShowErrors();
    }
});

var dxValidator = {
    validator: null,
    errorPlacement: "right", //right,bottom
    currentForm: "",
    valid: function () {
        var valid = dxValidator.validator.errorList.length <= 0;
        return valid;
    },
    validateForm: function (options) {
        var target = options;
        this.validator = $(this.currentForm).validate({
            rules: target.rules,
            messages: target.messages,
            success: function (label) {
                //label.addClass("valid");
                label.remove();
            },
            submitHandler: function () {
                $(".form-error-summary").remove();
                if (target.submitHandler) {
                    target.submitHandler();
                }
            },

            errorPlacement: function (error, element) {
                if (dxValidator.errorPlacement === "bottom") {
                    //error.insertAfter(element.parent(".form-item"));
                    error.appendTo(element.parent(".form-item"));
                }
                else if (dxValidator.errorPlacement === "right") {
                    error.appendTo(element.parent(".form-item"));
                }
            }
        });
    }
};