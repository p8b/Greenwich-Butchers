/** In this validation I have used javaScript for the structure of the file
 * and jquery to access the HTML tags and their attributes
 * **/

// Regex expression variables used for validation
var rxKeepDigitsDecimal = /[^\d\.]+/g;
var rxKeepDigits = /[^\d\s]+/g;
var rxKeepAlphaSymbols = /[\d\.]/g;
var rxKeepAlphabet = /[a-zA-Z]/g;
var rxKeepSymbols = /[a-zA-Z0-9\.\s]/g;
var rxKeepDecimal = /[^.]/g;
var rxKeepValidEmailSymbols = /[^@._+-]/g;
var rxKeepValidEmailChars = /[@.-_+a-zA-Z0-9]/g;
var rxKeepAtSymbols = /[^@]/g;
var rxKeepDecimal2 = /^[0-9]+(\.[0-9]{1,2})?$/g;
var rxRemoveSpaces = /[/\s]/g;
var rxRemoveDigits = /[/\d\s]+/g;
var rxRemoveAlphabet = /[/a-zA-Z\s]/g;
var rxRemoveValidEmailChars = /[\@._+ \w \d \-]/g;
var rxRemoveValidEmailServerSideSymbols = /[\@.a-zA-Z0-9]/g;
var rxUKPostcode = /^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$/g;

// Select random icon from available icons
function Icon() {
    var iconNum = new Random().Next(1, 5)

    var facvicon = "favicon " + iconNum + ".ico";

    return facvicon;
}

// Check if input field e.g. Text box is empty
// return true if it is empty
// return false if it is NOT empty
function FieldRequiredCheck(varId) {
    if ($('#' + varId).val() == null ||
        $('#' + varId).val().toString().replace(rxRemoveSpaces, "") == "") {
        // If the value is empty
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('* Required');
        return false;
    } else {
        // If the value is not empty
        $('#' + varId).removeClass('is-invalid');
        $('#' + varId).addClass('is-valid');

        $('#lbl' + varId).text('');
        return true;
    }
}

///** Live Validation Functions **///
// Check if the parameter has only alphabetic
// characters and spaces e.g.Name, Surname
function AlphabeticCheck(varId, checkEmpty) {
    // remove all alphabet and spaces and if there are anything left then the value is invalid
    if ($('#' + varId).val().toString().replace(rxRemoveAlphabet, "").split('').length != 0) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('* Alphabetic characters only');
        return false;
    }

    if (checkEmpty == true) {
        // If the value is empty
        if ($('#' + varId).val().toString() == "") {
            $('#' + varId).addClass('is-invalid');

            $('#lbl' + varId).text('* Field Required');
            return false;
        }
    }
    $('#' + varId).removeClass('is-invalid');
    $('#lbl' + varId).text('');
    return true;
}

// Telephone numbers up to 13 digits excluding empty space value
// varId parameter is the ID of the text-box
// checkEmpty is boolean to tell the function to check for if the
// text-box is empty (true) or not (false)
function TelChecker(varId, checkEmpty) {
    if ($('#' + varId).val().toString().replace(rxRemoveDigits, "").split('').length != 0) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('* Digits Only');
        return false;
    }
    else if ($('#' + varId).val().toString().replace(rxKeepDigits, "").split('').length > 20) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('* 13 digits only');
        return false;
    }
    if (checkEmpty == true) {
        if ($('#' + varId).val().toString() == "") {
            $('#' + varId).addClass('is-invalid');

            $('#lbl' + varId).text('* Field Required');
            return false;
        }
    }

    $('#' + varId).removeClass('is-invalid');
    $('#lbl' + varId).text('');
    return true;
}

// Email Check
// Check if a string contains invalid characters
// Must have only one "@" and can contain many "."
function ContainsValidEmail(varId, checkEmpty) {
    $('#' + varId).removeClass('is-invalid');

    // First check to see if there are any invalid characters Must be 0
    // Second check allow only one "@" symbols
    if ($('#' + varId).val().toString().replace(rxRemoveValidEmailChars, "").split('').length != 0
        || $('#' + varId).val().toString().replace(rxKeepAtSymbols, "").split('').length > 1) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('Invalid Email');
        return false;
    }

    if (checkEmpty == true) {
        if ($('#' + varId).val().toString() == "") {
            $('#' + varId).addClass('is-invalid');

            $('#lbl' + varId).text('* Required');
            return false;
        }
    }

    $('#' + varId).removeClass('is-invalid');
    $('#lbl' + varId).text('');
    return true;
}

// Check Confirm password is the same as password
function ConfirmPassCheck(varId) {
    varId = varId.replace('Confirm', '')
    var Pass = $('#' + varId);
    var ConfirmPass = $('#' + varId + 'Confirm');

    // If both passwords match
    // and the password is not empty
    // and password value is more than 5 chars
    // and password value does not contain space
    if (Pass.val().toString() == ConfirmPass.val().toString() &&
        Pass.val().toString() != '' && Pass.val().length > 5 &&
        Pass.val().includes(' ') == false) {
        // Add Classes
        $('#lbl' + varId).addClass('is-valid text-green');
        $('#lbl' + varId + 'Confirm').addClass('is-valid text-green');
        $('#' + varId).addClass('is-valid');
        $('#' + varId + 'Confirm').addClass('is-valid');

        // Remove Classes
        $('#lbl' + varId).removeClass('is-invalid text-red');
        $('#lbl' + varId + 'Confirm').removeClass('is-invalid text-red');
        $('#' + varId).removeClass('is-invalid');
        $('#' + varId + 'Confirm').removeClass('is-invalid');

        // Add label messages
        $('#lbl' + varId).text(' Matched');
        $('#lbl' + varId + 'Confirm').text(' Matched');
        return true
    }

    // Set both the label of both password inputs to null
    $('#lbl' + varId).text('');
    $('#lbl' + varId + 'Confirm').text('');

    // If password is empty or just space character
    if (Pass.val() == '' || Pass.val().includes(' ') == true) {
        // Add label messages
        $('#lbl' + varId).text('* Required (Must Not Contain Space)');
    }
    // If confirm password is empty or just space character
    else if (ConfirmPass.val() == '' || ConfirmPass.val().includes(' ') == true) {
        // Add label messages
        $('#lbl' + varId + 'Confirm').text('* Required (Must Not Contain Space)');
    }
    // if password OR confirm password has less than 5 characters
    else if (Pass.val().length < 5 || ConfirmPass.val().length < 5) {
        // Add label messages
        $('#lbl' + varId).text('* Must be more than 5 characters');
        $('#lbl' + varId + 'Confirm').text('* Must be more than 5 characters');
    }
    // if password and confirm password do not match
    else if (Pass.val().toString() != ConfirmPass.val().toString()) {
        // Add label messages
        $('#lbl' + varId).text('* Not Matched');
        $('#lbl' + varId + 'Confirm').text('* Not Matched');
    }
    // If the more than 5 characters and they do not match
    if ((Pass.val().length > 5 || ConfirmPass.val().length > 5) &&
        Pass.val().toString() != ConfirmPass.val().toString() &&
        ConfirmPass.val().includes == false &&
        Pass.val().includes == false) {
        // Add label messages
        $('#lbl' + varId).text('* Not Matched');
        $('#lbl' + varId + 'Confirm').text('* Not Matched');
    }

    // Remove Classes
    $('#lbl' + varId).removeClass('is-valid text-green');
    $('#lbl' + varId + 'Confirm').removeClass('is-valid text-green');
    $('#' + varId).removeClass('is-valid');
    $('#' + varId + 'Confirm').removeClass('is-valid');

    // Add Classes
    $('#lbl' + varId).addClass('is-invalid text-red');
    $('#lbl' + varId + 'Confirm').addClass('is-invalid text-red');
    $('#' + varId).addClass('is-invalid');
    $('#' + varId + 'Confirm').addClass('is-invalid');
    return false;
}

// Check if the postcode is a valid UK postcode
function PostcodeCheckUK(varId, Required) {
    if (Required) {
        if (!FieldRequiredCheck(varId)) {
            $('#' + varId).addClass('is-invalid text-red');
            $('#lbl' + varId).addClass('is-invalid text-red');
            return false;
        }
    }
    if ($('#' + varId).val().toString().search(rxUKPostcode) != 0) {
        $('#' + varId).removeClass('is-valid text-green');
        $('#lbl' + varId).removeClass('is-valid text-green');

        $('#' + varId).addClass('is-invalid text-red');
        $('#lbl' + varId).addClass('is-invalid text-red');
        $('#lbl' + varId).text('* Invalid Postcode');
        return false;
    }
    $('#' + varId).removeClass('is-invalid text-red');
    $('#lbl' + varId).removeClass('is-invalid text-red');

    $('#' + varId).addClass('is-valid text-green');
    $('#lbl' + varId).addClass('is-valid text-green');
    $('#lbl' + varId).text('Valid Postcode');
    return true;
}

// CheckBox css Controller
function CheckboxCss(varId) {
    if ($('#' + varId).is(':checked') == true) {
        $('#' + varId).removeClass('is-invalid')
        $('#' + varId).addClass('is-valid')
    } else {
        $('#' + varId).removeClass('is-valid')
        $('#' + varId).addClass('is-invalid')
    }
}

// Check if value is decimal with 2 places (valid Currency and quantity)
function CheckDecimal2(varId, MaxValue) {
    if ($('#' + varId).val() == "" || parseFloat($('#' + varId).val()).toFixed(2) == 0) {
        $('#' + varId).val("0")
    }
    if ($('#' + varId).val().length > 1
        && $('#' + varId).val()[0] == "0") {
        while ($('#' + varId).val()[0] == "0") {
            $('#' + varId).val($('#' + varId).val().substring(1, $('#' + varId).val().length)) 
        }

        if ($('#' + varId).val().length == 0) {
            $('#' + varId).val("0")
        }
    }
    if ($('#' + varId).val().toString().replace(rxKeepDecimal2, "") != ""
        || parseFloat($('#' + varId).val()).toFixed(2) > MaxValue
        || parseFloat($('#' + varId).val()).toFixed(2) < 0) {
        // If the value is empty
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId).text('* Please enter correct value (0.00)');
        return false;
    } else {
        // If the value is not empty
        $('#' + varId).removeClass('is-invalid');
        $('#' + varId).addClass('is-valid');

        $('#lbl' + varId).text('');
        return true;
    }
}

// Photo Upload Live Validation. Upload is required
function UploadPhotoCheck(varId) {
    // Photo Up-load File
    if ($('#' + varId).val().toString() == "") {
        $('#' + varId).addClass('is-invalid');
        $('#lblIn' + varId).text('Image used in "Shop" page');
        $('#lbl' + varId).text('Image is Required');
        return false;
    } else {
        $('#' + varId).addClass('is-valid');
        var FileName = $('#' + varId).val().toString().split("\\");
        $('#lblIn' + varId).text(FileName[FileName.length - 1]);
        $('#lbl' + varId).text("");
        $('#' + varId).removeClass('is-invalid');
        return true;
    }
}

// Photo Upload Live Validation. Upload is NOT required
function UploadPhotoCheckNotRQ(varId) {
    // Photo Up-load File
    if ($('#' + varId).val().toString() == "") {
        $('#lbl' + varId).text('Image used in "Shop" page');
        return true;
    } else {
        $('#' + varId).addClass('is-valid');
        var FileName = $('#' + varId).val().toString().split("\\");
        $('#lbl' + varId).text(FileName[FileName.length - 1]);
        $('#' + varId).removeClass('is-invalid');
        return true;
    }
}

// This function is used to add next and previous functionality to the pagination
function PaginatinOverFlow() {
    if ($('#UlPagination').val() != null) {
        // Find the active page by finding and splitting the id of the child element of
        // "UlPagination" element which has an "active" class
        var ActivePage = $('#UlPagination').children('.active')[0].id.toString().split("_")[1]

        // Hide all the page elements
        for (var i = 1; i <= $('#TotalPages').val(); i++) {
            $('#LiPage_' + i).attr("hidden", "hidden")
        }
        // Remove the Hidden attribute of two element before and after the active element
        $('#LiPage_' + ActivePage).removeAttr("hidden")
        $('#LiPage_' + (parseInt(ActivePage) + 1).toString()).removeAttr("hidden")
        $('#LiPage_' + (parseInt(ActivePage) + 2).toString()).removeAttr("hidden")
        $('#LiPage_' + (parseInt(ActivePage) - 1).toString()).removeAttr("hidden")
        $('#LiPage_' + (parseInt(ActivePage) - 2).toString()).removeAttr("hidden")
    }
}
// This method is used to set the next page of pagination active if possible
function NextPagination() {
    // Find the active page by finding and splitting the id of the child element of
    // "UlPagination" element which has an "active" class
    var ActivePage = $('#UlPagination').children('.active')[0].id.toString().split("_")[1]
    // if the active page number is less than "TotalPages" number
    if (ActivePage < $('#TotalPages').val()) {
        // fined the Next element id
        var NextPageBtnID = $('#UlPagination').children('.active').next().children()[0].id

        // trigger the click event of the previous element button
        $('#' + NextPageBtnID).trigger('click')
    }
}
// This method is used to set the previous page of pagination active if possible
function PreviousPagination() {
    // Find the active page by finding and splitting the id of the child element of
    // "UlPagination" element which has an "active" class
    var ActivePage = $('#UlPagination').children('.active')[0].id.toString().split("_")[1]
    // if the active page number is more than 1
    if (ActivePage > 1) {
        // fined the previous element id
        var PrevPageBtnID = $('#UlPagination').children('.active').prev().children()[0].id

        // trigger the click event of the previous element button
        $('#' + PrevPageBtnID).trigger('click')
    }
}

// This method is used to set the amount of items in each page result
function Set_ShowPageNum(varNum) {
    $('#ShowPageNumItem').val(varNum)

    return true
}

// This method is used to force close the modal with JS
function CloseModal(varId) {
    $('#' + varId).removeAttr('style');
    $('#' + varId + 'BG').removeAttr('style');
}

function CopyElementValue(FromId,ToId) {
    $('#' + ToId).val($('#' + FromId).val());
    return true
}

function SelectIDCopy(varId) {
    $('#SelectIDValue').val(varId);
    return true;
}

///** Page specific validation checker**///
// On Submit form validation for Adoption Register Page(Only on submit check for empty fields)
// Separated if statements are used to check different tags and if one of them change the check variable
// to false then the this method will return false else it would return true
function RegisterFinalValidation() {
    var check = true;
    // Adoption Date
    if ($('#MainContent_txtAdoptDate').val().toString() == "" || DateChecker('MainContent_txtAdoptDate') == false) {
        // Add text to the HTML elemnt
        $('#txtAdoptDate').text('Field Required');

        // Add Css class
        $('#MainContent_txtAdoptDate').addClass('is-invalid');
        // set check variable to false
        check = false;
    } else {
        $('#MainContent_txtAdoptDate').removeClass('is-invalid');

        DateChecker('MainContent_txtAdoptDate');
    }

    // Name input
    if ($('#MainContent_txtName').val() == "") {
        $('lbltxtName').text('Field Required');
        $('#MainContent_txtName').addClass('is-invalid');

        check = false;
    } else {
        $('#MainContent_txtName').removeClass('is-invalid');
        AlphabeticCheck('MainContent_txtName');
    }

    // Surname Input
    if ($('#MainContent_txtSurname').val() == "") {
        $('lbltxtSurname').text('Field Required');
        $('#MainContent_txtSurname').addClass('is-invalid');

        check = false;
    } else {
        $('#MainContent_txtSurname').removeClass('is-invalid');
        AlphabeticCheck('MainContent_txtSurname');
    }

    // Email Input
    if ($('#MainContent_txtEmail').val() == "" || ContainsValidEmail('MainContent_txtEmail') == false
        || $('#MainContent_txtEmail').val().toString().replace(rxKeepAtSymbols, "").split('').length != 1) {
        // Handling Error Message
        if ($('#MainContent_txtEmail').val() == "") {
            $('#lbltxtEmail').text('Field Required');
        } else {
            $('#lbltxtEmail').text('@ symbol Required');
        }
        // Set the Css class
        $('#MainContent_txtEmail').addClass('is-invalid');

        // Change the check value to false
        check = false;
    } else {
        $('#MainContent_txtEmail').removeClass('is-invalid');
        // Add the method for further checks
    }

    // Tel Input
    if ($('#MainContent_txtTel').val() == "") {
        $('lbltxtTel').text('Field Required');
        $('#MainContent_txtTel').addClass('is-invalid');

        check = false;
    } else {
        $('#MainContent_txtTel').removeClass('is-invalid');
        TelChecker('MainContent_txtTel');
    }
    if (UploadPhotoCheck('MainContent_UploadPhoto') == false) {
        check = false;
    }

    return check
}

// Check the parameter (date YYYY-MM-DD) against the system date.
// (date passed = false, Date NOT passed = true)
function DateChecker(varId, required) {
    // if input value is not empty
    if ($('#' + varId).val().toString() != "") {
        // get the system year
        var nowYYYY = (new Date()).getFullYear();
        // month
        var nowMM = (new Date()).getMonth() + 1;
        // day
        var nowDD = (new Date()).getDate();

        // split the string containing the received data and remove all the characters except digits
        var DateArr = $('#' + varId).val().toString().replace(rxKeepDigits, "").split('');
        // the first 4 character must be the year
        var PraYYYY = parseInt(DateArr[0] + DateArr[1] + DateArr[2] + DateArr[3]);
        // the next 2 must be the Month
        var PraMM = parseInt(DateArr[4] + DateArr[5]);
        // and the last two must be the date
        var PraDD = parseInt(DateArr[6] + DateArr[7]);

        // If date is 0 set it to 31
        // used for card expiry date
        if (PraDD == 0) { PraDD = 31; }

        // Check year
        if (PraYYYY > nowYYYY) { $('#' + varId).removeClass('is-invalid'); return true; }
        // years are the same
        else if (PraYYYY == nowYYYY) {
            // Check Month
            if ((PraMM <= 12 || PraMM > 0) && PraMM > nowMM) {
                $('#' + varId).removeClass('is-invalid');
                return true;
                // if month are the same
            } else if (PraMM == nowMM) {
                // Check day
                if (PraDD <= 31 && PraDD > 0 && PraDD > nowDD) {
                    $('#' + varId).removeClass('is-invalid');
                    return true;
                    // used for car expiry date
                } else if (PraMM == nowMM && varId == "MainContent_txtExpiryDate") {
                    $('#' + varId).removeClass('is-invalid');
                    return true;
                }
            }
        }
        // If return true was not triggered then the input is invalid
        // set the text value of the date input error label
        $('#lbl' + varId).text('Invalid Date');
        // add the CSS class to make it invalid
        $('#' + varId).addClass('is-invalid');
        return false;
    } else if (required == true) {
        $('#lbl' + varId).text('Date Required');
        // add the CSS class to make it invalid
        $('#' + varId).addClass('is-invalid');
        return false;

    } else { // else if the input is empty
        // remove the 'is-invalid' CSS class
        $('#' + varId).removeClass('is-invalid');
        return false;
    }
}

// Validates Card Number Must be 16 digits only
function CardNumberCheck(varId) {
    // Remove any non digit characters
    $('#' + varId).val($('#' + varId).val().replace(rxKeepDigits, ""));

    // Must be 16 digits only
    if ($('#' + varId).val().toString().replace(rxKeepDigits, "").length > 16) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId.replace("MainContent_", "").toString()).text('Must Be 16 digits only');

        return false;
    } else {
        $('#' + varId).removeClass('is-invalid');
        return true;
    }
}
// CVC check must be 3 digits only
function CVCNumberCheck(varId) {
    // Remove any non digit characters
    $('#' + varId).val($('#' + varId).val().replace(rxKeepDigits, ""));

    // Must be 16 digits only
    if ($('#' + varId).val().toString().replace(rxKeepDigits, "").length > 3) {
        $('#' + varId).addClass('is-invalid');

        $('#lbl' + varId.replace("MainContent_", "").toString()).text('Must Be 3 digits only');

        return false;
    } else {
        $('#' + varId).removeClass('is-invalid');
        return true;
    }
}