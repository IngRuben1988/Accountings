$(document).ready(function () {
    InWorking();
});

$('#workout').click(function () {
    OutWorking();
});

$('#sidebarCollapse').on('click', function () {
    $('#sidebar').toggleClass('active');
});

function OutWorking() {
    $.ajax({
        method: "POST",
        url: Account.LogOut,
        datatype: 'json',
    })
        .done(function (response, textStatus, jqXHR) {
            if (response.data == Account.NotWork) {
                window.location.href = Account.LogOutIn;
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });  
}

function InWorking() {
    $.ajax({
        url: Account.Obtain,
        type: "GET",
        datatype: "json",
        data: { parameters: null },
    })
        .done(function(response, textStatus, jqXHR) {
            if (response.data != null) {
                Account.Name.text(response.data.userLoginName);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });  

}