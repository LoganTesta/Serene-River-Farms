


$(document).ready(function (){
   
});


function setCurrentPage(linkNumber, navBarName) {

    let navBar = $("#" + navBarName);
    let navBarItems = $("#" + navBarName + " .nav__nav-link");

    for (let i = 0; i < navBarItems.length; i++) {
        $(navBarItems[i]).removeClass("current-page");
    }
    $(navBarItems[linkNumber]).addClass("current-page");
}



