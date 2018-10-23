"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveOrder", function () {
    window.setTimeout(function () { window.location.reload(true); }, 5000);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

var buttons = document.getElementsByClassName("statusChangeButton");
console.log(buttons)
for (var i = 0; i < buttons.length; i++) {
    console.log("here")
    buttons[i].addEventListener("click", function (event) {
        event.preventDefault();
        console.log("clicked3");
        connection.invoke("UpdateStatus").catch(function (err) {
            return console.error(err.toString());
        });
    });
}