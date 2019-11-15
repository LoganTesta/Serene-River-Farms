
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateChangePasswordForm() {
    if (clickedSubmit) {
        let userCurrentPassword = $("#userCurrentPassword").val().trim();
        let userNewPassword = $("#userNewPassword").val().trim();
        let userConfirmPassword = $("#userConfirmPassword").val().trim();
        let validForm = true;


        let validPassword = true;
        if (userCurrentPassword === null || userCurrentPassword === "") {
            validPassword = false;
        }

        if (validPassword) {
            $("#userCurrentPassword").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userCurrentPassword").addClass("required-field-needed");
        }     


        let validNewPassword = true;
        if (userNewPassword === null || userNewPassword === "") {
            validNewPassword = false;
        }

        /* Check new password length. */
        if (userNewPassword.length < 7 || userNewPassword.length > 100) {
            validNewPassword = false;
        }


        let validConfirmPassword = true;
        if (userConfirmPassword === null || userConfirmPassword === "") {
            validConfirmPassword = false;
        }

        /* Check confirm password length. */
        if (userConfirmPassword.length < 7 || userConfirmPassword.length > 100) {
            validConfirmPassword = false;
        }


        /* Check if the two passwords match. */
        if (userNewPassword !== userConfirmPassword) {
            validNewPassword = false;
            validConfirmPassword = false;
        }


        if (validNewPassword) {
            $("#userNewPassword").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userNewPassword").addClass("required-field-needed");
        }

        if (validConfirmPassword) {
            $("#userConfirmPassword").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userConfirmPassword").addClass("required-field-needed");
        }


        if (validForm === false) {
            $(".javascript-validation-results-contact-us").eq(0).addClass("show");
            $(".javascript-validation-results-contact-us").eq(0).html("Please fill all required fields in the correct format.");
            return false;
        } else if (validForm) {
            $("javascript-validation-results-contact-us").eq(0).removeClass("show");
            $("javascript-validation-results-contact-us").eq(0).html("");
            return true;
        }
    } else {
        return false;
    }
}


function setClickedChangePasswordButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (changePasswordButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#changePasswordButton").on("click", function () {
    setClickedChangePasswordButtonTrue();
});

$("#changePasswordButton").on("click", function () {
    validateChangePasswordForm()
});

$("#userCurrentPassword").on("change", function () {
    validateChangePasswordForm();
});

$("#userNewPassword").on("change", function () {
    validateChangePasswordForm();
});

$("#userConfirmPassword").on("change", function () {
    validateChangePasswordForm();
});
