"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("OnConnected", function () {
    OnConnected();
});

connection.on("ReceivedNotification", function (message) {
    // alert(message);
    DisplayGeneralNotification(message, 'General Message');
});
