
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateForgotPasswordForm() {
    if (clickedSubmit) {
        let userEmail = $("#userEmail").val().trim();
        let validForm = true;


        /* If the @ position is at the start (or less) position of value 0, validForm = false. */
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


function setClickedForgotPasswordButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (forgotPasswordButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#forgotPasswordButton").on("click", function () {
    setClickedForgotPasswordButtonTrue();
});

$("#userEmail").on("change", function () {
    validateForgotPasswordForm();
});
