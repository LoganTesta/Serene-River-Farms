
/* JavaScript Form Validation. */
let clickedSubmit = false;


function validateLoginForm() {
    if (clickedSubmit) {
        let userName = $("#userName").val().trim();
        let userPassword = $("#userPassword").val().trim();
        let validForm = true;

        let validName = true;


        if (userName === null || userName === "") {
            validName = false;
        } 

        if (validName) {
            $("#userName").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userName").addClass("required-field-needed");
        }


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


function setClickedLoginButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (loginButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#loginButton").on("click", function () {
    setClickedLoginButtonTrue();
});

$("#loginButton").on("click", function () {
    validateLoginForm()
});

$("#userName").on("change", function () {
    validateLoginForm();
});

$("#userPassword").on("change", function () {
    validateLoginForm();
});
