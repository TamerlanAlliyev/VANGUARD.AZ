var swiper = new Swiper(".product-detail-images .mySwiper", {
  spaceBetween: 10,
  slidesPerView: 4,
  freeMode: true,
  watchSlidesProgress: true,
});

var swiper2 = new Swiper(".product-detail-images .mySwiper2", {
  spaceBetween: 10,

  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
  thumbs: {
    swiper: swiper,
  },
});






const stars = document.querySelectorAll('.bought .stars-rating-block input');
const labels = document.querySelectorAll('.bought .stars-rating-block label');

labels.forEach((label, index) => {
  label.addEventListener('click', function () {
    const selectedIndex = index;

    stars.forEach((star, i) => {
      if (i <= selectedIndex) {
        star.checked = true;
        labels[i].innerHTML = `<i class="fas fa-star checked star_on"></i>`;
      } else {
        star.checked = false;
        labels[i].innerHTML = `<i class="far fa-star"></i>`;
      }
    });
  });
});



var descriptionElement = document.querySelector('.product-description');
if (descriptionElement) {
  var text = descriptionElement.textContent.trim();
  if (text.length > 20) {
    var shortenedText = text.substring(0, 400) + '...';
    descriptionElement.textContent = shortenedText;
  }
}








// Sizes

const sizes = document.querySelectorAll('.size-block');

sizes.forEach((size) => {
  size.addEventListener('click', function () {
    if (!size.classList.contains('sold-out')) {
      sizes.forEach((size) => {
        size.classList.remove('active');
      });
      this.classList.add('active');
    }
  });
});

// Sizes




// Colors

//COLORS Swiper

var swiper = new Swiper(".detail-color-box .mySwiper", {
  slidesPerView: 1,
  spaceBetween: 10,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".detail-color-box .swiper-button-next",
    prevEl: ".detail-color-box .swiper-button-prev",
  },
  breakpoints: {
    "@0.00": {
      slidesPerView: 4,
      spaceBetween: 10,
    },
    "@0.75": {
      slidesPerView: 3,
      spaceBetween: 20,
    },
    "@1.00": {
      slidesPerView: 4,
      spaceBetween: 40,
    },
    "@1.50": {
      slidesPerView: 5,
      spaceBetween: 15,
    },
  },
});



const colors = document.querySelectorAll('.detail-selects .swiper-slide');

colors.forEach((color) => {

  const cName = color.querySelector('span');


  color.addEventListener('click', function () {
    if (!this.classList.contains('sold-out')) {
      colors.forEach((color) => {
        color.classList.remove('active');
      });
      this.classList.add('active');
      cName.style.display = 'block ';
    }
  });
});

//COLORS Swiper





// Add to cart

const input = document.querySelector('.detail-buttons input');
const plusBtn = document.querySelector('.detail-buttons .plus');
const minusBtn = document.querySelector('.detail-buttons .minus');

plusBtn.addEventListener('click', (event) => {

  var value = parseFloat(input.value);
  input.value = value + 1;
});
minusBtn.addEventListener('click', (event) => {
  var value = parseFloat(input.value);
  if (value > 1) {
    input.value = value - 1;
  }
});


const AddBasket = document.querySelector('.detail-buttons .add-cart');

// console.log(parseInt(basketSup.innerText)+parseInt(HeaderBasaketCount.innerText))

// console.log(parseInt(input.value))


let BasketValue = document.querySelector('.i-basket sup');

console.log(parseInt(BasketValue.innerText) + parseInt(input.value))
AddBasket.addEventListener('click', (event) => {

  BasketValue.innerText = parseInt(BasketValue.innerText) + parseInt(input.value);
});








// Wish

const wishBtn = document.querySelector('.wish-share img');


let wishCount = document.querySelector('.i-wish sup');
wishBtn.addEventListener('click', (event) => {
  event.preventDefault();
  if (wishBtn.classList.contains('active')) {
    wishBtn.classList.remove('active');
    wishBtn.src = '/cilent/assets/icons/wish-icon.svg';
    wishCount.innerText = parseInt(wishCount.innerText) - 1;
  } else {
    wishBtn.classList.add('active');
    wishBtn.src = '/cilent/assets/icons/wish-icon-active.svg';
    wishCount.innerText = parseInt(wishCount.innerText) + 1;
  }
});


const wishspan = document.querySelector('.wish-share span');

console.log(wishspan)
wishspan.addEventListener('click', (event) => {
  wishBtn.click()
});



















































const image = document.querySelector(".main-image img");

const shareData = {
  title: "MDN",
  text: "Learn web development on MDN!",
  url: window.location.href,
  image: image,
};

const btn = document.querySelector(".share");
const resultPara = document.querySelector("#resultPara");

btn.addEventListener("click", async () => {
  try {
    await navigator.share(shareData);
    resultPara.textContent = "MDN shared successfully";
  } catch (err) {
    resultPara.textContent = `Error: ${err}`;
  }
});




const infoBtns = document.querySelectorAll('.information-reviews-list li a');

infoBtns.forEach((infoBtn) => {
  infoBtn.addEventListener('click', (event) => {
    const name = infoBtn.dataset.name;

    infoBtns.forEach((inf) => {
      inf.classList.remove('active');
    });

    infoBtn.classList.add('active');

    const listBoxes = document.querySelectorAll('.list-box');
    listBoxes.forEach((list) => {
      list.classList.remove('active');
      if (list.classList.contains(name)) {
        list.classList.add('active');
      }
    });
  });
});























var swiper = new Swiper(".related-products .mySwiper", {
  slidesPerView: 1,
  spaceBetween: 10,
  autoplay: {
    delay: 5000,
    disableOnInteraction: false,
  },
  // pagination: {
  //   el: ".swiper-pagination",
  //   clickable: true,
  //   dynamicBullets: true,
  // },
  breakpoints: {
    "@0.00": {
      slidesPerView: 2,
      spaceBetween: 10,
    },
    "@0.75": {
      slidesPerView: 3,
      spaceBetween: 20,
    },
    "@1.00": {
      slidesPerView: 4,
      spaceBetween: 40,
    },
    "@1.50": {
      slidesPerView: 4,
      spaceBetween: 50,
    },
  },
});






















// HOVER IMAGE
var swiper = new Swiper(".hover-image .mySwiper", {
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },

});









var itemImages = document.querySelectorAll('.related-products .image-box');

itemImages.forEach(itemImage => {
  let mainImage = itemImage.querySelector('.main-image');
  let hoverImage = itemImage.querySelector('.hover-image');
  let currentAddBtn = itemImage.querySelector('.add-btn');
  let sizeAddBtn = itemImage.querySelector('.add-size');
  let sizeBtn = itemImage.querySelectorAll('.btn-size');



  sizeBtn.forEach(sizeBtn => {
    sizeBtn.addEventListener('click', (event) => {
      BasketValue.innerText= parseInt(BasketValue.innerText)+1;
      sizeAddBtn.classList.remove('active');
    });
  });

  itemImage.addEventListener('mouseover', (event) => {
    if (event.relatedTarget.classList.contains('main-image')) {
      mainImage.classList.remove('active');
    }
    
    currentAddBtn.classList.add('active');
    hoverImage.classList.add('active');
  });

  currentAddBtn.addEventListener('click', (event) => {
    sizeAddBtn.classList.add('active');
    currentAddBtn.classList.remove('active');
    hoverImage.classList.add('active');
  });

  hoverImage.addEventListener('mouseover', (event) => {
    sizeAddBtn.classList.remove('active');
    hoverImage.classList.add('active');
  });

  itemImage.addEventListener('mouseout', (event) => {
    if (!itemImage.contains(event.relatedTarget)) {
      hoverImage.classList.remove('active');
      currentAddBtn.classList.remove('active');
      sizeAddBtn.classList.remove('active');
    }
    mainImage.classList.add('active');
  });


});


const wishBoxBtns = document.querySelectorAll('.right-box .wish-btn'); 


wishBoxBtns.forEach(wishBoxBtn => {

  wishBoxBtn.addEventListener('click', (event) => {
    let wishIcon = wishBoxBtn.querySelector('img');

    if (wishBoxBtn.classList.contains('active')) {
      wishBoxBtn.classList.remove('active');
      wishIcon.src = '/cilent/assets/icons/wish-icon.svg';
      wishCount.innerText = parseInt(wishCount.innerText) - 1;
    } else {
      wishBoxBtn.classList.add('active');
      wishIcon.src = '/cilent/assets/icons/wish-icon-active.svg';
      wishCount.innerText = parseInt(wishCount.innerText) + 1;
    }
  });
})




///////////////////////////


// var itemImages = document.querySelectorAll('.related-products .image-box');

// itemImages.forEach(itemImage => {
//   let mainImage = itemImage.querySelector('.main-image');
//   let hoverImage = itemImage.querySelector('.hover-image');
//   let currentAddBtn = itemImage.querySelector('.add-btn');
//   let sizeAddBtn = itemImage.querySelector('.add-size');

//   itemImage.addEventListener('mouseover', (event) => {
//     if (!hoverImage.classList.contains('active')) {
//       hoverImage.classList.add('active');
//       mainImage.classList.remove('active');
//     }
//     currentAddBtn.classList.add('active');
//   });

//   currentAddBtn.addEventListener('click', (event) => {
//     sizeAddBtn.classList.add('active');
//     currentAddBtn.classList.remove('active');
//   });

//   hoverImage.addEventListener('mouseover', (event) => {
//     sizeAddBtn.classList.remove('active');
 
//   });

//   itemImage.addEventListener('mouseout', (event) => {
//     if (!itemImage.contains(event.relatedTarget)) {
//       hoverImage.classList.remove('active');
//       mainImage.classList.add('active');
//       currentAddBtn.classList.remove('active');
//       sizeAddBtn.classList.remove('active');
//     }
//   });
// });





// HOVER IMAGE











// const shareData = {
//   text: "Check out this link: https://www.pinterest.com/pin/6473993209443302/",
//   image: "https://i.pinimg.com/564x/d7/c6/02/d7c60291197f2124a0879a02c5fc7607.jpg"
// };

// const btn = document.querySelector(".share");
// const resultPara = document.querySelector("#resultPara");

// btn.addEventListener("click", () => {
//   const whatsappMessage = `${shareData.text} ${shareData.image}`;
//   const whatsappUrl = `whatsapp://send?text=${encodeURIComponent(whatsappMessage)}`;
//   window.location.href = whatsappUrl;
// });


















// window.onload = function() {
//   const shareData = {
//       title: "MDN",
//       text: "Learn web development on MDN!",
//       url: 'https://www.pinterest.com/pin/6473993209443302/',
//       image: 'https://i.pinimg.com/564x/d7/c6/02/d7c60291197f2124a0879a02c5fc7607.jpg'
//   };

//   const btn = document.querySelector(".share");

//   btn.addEventListener("click", () => {
//       FB.ui({
//           method: 'share_open_graph',
//           action_type: 'og.shares',
//           version: 'v12.0',
//           appId:'1010314584091002',
//           action_properties: JSON.stringify({
//               object: {
//                   'og:title': shareData.title,
//                   'og:description': shareData.text,
//                   'og:url': shareData.url,
//                   'og:image': shareData.image
//               }
//           })
//       }, function(response){});
//   });
// };











// const shareData = {
//   title: "MDN",
//   text: "Learn web development on MDN!",
//   url: 'https://www.pinterest.com/pin/6473993209443302/',
//   image: 'https://i.pinimg.com/564x/d7/c6/02/d7c60291197f2124a0879a02c5fc7607.jpg'
// };

// const btn = document.querySelector(".share");
// const resultPara = document.querySelector("#resultPara");

// btn.addEventListener("click", () => {
//   FB.ui({
//       method: 'share_open_graph',
//       action_type: 'og.shares',
//       action_properties: JSON.stringify({
//           object: {
//               'og:title': shareData.title,
//               'og:description': shareData.text,
//               'og:url': shareData.url,
//               'og:image': shareData.image
//           }
//       })
//   }, function(response){});
// });




// const image = document.querySelector(".main-image img");
// const output = document.querySelector(".output");

// document.querySelector(".share").addEventListener("click", async () => {

//   if (!navigator.share) {
//     output.textContent = `Your browser doesn't support the Web Share API.`;
//     return;
//   }

//   try {
//     await navigator.share({
//       files: [image.src], // Assuming you want to share the source URL of the image
//       title: "Images",
//       text: "Beautiful images",
//     });
//     output.textContent = "Shared!";
//   } catch (error) {
//     output.textContent = `Error: ${error.message}`;
//   }

// });

