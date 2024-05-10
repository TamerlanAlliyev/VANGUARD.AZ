var swiper = new Swiper(".mySwiper.blogs-swp", {
  slidesPerView: 1,
  spaceBetween: 10,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
    dynamicBullets: true,
  },
  breakpoints: {
    "@0.00": {
      slidesPerView: 1,
      spaceBetween: 10,
    },
    "@0.75": {
      slidesPerView: 2,
      spaceBetween: 20,
    },
    "@1.00": {
      slidesPerView: 2,
      spaceBetween: 40,
    },
    "@1.50": {
      slidesPerView: 2,
      spaceBetween: 50,
    },
  },
});






window.addEventListener('scroll', function () {
  var header = document.querySelector('.header-info');
  header.classList.toggle('scrolled', window.scrollY > 0);
});









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
  var blogInfoElements = document.querySelectorAll(".blog-info");
  blogInfoElements.forEach(function (blogInfoElement) {
    var h3Element = blogInfoElement.querySelector("h3");
    if (h3Element) {
      var text = h3Element.textContent.trim();
      if (text.length > 20) {
        var truncatedText = text.substring(0, 35) + " ...";
        h3Element.textContent = truncatedText;
      }
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


document.addEventListener("DOMContentLoaded", function () {
  var blogElements = document.querySelectorAll(".blog-box-info a");
  blogElements.forEach(function (blogElement) {
    var text = blogElement.textContent.trim();
    if (text.length > 30) {
      var truncatedText = text.substring(0, 34) + " ...";
      blogElement.textContent = truncatedText;
    }
  });
});





/* Search Video Play */

var searchLists = document.querySelectorAll(".srch-blog");

searchLists.forEach(function (searchList) {
  searchList.addEventListener("mouseover", function (event) {
    var video = searchList.querySelector("video");
    if (video) {
      video.play();
    }
  })

  searchList.addEventListener("mouseleave", function (event) {
    var video = searchList.querySelector("video");
    if (video) {
      video.pause();
    }
  })
});






/* =========== .searchs-container - End ========== */



var searchbtn = document.querySelector('.blog-container .searchs-btn');
var searchClose = document.querySelector('.blog-container .searchs-close-btn');
var blogSearchsMain = document.querySelector('.blog-searchs');

searchbtn.addEventListener('click', function (event) {
  console.log('click');
  blogSearchsMain.style.display = 'block';
  blogSearchsMain.style.top = '50%';
  blogSearchsMain.style.left = '50%';
  blogSearchsMain.style.transform = 'translate(-50%, -50%)';
});

searchClose.addEventListener('click', function (event) {
  console.log('click');
  blogSearchsMain.style.display = 'none';
});








var replies = document.querySelectorAll('.reply');
replies.forEach(function (reply) {
  reply.addEventListener('click', function (event) {
    console.log('click');

    // .reply öğesinin üst öğesini seçiyoruz
    var parentComment = event.currentTarget.closest('.com-user-data');

    // Eğer .reply-comment form içinde ise, active class'ını değiştir
    var commentBox = parentComment.querySelector('.reply-comment form');
    if (commentBox) {
      commentBox.classList.toggle('active');
    }
  });
});
