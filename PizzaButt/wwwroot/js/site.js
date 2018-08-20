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


var time = new Date().getTime();
$(document.body).bind("mousemove keypress", function(e) {
    time = new Date().getTime();
});

function refresh() {
    if(new Date().getTime() - time >= 60000)
        window.location.reload(true);
    else
        setTimeout(refresh, 60000);
}

setTimeout(refresh, 60000);