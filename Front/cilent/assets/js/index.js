'use strict';



// var swiper = new Swiper(".mySwiper", {
//   slidesPerView: 5,
//   spaceBetween: 10,
//   // autoplay: {
//   //   delay: 5000, 
//   //   disableOnInteraction: false, 
//   // },
//   pagination: {
//     el: ".swiper-pagination",
//     clickable: true,
//     dynamicBullets: true,
//   },

//   breakpoints: {
//     "@0.00": {
//       slidesPerView: 1,
//       spaceBetween: 10,
//       grid:{
//         rows: 1,
//         fill: "row",
//       },
//     },
//     "@0.75": {
//       slidesPerView: 2,
//       spaceBetween: 20,
//       grid:{
//         rows: 2,
//         fill: "row",
//       },
//     },
//     "@1.00": {
//       slidesPerView: 2,
//       spaceBetween: 40,
//       grid:{
//         rows: 2,
//         fill: "row",
//       },
//     }
//     ,
//     "@1.50": {
//       slidesPerView: 5,
//       spaceBetween: 35,
//       grid:{
//         rows: 2,
//         fill: "row",
//       },
//     },
//   },
// });

var swiper = new Swiper(".mySwiper", {
  slidesPerView: 1,
  spaceBetween: 10,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
    dynamicBullets: true,
  },
  autoplay: {
    delay: 5000,
    disableOnInteraction: false,
  },
  breakpoints: {
    "@0.00": {
      slidesPerView: 2,
      spaceBetween: 10,
      grid: {
        rows: 1,
        fill: "row",
      }
    },
    "@0.75": {
      slidesPerView: 3,
      spaceBetween: 25,
      grid: {
        rows: 2,
        fill: "row",
      }
    },
    "@1.00": {
      slidesPerView: 4,
      spaceBetween: 20,
      grid: {
        rows: 2,
        fill: "row",
      }
    },
    "@1.50": {
      slidesPerView: 5,
      spaceBetween: 35,
      grid: {
        rows: 2,
        fill: "row",
      }
    },
  },
});
















/* =================================
       BASKET TOGGLE
===================================== */
var basaketIcon = document.querySelector('.i-basket');
var basketList = document.querySelector('.basket-list');
var basketClose = document.querySelector('.basket-close');

basaketIcon.addEventListener('click', (event) => {
  event.preventDefault();

  event.stopPropagation();
  basketList.classList.add('show');
});


basketClose.addEventListener('click', (event) => {
  event.preventDefault();

  event.stopPropagation();
  basketList.classList.remove('show');
});

document.addEventListener('click', (event) => {
  event.preventDefault();

  const clickedElement = event.target;
  if (!basketList.contains(clickedElement) && !basaketIcon.contains(clickedElement)) {
    basketList.classList.remove('show');
  }
});







/* =================================
       BURGER MENU
===================================== */

var burgerMenu = document.querySelector('.bur-menu');
var burgerPageList = document.querySelector('.pages-mobile');
var burgerPageClose = document.querySelector('.pages-close');

burgerMenu.addEventListener('click', (event) => {
  event.stopPropagation();
  event.preventDefault();
  burgerPageList.classList.add('show');
});

burgerPageClose.addEventListener('click', (event) => {
  event.preventDefault();
  event.stopPropagation();

  burgerPageList.classList.remove('show');
});

document.addEventListener('click', (event) => {
  event.preventDefault();
  const clickedElement = event.target;

  if (!burgerPageList.contains(clickedElement) && !burgerMenu.contains(clickedElement)) {
    burgerPageList.classList.remove('show');
  }
});



/* =================================
       SEARCH INPUT
===================================== */

var searchBtn = document.querySelector(".i-search");
var searchCloseBtn = document.querySelector(".i-search img");
var searchInput = document.querySelector('.search-list input');
var searchList = document.querySelector(".search-list ul");

let searchToggle = false;
document.addEventListener('click', (event) => {
  // event.preventDefault(); 
  const isClickInsideSearch = searchBtn.contains(event.target) || searchInput.contains(event.target);
  if (!isClickInsideSearch) {
    searchList.style.display = "none";
    searchInput.style.display = "none";
    searchCloseBtn.src = "/cilent/assets/icons/search.svg"
    searchToggle = false;
  }
});

searchBtn.addEventListener('click', (event) => {
  // event.preventDefault(); 
  event.stopPropagation();
  if (!searchToggle) {
    searchInput.style.display = "block";
    searchCloseBtn.src = "/cilent/assets/icons/close-white.svg"
    searchToggle = true;
  }
  else {
    searchInput.style.display = "none";
    searchCloseBtn.src = "/cilent/assets/icons/search.svg"
    searchToggle = false;
  }
});


searchInput.addEventListener('input', () => {
  searchList.style.display = "block";
});

window.addEventListener('scroll', function () {
  var header = document.querySelector('.header-info');
  header.classList.toggle('scrolled', window.scrollY > 0);
});






/* =================================
       USER INFO
===================================== */

var userBtn = document.querySelector(".i-user");
var usersInfo = document.querySelector('.user-info');
var usersToggle = false;

userBtn.addEventListener('mouseover', () => {
  usersInfo.style.display = "block";
});

userBtn.addEventListener('mouseleave', () => {
  setTimeout(() => {
    if (!isHovered(userBtn) && !isHovered(usersInfo)) {
      usersInfo.style.display = "none";
    }
  }, 100);
});

usersInfo.addEventListener('mouseleave', () => {
  setTimeout(() => {
    if (!isHovered(userBtn) && !isHovered(usersInfo)) {
      usersInfo.style.display = "none";
    }
  }, 500);
});

function isHovered(element) {
  return element.matches(':hover') || element.querySelector(':hover');
}





/* =================================
       PRODUCT INPUT
===================================== */

var increaseBtns = document.querySelectorAll('.increase-btn');
var quantityInputs = document.querySelectorAll('.quantity-input');
var decreaseBtns = document.querySelectorAll('.decrease-btn');

increaseBtns.forEach(btn => {
  btn.addEventListener('click', (event) => {
    event.preventDefault();
    let inpValue = btn.nextElementSibling.value;
    if (inpValue > 1) {
      btn.nextElementSibling.value = --inpValue;
    }
  });
});

decreaseBtns.forEach(btn => {
  btn.addEventListener('click', function (event) {
    event.preventDefault();

    let inpValue = btn.previousElementSibling.value;
    btn.previousElementSibling.value = ++inpValue;
  });
});





































/* =========== SLIDER - START =========== */

document.addEventListener('DOMContentLoaded', function () {
  var sliderLeftBtn = document.querySelector('.slider-left-btn');
  var sliderRightBtn = document.querySelector('.slider-right-btn');
  var slider = document.querySelector('.sliders');
  var slides = document.querySelectorAll('.sliders .slide');
  var slideCount = slides.length;

  var currentSlide = 0;

  var sliderCircleBtnContainer = document.querySelector('.slider-cricle-btn');

  for (var i = 0; i < slideCount; i++) {
    var circleBtn = document.createElement('div');
    circleBtn.classList.add('cricle-btn');
    circleBtn.setAttribute('data-slide-index', i);
    sliderCircleBtnContainer.appendChild(circleBtn);
  }

  var sliderCircleBtns = document.querySelectorAll('.slider-cricle-btn .cricle-btn');
  sliderCircleBtns.forEach(function (circleBtn, index) {
    circleBtn.addEventListener('click', function () {
      goToSlide(index);
    });
  });

  slides[0].classList.add('active');
  sliderCircleBtns[0].classList.add('active');

  if (slideCount <= 1) {
    sliderLeftBtn.style.display = "none";
    sliderRightBtn.style.display = "none";
  }

  function moveSlider(direction) {
    const slideWidthPercent = 100 / slideCount;
    let newTranslatePercent = currentSlide * -100;

    if (direction === 'left') {
      currentSlide = (currentSlide === 0) ? slideCount - 1 : currentSlide - 1;
    } else if (direction === 'right') {
      currentSlide = (currentSlide === slideCount - 1) ? 0 : currentSlide + 1;
    }

    newTranslatePercent = currentSlide * -100;

    slider.style.transform = `translateX(${newTranslatePercent}%)`;

    updateCircleButtons();
  }

  function goToSlide(slideIndex) {
    currentSlide = slideIndex;
    moveSlider('stay');
  }

  function updateCircleButtons() {
    sliderCircleBtns.forEach(function (circleBtn) {
      circleBtn.classList.remove('active');
    });
    sliderCircleBtns[currentSlide].classList.add('active');
  }

  sliderLeftBtn.addEventListener('click', function (event) {
    event.preventDefault();
    moveSlider('left');
  });

  sliderRightBtn.addEventListener('click', function (event) {
    event.preventDefault();
    moveSlider('right');
  });

  window.addEventListener('resize', function () {
    moveSlider('stay');
  });

  var autoSlideInterval = setInterval(function () {
    moveSlider('right');
  }, 5000);

  slider.addEventListener('mouseenter', function () {
    clearInterval(autoSlideInterval);
  });

  slider.addEventListener('mouseleave', function () {
    autoSlideInterval = setInterval(function () {
      moveSlider('right');
    }, 5000);
  });
});




/* =========== SLIDER - End =========== */














// =============== TRENDY SLIDER - Start ===============



var prodImage = document.querySelectorAll('.trendy-product .product-image img');
var produtCard = document.querySelectorAll('.trendy-product .product-image');
var produtAddBtn = document.querySelectorAll('.trendy-product .prod-add');

produtCard.forEach(product => {
  product.addEventListener('mouseover', (event) => {
    event.preventDefault();
    produtAddBtn.forEach(btn => {
      btn.classList.remove('active');
    });
    var img = product.querySelector('img');
    img.style.opacity = 0.8;
    product.querySelector('.prod-add').classList.add('active');
  });

  product.addEventListener('mouseout', (event) => {
    event.preventDefault();
    var img = product.querySelector('img');
    img.style.opacity = 1;
    product.querySelector('.prod-add').classList.remove('active');
  });
});









var wishIconBtn = document.querySelectorAll('.trendy-product .wish');

wishIconBtn.forEach(btn => {
  btn.addEventListener('click', (event) => {
    event.preventDefault();
    var wishImg = btn.querySelector('img');

    if (btn.classList.contains('active')) {
      btn.classList.remove('active');
      wishImg.src = '/cilent/assets/icons/wish-icon.svg';
    } else {
      btn.classList.add('active');
      wishImg.src = '/cilent/assets/icons/wish-icon-active.svg';
    }
  });
});



var trendyCats = document.querySelectorAll('.trendy-list a');
var trendySlider = document.querySelectorAll('.trendy .swiper');

trendyCats.forEach(cat => {
  cat.addEventListener('click', (event) => {
    event.preventDefault();

    let catName = cat.dataset.name;
    
    console.log(catName);
    trendyCats.forEach(category => {
      category.classList.remove('active');
    });
    cat.classList.add('active');

    

    trendySlider.forEach(slider => {
      slider.classList.remove('active');
      if (slider.classList.contains(catName)) {
        slider.classList.add('active'); 
      }
    });

  });
});



// =============== TRENDY SLIDER - End ===============


























































































