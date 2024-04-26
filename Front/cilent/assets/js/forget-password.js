window.addEventListener('scroll', function () {
    var header = document.querySelector('.header-info');
    header.classList.toggle('scrolled', window.scrollY > 0);
  });
  
  
  const passInput = document.getElementById('forget-password-pass');
  const confirmPassInput = document.getElementById('forget-password-confirm-password');
  const showHideIcons = document.querySelectorAll('.forget-password-eye');
  
  showHideIcons.forEach(eyeIcon => {
    eyeIcon.addEventListener('click', () => {
      const passwordInput = eyeIcon.previousElementSibling.previousElementSibling;
  
      if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        eyeIcon.classList.add('fa-eye-slash');
        eyeIcon.classList.remove('fa-eye');
      } else {
        passwordInput.type = 'password';
        eyeIcon.classList.remove('fa-eye-slash');
        eyeIcon.classList.add('fa-eye');
      }
    });
  });