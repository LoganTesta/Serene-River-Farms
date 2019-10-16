
/* JavaScript Contact Form Validation. */
let clickedSubmitContact = false;


function validateSetUpTourForm() {
    if (clickedSubmitContact) {
        let userName = $("#userName").val().trim();
        let userEmail = $("#userEmail").val().trim();
        let userPhone = $("#userPhone").val().trim();
        let partySize = $("#partySize").val().trim();
        let visitDate = $("#visitDate").val().trim();
        let visitTime = $("#visitTime").val().trim();
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


        let validPhone = true;
        if (userPhone === null || userPhone === "") {
            validPhone = false;
        }

        /* Phone validation needs more work here. */
        if (userPhone.length !== 10) {
            validPhone = false;
        }

        if (validPhone) {
            $("#userPhone").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#userPhone").addClass("required-field-needed");
        }


        let validPartySize = true;
        if (partySize === null || partySize === "") {
            validPartySize = false;
        }

        if (validPartySize) {
            $("#partySize").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#partySize").addClass("required-field-needed");
        }


        let validVisitDate = true;
        if (visitDate === null || visitDate === "") {
            validVisitDate = false;
        }

        if (validVisitDate) {
            $("#visitDate").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#visitDate").addClass("required-field-needed");
        }


        let validVisitTime = true;
        if ($("#visitTime").val() === undefined || $("#visitTime").val() < 1) {
            validVisitTime = false;
        }

        if (validVisitTime) {
            $("#visitTime").removeClass("required-field-needed");
        } else {
            validContactForm = false;
            $("#visitTime").addClass("required-field-needed");
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
    if (setUpTourButton === elementWithFocus) {
        clickedSubmitContact = true;
    }
}

$("#setUpTourButton").on("click", function () {
    setClickedContactButtonTrue();
});

$("#setUpTourButton").on("click", function () {
    validateSetUpTourForm()
});

$("#userName").on("change", function () {
    validateSetUpTourForm();
});

$("#userEmail").on("change", function () {
    validateSetUpTourForm();
});

$("#userPhone").on("change", function () {
    validateSetUpTourForm();
});

$("#partySize").on("change", function () {
    validateSetUpTourForm();
});

$("#visitDate").on("change", function () {
    validateSetUpTourForm();
});

$("#visitTime").on("change", function () {
    validateSetUpTourForm();
});

$("#userComments").on("change", function () {
    validateSetUpTourForm();
});
