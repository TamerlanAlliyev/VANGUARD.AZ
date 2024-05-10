document.addEventListener("DOMContentLoaded", function() {
    var addressLink = document.getElementById("addressLink");
    var iframe = document.querySelector("iframe");

    addressLink.addEventListener("click", function(event) {
        event.preventDefault();
        iframe.src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48613.5977531735!2d49.819509220986006!3d40.40109968039558!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40307d6bd6211cf9%3A0x343f6b5e7ae56c6b!2zQmFrxLE!5e0!3m2!1saz!2saz!4v1714147771064!5m2!1saz!2saz";
    });
});
