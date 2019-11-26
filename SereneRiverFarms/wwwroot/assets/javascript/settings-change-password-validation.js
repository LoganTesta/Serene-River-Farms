
/* JavaScript Form Validation. */
let clickedSubmitChangeEmail = false;
let clickedSubmitChangePassword = false;

function validateChangeEmailForm() {
    if (clickedSubmitChangeEmail) {
        let userNewEmail = $("#userNewEmail").val().trim();
        let validForm = true;


        /*If the @ position is at the start (or less) position of value 0,  validContactForm = false. */
        /* There must be at least 1 character after the @ position and the last dot position. */
        /* There must be at least two characters after the last "." symbol.  */
        let validNewEmail = true;
        let atPosition = userNewEmail.indexOf("@");
        let dotPosition = userNewEmail.lastIndexOf(".");
        let lastEmailCharacter = userNewEmail.length - 1;

        if (userNewEmail === null || userNewEmail === "") {
            validNewEmail = false;
        } else if (atPosition <= 0) {
            validNewEmail = false;
        } else if (atPosition + 1 >= dotPosition) {
            validNewEmail = false;
        } else if (dotPosition + 1 >= lastEmailCharacter) {
            validNewEmail = false;
        }

        if (validNewEmail) {
            $("#userNewEmail").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userNewEmail").addClass("required-field-needed");
        }


        if (validForm === false) {
            $(".javascript-validation-results-contact-us.zero").eq(0).addClass("show");
            $(".javascript-validation-results-contact-us.zero").eq(0).html("Please fill all required fields in the correct format.");
            return false;
        } else if (validForm) {
            $("javascript-validation-results-contact-us.zero").eq(0).removeClass("show");
            $("javascript-validation-results-contact-us.zero").eq(0).html("");
            return true;
        }
    } else {
        return false;
    }
}


function validateChangePasswordForm() {
    if (clickedSubmitChangePassword) {
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
            $(".javascript-validation-results-contact-us.one").eq(0).addClass("show");
            $(".javascript-validation-results-contact-us.one").eq(0).html("Please fill all required fields in the correct format.");
            return false;
        } else if (validForm) {
            $("javascript-validation-results-contact-us.one").eq(0).removeClass("show");
            $("javascript-validation-results-contact-us.one").eq(0).html("");
            return true;
        }
    } else {
        return false;
    }
}


/*Change email related functions/event handlers.*/
function setClickedChangeEmailButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (changeEmailButton === elementWithFocus) {
        clickedSubmitChangeEmail = true;
    }
}

$("#changeEmailButton").on("click", function () {
    setClickedChangeEmailButtonTrue();
});

$("#changeEmailButton").on("click", function () {
    validateChangeEmailForm()
});

$("#userNewEmail").on("change", function () {
    validateChangeEmailForm();
});


/*Change password related functions/event handlers.*/
function setClickedChangePasswordButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (changePasswordButton === elementWithFocus) {
        clickedSubmitChangePassword = true;
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
