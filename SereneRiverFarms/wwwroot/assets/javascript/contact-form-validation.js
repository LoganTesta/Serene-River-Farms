/* JavaScript Contact Form Validation. */
let clickedSubmitContact = false;


function validateContactForm() {
    if (clickedSubmitContact) {
        let userName = $("#userName").val().trim();
        let userEmail = $("#userEmail").val().trim();
        let userSubject = $("#userSubject").val().trim();
        let userComments = $("#userComments").val().trim();
        let validContactForm = true;


        let validName = true;
        if (userName === null || userName === "") {
            validName = false;
        }

        if (validName) {
            $("#userName").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userName").addClass("required-field-needed");
        }


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


        let validComments = true;
        if (userComments === null || userComments === "") {
            validComments = false;
        }

        if (validComments) {
            $("#userComments").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userComments").addClass("required-field-needed");
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


function setClickedContactButtonTrue() {
    let elementWithFocus = $(':focus')[0];
    if (contactButton === elementWithFocus) {
        clickedSubmitContact = true;
    }
}

$("#contactButton").on("click", function (){
    setClickedContactButtonTrue();
});

$("#contactButton").on("click", function () {
    validateContactForm()
});

$("#userName").on("change", function () {
    validateContactForm();
});

$("#userSubject").on("change", function () {
    validateContactForm();
});

$("#userEmail").on("change", function () {
    validateContactForm();
});

$("#userComments").on("change", function () {
    validateContactForm();
});
