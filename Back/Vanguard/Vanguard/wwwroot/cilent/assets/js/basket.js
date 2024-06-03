//const prodListItem = document.querySelectorAll('.cart-table-container li');
//const subTotal = document.querySelector('.cart-info-box.subtotal span');
//const discountAmount = document.querySelector('.cart-info-box.discount span');
//const cartTotal = document.querySelector('.cart-total span');
//const cartTotalItem = document.querySelector('.cart-info-box.items span');
//const cartTotalHeadItem = document.querySelector('.cart-title span');


//let totalPrice = 0;
//let disAmount = 0;
//let total = 0;
//let totalItem = 0;


//prodListItem.forEach(items => {
//    const input = items.querySelector('input[type="number"]');
//    const plus = items.querySelector('.plus');
//    const minus = items.querySelector('.minus');
//    var itemTotal = items.querySelector('.cart-prod-total.item .tot-price');
//    var DeleteBtn = items.querySelector('.item-delete');
//    const sellPrice = items.querySelector('#sell-price');
//    const disPrice = items.querySelector('.dis-price');


//    let itemTotalfixed = itemTotal.innerText.replace('$', '').trim();
//    let currentQuantity = parseFloat(input.value);
//    let sellPricefixed = sellPrice.innerText.replace('$', '').trim();



//    itemTotal.innerText = '$' + `${(input.value) * parseFloat(sellPricefixed)}`;
//    if (disPrice != null) {
//        let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//        totalPrice += disPricefixed * input.value
//    } else {
//        totalPrice += parseFloat(sellPricefixed) * input.value
//    }
//    subTotal.innerText = '$' + totalPrice;



//    if (disPrice != null) {
//        let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//        disAmount += (disPricefixed - parseFloat(sellPricefixed)) * input.value;

//        discountAmount.innerText = '$' + disAmount;
//    }
//    cartTotal.innerText = '$' + (totalPrice - disAmount);



//    totalItem += parseInt(input.value);
//    cartTotalItem.innerText = totalItem;
//    cartTotalHeadItem.innerText = totalItem;



//    plus.addEventListener('click', () => {
//        var value = parseFloat(input.value);
//        input.value = value + 1;

//        itemTotal.innerText = '$' + `${(value + 1) * parseFloat(sellPricefixed)}`;

//        if (disPrice != null) {
//            let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//            totalPrice += disPricefixed
//        } else {
//            totalPrice += parseFloat(sellPricefixed)
//        }

//        subTotal.innerText = '$' + totalPrice;

//        if (disPrice != null) {
//            let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//            disAmount += (disPricefixed - parseFloat(sellPricefixed));

//            discountAmount.innerText = '$' + disAmount;
//        }
//        cartTotal.innerText = '$' + (totalPrice - disAmount);


//        ++totalItem; 
//        cartTotalItem.innerText = totalItem;
//        cartTotalHeadItem.innerText = totalItem;

//    });



//    minus.addEventListener('click', () => {
//        var value = parseFloat(input.value);
//        if (value > 1) {
//            input.value = value - 1;
//            itemTotal.innerText = '$' + `${(value - 1) * parseFloat(sellPricefixed)}`;

//            if (disPrice != null) {
//                let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//                totalPrice -= disPricefixed
//            } else {
//                totalPrice -= parseFloat(sellPricefixed)
//            }

//            subTotal.innerText = '$' + totalPrice;

//            if (disPrice != null) {
//                let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//                disAmount -= (disPricefixed - parseFloat(sellPricefixed));

//                discountAmount.innerText = '$' + disAmount;
//            }

//            cartTotal.innerText = '$' + (totalPrice - disAmount);


//            --totalItem;
//            cartTotalItem.innerText = totalItem;
//            cartTotalHeadItem.innerText = totalItem;
//        }

//    });



//    DeleteBtn.addEventListener('click', () => {
//        if (disPrice != null) {
//            let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//            totalPrice -= disPricefixed * input.value
//        } else {
//            totalPrice -= parseFloat(sellPricefixed) * input.value
//        }

//        subTotal.innerText = '$' + totalPrice;

//        if (disPrice != null) {
//            let disPricefixed = parseFloat(disPrice.innerText.replace('$', '').trim());
//            disAmount -= (disPricefixed - parseFloat(sellPricefixed)) * input.value;

//            discountAmount.innerText = '$' + disAmount;
//        }

//        cartTotal.innerText = '$' + (totalPrice - disAmount);

//        totalItem -= input.value;
//        cartTotalItem.innerText = totalItem;
//        cartTotalHeadItem.innerText = totalItem;

//        items.remove();
//    });


//});