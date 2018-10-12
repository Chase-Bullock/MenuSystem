"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveOrder", function () {
    window.location.reload(true);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});