"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//var audioElement = document.createElement('audio');
var audio = new Audio('https://notificationsounds.com/soundfiles/37f0e884fbad9667e38940169d0a3c95/file-sounds-1076-appointed.mp3');

window.onload = function () {
    var context = new AudioContext();
}

connection.start().catch(function (err) {
  return console.error(err.toString());
});

connection.onclose(function () {
    setTimeout(function () {
        connection.start();
    }, 5000); // Restart connection after 5 seconds.
});

connection.on("ReceiveOrder", function () {
    audio.play();
    window.setTimeout(function () {
        window.location.reload(true);
    }, 5000);
});

var buttons = document.getElementsByClassName("statusChangeButton");

for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function (event) {
        connection.invoke("UpdateStatus").catch(function (err) {
            return console.error(err.toString());
        });
    });
}