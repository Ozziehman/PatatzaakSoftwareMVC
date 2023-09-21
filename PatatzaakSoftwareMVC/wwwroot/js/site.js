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

//get next order number from database and load it into orderNumber var, display that var in the p1 on the CustomerPage where ordernumber should be displayed
function addToReceipt(item, itemPrice, itemId) {
    //create list of all added items
    var itemList = []
    if (itemList.includes(item)) {
        //add 1 to item quantity
    } else {
        itemList.push(item)
    }

    if (dummySpanPresent) {
        var element = document.getElementById("dummySpan");
        element.parentNode.removeChild(element);
        dummySpanPresent = false;

    }

    for (let i = 0; i < itemList.length; i++) {

        const receipt = document.getElementById("receipt")

        const newRow = document.createElement("div");
        newRow.classList.add("row");

        const productCol = document.createElement("div");
        productCol.classList.add("col-md-8")
        const textDiv = document.createElement("div");
        textDiv.textContent = itemList[i];
        productCol.appendChild(textDiv);

        const priceCol = document.createElement("div");
        priceCol.classList.add("col-md-4")
        const priceDiv = document.createElement("div");
        priceDiv.textContent = "\u20AC" + itemPrice; //let ths load from item, not from itemList in the dummy list
        priceCol.appendChild(priceDiv);



        newRow.appendChild(productCol);
        newRow.appendChild(priceCol);
        receipt.appendChild(newRow);
    }



    //add item to list
    //display list in receipt

    console.log(item + " added to receipt");
   /* sendItemToList(itemId);*/


    
}

//WORK IN PROGRESS!!!! AJAX IS NEW TO ME
/*function sendItemToList(itemId) {
    // Make an AJAX request to a server-side endpoint to add the itemId to the C# list.
    $.ajax({
        type: "POST",
        url: '/Controllers/CustomerController/AddToReceipt', // Replace with your server-side endpoint URL.
        data: { itemId: itemId },
        success: function (response) {
            // Handle the response from the server if needed.
            console.log(response);
        }
    });
}
*/

