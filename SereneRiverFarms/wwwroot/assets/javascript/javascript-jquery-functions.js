
$(document).ready(function () {
   
});

function setCurrentPage(linkNumber, navBarName, navBarLinkName) {

    let navBar = $("" + navBarName);
    let navBarItems = $("" + navBarName + " " + navBarLinkName);

    for (let i = 0; i < navBarItems.length; i++) {
        $(navBarItems[i]).removeClass("current-page");
    }
    $(navBarItems[linkNumber]).addClass("current-page");
}
