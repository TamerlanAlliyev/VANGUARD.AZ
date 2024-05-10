window.addEventListener('scroll', function () {
  var header = document.querySelector('.header-info');
  header.classList.toggle('scrolled', window.scrollY > 0);
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








const passInput = document.getElementById('register-pass');
const confirmPassInput = document.getElementById('register-confirm-password');
const showHideIcons = document.querySelectorAll('.register-eye');

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
