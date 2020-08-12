// This function is executed when the page is loaded
$(function () {
    // This method is called to set parameters of the pagination overflow
    PaginatinOverFlow()
});

// Panel 1 Submit Check (Product/Stock (Modify))
function POneSubmitCheck(NumProducts, NumStockLocation, CurrentPageNum) {
    // Initial return value is set to true
    var ReturnVal = true;

    var i = ((CurrentPageNum - 1) * $('#ShowPageNumItem').val());
    if (NumProducts >= (CurrentPageNum * $('#ShowPageNumItem').val())) {
        NumProducts = CurrentPageNum * $('#ShowPageNumItem').val()
    }

    // Loop through the products
    while (i < NumProducts) {
        if (FieldRequiredCheck("txtProductName_" + i) == false) {
            ReturnVal = false;
        }
        if (FieldRequiredCheck("txtRetailUnit_" + i) == false) {
            ReturnVal = false;
        }
        if (FieldRequiredCheck("txtRetailPrice_" + i) == false ||
            CheckDecimal2("txtRetailPrice_" + i, 999999.99) == false) {
            ReturnVal = false;
        }
        for (b = 0; b < NumStockLocation; b++) {
            if (FieldRequiredCheck("txtStockLevel_" + b + i) == false ||
                CheckDecimal2("txtStockLevel_" + b + i, 999999.99) == false) {
                ReturnVal = false;
            }
        }
        i++
    }

    if (ReturnVal == false) {
        $("#ErrorMassage").text("Invalid Inputs");
        $("#PErrorModal").removeAttr("hidden");
        $("#PErrorModalBG").removeAttr("hidden");
    }
    else if (ReturnVal == true) {
        $("#PErrorModal").attr("hidden", "hidden");
        $("#PErrorModalBG").attr("hidden", "hidden");
    }
    return ReturnVal;
}

// Panel 2 Submit Check (Product (Add))
function PTwoSubmitCheck() {
    var ReturnVal = true;

    if (FieldRequiredCheck("ddCategoryP2") == false) {
        ReturnVal = false
    }
    if (FieldRequiredCheck("txtProductNameP2") == false) {
        ReturnVal = false
    }
    if (FieldRequiredCheck("txtRetailPriceP2") == false ||
        CheckDecimal2("txtRetailPriceP2", 999999.99) == false) {
        ReturnVal = false
    }
    if (FieldRequiredCheck("txtRetailUnitP2") == false) {
        ReturnVal = false
    }

    if (ReturnVal == false) {
        $("#ErrorMassage").html("Invalid Inputs");
    }
    return ReturnVal
}

// Panel 3 Submit Check (Product Category (Add/Modify)).
function PThreeSubmitCheck(SubPanel) {
    var ReturnValue = true;
    if (SubPanel == "Add") {
        if (!FieldRequiredCheck('txtNewCategoryName')) {
            ReturnValue = false;
        }
        if (!UploadPhotoCheck('UploadPhoto1')) {
            ReturnValue = false;
        }
        return ReturnValue;
    }
    else if (SubPanel == "Modify") {
    }
    else {
        return false;
    }
}

// This method is used to show preview of modified category
// used in "Shop" page
function CategoryNamePreview(varId, previewTextId) {
    $('#' + previewTextId).html($('#' + varId).val());
}
// This method is used to show preview of modified category
// used in "Shop" page
function CategoryImagePreview(varId, event, previewImageId) {
    UploadPhotoCheckNotRQ(varId);

    var reader = new FileReader();
    reader.onload = function () {
        $('#' + previewImageId).attr('src', reader.result);
    }
    reader.readAsDataURL(event.target.files[0]);
}

// This method is used to set the delete confirmation for the stocklocation selected
function SetStockLMsg(varid) {
    $('#DelStockLocation').text($('#' + varid).val())
}

