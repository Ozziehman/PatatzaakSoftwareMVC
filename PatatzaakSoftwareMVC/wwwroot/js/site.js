document.addEventListener('DOMContentLoaded', () => {
    //make this following part load from db instead of dummy item

    //rewrite this to menu items
});

/*var itemList = ["friet", "Frikandel", "Banaan", "Gehaktbal"]; //Test itemList this should come for db/C#API

for (let i = 0; i < itemList.length; i++) {
    CreateMenuItem(itemList[i]);
}

function CreateMenuItem(itemName, itemPrice = "9.000.000", imagePath = "../Resources/Images/placeholder.jpg") {

    const DivContainer = document.getElementById("menuColumn");

    const newDiv = document.createElement("div");
    newDiv.classList.add("col-md-4", "text-center", "itemCol", "p-5");
    newDiv.style.display = "inline-block";

    const newImage = document.createElement("img");
    newImage.id = "itemImage";
    newImage.src = imagePath;
    newImage.classList.add("mw-100", "h-auto");

    const newButton = document.createElement("a");
    newButton.classList.add("btn");
    newButton.text = itemName
    newButton.onclick = function () { addToReceipt(itemName) };


    newDiv.appendChild(newImage);
    newDiv.appendChild(newButton);
    DivContainer.appendChild(newDiv);

}*/


var dummySpanPresent = true;

var itemList = [];  



function addToReceipt(itemName, itemPrice, itemDiscount, itemId, orderId) {
    // Create list of all added items
    
    itemList.push(itemName);
    

    if (dummySpanPresent) {
        var element = document.getElementById("dummySpan");
        element.parentNode.removeChild(element);
        dummySpanPresent = false;
    }

    var totalPrice = document.getElementById("totalPrice");
    var totalPriceToDisplay = 0;

    // When an item is added, render it into the receipt: DO NOT LET THIS USE ANY IMPORTANT LOGIC TO THE DB, THIS IS PURELY FOR DISPLAY
    const receipt = document.getElementById("receipt");

    receipt.innerHTML = '';

    for (let i = 0; i < itemList.length; i++) {
        const newRow = document.createElement("div");
        newRow.classList.add("row");

        const productCol = document.createElement("div");
        productCol.classList.add("col-md-8");
        const textDiv = document.createElement("div");
        textDiv.textContent = itemList[i];
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
    }

    totalPrice.innerHTML = "\u20AC" + totalPriceToDisplay.toFixed(2);

    // Display list in receipt
    console.log(itemList.join(", ") + " added to receipt");
    sendItemToList(itemId, orderId);
}
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


function PlaceOrderButtonClicked(orderId) {
    $.ajax({
        type: "POST",
        url: '/Customer/PlaceOrder',
        data: { orderId: orderId },
        success: function (response) {
            console.log(response);
            var button = document.getElementById("placeOrderButton");
            button.disabled = true;
            button.style.display = "none";

            var message = document.getElementById("messageDiv");
            message.style.display = "block";

            var menu = document.getElementById("menuColumn");
            menu.style.display = "none";
        }
    });
}


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


function showDropdown(orderId) {
    var dropdownContent = document.getElementById("dropdownContent-" + orderId);
    if (dropdownContent) {
        dropdownContent.style.display = "block";
    }
}

function hideDropdown(orderId) {
    var dropdownContent = document.getElementById("dropdownContent-" + orderId);
    if (dropdownContent) {
        dropdownContent.style.display = "none";
    }
}