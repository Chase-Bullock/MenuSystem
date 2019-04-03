//////////////////////////// PRINT ORDER /////////////////////////////////

function PrintDiv(id, count) {
    var orders = [];
    var customerName = document.getElementById(id + "-customerName");
    var orderTime = document.getElementById(id + "-orderTime");
    var orderCommunity = document.getElementById(id + "-orderCommunity");
    var orderAddress = document.getElementById(id + "-orderAddress");
    var orderNote = document.getElementById(id + "-orderNote");
    for (var i=1; i < count; i++) {
        orders.push(document.getElementById(id +"-order-"+i))
    }
    var arr = [...orders];
    var orderItems = arr.map(i => { return i.innerText })
    console.log(orderItems);
    var popupWin = window.open('', '_blank', 'width=800,height=800,location=no,left=200px');
    popupWin.document.open();
    popupWin.document.write('<html><body onload="window.print()"> <div style="text-align:center"><div> <span style="font-weight:600">Order Number:</span> ' + id + '</div><div> <span style="font-weight:600">Name:</span> ' + customerName.innerHTML +
        '</div><div> <span style="font-weight:600">Address:</span>' + orderAddress.innerHTML + '</div><div> <span style="font-weight:600">Community:</span> ' + orderCommunity.innerHTML + '</div> <div> <span style="font-weight:600">Order:</span>' + orderItems.map(i => { return '<div>' + i + '</div>' }).join("")
        + '</div><div> <span style="font-weight: 600">Note:</span> ' + orderNote.innerHTML + '</div> <div>  <span style="font-weight:600">Order Time:</span> ' + orderTime.innerHTML + '</div> </html>');
    popupWin.document.close();

}