
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateRegisterForm() {
    if (clickedSubmit) {
        let userName = $("#userName").val().trim();
        let userEmail = $("#userEmail").val().trim();
        let userPassword = $("#userPassword").val().trim();
        let userConfirmPassword = $("#userConfirmPassword").val().trim();
        let validForm = true;


        let validName = true;
        if (userName === null || userName === "") {
            validName = false;
        }

        /* Check password length. */
        if (userName.length < 3 || userName.length > 40) {
            validName = false;
        }

        if (validName) {
            $("#userName").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userName").addClass("required-field-needed");
        }


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


        let validPassword = true;
        if (userPassword === null || userPassword === "") {
            validPassword = false;
        }

        /* Check password length. */
        if (userPassword.length < 7 || userPassword.length > 100) {
            validPassword = false;
        }


        let validConfirmPassword = true;
        if (userConfirmPassword === null || userConfirmPassword === "") {
            validConfirmPassword = false;
        }

        /* Check second password length. */
        if (userConfirmPassword.length < 7 || userConfirmPassword.length > 100) {
            validConfirmPassword = false;
        }


        /* Check if the two passwords match. */
        if (userPassword !== userConfirmPassword) {
            validPassword = false;
            validConfirmPassword = false;
        }


        if (validPassword) {
            $("#userPassword").removeClass("required-field-needed");
        } else {
            validForm = false;
            $("#userPassword").addClass("required-field-needed");
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


function setClickedRegisterButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (registerButton === elementWithFocus) {
        clickedSubmit = true;
    }
}

$("#registerButton").on("click", function () {
    setClickedRegisterButtonTrue();
});

$("#registerButton").on("click", function () {
    validateRegisterForm()
});

$("#userName").on("change", function () {
    validateRegisterForm();
});

$("#userEmail").on("change", function () {
    validateRegisterForm();
});

$("#userPassword").on("change", function () {
    validateRegisterForm();
});

$("#userConfirmPassword").on("change", function () {
    validateRegisterForm();
});
