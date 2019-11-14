
/* JavaScript Contact Form Validation. */
let clickedSubmit = false;


function validateLoginForm() {
    if (clickedSubmit) {
        let userEmail = $("#userEmail").val().trim();
        let userPassword = $("#userPassword").val().trim();
        let validContactForm = true;


        /*If the @ position is at the start (or less) position of value 0,  validContactForm=false. */
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
            validContactForm = false;
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

        if (validPassword) {
            $("#userPassword").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userPassword").addClass("required-field-needed");
        }     


        if (validContactForm === false) {
            $(".javascript-validation-results-contact-us").eq(0).addClass("show");
            $(".javascript-validation-results-contact-us").eq(0).html("Please fill all required fields in the correct format.");
            return false;
        } else if (validContactForm) {
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

$("#userEmail").on("change", function () {
    validateLoginForm();
});

$("#userPassword").on("change", function () {
    validateLoginForm();
});
