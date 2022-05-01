
$(document).ready(function () {

    /* Navbar */
    function setCurrentPage(linkNumber, navBarName, navBarLinkName) {
        let navBar = $("" + navBarName);
        let navBarItems = $("" + navBarName + " " + navBarLinkName);

        for (let i = 0; i < navBarItems.length; i++) {
            $(navBarItems[i]).removeClass("current-page");
        }
        $(navBarItems[linkNumber]).addClass("current-page");
    }


    /* Hamburger menue toggle */
    let dropdownButton = $("#dropdownButton");

    dropdownButton.click(function () {
        toggleHamburgerMenu();
    });

    function toggleHamburgerMenu() {
        $("#dropdownContent").toggleClass("show");
    }

});
