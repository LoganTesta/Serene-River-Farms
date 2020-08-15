/*Start of Slideshow */
let currentSlide;
let slideshowCounter = 0;
let paused = false;
let updateSlideSettings = true;
let currentSlideNumber = 0;
const maxSlideNumber = 4;
let pausePlayButton;
let slideshowHeader;
let slide0 = new Image(600, 400);
slide0.src = "../assets/images/blueberries-and-raspberries-in-pint-cartons.jpg";
let slide1 = new Image(600, 400);
slide1.src = "../assets/images/cherry-farm-trees.jpg";
let slide2 = new Image(600, 400);
slide2.src = "../assets/images/tractor.jpg";
let slide3 = new Image(600, 400);
slide3.src = "../assets/images/oregon-farm-grassland.jpg";
let slide4 = new Image(600, 400);
slide4.src = "../assets/images/apple-trees-harvesting-apples.jpg";

let slideButton0 = document.getElementById('slideButton0');
let slideButton1 = document.getElementById('slideButton1');
let slideButton2 = document.getElementById('slideButton2');
let slideButton3 = document.getElementById('slideButton3');
let slideButton4 = document.getElementById('slideButton4');

slideButtons = new Array(slideButton0, slideButton1, slideButton2, slideButton3, slideButton4);

for (let i = 0; i < maxSlideNumber + 1; i++) {
    slideButtons[i].addEventListener('click', function () {
        setSlide(i);
    }, false);
}

let slideshowImageLink = $(".slideshow__image-link").eq(0);
let slideshowImageLinkText = $(".slideshow__image-link-text").eq();



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
        if (slideshowCounter === 0) {
            currentSlide.css("opacity", 0.1);
        }
        if (slideshowCounter < 180) {
            let newOpacity = (parseFloat(currentSlide.css("opacity")) + 0.005);
            currentSlide.css("opacity", "" + newOpacity);
        }
        if (180 <= slideshowCounter && slideshowCounter < 680) {
            currentSlide.css("opacity", 1);
        }
        if (slideshowCounter >= 680) {
            let newOpacity = (parseFloat(currentSlide.css("opacity")) - 0.005);
            currentSlide.css("opacity", "" + newOpacity);
        }
        if (slideshowCounter >= 860) {
            slideshowCounter = 0;
            updateSlideSettings = true;
            currentSlide.css("opacity", 0.1);
            currentSlideNumber++;
        }

        if (currentSlideNumber < 0) {
            currentSlideNumber = maxSlideNumber;
        } else if (currentSlideNumber > maxSlideNumber) {
            currentSlideNumber = 0;
        }

        if (updateSlideSettings) {
            updateSlideSettings = false;
            for (let i = 0; i < maxSlideNumber + 1; i++) {
                slideButtons[i].classList.remove("active");
            }
            if (currentSlideNumber === 0) {
                slideshowHeader.html("Summer Fresh Berries");
                slideshowImageLink.attr("href", "/OurProducts");
                slideshowImageLinkText.html("Our Products");
                currentSlide.css("backgroundImage", "url(" + slide0.src + ")");
                slideButton0.classList.add("active");
            } else if (currentSlideNumber === 1) {
                slideshowHeader.html("Some of our Cherry Trees");
                slideshowImageLink.attr("href", "/OurFarm");
                slideshowImageLinkText.html("Our Farm");
                currentSlide.css("backgroundImage", "url(" + slide1.src + ")");
                slideButton1.classList.add("active");
            } else if (currentSlideNumber === 2) {
                slideshowHeader.html("One of our Tractors at Work");
                slideshowImageLink.attr("href", "/OurFarm");
                slideshowImageLinkText.html("Our Farm");
                currentSlide.css("backgroundImage", "url(" + slide2.src + ")");
                slideButton2.classList.add("active");
            } else if (currentSlideNumber === 3) {
                slideshowHeader.html("Some of our Farmland");
                slideshowImageLink.attr("href", "/About");
                slideshowImageLinkText.html("About Us");
                currentSlide.css("backgroundImage", "url(" + slide3.src + ")");
                slideButton3.classList.add("active");
            } else if (currentSlideNumber === 4) {
                slideshowImageLink.attr("href", "/Events");
                slideshowHeader.html("Apples Galore!");
                slideshowImageLinkText.html("Events");
                currentSlide.css("backgroundImage", "url(" + slide4.src + ")");
                slideButton4.classList.add("active");
            }
        }
        slideshowCounter++;
    }
}

function togglePausePlay() {
    paused = !paused;
    if (paused === false) {
        pausePlayButton.removeClass("paused");
    } else if (paused) {
        pausePlayButton.addClass("paused");
    }
}

let pausePlay = $("#pausePlayButton");
pausePlay.on("click", function () {
    togglePausePlay();
});

function setSlide(slideNumber) {
    slideshowCounter = 20;
    currentSlide.css("opacity", "0.10");
    currentSlideNumber = slideNumber;
    paused = false;
    pausePlayButton.removeClass("paused");
    updateSlideSettings = true;
}
