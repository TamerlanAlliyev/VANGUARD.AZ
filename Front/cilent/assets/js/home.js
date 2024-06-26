'use strict';

window.addEventListener('scroll', function () {
  var header = document.querySelector('.header-info');
  header.classList.toggle('scrolled', window.scrollY > 0);
});


// /* TRENDY SWIPER */
// var swiper = new Swiper(".trendy .mySwiper", {
//   slidesPerView: 1,

//   spaceBetween: 10,
//   pagination: {
//     el: ".swiper-pagination",
//     clickable: true,
//     dynamicBullets: true,
//   },
//   // autoplay: {
//   //   delay: 5000,
//   //   disableOnInteraction: false,
//   // },
//   breakpoints: {
//     "@0.00": {
//       slidesPerView: 2,
//       spaceBetween: 10,
//       grid: {
//         rows: 1,
//         fill: "row",
//       }
//     },
//     "@0.75": {
//       slidesPerView: 3,
//       spaceBetween: 25,
//       grid: {
//         rows: 2,
//         fill: "row",
//       }
//     },
//     "@1.00": {
//       slidesPerView: 4,
//       spaceBetween: 20,
//       grid: {
//         rows: 2,
//         fill: "row",
//       }
//     },
//     "@1.50": {
//       slidesPerView: 5,
//       spaceBetween: 35,
//       grid: {
//         rows: 2,
//         fill: "row",
//       }
//     },
//   },
// });



/* BLOG SWIPER */

var swiper = new Swiper(".blog .mySwiper", {
  effect: "coverflow",
  centeredSlides: true,
  slidesPerView: 3,
  autoplay: {
    delay: 5000,
    disableOnInteraction: false,
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
    dynamicBullets: true,
  },
  coverflowEffect: {
    rotate: 70,
    stretch: 0,
    depth: 100,
    modifier: 1,
    slideShadows: true,
  },
  breakpoints: {
    "@0.00": {
      slidesPerView: 1,
    },
    "@0.75": {
      slidesPerView: 2,
    },
    "@1.00": {
      slidesPerView: 3,
    },
    "@1.50": {
      slidesPerView: 3,
    },
  }
});


/* BLOG SWIPER */




















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
    img.style.opacity = 0.85;
    product.querySelector('.prod-add').classList.add('active');
  });

  product.addEventListener('mouseout', (event) => {
    event.preventDefault();
    var img = product.querySelector('img');
    img.style.opacity = 1;
    product.querySelector('.prod-add').classList.remove('active');
  });
});








var wishCnt = document.querySelector('.i-wish sup');
var wishIconBtn = document.querySelectorAll('.trendy-product .wsh');

wishIconBtn.forEach(btn => {
  btn.addEventListener('click', (event) => {
    event.preventDefault();

    if (!wishCnt) {
      wishCnt = document.createElement('sup');
      wishCnt.textContent = '1';
      document.querySelector('.i-wish').appendChild(wishCnt);
      btn.classList.add('active');
      btn.src = '/cilent/assets/icons/wish-icon-active.svg';
    } else {
      if (btn.classList.contains('active')) {
        btn.classList.remove('active');
        btn.src = '/cilent/assets/icons/wish-icon.svg';
        wishCnt.innerText--;
        if (parseInt(wishCnt.innerText) == 0) {
          wishCnt.parentNode.removeChild(wishCnt);
          wishCnt = null;
        }
      } else {
        btn.classList.add('active');
        btn.src = '/cilent/assets/icons/wish-icon-active.svg';
        wishCnt.innerText++;
      }
    }
  });
});




var trendyCats = document.querySelectorAll('.trendy-list a');
var trendySlider = document.querySelectorAll('.trendy .swiper');

trendyCats.forEach(cat => {
  cat.addEventListener('click', (event) => {
    event.preventDefault();

    let catName = cat.dataset.name;

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







































// <!-- =============== DEAL OF WEEK   SECTION - Start =============== -->

//================ TIME
const sqlDatetime2 = '2024-04-25 22:29:12.345';

const jsDate = new Date(sqlDatetime2);


const year = jsDate.getFullYear();
const month = jsDate.getMonth() + 1;
const date = jsDate.getDate();
const hours = jsDate.getHours();
const minutes = jsDate.getMinutes();
const seconds = jsDate.getSeconds();

const paddedMonth = month.toString().padStart(2, '0');
const paddedDate = date.toString().padStart(2, '0');
const paddedHours = hours.toString().padStart(2, '0');
const paddedMinutes = minutes.toString().padStart(2, '0');
const paddedSeconds = seconds.toString().padStart(2, '0');

//================ TIME













function updateCountdowns() {
  const offerTimes = document.querySelectorAll('.offer-time');

  offerTimes.forEach(offerTime => {
    const dataTime = offerTime.getAttribute('data-time');

    const targetDate = new Date(dataTime);

    const now = new Date();
    const remainingTime = targetDate - now;

    const days = Math.floor(remainingTime / (1000 * 60 * 60 * 24));
    const hours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

    const dayElement = offerTime.querySelector('.day .num');
    const hoursElement = offerTime.querySelector('.hours .num');
    const minsElement = offerTime.querySelector('.mins .num');
    const secElement = offerTime.querySelector('.sec .num');

    dayElement.textContent = days;
    hoursElement.textContent = hours;
    minsElement.textContent = minutes;
    secElement.textContent = seconds;
  });
}

updateCountdowns();
setInterval(updateCountdowns, 1000);




var wishCount = document.querySelector('.i-wish sup');
var wishIconBtn = document.querySelectorAll('.dealofweek-wish .wsh');

wishIconBtn.forEach(btn => {
  btn.addEventListener('click', (event) => {
    event.preventDefault();

    if (!wishCount) {
      wishCount = document.createElement('sup');
      wishCount.textContent = '1';
      document.querySelector('.i-wish').appendChild(wishCount);
      btn.classList.add('active');
      btn.src = '/cilent/assets/icons/wish-icon-active.svg';
    } else {

      if (btn.classList.contains('active')) {

        btn.classList.remove('active');
        btn.src = '/cilent/assets/icons/wish-white.svg';
        wishCount.innerText--;
        if (parseInt(wishCount.innerText) == 0) {
          wishCount.parentNode.removeChild(wishCount);
          wishCount = null;
        }
      } else {
        btn.classList.add('active');
        btn.src = '/cilent/assets/icons/wish-icon-active.svg';
        wishCount.innerText++;
      }
    }
  });
});












var basketCount = document.querySelector('.i-basket');
var addToCart = document.querySelectorAll('.prod-add');


addToCart.forEach(btn => {
  btn.addEventListener('click', (event) => {
    event.preventDefault();

    if (!basketCount.querySelector('.i-basket sup')) {
      var supElement = document.createElement('sup');
      supElement.textContent = '1';
      basketCount.appendChild(supElement);
    } else {
      var supElement = basketCount.querySelector('.i-basket sup');
      var currentValue = parseInt(supElement.textContent);
      supElement.textContent = currentValue + 1;
    }
  });
});






document.addEventListener('DOMContentLoaded', function () {
  var dealprodutCard = document.querySelectorAll('.dealofweek-prod-info');

  dealprodutCard.forEach(product => {
    var productImg = product.querySelector('.prod-img img');
    var addToCartBtn = product.querySelector('.dealofweek-basket');

    productImg.addEventListener('mouseenter', function (event) {
      event.preventDefault();
      console.log("hover");
      productImg.style.opacity = 0.85;
      addToCartBtn.classList.add('active');
    });

    productImg.addEventListener('mouseleave', function (event) {
      event.preventDefault();
      productImg.style.opacity = 1;
      addToCartBtn.classList.remove('active');
    });

    addToCartBtn.addEventListener('mouseenter', function (event) {
      event.preventDefault();
      productImg.style.opacity = 0.85;
      addToCartBtn.classList.add('active');
    });

    addToCartBtn.addEventListener('mouseleave', function (event) {
      event.preventDefault();
      productImg.style.opacity = 1;
      addToCartBtn.classList.remove('active');
    });
  });
});






// <!-- =============== DEAL OF WEEK   SECTION - End =============== -->





























































// /* TRENDY SWIPER */

var swiper = new Swiper(".mySwiper.sub", {
  slidesPerView: 1,
  spaceBetween: 10,
  autoplay: {
    delay: 7000,
    disableOnInteraction: false,
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
    dynamicBullets: true,
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
      slidesPerView: 4,
      spaceBetween: 45,
      grid: {
        rows: 2,
        fill: "row",
      }
    },
  },
});



















// =====================================
//          PRODUCT ITEMS
// =====================================

const wishBtnP = document.querySelectorAll('.trendy .p-pric img');

wishBtnP.forEach(wishBtn => {

  wishBtn.addEventListener('click', function (event) {
    wishBtn.classList.toggle('active');

    if (wishBtn.classList.contains('active')) {
      wishBtn.src = '/cilent/assets/icons/wish-icon-active.svg';
      wishCount.innerText = parseInt(wishCount.innerText) + 1;
    } else {
      wishBtn.src = '/cilent/assets/icons/wish-icon.svg';
      wishCount.innerText = parseInt(wishCount.innerText) - 1;
    }
  });

});


var swiper = new Swiper(".product-image-box .mySwiper.hover-image", {
  autoHeight: true,
  spaceBetween: 20,
  autoplay: {
    delay: 2000,
    disableOnInteraction: false,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});




const productImageBox = document.querySelectorAll('.product-image-box');
const basketCounts = document.querySelector('.i-basket sup');

productImageBox.forEach(prodBtn => {

  const productAddBtn = prodBtn.querySelector('.add-btn');
  const productAddSize = prodBtn.querySelector('.add-size');

  const mainImage = prodBtn.querySelector('.main-image');
  const hoverImage = prodBtn.querySelector('.hover-image');


  const swiperBtns = prodBtn.querySelectorAll('.p-btn');


  prodBtn.addEventListener('mouseover', function (event) {
    prodBtn.classList.add('active');
    productAddBtn.classList.add('active');
  });





  prodBtn.addEventListener('mouseout', (event) => {
    if (!prodBtn.contains(event.relatedTarget)) {
      hoverImage.classList.remove('active');
      productAddBtn.classList.remove('active');
      productAddSize.classList.remove('active');
    }
    prodBtn.classList.remove('active');
  });

  productAddBtn.addEventListener('click', function (event) {
    productAddSize.classList.add('active');
  });



  productAddSize.addEventListener('click', function (event) {
    productAddSize.classList.remove('active');
    basketCounts.innerText = parseInt(basketCounts.innerText) + 1;;
  });


  productAddSize.addEventListener('mouseover', (event) => {

    swiperBtns.forEach(swiperBtn => {
      swiperBtn.style.display = 'none';
    });

  });

  productAddSize.addEventListener('mouseout', (event) => {

    if (!productAddSize.contains(event.relatedTarget)) {
      productAddSize.classList.remove('active');
      swiperBtns.forEach(swiperBtn => {
        swiperBtn.style.display = 'block';
      });
    }

  });


});

// =====================================
//          PRODUCT ITEMS
// =====================================





















// =====================================
//       PRODUCT ITEMS - DEAL OF WEEK
// =====================================




var swiper = new Swiper(".dealofweek .mySwiper", {
  slidesPerView: 1,
  spaceBetween: 10,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
    dynamicBullets: true,
  },
  autoplay: {
    delay: 4000,
    disableOnInteraction: false,
  },
  breakpoints: {
    "@0.00": {
      slidesPerView: 2,
      spaceBetween: 30,
    },
    "@0.75": {
      slidesPerView: 2,
      spaceBetween: 40,
    },
    "@1.00": {
      slidesPerView: 3,
      spaceBetween: 40,
    },
    "@1.50": {
      slidesPerView: 3,
      spaceBetween: 30,
    },
  },
});






const wishBtnDel = document.querySelectorAll('.dealofweek-prod .p-pric img');

wishBtnDel.forEach(wishBtnD => {

  wishBtnD.addEventListener('click', function (event) {
    wishBtnD.classList.toggle('active');

    if (wishBtnD.classList.contains('active')) {
      wishBtnD.src = '/cilent/assets/icons/wish-icon-active.svg';
      wishCount.innerText = parseInt(wishCount.innerText) + 1;
    } else {
      wishBtnD.src = '/cilent/assets/icons/wish-white.svg';
      wishCount.innerText = parseInt(wishCount.innerText) - 1;
    }
  });

});

var swiper = new Swiper(".product-image-b .mySwiper", {
  autoHeight: true,
  spaceBetween: 20,
  autoplay: {
    delay: 2000,
    disableOnInteraction: false,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});




const productImageBoxDeal = document.querySelectorAll('.product-image-b');

console.log(productImageBoxDeal)
productImageBoxDeal.forEach(prodBtn => {

  const productAddBtn = prodBtn.querySelector('.add-btn');
  const productAddSize = prodBtn.querySelector('.add-size');

  const mainImage = prodBtn.querySelector('.main-image');
  const hoverImage = prodBtn.querySelector('.hover-image');


  const swiperBtns = prodBtn.querySelectorAll('.p-btn');


  prodBtn.addEventListener('mouseover', function (event) {


    prodBtn.classList.add('active');

    productAddBtn.classList.add('active');
  });





  prodBtn.addEventListener('mouseout', (event) => {
    if (!prodBtn.contains(event.relatedTarget)) {
      productAddBtn.classList.remove('active');
    }
    prodBtn.classList.remove('active');
  });

  productAddBtn.addEventListener('click', function (event) {
    productAddSize.classList.add('active');
  });



  productAddSize.addEventListener('click', function (event) {
    productAddSize.classList.remove('active');
    basketCounts.innerText = parseInt(basketCounts.innerText) + 1;;
  });


  productAddSize.addEventListener('mouseover', (event) => {

    swiperBtns.forEach(swiperBtn => {
      swiperBtn.style.display = 'none';
    });

  });

  productAddSize.addEventListener('mouseout', (event) => {

    if (!productAddSize.contains(event.relatedTarget)) {
      productAddSize.classList.remove('active');
      swiperBtns.forEach(swiperBtn => {
        swiperBtn.style.display = 'block';
      });
    }

  });


});

// =====================================
//      PRODUCT ITEMS - DEAL OF WEEK
// =====================================
