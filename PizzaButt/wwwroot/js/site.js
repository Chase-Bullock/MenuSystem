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

$('.order-section').waypoint(function(direction) {
    $('.order-section').addClass('animated fadeIn');
}, {
    offset: '30%'
});

$('.activateNav').click(function (e) {
    e.preventDefault();

    $('.orderForm').submit();
    
});

if (window.location.pathname !== '/'){
    $('.activeNav').css("display", "flex").fadeIn();
}

if (window.location.pathname === '/'){
    $('.homeNav').css("display", "flex").fadeIn();
}


if (window.location.pathname == '/Orders/Status') {
    var time = new Date().getTime();
    $(document.body).bind("mousemove keypress", function () {
        time = new Date().getTime();
    });

    setInterval(function () {
        if (new Date().getTime() - time >= 60000) {
            window.location.reload(true);
        }
    }, 1000);
}