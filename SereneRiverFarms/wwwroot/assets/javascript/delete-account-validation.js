
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateDeleteAccountForm() {
    if (clickedSubmit) {
        let userPassword = $("#userPassword").val().trim();
        let validForm = true;
        

        let validPassword = true;
        if (userPassword === null || userPassword === "") {
            validPassword = false;
        }

        /* Check password length. */
        if (userPassword.length < 7) {
            validPassword = false;
        }

        if (validPassword) {
            $("#userPassword").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userPassword").addClass("required-field-needed");
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


function setClickedDeleteAccountButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (deleteAccountButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#deleteAccountButton").on("click", function () {
    setClickedDeleteAccountButtonTrue();
});

$("#deleteAccountButton").on("click", function () {
    validateDeleteAccountForm()
});

$("#userPassword").on("change", function () {
    validateDeleteAccountForm();
});
