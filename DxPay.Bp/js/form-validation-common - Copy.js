var validate;

jQuery.validator.setDefaults({
    debug: true,
    invalidHandler: function (event, validator) {
        // 'this' refers to the form
        var errors = validator.numberOfInvalids();
        if (errors) {
            var message = "当前表单有" + errors + "个项验证未通过,请处理.";
            $(".form-error-summary").remove();
            $(validator.currentForm).prepend(
                '<div class="form-error-summary error">' + message + "</div>");
        } else {
            $("div.error").hide();
        }
    }
});

var dxValidator = {
    validator: null,
    errorPlacement: "right", //right,bottom
    currentForm: "",
    valid: function () {
        var valid =  dxValidator.validator.errorList.length <= 0;
        return valid;
    },
    validateForm: function (options) {
        var target = options;
        this.validator = $(this.currentForm).validate({
            rules: target.rules,
            messages: target.messages,
            success: function (label) {
                label.text("验证通过").addClass("valid");
            },
            submitHandler: function () {
                target.submitHandler();
            },

            errorPlacement: function (error, element) {
                if (dxValidator.errorPlacement === "bottom") {
                    error.insertAfter(element.parent("div.form-item"));
                }
                else if (dxValidator.errorPlacement === "right") {
                    error.appendTo(element.parent("div.form-item"));
                }
            }
        });
    }
};