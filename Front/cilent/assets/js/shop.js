// const filterContainer = document.querySelector('.filter');
// const productContainer = document.querySelector('.product-container');

// productContainer.addEventListener('scroll', () => {
//   // Calculate how much to translate the filter container based on how far 
//   // the product container has been scrolled
//   const translationY = Math.min(0, productContainer.scrollTop);

//   // Apply the calculated translation to the filter container
//   filterContainer.style.transform = `translateY(${translationY}px)`;
// });






const filterList = document.querySelectorAll('.filter-box');

filterList.forEach(filter => {
    const toggleBtn = filter.querySelector('.box-head');

    toggleBtn.addEventListener('click', function (event) {
        filter.classList.toggle('active');
        if (filter.classList.contains('active')) {
            toggleBtn.style.borderBottom = '1.5px solid #fff';
        } else {
            toggleBtn.style.borderBottom = 'none';
        }
    });



    const subCat = filter.querySelectorAll('.sub-category.full');

    subCat.forEach(cat => {
        const toggBtn = cat.querySelector('.open-close');
        const catName = cat.querySelector('.parent-cat');
        const inputs = cat.querySelectorAll('input[type="checkbox"]');


        toggBtn.addEventListener('click', function (event) {
            cat.classList.toggle('active');
            if (cat.classList.contains('active')) {
                catName.style.borderBottom = '0.5px solid #ffffffb2';
            } else {
                catName.style.borderBottom = 'none';
            }




            inputs.forEach(input => {

                toggleBtn.addEventListener('click', function (event) {
                    if (input.checked) {
                        cat.classList.add('active');
                        catName.style.borderBottom = '0.5px solid #ffffffb2';
                    } else {
                    }
                });

            });




        });





    });

});















// COLORS

const colorbox = document.querySelectorAll('.filter-container .color-box');

colorbox.forEach(color => {
    const colorName = color.querySelector('.color-name');
    const col = color.querySelector('.color');


    colorName.addEventListener('click', function (event) {
        if (!color.classList.contains('active')) {
            col.style.border = '2px solid #d90000';
            color.classList.add('active')
        } else {
            col.style.border = '2px solid transparent';
            color.classList.remove('active')
        }

    });


    col.addEventListener('click', function (event) {
        if (!color.classList.contains('active')) {
            col.style.border = '2px solid #d90000';
            color.classList.add('active')
        } else {
            col.style.border = '2px solid transparent';
            color.classList.remove('active')
        }

    });

    colorName.addEventListener('mouseover', function (event) {
        col.style.border = '2px solid #fff';
    });

    colorName.addEventListener('mouseout', function (event) {
        if (color.classList.contains('active')) {
            col.style.border = '2px solid #d90000';
        } else {
            col.style.border = '2px solid transparent';
        }

    });


    col.addEventListener('mouseover', function (event) {
        col.style.border = '2px solid #fff';
    });

    col.addEventListener('mouseout', function (event) {
        if (color.classList.contains('active')) {
            col.style.border = '2px solid #d90000';
        } else {
            col.style.border = '2px solid transparent';
        }

    });
});


// SIZE

const sizebox = document.querySelectorAll('.filter-container.size .size-box');

sizebox.forEach(size => {
    const sizeBtn = size.querySelector('span');

    sizeBtn.addEventListener('click', function (event) {
        size.classList.toggle('active');

        if (size.classList.contains('active')) {
            sizeBtn.style.color = '#fff';
            sizeBtn.style.border = '1.5px solid #fff';
            sizeBtn.style.background = '#d90000';
        } else {
            sizeBtn.style.color = '#fff';
            sizeBtn.style.border = '1.5px solid #fff';
            sizeBtn.style.background = 'transparent';
        }
    });

    sizeBtn.addEventListener('mouseover', function (event) {
        if (!size.classList.contains('active')) {
            sizeBtn.style.color = '#000';
            sizeBtn.style.border = '1.5px solid #fff';
            sizeBtn.style.background = '#fff';
        }
    });

    sizeBtn.addEventListener('mouseout', function (event) {
        if (!size.classList.contains('active')) {
            sizeBtn.style.color = '#fff';
            sizeBtn.style.border = '1.5px solid #fff';
            sizeBtn.style.background = 'transparent';
        }
    });
});






























// SEARCH TAGS


/* =========== .searchs-container - Start ========== */


document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById("searchInput");
    var searchResults = document.getElementById("searchResults");

    searchInput.addEventListener("input", function () {
        searchResults.style.display = "flex";
    });

    document.addEventListener("click", function (event) {
        var isClickInside = searchResults.contains(event.target) || searchInput.contains(event.target);
        if (!isClickInside) {
            searchResults.style.display = "none";
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var tagElements = document.querySelectorAll(".srch-tag a");
    tagElements.forEach(function (tagElement) {
        var text = tagElement.textContent.trim();
        if (text.length > 20) {
            var truncatedText = text.substring(0, 18) + " ...";
            tagElement.textContent = truncatedText;
        }
    });
});















/* =========== .searchs-container - End ========== */


/* =========== TAGS - End ========== */



const searchbtn = document.querySelectorAll('.searchs-container #searchResults li');
const tagList = document.querySelector('.searchs-container .selected-tags-list');

searchbtn.forEach(search => {
    let searchText = "";
    search.addEventListener('click', function (event) {
        searchText = search.innerText.replace('# ', '').trim();

        tagList.innerHTML += `
            <li>
                <p>${searchText}</p>
                <i class="fa-solid fa-xmark"></i>
            </li>`;
    });
});

tagList.addEventListener('click', function (event) {
    if (event.target.classList.contains('fa-solid') && event.target.classList.contains('fa-xmark')) {
        const tag = event.target.closest('li');
        tag.remove();
    }
});

/* =========== TAGS - End ========== */









const rangevalue =
    document.querySelector(".slider-container .price-slider");
const rangeInputvalue =
    document.querySelectorAll(".range-input input");

let priceGap = 500;

const priceInputvalue =
    document.querySelectorAll(".price-input input");
for (let i = 0; i < priceInputvalue.length; i++) {
    priceInputvalue[i].addEventListener("input", e => {

        let minp = parseInt(priceInputvalue[0].value);
        let maxp = parseInt(priceInputvalue[1].value);
        let diff = maxp - minp

        if (minp < 0) {
            alert("minimum price cannot be less than 0");
            priceInputvalue[0].value = 0;
            minp = 0;
        }

        if (maxp > 10000) {
            alert("maximum price cannot be greater than 10000");
            priceInputvalue[1].value = 10000;
            maxp = 10000;
        }

        if (minp > maxp - priceGap) {
            priceInputvalue[0].value = maxp - priceGap;
            minp = maxp - priceGap;

            if (minp < 0) {
                priceInputvalue[0].value = 0;
                minp = 0;
            }
        }

        if (diff >= priceGap && maxp <= rangeInputvalue[1].max) {
            if (e.target.className === "min-input") {
                rangeInputvalue[0].value = minp;
                let value1 = rangeInputvalue[0].max;
                rangevalue.style.left = `${(minp / value1) * 100}%`;
            }
            else {
                rangeInputvalue[1].value = maxp;
                let value2 = rangeInputvalue[1].max;
                rangevalue.style.right =
                    `${100 - (maxp / value2) * 100}%`;
            }
        }
    });

    for (let i = 0; i < rangeInputvalue.length; i++) {
        rangeInputvalue[i].addEventListener("input", e => {
            let minVal =
                parseInt(rangeInputvalue[0].value);
            let maxVal =
                parseInt(rangeInputvalue[1].value);

            let diff = maxVal - minVal

            if (diff < priceGap) {

                if (e.target.className === "min-range") {
                    rangeInputvalue[0].value = maxVal - priceGap;
                }
                else {
                    rangeInputvalue[1].value = minVal + priceGap;
                }
            }
            else {

                priceInputvalue[0].value = minVal;
                priceInputvalue[1].value = maxVal;
                rangevalue.style.left =
                    `${(minVal / rangeInputvalue[0].max) * 100}%`;
                rangevalue.style.right =
                    `${100 - (maxVal / rangeInputvalue[1].max) * 100}%`;
            }
        });
    }
}







// =====================================
const closeFilter = document.querySelector('.close-filter');
const filterContainer = document.querySelector('.sections.filter');


closeFilter.addEventListener('click', function (event) {
    filterContainer.style.display = 'none';;
});





function toggleFilter() {
    filterContainer.classList.toggle('open');
    if (filterContainer.classList.contains('open')) {
        filterContainer.style.display = 'flex';
    } else {
        filterContainer.style.display = 'none';
    }
}









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


var swiper = new Swiper(".mySwiper", {
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





    // prodBtn.addEventListener('mouseover', function (event) {
    //     productAddBtn.style.display = 'flex';
    //     hoverImage.style.display = 'flex';
    //     mainImage.style.display = 'none';
    // });

    // prodBtn.addEventListener('mouseout', function (event) {
    //     productAddBtn.style.display = 'none';
    //     hoverImage.style.display = 'none';
    //     mainImage.style.display = 'flex';
    // });





});

// =====================================
//          PRODUCT ITEMS
// =====================================

















// =====================================
//          CLICK PROBLEMS - 1
// =====================================

// const searchbtn = document.querySelectorAll('.searchs-container #searchResults li');
// const tagList = document.querySelector('.searchs-container .selected-tags-list');

// searchbtn.forEach(search => {
//     let searchText = "";
//     search.addEventListener('click', function (event) {
//         searchText = search.innerText.replace('# ', '').trim();

//         tagList.innerHTML += `
//             <li>
//                 <p>${searchText}</p>
//                 <i class="fa-solid fa-xmark"></i>
//             </li>`;
//     });
// });

// // Event Delegation for close buttons
// tagList.addEventListener('click', function(event) {
//     if (event.target.classList.contains('fa-solid') && event.target.classList.contains('fa-xmark')) {
//         const tag = event.target.closest('li');
//         tag.remove();
//     }
// });


// =====================================
//          CLICK PROBLEMS - 2
// =====================================

// const searchbtn = document.querySelectorAll('.searchs-container #searchResults li');
// let tagList = document.querySelector('.searchs-container .selected-tags-list');
// let tags = document.querySelectorAll('.searchs-container .selected-tags-list li');

// searchbtn.forEach(search => {
//     let searchText = "";
//     search.addEventListener('click', function (event) {
//         searchText = search.innerText.replace('# ', '').trim();
//         tagList.innerHTML += ` <li>
//         <p>${searchText}</p>
//             <i class="fa-solid fa-xmark"></i>
//         </li>`;
//         // Yeni eklenen etiketi 'tags' değişkenine ekle
//         tags = document.querySelectorAll('.searchs-container .selected-tags-list li');
//         // Her etiket oluşturulduğunda close butonuna tıklanabilmesi için event listener ekle
//         tags.forEach(tag => {
//             const closeBtn = tag.querySelector('i');
//             closeBtn.addEventListener('click', function (event) {
//                 tag.remove();
//             });
//         });
//     });
// });