// This function is executed when the page is loaded
$(function () {
    // This method is called to set parameters of the pagination overflow
    PaginatinOverFlow()
});


// This method is used to check the submit changes of 
// Customer panel in customer account page
function UpdateEmployeeInfo() {
    var ReturnValue = true;
    if (!AlphabeticCheck('txtName', true)) {
        ReturnValue = false
    }
    if (!AlphabeticCheck('txtSurname', true)) {
        ReturnValue = false
    }
    if (!FieldRequiredCheck('txtPosition')) {
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

function NewEmployee() {
    var ReturnValue = true;

    if (!ConfirmPassCheck('txtNewPassword')) {
        ReturnValue = false
    }
    if (!UpdateEmployeeInfo()) {
        ReturnValue = false
    }
    return ReturnValue
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

// This function is used to add the selected employee id to the hidden element "SelectedEmpID"
function SelectEmployee(varId) {
    $('#SelectedEmpID').val($('#' + varId).val());
    return true
}