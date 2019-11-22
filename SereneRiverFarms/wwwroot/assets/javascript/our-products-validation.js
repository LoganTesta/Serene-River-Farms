
/* JavaScript Form Validation. */
let clickedSubmitOrderEstimate = false;


function validateEstimateForm() {
    if (clickedSubmitOrderEstimate) {
        let userName = $("#userName").val().trim();
        let userEmail = $("#userEmail").val().trim();
        let userPhone = $("#userPhone").val().trim();
        let userAddress = $("#userAddress").val().trim();
        let userCity = $("#userCity").val().trim();
        let userState = $("#userState").val().trim();
        let userZipCode = $("#userZipCode").val().trim();
        let userComments = $("#additionalNotes").val().trim();
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


        /*If the @ position is at the start (or less) position of value 0,  validContactForm = false. */
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


        //Check the phone's length and if it is an integer.
        let validPhone = true;
        if (userPhone === null || userPhone === "") {
            validPhone = false;
        }

        let userPhoneCheck = /^\d{10}$/;
        if (userPhoneCheck.test(userPhone) === false) {
            validPhone = false;
        }

        if (validPhone) {
            $("#userPhone").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userPhone").addClass("required-field-needed");
        }


        let validAddress = true;
        if (userAddress === null || userAddress === "") {
            validAddress = false;
        }

        if (validAddress) {
            $("#userAddress").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userAddress").addClass("required-field-needed");
        }


        let validCity = true;
        if (userCity === null || userCity === "") {
            validCity = false;
        }

        if (validCity) {
            $("#userCity").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userCity").addClass("required-field-needed");
        }


        let validState = true;
        if (userState === null || userState === "") {
            validState = false;
        }

        if (validState) {
            $("#userState").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userState").addClass("required-field-needed");
        }


         //Check the Zip Codes' length and if it is an integer.
        let validZipCode = true;
        if (userZipCode === null || userZipCode === "") {
            validZipCode = false;
        }

        let zipCodeRegularExpression = /^\d{5}$/;
        let userZipCodeIsA5DigitNumber = zipCodeRegularExpression.test(userZipCode);
        if (userZipCodeIsA5DigitNumber === false) {
            validZipCode = false;
        }

        if (validZipCode) {
            $("#userZipCode").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userZipCode").addClass("required-field-needed");
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
    if (submitEstimateButton === elementWithFocus) {
        clickedSubmitOrderEstimate = true;
    }
}

$("#submitEstimateButton").on("click", function () {
    setClickedContactButtonTrue();
});

$("#submitEstimateButton").on("click", function () {
    validateEstimateForm()
});

$("#userName").on("change", function () {
    validateEstimateForm();
});

$("#userEmail").on("change", function () {
    validateEstimateForm();
});

$("#userPhone").on("change", function () {
    validateEstimateForm();
});

$("#userAddress").on("change", function () {
    validateEstimateForm();
});

$("#userCity").on("change", function () {
    validateEstimateForm();
});

$("#userState").on("change", function () {
    validateEstimateForm();
});

$("#userZipCode").on("change", function () {
    validateEstimateForm();
});

$("#additionalNotes").on("change", function () {
    validateEstimateForm();
});
