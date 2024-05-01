var accountList = document.querySelectorAll('.account-list a');

accountList.forEach(cat => {
    cat.addEventListener('click', (event) => {
        event.preventDefault();

        let catName = cat.dataset.name;

        console.log(catName);

        // Remove 'active' class from all categories
        accountList.forEach(category => {
            category.classList.remove('active');
        });
        // Add 'active' class to the clicked category
        cat.classList.add('active');

        var accountContainers = document.querySelectorAll('.account-container');

        // Close all containers
        accountContainers.forEach(container => {
            container.querySelectorAll('.acc').forEach(acc => {
                acc.classList.remove('active');
            });
        });

        // Open the container associated with the clicked category
        var containerToOpen = document.querySelector('.account-container .' + catName);
        if (containerToOpen) {
            containerToOpen.classList.add('active');
        }
    });
});









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
    console.log(editName)

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

WishContainer();

function SwiperCustom() {


    var swiper = new Swiper(".wish.mySwiper", {
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
                    rows: 2,
                    fill: "row",
                }
            },
            "@0.75": {
                slidesPerView: 2,
                spaceBetween: 20,
                grid: {
                    rows: 2,
                    fill: "row",
                }
            },
            "@1.00": {
                slidesPerView: 2,
                spaceBetween: 40,
                grid: {
                    rows: 2,
                    fill: "row",
                }
            },
            "@1.50": {
                slidesPerView: 4,
                spaceBetween: 35,
                grid: {
                    rows: 2,
                    fill: "row",
                }
            },
        },
    });
}

SwiperCustom()
var swiper = new Swiper(".hover-image .mySwiper", {
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});




var itemImages = document.querySelectorAll('.item-image');

itemImages.forEach(itemImage => {
    let mainImage = itemImage.querySelector('.main-image');
    let hoverImage = itemImage.querySelector('.hover-image');
    let currentAddBtn = itemImage.querySelector('.add-btn');

    itemImage.addEventListener('mouseover', (event) => {
        if (event.target.classList.contains('main-image')) {
            hoverImage.classList.add('active');
            mainImage.classList.remove('active');
        }
        currentAddBtn.classList.add('active');
    });

    itemImage.addEventListener('mouseout', (event) => {
        if (event.target.classList.contains('main-image')) {
            hoverImage.classList.remove('active');
            mainImage.classList.add('active');
        }
        currentAddBtn.classList.remove('active');
    });
});






var productItems = document.querySelectorAll('.product-item');
var wishCnt = document.querySelector('.i-wish sup');
var basketCount = document.querySelector('.i-basket');

productItems.forEach(item => {
    var wishIconBtn = item.querySelector('.wish-icon img');
    var addToCart = item.querySelector('.add-btn a'); // Değişiklik burada: .add-btn sınıfı hedef alındı.

    wishIconBtn.addEventListener('click', (event) => {
        event.preventDefault();

        wishCnt.innerText--;

        item.remove();
        
        WishContainer();
        SwiperCustom()
    });

    addToCart.addEventListener('click', (event) => {
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

    WishContainer();

});




