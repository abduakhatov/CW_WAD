function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function Validate() {
    var x = document.getElementById("emailValn").value;
    var btnCreateNewProduct = document.getElementById("btnCreateNewProduct");
    var lbluserExists = document.getElementById("lblUserExistsValidation");
    lbluserExists.innerHTML = "User with this email address already exists";
    var res = userExists(x)
    if (res == true){
        lbluserExists.innerHTML = "User with this email address already exists";
        btnCreateNewProduct.disabled = true;
    } else {
        lbluserExists.innerHTML = "";
        btnCreateNewProduct.disabled = false;
    }

}

function userExists(email) {
    
    var result = true;
    $.ajax({
        url: "/api/SignUpIn/UserExists",
        data: { email: email},
        dataType: 'json',
        type: "GET",
        async: false,
        success: function (data) {
            result = data;
        },
        error: function () {
            alert("Error during request");
        }
    });
    return result;
}