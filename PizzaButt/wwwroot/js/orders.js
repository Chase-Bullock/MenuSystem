"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().catch(function (err) {
  return console.error(err.toString());
});

connection.on("ReceiveOrder", function () {
    window.setTimeout(function () { window.location.reload(true); }, 5000);
});

var buttons = document.getElementsByClassName("statusChangeButton");

for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function (event) {
        connection.invoke("UpdateStatus").catch(function (err) {
            return console.error(err.toString());
        });
    });
}