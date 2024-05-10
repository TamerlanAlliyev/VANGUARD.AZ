// window.addEventListener('scroll', function () {
//   var header = document.querySelector('.header-info');
//   header.classList.toggle('scrolled', window.scrollY > 0);
// });





// $(document).ready(function() {
//   $(window).on('beforeunload', function() {
//     $('#loader').fadeIn();
//   });
// });








/* =================================
       BURGER MENU
===================================== */

var burgerMenu = document.querySelector('.bur-menu');
var burgerPageList = document.querySelector('.pages-mobile');
var burgerPageClose = document.querySelector('.pages-close');

burgerMenu.addEventListener('click', (event) => {
  event.stopPropagation();
  //event.preventDefault();
  burgerPageList.classList.add('show');
});

burgerPageClose.addEventListener('click', (event) => {
  //event.preventDefault();
  event.stopPropagation();

  burgerPageList.classList.remove('show');
});

document.addEventListener('click', (event) => {
  //event.preventDefault();
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
var searchInput = document.querySelector('.search-list');
var searchList = document.querySelector(".search-list ul");

let searchToggle = false;
document.addEventListener('click', (event) => {
  // //event.preventDefault(); 
  const isClickInsideSearch = searchBtn.contains(event.target) || searchInput.contains(event.target);
  if (!isClickInsideSearch) {
    searchList.style.display = "none";
    searchInput.style.display = "none";
    searchCloseBtn.src = "/cilent/assets/icons/search.svg"
    searchToggle = false;
  }
});

searchBtn.addEventListener('click', (event) => {
  // //event.preventDefault(); 
  event.stopPropagation();
  if (!searchToggle) {
    searchInput.style.display = "flex";
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
       BASKET TOGGLE
===================================== */
var basaketIcon = document.querySelector('.i-basket');
var basketList = document.querySelector('.basket-list');
var basketClose = document.querySelector('.basket-close');

basaketIcon.addEventListener('click', (event) => {
  //event.preventDefault();

  event.stopPropagation();
  basketList.classList.add('show');
});


basketClose.addEventListener('click', (event) => {
  //event.preventDefault();

  event.stopPropagation();
  basketList.classList.remove('show');
});

document.addEventListener('click', (event) => {
  //event.preventDefault();

  const clickedElement = event.target;
  if (!basketList.contains(clickedElement) && !basaketIcon.contains(clickedElement)) {
    basketList.classList.remove('show');
  }
});









const basketProdList = document.querySelectorAll('.basket-product-list li');
const basketItemCount = document.querySelector('.basket-header span');

var HeaderBasaketCount = document.querySelector('.i-basket sup');




const discountPrice = document.querySelector('.basket-total-prices .sell-price.dis');
const sellDisPrice = document.querySelector('.basket-total-prices .dis-price');

const sellPrice = document.querySelector('.basket-total-prices .sell');


const basketTotalPrices = document.querySelector('.basket-total-prices');

const basketSup = document.querySelector('.i-basket sup');



let basketTotalCount = 0;

let DisPrice = 0;
let SellPrice = 0;

let Dis = false;
let DisItems = 0;



function updateTotalPriceDisplay() {
  if (Dis) {
    basketTotalPrices.innerHTML = `<span class="sell-price dis" id="sell-price">$${DisPrice}</span> <del class="sell-price dis-price">$${SellPrice}</del>`;
  } else {
    basketTotalPrices.innerHTML = `<span class="sell-price sell" id="sell-price">$${SellPrice} </span>`;
  }
  discountPrice.innerText = '$' + DisPrice;
  sellDisPrice.innerText = `$ ${SellPrice}`;
}
updateTotalPriceDisplay()
basketProdList.forEach((item) => {
  const removeBtn = item.querySelector('.basket-price-close a');
  const input = item.querySelector('.basket-quantity-box input');
  const plusBtn = item.querySelector('.plus');
  const minusBtn = item.querySelector('.minus');

  const sellDisPriceElement = item.querySelector('.sell-price.dis');
  const discountPriceElement = item.querySelector('.dis-price');



  const sellPriceElement = item.querySelector('#sell-price-sell');



  if (Dis) {
    basketTotalPrices.innerHTML = '<span class="sell-price dis" id="sell-price">$80</span> <del class="sell-price dis-price">$100</del>';
  }
  else {
    basketTotalPrices.innerHTML = '<span class="sell-price sell" id="sell-price">$100 </span>';
  }






  if (sellDisPriceElement !== null) {
    SellPrice += parseFloat(discountPriceElement.innerText.replace('$', '').trim());
    DisPrice += parseFloat(sellDisPriceElement.innerText.replace('$', '').trim());
    Dis = true;
    ++DisItems;
    if (DisItems == 0) {
      Dis = false;
    }
  } else {
    SellPrice += parseFloat(sellPriceElement.innerText.replace('$', '').trim());
    DisPrice += parseFloat(sellPriceElement.innerText.replace('$', '').trim());
  }

  updateTotalPriceDisplay()







  if (removeBtn) {
    removeBtn.addEventListener('click', (event) => {
      event.preventDefault();


      HeaderBasaketCount.innerText = parseInt(HeaderBasaketCount.innerText) - input.value;

      basketItemCount.innerText = parseInt(basketItemCount.innerText) - input.value;




      if (sellDisPriceElement !== null) {
        SellPrice -= parseFloat(discountPriceElement.innerText.replace('$', '').trim()) * input.value;
        DisPrice -= parseFloat(sellDisPriceElement.innerText.replace('$', '').trim()) * input.value;

        --DisItems*input.value;
        if (DisItems == 0) {
          Dis = false;
        }
      } else {
        SellPrice -= parseFloat(sellPriceElement.innerText.replace('$', '').trim()) * input.value;
        DisPrice -= parseFloat(sellPriceElement.innerText.replace('$', '').trim()) * input.value;
      
      }
      updateTotalPriceDisplay()

      if(parseInt(basketSup.innerText)<=0){
        basketSup.remove()
      }

      item.remove();
      event.stopPropagation();
    });
  }







  if (plusBtn) {
    plusBtn.addEventListener('click', (event) => {
      var value = parseFloat(input.value);
      input.value = value + 1;

      basketItemCount.innerText = parseInt(basketItemCount.innerText) + 1;

      HeaderBasaketCount.innerText = parseInt(HeaderBasaketCount.innerText) + 1




      if (sellDisPriceElement !== null) {
        SellPrice += parseFloat(discountPriceElement.innerText.replace('$', '').trim());
        DisPrice += parseFloat(sellDisPriceElement.innerText.replace('$', '').trim());

      } else {
        SellPrice += parseFloat(sellPriceElement.innerText.replace('$', '').trim());
        DisPrice += parseFloat(sellPriceElement.innerText.replace('$', '').trim());
      }


      updateTotalPriceDisplay()

    });
  }


  if (minusBtn) {
    minusBtn.addEventListener('click', (event) => {
      var value = parseFloat(input.value);

      if (value > 1) {
        input.value = value - 1;

        basketItemCount.innerText = parseInt(basketItemCount.innerText) - 1;

        HeaderBasaketCount.innerText = parseInt(HeaderBasaketCount.innerText) - 1



        if (sellDisPriceElement !== null) {
          SellPrice -= parseFloat(discountPriceElement.innerText.replace('$', '').trim());
          DisPrice -= parseFloat(sellDisPriceElement.innerText.replace('$', '').trim());

        } else {
          SellPrice -= parseFloat(sellPriceElement.innerText.replace('$', '').trim());
          DisPrice -= parseFloat(sellPriceElement.innerText.replace('$', '').trim());
        }

        updateTotalPriceDisplay()
      }


    });
  }


  basketTotalCount += parseFloat(input.value);
  basketItemCount.innerText = basketTotalCount;

});


