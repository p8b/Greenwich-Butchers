// This method is used to check the submit changes of 
// add Suppliers panel in customer account page
function CheckSupplierInfo() {
    var ReturnValue = true;
    if (!AlphabeticCheck('txtSFullName', true)) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('txtSCompany')) {
        ReturnValue = false
    }
    if (!ContainsValidEmail('txtSEmail', false)) {
        ReturnValue = false
    }
    if (!TelChecker('txtSTel', true)) {
        ReturnValue = false
    }

    return ReturnValue;
}