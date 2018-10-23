"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveStatus", function () {
    window.setTimeout(function () { window.location.reload(true); }, 5000);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});