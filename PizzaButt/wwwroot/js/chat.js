"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("activateNav").addEventListener("click", function (event) {
    event.preventDefault();
    $('.orderForm').submit();
    connection.invoke("UpdateOrder").catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("activateNav").addEventListener("click", function (event) {
    event.preventDefault();
    $('.checkoutForm').submit();
    connection.invoke("UpdateOrder").catch(function (err) {
        return console.error(err.toString());
    });
});