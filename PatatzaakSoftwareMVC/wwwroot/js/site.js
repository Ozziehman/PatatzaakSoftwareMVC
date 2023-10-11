document.addEventListener('DOMContentLoaded', () => {
  
});




var dummySpanPresent = true;
var totalPrice = document.getElementById("totalPrice");
var totalPriceToDisplay = 0;
function addToReceipt(itemName, itemPrice, itemDiscount, itemId, orderId) {
    if (dummySpanPresent) {
        var element = document.getElementById("dummySpan");
        element.parentNode.removeChild(element);
        dummySpanPresent = false;
    }
    // When an item is added, render it into the receipt: DO NOT LET THIS USE ANY IMPORTANT LOGIC TO THE DB, THIS IS PURELY FOR DISPLAY
    const receipt = document.getElementById("receipt");

    const newRow = document.createElement("div");
    newRow.classList.add("row");

    const productCol = document.createElement("div");
    productCol.classList.add("col-md-8");
    const textDiv = document.createElement("div");
    textDiv.textContent = itemName;
    productCol.appendChild(textDiv);

    const priceCol = document.createElement("div");
    priceCol.classList.add("col-md-4");
    const priceDiv = document.createElement("div");

    const itemPriceAfterDiscount = itemPrice - (itemPrice * (itemDiscount / 100));
    priceDiv.textContent = "\u20AC" + itemPriceAfterDiscount.toFixed(2);
    totalPriceToDisplay += itemPriceAfterDiscount;

    priceCol.appendChild(priceDiv);

    newRow.appendChild(productCol);
    newRow.appendChild(priceCol);
    receipt.appendChild(newRow);
    

    totalPrice.innerHTML = "\u20AC" + totalPriceToDisplay.toFixed(2);

    sendItemToList(itemId, orderId);
}

//Send item to the list of items in the order AJAX
function sendItemToList(itemId, orderId) {
    // Make an Ajax request to a server-side endpoint to add the itemId to the C# list.
    $.ajax({
        type: "POST",
        url: '/Customer/FillOrderWith',
        data: { itemId: itemId, orderId: orderId },
        success: function (response) {
            console.log(response);
        }
    });
}

//Place the order and updat it in the databse and update the UI accordingly AJAX
function PlaceOrderButtonClicked(orderId) {
    $.ajax({
        type: "POST",
        url: '/Customer/PlaceOrder',
        data: {
            orderId: orderId,
            voucherId: document.getElementById("voucherDropdown").value
        },
        success: function (response) {
            console.log(response);
            var button = document.getElementById("placeOrderButton");
            button.disabled = true;
            button.style.display = "none";

            var message = document.getElementById("messageDiv");
            message.style.display = "block";

            var menu = document.getElementById("menuColumn");
            menu.style.display = "none";

            var pointsDisplay = document.getElementById("pointsDisplay");
            pointsDisplay.innerHTML = "Points: " + response.currentPoints;

        }
    });
}

//Revert the status of an order to "Placed" AJAX
function RevertToPlaced(orderId) {
    $.ajax({
        type: "POST",
        url: '/OrderHistory/RevertToPlaced',
        data: {
            orderId: orderId,
        },
        success: function (response) {
            console.log(response);
            handleSuccessResponse(response);
        }
    });
}


//Handle success response from AJAX, it will refresh the page if refreshPage = true is returned
function handleSuccessResponse(response) {
    if (response.success) {
        console.log(response.message);

        if (response.refreshPage) {
            // Refresh page if refreshpage = rue is returned
            location.reload(true);
        }
    } else {
        // Handle failure
        console.error(response.message);
    }
}

//Ready the order for pickup AJAX
function ReadyOrder(orderId) {
    $.ajax({
        type: "POST",
        url: '/Company/ReadyOrder',
        data: { orderId: orderId },
        success: function (response) {
            console.log(response);
            handleSuccessResponse(response);
        }
    });
}

//Complete the order AJAX
function CompleteOrder(orderId) {
    $.ajax({
        type: "POST",
        url: '/Company/CompleteOrder',
        data: { orderId: orderId },
        success: function (response) {
            console.log(response);
            handleSuccessResponse(response);
        }
    });
}

//Buy a voucher AJAX
function buyVoucher(voucherPercentage, voucherCode, userId) {
    $.ajax({
        type: "POST",
        url: '/VoucherStore/buyVoucher',
        data: {
            voucherPercentage: voucherPercentage,
            voucherCode: voucherCode,
            userId : userId
        },
        success: function (response) {
            console.log(response);
            if (response.success) {
                var voucherContainer = document.getElementById("voucherContainer");
                voucherContainer.style.display = "none";
                var buyMessage = document.getElementById("buyMessage");
                buyMessage.style.display = "block";

                var pointsDisplay = document.getElementById("pointsDisplay");
                pointsDisplay.innerHTML = "Points: " + response.currentPoints;
            }
            else {
                var voucherContainer = document.getElementById("voucherContainer");
                var pointsLabel = document.getElementById("currentPoints");
                pointsLabel.innerHTML = response.currentPoints;
                voucherContainer.style.display = "none";
                var buyMessage = document.getElementById("failMessage");
                buyMessage.style.display = "block";
            }
            
  
        }
    });
}


///Dropdown hide
function showDropdown(orderId) {
    var dropdownContent = document.getElementById("dropdownContent-" + orderId);
    if (dropdownContent) {
        dropdownContent.style.display = "block";
    }
}

//Dropdown hide
function hideDropdown(orderId) {
    var dropdownContent = document.getElementById("dropdownContent-" + orderId);
    if (dropdownContent) {
        dropdownContent.style.display = "none";
    }
}