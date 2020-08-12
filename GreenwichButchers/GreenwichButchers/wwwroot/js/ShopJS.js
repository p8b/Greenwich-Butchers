// This function is executed when the page is loaded
$(document).ready(function () {
    var test = $('#OrderType').val();
    switch ($('#OrderType').val()) {
        case "Bulk":
            OrderSelector($('#OrderType').val() + "Order");
            break;
        case "Shop":
            OrderSelector($('#OrderType').val()+"Order");
            break;
        default:
            OrderSelector('BulkOrder');
            break;
    }
});

function OrderSelector(varId) {
    $('#' + varId).removeClass("btn-light text-black-50");
    $('#' + varId).addClass("btn-secondary");
    $('#OrderType').val(varId.toString().replace("Order", ""));

    var test = parseInt($('#ProductCount').val());
    switch (varId) {
        case "ShopOrder":
            $('#BulkOrder').addClass("btn-light text-black-50");
            $('#BulkOrder').removeClass("btn-secondary");
            $('#RetailPriceHeader').html("Retail Price");
            for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
                $('#RetailPriceLabel_' + i).removeAttr("hidden");
            }
            break;
        case "BulkOrder":
            $('#ShopOrder').addClass("btn-light text-black-50");
            $('#ShopOrder').removeClass("btn-secondary");
            $('#RetailPriceHeader').html("Unit");
            for (var i = 0; i < parseInt($('#ProductCount').val()); i++) {
                $('#RetailPriceLabel_' + i).attr("hidden","hidden");
            }
            break;
    }
}