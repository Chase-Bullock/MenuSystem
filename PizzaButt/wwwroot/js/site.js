// Write your JavaScript code.

$('#menuSelect').on('change', function(e) {
    var val = $(this).val();
    console.log(val);
    if (val === 'Taco') {
        $('#toppingsForPizza').fadeOut();
        $('#sizesForPizza').fadeOut();
        $('#sauceForTaco').fadeIn();
        $('#labelForTaco').fadeIn();
        $('#extrasForTaco').fadeIn();
    }
    else if (val === 'Pizza') {
        $('#sauceForTaco').fadeOut();
        $('#labelForTaco').fadeOut();
        $('#extrasForTaco').fadeOut();
        $('#toppingsForPizza').fadeIn();
        $('#sizesForPizza').fadeIn();
    }
    else {
        $('#sauceForTaco').fadeOut();
        $('#labelForTaco').fadeOut();
        $('#extrasForTaco').fadeOut();
        $('#toppingsForPizza').fadeOut();
        $('#sizesForPizza').fadeOut();
    }
});

$('.js--scroll-to-order').click(function () {
    $('html, body').animate({scrollTop: $('.js--order-section').offset().top}, 1000);
});

$('.activateNav').click(function (e) {
    e.preventDefault();

    $('.orderForm').submit();
    
})

if (window.location.pathname != '/'){
    $('.activeNav').css("display", "flex");
    $('.activeNav').fadeIn();
}
