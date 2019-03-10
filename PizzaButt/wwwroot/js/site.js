// Write your JavaScript code.


//////////////////////////// MENUITEM SELECT ////////////////////////////

$('#menuSelect').on('change', function(e) {
    var val = $(this).val();
    console.log(val);
    $('.toppings').prop('checked', false);
    if (val === 'Taco') {
        $('#toppingsForPizza').fadeOut();
        $('#sizesForPizza').fadeOut();
        $('#extraSauceForOthers').fadeOut();
        $('#extraForOthers').fadeOut();
        $('#sauceForTaco').fadeIn();
        $('#labelForTaco').fadeIn();
        $('#extrasForTaco').fadeIn();
    }
    else if (val === 'Pizza') {
        $('#sauceForTaco').fadeOut();
        $('#labelForTaco').fadeOut();
        $('#extrasForTaco').fadeOut();
        $('#extraSauceForOthers').fadeOut();
        $('#extraForOthers').fadeOut();
        $('#toppingsForPizza').fadeIn();
        $('#sizesForPizza').fadeIn();
    }
    else if (val.length > 0) {
        $('#sauceForTaco').fadeOut();
        $('#labelForTaco').fadeOut();
        $('#extrasForTaco').fadeOut();
        $('#toppingsForPizza').fadeOut();
        $('#sizesForPizza').fadeOut();
        $('#extraSauceForOthers').fadeIn();
        $('#extraForOthers').fadeIn();
    }
    else {
        $('#sauceForTaco').fadeOut();
        $('#labelForTaco').fadeOut();
        $('#extrasForTaco').fadeOut();
        $('#extraSauceForOthers').fadeOut();
        $('#extraForOthers').fadeOut();
        $('#toppingsForPizza').fadeOut();
        $('#sizesForPizza').fadeOut();
    }
});

//////////////////////////// SCROLL TO ORDER ////////////////////////////

$('.js--scroll-to-order').click(function () {
    $('html, body').animate({scrollTop: $('.js--order-section').offset().top}, 1000);
});

$('.order-section').waypoint(function(direction) {
    $('.order-section').addClass('animated fadeIn');
}, {
    offset: '30%'
});

//////////////////////////// CHANGE HEADER ////////////////////////////

if (window.location.pathname !== '/'){
    $('.activeNav').css("display", "flex").fadeIn();
}

if (window.location.pathname === '/'){
    $('.homeNav').css("display", "flex").fadeIn();
}


//////////////////////////// COMMENT / SPECIAL INSTRUCTIONS FOR ORDER ////////////////////////////

//$('textarea').keyup(function () {
//    var tlength = $(this).val().length;
//    $(this).val($(this).val().substring(0, 355));
//    remain = maxchars - parseInt(tlength);
//    $('#remain').text(remain);
//});

//////////////////////////// SUBMIT ORDER ////////////////////////////

// $('.activateNav').click(function (e) {
//     e.preventDefault();
//
//     $('.orderForm').submit();
//
// });
//


//////////////////////////// OLD WAY TO REFRESH PAGE(NEW WAY NOT IMPLEMENTED) ////////////////////////////

//if (window.location.pathname === '/Orders/Status') {
//    var time = new Date().getTime();
//    $(document.body).bind("mousemove keypress", function () {
//        time = new Date().getTime();
//    });

//    setInterval(function () {
//        if (new Date().getTime() - time >= 12000) {
//           window.location.reload(true);
//        }
//    }, 1000);
//}
