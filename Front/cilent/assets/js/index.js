var basaketIcon = document.querySelector('.i-basket');
var basketList = document.querySelector('.basket-list');
var basketClose = document.querySelector('.basket-close');

basaketIcon.addEventListener('click', (event) => {
    event.stopPropagation(); 
    basketList.classList.add('show');
});


basketClose.addEventListener('click', (event) => {
    event.stopPropagation(); 
    basketList.classList.remove('show');
});

document.addEventListener('click', (event) => {
    const clickedElement = event.target;
    if (!basketList.contains(clickedElement) && !basaketIcon.contains(clickedElement)) {
        basketList.classList.remove('show');
    }
});









var burgerMenu = document.querySelector('.page-list');
var burgerPageList = document.querySelector('.pages-mobile');
var burgerPageClose = document.querySelector('.pages-close');

burgerMenu.addEventListener('click', (event) => {
    event.stopPropagation(); /
    burgerPageList.classList.add('show');
});

burgerPageClose.addEventListener('click', (event) => {
    event.stopPropagation(); 
    burgerPageList.classList.remove('show');
});

document.addEventListener('click', (event) => {
    const clickedElement = event.target;
    if (!burgerPageList.contains(clickedElement) && !burgerMenu.contains(clickedElement)) {
        burgerPageList.classList.remove('show');
    }
});





var searchBtn = document.querySelector(".i-search");
var searchCloseBtn = document.querySelector(".i-search img");
var searchInput = document.querySelector('.search-list input');
var searchList = document.querySelector(".search-list ul");

let searchToggle = false;

// Dışarı tıklandığında arama kapanacak
document.addEventListener('click', (event) => {
    const isClickInsideSearch = searchBtn.contains(event.target) || searchInput.contains(event.target);
    if (!isClickInsideSearch) {
        searchList.style.display = "none";
        searchInput.style.display = "none";
        searchCloseBtn.src = "/cilent/assets/icons/search.svg"
        searchToggle = false;
    }
});

searchBtn.addEventListener('click', (event) => {
    event.stopPropagation(); /
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

window.addEventListener('scroll', function() {
    var header = document.querySelector('.header-info');
    header.classList.toggle('scrolled', window.scrollY > 0);
});








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

