//var accountList = document.querySelectorAll('.account-list a');

//accountList.forEach(cat => {
//    cat.addEventListener('click', (event) => {
//        event.preventDefault();

//        let catName = cat.dataset.name;


//        accountList.forEach(category => {
//            category.classList.remove('active');
//        });
//        // Add 'active' class to the clicked category
//        cat.classList.add('active');

//        var accountContainers = document.querySelectorAll('.account-container');

//        accountContainers.forEach(container => {
//            container.querySelectorAll('.acc').forEach(acc => {
//                acc.classList.remove('active');
//            });
//        });

//        var containerToOpen = document.querySelector('.account-container .' + catName);
//        if (containerToOpen) {
//            containerToOpen.classList.add('active');
//        }
//    });
//});









const imageFileInput = document.getElementById('image-file');
const photo = document.querySelector('.photo');
const mainImage = document.querySelector('.image img');
const imageBtn = document.getElementById('label');

photo.addEventListener('click', () => {
    imageFileInput.click();
});

imageFileInput.addEventListener('change', (event) => {
    const selectedFile = event.target.files[0];

    if (selectedFile) {
        const reader = new FileReader();

        reader.onload = function (e) {
            mainImage.src = e.target.result;
        };

        if (selectedFile.type.match('image.*')) {
            reader.readAsDataURL(selectedFile);
        } else {
            alert("Please select an image file!");
            imageFileInput.value = "";
        }
    }
});

imageBtn.addEventListener('mouseover', () => {
    photo.style.bottom = '0';

});

imageBtn.addEventListener('mouseout', () => {
    photo.style.bottom = '-150%';
});

document.querySelector('.image').addEventListener('mouseover', () => {
    photo.style.bottom = '0';
});

document.querySelector('.image').addEventListener('mouseout', () => {
    photo.style.bottom = '-150%';
});











var editBoxBtn = document.querySelectorAll('.box-edit .edit');
editBoxBtn.forEach(btn => {
    btn.addEventListener('click', (event) => {
        btn.closest('.box-edit').querySelector('.box').classList.toggle('active');
    });
});




var editBox = document.querySelectorAll('.box-edit');
editBox.forEach(box => {

    var editName = box.closest('.box-edit').querySelector('.data-title p').innerText

    box.closest('.box-edit').querySelector('input').value = editName;
});











var AdressesBox = document.querySelectorAll('.box-edit.address');

AdressesBox.forEach(box => {
    box.querySelector('.box').classList.toggle('active');

    var country = box.querySelector('.country');
    var city = box.querySelector('.city');
    var postal = box.querySelector('.postal');
    var homeAddress = box.querySelector('.home-address');

    box.querySelector('.form-b-inputs.country input').value = country.textContent.trim();
    box.querySelector('.form-b-inputs.city input').value = city.textContent.trim();
    box.querySelector('.form-b-inputs.postal input').value = postal.textContent.trim();
    box.querySelector('.form-b-inputs.homeAddress input').value = homeAddress.textContent.trim();
});








/*========== HISTORY ========== */



/*========== HISTORY ========== */








/* ========================
     WISH LIST - START
===========================*/



function WishContainer(params) {
    const swiperWrapper = document.querySelector('.swiper-wrapper');

    if (swiperWrapper && swiperWrapper.querySelector('.swiper-slide') == null) {
        const notFound = swiperWrapper.querySelector('.product-not-found');
        if (notFound) {
            notFound.classList.add('active');
        }
    }
}





























var swiper = new Swiper(".mySwiper.sub", {
    slidesPerView: 1,
    spaceBetween: 10,
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

const wishBtnP = document.querySelectorAll('.p-pric img');

const wishCount = document.querySelector('.i-wish sup');
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




// const productImageBox = document.querySelectorAll('.product-image-box');
// const basketCount = document.querySelector('.i-basket sup');

// productImageBox.forEach(prodBtn => {

//     const productAddBtn = prodBtn.querySelector('.add-btn');
//     const productAddSize = prodBtn.querySelector('.add-size');

//     const mainImage = prodBtn.querySelector('.main-image');
//     const hoverImage = prodBtn.querySelector('.hover-image');

//     productAddBtn.addEventListener('click', function (event) {
//         productAddSize.style.display = 'flex';
//     });

//     productAddSize.addEventListener('click', function (event) {
//         productAddSize.style.display = 'none';
//         basketCount.innerText = parseInt(basketCount.innerText)+1;;
//     });




//     prodBtn.addEventListener('mouseover', function (event) {
//         productAddBtn.style.display = 'flex';
//         hoverImage.style.display = 'flex';
//         mainImage.style.display = 'none';
//     });

//     prodBtn.addEventListener('mouseout', function (event) {
//         productAddBtn.style.display = 'none';
//         hoverImage.style.display = 'none';
//         mainImage.style.display = 'flex';
//     });







// });


var swiper = new Swiper(".product-image-box .mySwiper", {
    autoHeight: true,
    spaceBetween: 20,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});















const productImageBox = document.querySelectorAll('.product-image-box');
const basketCount = document.querySelector('.i-basket sup');

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
        basketCount.innerText = parseInt(basketCount.innerText) + 1;;
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
