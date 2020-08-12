// This function is executed when the page is loaded
window.onload = function () {
    // If register fails ShowPanel2
    if ($('#PanelControllE').html() != "") {
        CustomerRegPanelSwitch($('#PanelControllE').html())
    }
}

// Customer registration panel controller
function CustomerRegPanelSwitch(varId) {
    switch (varId) {
        case 'ShowPanel2':
            $('#RegPage1').attr('hidden', 'hidden')
            $('#RegPage2').removeAttr('hidden')
            $('#lblShowPageNum').text('Registration 2 of 2')
            break;
        case 'ShowPanel1':
            $('#RegPage1').removeAttr('hidden')
            $('#RegPage2').attr('hidden', 'hidden')
            $('#lblShowPageNum').text('Registration 1 of 2')
            break;
    }
}
// Panel 1 Continue button check
function Panel1SubmitCheck(varId) {
    var ReturnValue = true;

    if (!AlphabeticCheck("txtName", true)) {
        ReturnValue = false;
    }
    if (!AlphabeticCheck("txtSurname", true)) {
        ReturnValue = false;
    }
    if (!TelChecker("txtTel", true)) {
        ReturnValue = false;
    }
    if (!FieldRequiredCheck("txtAddressName")) {
        ReturnValue = false;
    }
    if (!FieldRequiredCheck("txtFirstLine")) {
        ReturnValue = false;
    }
    if (!FieldRequiredCheck("txtCity")) {
        ReturnValue = false;
    }
    if (!PostcodeCheckUK("txtPostcode", true)) {
        ReturnValue = false;
    }
    if (ReturnValue) {
        CustomerRegPanelSwitch(varId)
        $('#P2ErrorMsg').addClass('')
        $('#P2ErrorMsg').text('')
    }
    return ReturnValue;
}

// Panel 2 Confirm Button Check
function Panel2ConfirmCheck() {
    var ReturnValue = true;
    if (!Panel1SubmitCheck()) {
        $('#P2ErrorMsg').addClass('alert-danger')
        $('#P2ErrorMsg').text('Please go back to the first page and provide the required field(s).')
        ReturnValue = false;
    }
    if (!ContainsValidEmail("txtEmail", true)) {
        ReturnValue = false
    }
    if (!ConfirmPassCheck("txtPassword")) {
        ReturnValue = false
    }
    if ($('#CheckTerms').is(":checked")) {
    } else {
        $('#P2ErrorMsg').addClass('alert-danger')
        $('#P2ErrorMsg').text('Please agree to terms and conditions.')
        ReturnValue = false;
    }

    return ReturnValue;
}

// Custom modal close function for employee access of Register customer page
function CustomCloseModal(varId) {
    CloseModal(varId);
    $('#EmpRedirectBtn').submit()
}