"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

if (document.getElementById("cancelOrder")) {
    document.getElementById("cancelOrder").addEventListener("click", function (event) {
        connection.invoke("UpdateOrder").catch(function (err) {
            return console.error(err.toString());
        });
    });
}

if (document.getElementById("placeOrder") != null) {
    document.getElementById("placeOrder").addEventListener("click", function (event) {
        connection.invoke("UpdateOrder").catch(function (err) {
            return console.error(err.toString());
        });
    });
}