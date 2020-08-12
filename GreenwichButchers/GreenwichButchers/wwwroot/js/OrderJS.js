// This function is executed when the page is loaded
$(document).ready(function () {
    for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
        CalculateItemPrice("DDQuote_" + i)
    }
    
    switch ($('#OrderType').val()) {
        case "Bulk":
            if ($('#OrderStatus').val() != 'Complete') {
                OrderSelector($('#OrderType').val() + "Order");
            } else {
                for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
                    $('#PriceLabel_' + i).removeAttr("hidden");
                }
            }
            break;
        case "Shop":
            if ($('#OrderStatus').val() != 'Complete') {
                UpdateOrderTotalPrice();
            } 
            OrderSelector($('#OrderType').val() + "Order");
            break;
        default:
            OrderSelector('BulkOrder');
            break;
    }
});

function SubmitOrderCheck(UserRole) {
    var ReturnVal = false;

    switch (UserRole) {
        case "Staff":
        case "Manager":
            ReturnVal = true;
            break;
        default:
            ReturnVal = DateChecker('txtDeliveryDate', true)
            break;
    }

    return ReturnVal;
}
function SubmitShopOrder(UserRole) {
    var ReturnVal = false;

    switch (UserRole) {
        case "Staff":
        case "Manager":
            $('#SelectShippingLocation').modal("show");
            ReturnVal = false;
            break;
        default:
            ReturnVal = true;
            break;
    }

    return ReturnVal;
}

function ConfirmQuoteCheck(TotalItem) {
    var ReturnValue = true
    for (var i = 0; i < TotalItem; i++) {
        if ($('#DDQuote_' + i).val() == null || $('#DDQuote_' + i).val() < 1) {
            $('#DDQuote_' + i).addClass("is-invalid")
            ReturnValue = false;
        } else {
            $('#DDQuote_' + i).removeClass("is-invalid")
        }
    }
    return ReturnValue;
}

function OrderSelector(varId) {
    $('#' + varId).removeClass("btn-light text-black-50");
    $('#' + varId).addClass("btn-secondary");
    $('#OrderType').val(varId.toString().replace("Order", ""));

    var test = parseInt($('#ProductCount').val());
    switch (varId) {
        case "ShopOrder":
            $('#BulkOrder').addClass("btn-light text-black-50");
            $('#BulkOrder').removeClass("btn-secondary");
            $('#PriceHeader').html("Price");
            UpdateOrderTotalPrice();
            for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
                $('#PriceLabel_' + i).removeAttr("hidden");
            }
            break;
        case "BulkOrder":
            $('#ShopOrder').addClass("btn-light text-black-50");
            $('#ShopOrder').removeClass("btn-secondary");
            $('#PriceHeader').html("Unit");
            $('#txtOrderTotalPrice').html("N/A");
            for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
                $('#PriceLabel_' + i).attr("hidden", "hidden");
            }
            break;
    }
}


function CalculateItemPrice(varId) {
    var QuotePrice = parseFloat($('#txtQuotePrice_' + $('#' + varId).val()).val()).toFixed(2)

    if (QuotePrice == "NaN") {
        QuotePrice = 0
    }
    if (varId.includes("DDQuote_")) {
        $('#txtItemPriceQuote_' + varId.replace("DDQuote_", "")).html(parseInt(QuotePrice).toFixed(2))
    } else {
        $('#txtItemPrice_' + varId.replace("DDQuote_", "")).html(parseInt(QuotePrice).toFixed(2))
    }

    var TotalPrice = 0.00;
    for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
        if (varId.includes("DDQuote_")) {
            TotalPrice += parseFloat($('#txtItemPriceQuote_' + i).html()) * parseFloat($('#txtQuantity_' + i).val())
        } else {
            TotalPrice += parseFloat($('#txtItemPrice_' + i).val() * parseFloat($('#txtQuantity_' + i).val()))
        }
    }
    $('#txtOrderTotalPrice').html("£" + TotalPrice.toFixed(2));

    if (varId.includes("DDQuote_")) {
        $('#txtOrderTotalPriceQ').html("£" + TotalPrice.toFixed(2));
    }
}

function UpdateOrderTotalPrice() {
    var TotalPrice = 0.00;
    for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
        TotalPrice += parseFloat($('#txtItemPrice_' + i).val() * parseFloat($('#txtQuantity_' + i).val()))
    }
    $('#txtOrderTotalPrice').html("£" + TotalPrice.toFixed(2));
}