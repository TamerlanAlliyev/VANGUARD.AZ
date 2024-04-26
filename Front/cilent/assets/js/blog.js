window.addEventListener('scroll', function () {
    var header = document.querySelector('.header-info');
    header.classList.toggle('scrolled', window.scrollY > 0);
  });
  
  