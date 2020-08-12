// This function is executed when the page is loaded
$(function () {
    // This method is called to set parameters of the pagination overflow
    PaginatinOverFlow()

    if ($('#PasswordModal').val() == "Show") {
        $('#PasswordModal').submit();
    }
});


// This method is used to check the submit changes of 
// Customer panel in customer account page
function UpdateCustomerInfo() {
    var ReturnValue = true;
    if (!AlphabeticCheck('txtName', true)) {
        ReturnValue = false
    }
    if (!AlphabeticCheck('txtSurname', true)) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('txtCompany')) {
        ReturnValue = false
    }
    if (!ContainsValidEmail('txtEmail', true)) {
        ReturnValue = false
    }
    if (!TelChecker('txtTel', true)) {
        ReturnValue = false
    }

    return ReturnValue;
}



function UpdatePassword() {
    var ReturnValue = true;

    if (!ConfirmPassCheck('txtNewPassword')) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('OldPassword')) {
        ReturnValue = false
    }
    return ReturnValue
}

function AddressChanges() {
    var ReturnValue = true;
    if (!FieldRequiredCheck('txtAddressName')) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('txtFirstLine')) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('txtCity')) {
        ReturnValue = false
    }
    if (!PostcodeCheckUK('txtPostcode',true)) {
        ReturnValue = false
    }
    return ReturnValue;
}