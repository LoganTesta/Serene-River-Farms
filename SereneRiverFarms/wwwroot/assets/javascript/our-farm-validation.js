
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
        let dateFeedback = "";

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


        /* If the @ position is at the start (or less) position of value 0,  validContactForm = false. */
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


        //Check if the party size is a number, if it is, then convert it to a float, and check if it is an integer.
        let validPartySize = true;
        if (partySize === null || partySize === "") {
            validPartySize = false;
        }

        let partySizeIsNumeric = $.isNumeric(partySize);
        let partySizeToNumber = -1;
        if (partySizeIsNumeric) {
            partySizeToNumber = parseFloat(partySize);
        }
        if (partySizeIsNumeric === false || Math.floor(partySizeToNumber) !== partySizeToNumber) {
            validPartySize = false;
        }
        if (partySizeToNumber < 1 || partySizeToNumber > 99) {
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

        //More advanced date validation.
        let sectionOfDate = 0;
        let yearString = "";
        let monthString = "";
        let dayString = "";

        for (let i = 0; i < visitDate.length; i++) {

            if (visitDate[i] !== "-") {
                if (sectionOfDate === 0) {
                    yearString += visitDate[i];
                } else if (sectionOfDate === 1) {
                    monthString += visitDate[i];
                } else if (sectionOfDate === 2) {
                    dayString += visitDate[i];
                }
            } else {
                sectionOfDate++;
            }
        }

        let year = parseInt(yearString);
        let month = parseInt(monthString); 
        let day = parseInt(dayString);

        let currentDate = new Date();
        let currentYear = currentDate.getFullYear();
        let currentMonth = 1 + currentDate.getMonth();
        let currentDay = currentDate.getDate();

        if (year < currentYear) {
            validVisitDate = false;
            dateFeedback = " Please provide a date ranging from tomorrow to later this or next year.";

        } else if (year === currentYear) {
            if (month < currentMonth || (month === currentMonth && day < (currentDay + 1))) {
                validVisitDate = false;
                dateFeedback = " Please provide a date ranging from tomorrow to later this or next year.";
            }

        } else if (year > (currentYear + 1)) {
            validVisitDate = false;
            dateFeedback = " Please provide a date ranging from tomorrow to later this or next year.";
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
            $(".javascript-validation-results-contact-us").eq(0).html("Please fill all required fields in the correct format." + dateFeedback);
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
