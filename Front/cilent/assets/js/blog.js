window.addEventListener('scroll', function () {
  var header = document.querySelector('.header-info');
  header.classList.toggle('scrolled', window.scrollY > 0);
});





var trendyCats = document.querySelectorAll('.blog nav ul a');
// var trendySlider = document.querySelectorAll('.trendy .swiper');

trendyCats.forEach(cat => {
  cat.addEventListener('click', (event) => {
    event.preventDefault();

    let catName = cat.dataset.name;

    console.log(catName);
    trendyCats.forEach(category => {
      category.classList.remove('active');
    });
    cat.classList.add('active');



    // trendySlider.forEach(slider => {
    //   slider.classList.remove('active');
    //   if (slider.classList.contains(catName)) {
    //     slider.classList.add('active');
    //   }
    // });

  });
});




/* =========== .searchs-container - Start ========== */
document.addEventListener("DOMContentLoaded", function () {
  var searchInput = document.getElementById("searchInput");
  var searchResults = document.getElementById("searchResults");

  // Inputa yazıldığında
  searchInput.addEventListener("input", function () {
    searchResults.style.display = "flex";
  });

  // Input dışına tıklanınca
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


// document.addEventListener("DOMContentLoaded", function() {
//   var searchbtn = document.querySelector('.blog-searchs .searchs-btn');
//   var searchIcon = document.querySelector('.blog-searchs .searchs-btn i');

//   searchbtn.addEventListener('click', function(event) {
//       if (searchIcon.classList.contains('fa-magnifying-glass')) {
//           searchIcon.classList.remove('fa-magnifying-glass');
//           searchIcon.classList.add('fa-xmark');
//       } else if (searchIcon.classList.contains('fa-xmark')) {
//           searchIcon.classList.remove('fa-xmark');
//           searchIcon.classList.add('fa-magnifying-glass');
//       }
//   });
// });

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


