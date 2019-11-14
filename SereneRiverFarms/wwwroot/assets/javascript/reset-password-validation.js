
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateResetPasswordForm() {
    if (clickedSubmit) {
        let userEmail = $("#userEmail").val().trim();
        let userPassword = $("#userNewPassword").val().trim();
        let userConfirmPassword = $("#userConfirmPassword").val().trim();
        let validForm = true;


        /*If the @ position is at the start (or less) position of value 0, validForm = false. */
        /* There must be at least 1 character after the @ position and the last dot position. */
        /* There must be at least two characters after the last "." symbol.  */
        let validEmail = true;
        let atPosition = userEmail.indexOf("@");
        let dotPosition = userEmail.lastIndexOf(".");
        let lastEmailCharacter = userEmail.length - 1;

        if (userEmail === null || userEmail === "") {
            validEmail = false;
        } else if (atPosition <= 0) {
            validEmail = false;
        } else if (atPosition + 1 >= dotPosition) {
            validEmail = false;
        } else if (dotPosition + 1 >= lastEmailCharacter) {
            validEmail = false;
        }

        if (validEmail) {
            $("#userEmail").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userEmail").addClass("required-field-needed");
        }


        let validPassword = true;
        if (userPassword === null || userPassword === "") {
            validPassword = false;
        }

        /* Check password length. */
        if (userPassword.length < 7) {
            validPassword = false;
        }


        let validConfirmPassword = true;
        if (userConfirmPassword === null || userConfirmPassword === "") {
            validConfirmPassword = false;
        }

        /* Check second password length. */
        if (userConfirmPassword.length < 7) {
            validConfirmPassword = false;
        }


        /* Check if the two passwords match. */
        if (userPassword !== userConfirmPassword) {
            validPassword = false;
            validConfirmPassword = false;
        }


        if (validPassword) {
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


function setClickedResetPasswordButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (resetPasswordButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#resetPasswordButton").on("click", function () {
    setClickedResetPasswordButtonTrue();
});

$("#resetPasswordButton").on("click", function () {
    validateResetPasswordForm()
});

$("#userEmail").on("change", function () {
    validateResetPasswordForm();
});

$("#userNewPassword").on("change", function () {
    validateResetPasswordForm();
});

$("#userConfirmPassword").on("change", function () {
    validateResetPasswordForm();
});
