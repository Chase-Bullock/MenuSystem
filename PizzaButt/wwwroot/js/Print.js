//////////////////////////// PRINT ORDER /////////////////////////////////

function PrintDiv(id, count) {
    var customerName = document.getElementById(id + "-customerName");
    var orders = [];
    for (var i=1; i < count; i++) {
        orders.push(document.getElementById(id +"-order-"+i))
    }
    var arr = [...orders];
    console.log(orders + "orders");
    console.log(arr);
    var orderTime = document.getElementById(id + "-orderTime");
    var popupWin = window.open('', '_blank', 'width=800,height=800,location=no,left=200px');
    popupWin.document.open();
    popupWin.document.write('<html><body onload="window.print()"> <div><div> Order Number: ' + id + '</div> Name: ' + customerName.innerHTML + '</div><div> Order: <div>'
        + arr.map(i => { return i.innerHTML.replace('p','') }) + '</div> </div> <div> Order Time: ' + orderTime.innerHTML + '</div> </html>');
    popupWin.document.close();

}