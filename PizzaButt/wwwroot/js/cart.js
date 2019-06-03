$(function () {

    $("#placeOrder").click(function (e) {
        // Stop the normal navigation
        e.preventDefault();
        //Build the new URL
        var url = $(this).attr("href");
        var note = $("#note").val();
        url = url.replace("replacethis", note);

        //Navigate to the new URL
        window.location.href = url;

    });
});