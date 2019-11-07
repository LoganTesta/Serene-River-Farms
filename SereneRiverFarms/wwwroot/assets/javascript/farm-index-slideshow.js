/*Start of Slideshow */
let currentSlide;
let slideshowCounter = 0;
let paused = false;
let currentSlideNumber = 0;
const maxSlideNumber = 4;
let pausePlayButton;
let slideshowHeader;
let slide0 = new Image(600, 400);
slide0.src = "../assets/images/index/blueberries-and-raspberries-in-pint-cartons.jpg";
let slide1 = new Image(600, 400);
slide1.src = "../assets/images/index/cherry-farm-trees.jpg";
let slide2 = new Image(600, 400);
slide2.src = "../assets/images/index/tractor.jpg";
let slide3 = new Image(600, 400);
slide3.src = "../assets/images/index/oregon-farm-grassland.jpg";
let slide4 = new Image(600, 400);
slide4.src = "../assets/images/index/apple-trees-harvesting-apples.jpg";

let slideButton0 = $("#slideButton0");
let slideButton1 = $("#slideButton1");
let slideButton2 = $("#slideButton2");
let slideButton3 = $("#slideButton3");
let slideButton4 = $("#slideButton4");

$("#slideButton0").on("click", function () {
    setSlide(0);
});
$("#slideButton1").on("click", function () {
    setSlide(1);
});
$("#slideButton2").on("click", function () {
    setSlide(2);
});
$("#slideButton3").on("click", function () {
    setSlide(3);
});
$("#slideButton4").on("click", function () {
    setSlide(4);
});

$("#pausePlayButton").on("click", function () {
    togglePausePlay();
});



function init() {
    currentSlide = $(".slideshow__image").eq(0);
    slideshowHeader = $(".slideshow__header").eq(0);
    pausePlayButton = $("#pausePlayButton");
    currentSlide.css("opacity", 0);
    setInterval(function () {
        runFunctions();
    }, 10);
}
window.onload = init();


function runFunctions() {
    runSlideShow();
}

function runSlideShow() {

    if (paused === false) {
        pausePlayButton.css("background", "url(assets/images/index/pause-button.png)");
    } else if (paused) {
        pausePlayButton.css("background", "url(assets/images/index/play-button.png)");
    }
    if (paused === false) {
        if (slideshowCounter === 0) {
            currentSlide.css("opacity", 0);
            currentSlide.fadeTo(1500, 1);
        }

        if (slideshowCounter === 450) {
            currentSlide.fadeTo(750, 0);
        }

        if (slideshowCounter >= 525) {
            slideshowCounter = -1;
            currentSlideNumber++;
        }

        if (currentSlideNumber > maxSlideNumber) {
            currentSlideNumber = 0;
        }
        if (currentSlideNumber === 0) {
            slideshowHeader.html("Summer Fresh Berries");
            currentSlide.css("backgroundImage", "url(" + slide0.src + ")");
            slideButton0.css("opacity", 1.0);
            slideButton1.css("opacity", 0.40);
            slideButton2.css("opacity", 0.40);
            slideButton3.css("opacity", 0.40);
            slideButton4.css("opacity", 0.40);
        } else if (currentSlideNumber === 1) {
            slideshowHeader.html("Some of our Cherry Trees");
            currentSlide.css("backgroundImage", "url(" + slide1.src + ")");
            slideButton0.css("opacity", 0.40);
            slideButton1.css("opacity", 1.0);
            slideButton2.css("opacity", 0.40);
            slideButton3.css("opacity", 0.40);
            slideButton4.css("opacity", 0.40);
        } else if (currentSlideNumber === 2) {
            slideshowHeader.html("One of our Tractors at Work");
            currentSlide.css("backgroundImage", "url(" + slide2.src + ")");
            slideButton0.css("opacity", 0.40);
            slideButton1.css("opacity", 0.40);
            slideButton2.css("opacity", 1.0);
            slideButton3.css("opacity", 0.40);
            slideButton4.css("opacity", 0.40);
        } else if (currentSlideNumber === 3) {
            slideshowHeader.html("Some of our Farmland");
            currentSlide.css("backgroundImage", "url(" + slide3.src + ")");
            slideButton0.css("opacity", 0.40);
            slideButton1.css("opacity", 0.40);
            slideButton2.css("opacity", 0.40);
            slideButton3.css("opacity", 1.0);
            slideButton4.css("opacity", 0.40);
        } else if (currentSlideNumber === 4) {
            slideshowHeader.html("Apples Galore!");
            currentSlide.css("backgroundImage", "url(" + slide4.src + ")");
            slideButton0.css("opacity", 0.40);
            slideButton1.css("opacity", 0.40);
            slideButton2.css("opacity", 0.40);
            slideButton3.css("opacity", 0.40);
            slideButton4.css("opacity", 1.0);
        }

        slideshowCounter++;
    }
}

function togglePausePlay() {
    paused = !paused;
}

function setSlide(slideNumber) {
    slideshowCounter = 0;
    currentSlideNumber = slideNumber;
    paused = false;
}
