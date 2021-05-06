var $pagination = $('#pagination'),
    totalRecords = 0,
    records = [],
    displayRecords = [],
    recPerPage = 10,
    page = 1,
    totalPages = 0;
var frmUser = $("#formUser");
var jsPromiseSaveDocument;
var jsPromiseSaveDocumentEmpresas, jsPromiseGetCompanyById;
var jsPromiseSaveDocumentAccounts, jsPromiseGetAccountBy;
var jsPromiseSaveDocumentPermissions, jsPromiseGetPermissionsBy;
var getDocumentRequest;
var _dataObject = {};
var headerTable = ['Id Usuario', 'Nombre de Usuario', 'Nombre', 'Correo Electrónico', 'Perfil', 'Acciones'];
var hideColumn = ['idProfile', 'password', 'passwordconfirm', 'data']
$(document).ready(function () {
    getFillUsers();
    getFillProfile();
    getFillCompany();
    getFillAccount();
    loadPermissionsAll();

    VTAGridviewDinamic(null, Configuration.getUsers, headerTable, "editUser(this)", null, hideColumn)

    $("#idUser").on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $("#btnAdvanceSearch").click(function (event) {
        event.preventDefault();
        if ($("#idUser").val() == "" && $("#userName").val() == "") {
            VTAGridviewDinamic(null, Configuration.getUsers, headerTable, "editUser(this)", null, hideColumn)
        } else {
            var infoSearch = JSON.stringify({ idUser: $("#idUser").val() == "" ? "0" : $("#idUser").val(), userName: $("#userName").val() })
            VTAGridviewDinamic(infoSearch, Configuration.searchUsers, headerTable, "editUser(this)", null, hideColumn)
        }
    });

    $("#show_hide_password a").on('click', function (event) {
        event.preventDefault();
        if ($('#passwordconfirm').attr("type") == "text") {
            $('#passwordconfirm').attr('type', 'password');
            //$('#passwordconfirm i').addClass("fa-eye-slash");
            //$('#passwordconfirm i').removeClass("fa-eye");
            $('.changepass').addClass("fa-eye-slash");
            $('.changepass').removeClass("fa-eye");
        } else if ($('#passwordconfirm').attr("type") == "password") {
            $('#passwordconfirm').attr('type', 'text');
            //$('#show_hide_password i').removeClass("fa-eye-slash");
            //$('#show_hide_password i').addClass("fa-eye");
            $('.changepass').removeClass("fa-eye-slash");
            $('.changepass').addClass("fa-eye");

        }
    });

    function ValidatePassword() {
        var password = $("#password").val();
        var confirmPassword = $("#passwordconfirm").val();
        if (password != confirmPassword) {
            //alert("Passwords do not match.");
            return false;
        }
        return true;
    }

    $("#btnSaveUser").click(function (event) {
        event.preventDefault();
        var frmUser = $("#formUser");
        var valid = ValidatePassword();
        if (!valid) {
            frmUser.notify("Las contraseña de confirmación es diferente. ", {
                position: "top right",
                className: "error",
                hideDuration: 2000
            });
        } else {
            jsPromiseSaveDocument = Promise.resolve(saveInfoUser());
            jsPromiseSaveDocument.then(function (response) {
                _dataObject = response.data;
                if (response.success === true) {
                    if (_dataObject.data != null) {
                        frmUser.notify("No se puede guardar la contraseña. " + _dataObject.data, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                    } else {
                        frmUser.notify("Se ha guardado la información. " + response.message, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                        $("#password").val('')
                        $("#passwordconfirm").val('')
                    }
                }
                else {
                    var message = response.message == undefined ? response : response.message
                    frmUser.notify("No se puede guardar la información. " + message, {
                        position: "top right",
                        className: "error",
                        hideDuration: 2000
                    });
                }
            }).catch(function (error) {
                console.log("Failed Request! ->", error);
            });
        }
    });
    $("#btnSaveCompany").click(function (event) {
        event.preventDefault();

        jsPromiseSaveDocumentEmpresas = Promise.resolve(saveCompanies());
        jsPromiseSaveDocumentEmpresas.then(function (response) {
            _dataObject = response.data;
            var frmEmpresas = $("#formCompanies");
            if (response.success === true) {
                frmEmpresas.notify("Se ha guardado la información. " + response.message, {
                    position: "top right",
                    className: "success",
                    hideDuration: 500
                });
            }
            else {
                var message = response.message == undefined ? response : response.message
                frmEmpresas.notify("No se puede guardar la información. " + message, {
                    position: "top right",
                    className: "error",
                    hideDuration: 2000
                });
            }
        }).catch(function (error) {
            console.log("Failed Request! ->", error);
        });
    });
    $("#btnSaveAccount").click(function (event) {
        event.preventDefault();
        jsPromiseSaveDocumentAccounts = Promise.resolve(saveAccounts());
        jsPromiseSaveDocumentAccounts.then(function (response) {
            _dataObject = response.data;
            var frmAccounts = $("#formAccounts");
            if (response.success === true) {
                frmAccounts.notify("Se ha guardado la información. " + response.message, {
                    position: "top right",
                    className: "success",
                    hideDuration: 500
                });
            }
            else {
                var message = response.message == undefined ? response : response.message
                frmAccounts.notify("No se puede guardar la información. " + message, {
                    position: "top right",
                    className: "error",
                    hideDuration: 2000
                });
            }
        }).catch(function (error) {
            console.log("Failed Request! ->", error);
        });
    });
    $("#btnSavePermissions").click(function (event) {
        event.preventDefault();
        jsPromiseSaveDocumentPermissions = Promise.resolve(savePermissions());
        jsPromiseSaveDocumentPermissions.then(function (response) {
            _dataObject = response.data;
            var frmPermissions = $("#formPermissions");
            if (response.success === true) {
                frmPermissions.notify("Se ha guardado la información. " + response.message, {
                    position: "top right",
                    className: "success",
                    hideDuration: 500
                });
            }
            else {
                var message = response.message == undefined ? response : response.message
                frmPermissions.notify("No se puede guardar la información. " + message, {
                    position: "top right",
                    className: "error",
                    hideDuration: 2000
                });
            }
        }).catch(function (error) {
            console.log("Failed Request! ->", error);
        });
    });
    $("#btnSaveGeneral").click(function (event) {
        event.preventDefault();
        var frmUser = $("#formUser");
        var valid = ValidatePassword();
        if (!valid) {
            frmUser.notify("Las contraseña de confirmación es diferente. ", {
                position: "top right",
                className: "error",
                hideDuration: 2000
            });
        } else {
            jsPromiseSaveDocument = Promise.resolve(saveInfoUser());

            jsPromiseSaveDocument.then(function (response) {
                _dataObject = response.data;
                //var frmUser = $("#formUser");
                if (response.success === true) {
                    if (_dataObject.data != null) {
                        frmUser.notify("No se puede guardar la contraseña. " + _dataObject.data, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                    } else {
                        frmUser.notify("Se ha guardado la información. " + response.message, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                        $("#password").val('')
                        $("#passwordconfirm").val('')
                    }
                }
                else {
                    var message = response.message == undefined ? response : response.message
                    frmUser.notify("No se puede guardar la información. " + message, {
                        position: "top right",
                        className: "error",
                        hideDuration: 2000
                    });
                }
                jsPromiseSaveDocumentEmpresas = Promise.resolve(saveCompanies());

            }).then(response => jsPromiseSaveDocumentEmpresas)
                .then(response => {
                    var frmEmpresas = $("#formCompanies");
                    if (response.success === true) {
                        frmEmpresas.notify("Se ha guardado la información. " + response.message, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                    }
                    else {
                        var message = response.message == undefined ? response : response.message
                        frmEmpresas.notify("No se puede guardar la información. " + message, {
                            position: "top right",
                            className: "error",
                            hideDuration: 2000
                        });
                    }
                    jsPromiseSaveDocumentAccounts = Promise.resolve(saveAccounts());
                }).then(response => jsPromiseSaveDocumentAccounts)
                .then(response => {
                    var frmAccounts = $("#formAccounts");

                    if (response.success === true) {
                        frmAccounts.notify("Se ha guardado la información. " + response.message, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                    }
                    else {
                        var message = response.message == undefined ? response : response.message
                        frmAccounts.notify("No se puede guardar la información. " + message, {
                            position: "top right",
                            className: "error",
                            hideDuration: 2000
                        });
                    }
                    jsPromiseSaveDocumentPermissions = Promise.resolve(savePermissions());
                }).then(response => jsPromiseSaveDocumentPermissions)
                .then(response => {
                    var frmPermissions = $("#formPermissions");

                    if (response.success === true) {
                        frmPermissions.notify("Se ha guardado la información. " + response.message, {
                            position: "top right",
                            className: "success",
                            hideDuration: 500
                        });
                    }
                    else {
                        var message = response.message == undefined ? response : response.message
                        frmPermissions.notify("No se puede guardar la información. " + message, {
                            position: "top right",
                            className: "error",
                            hideDuration: 2000
                        });
                    }

                })
                .catch(function (error) {
                    console.log("Failed Request! ->", error);
                });
        }
    });


    //$('[data-toggle="tooltip"]').tooltip();
});

function editUser(value) {
    var $row = value.closest('tr')
    $(".Users option").removeAttr("selected");
    $("#idProfileAccount option").removeAttr("selected");

    var id = $($row).children("td:nth-child(1)").text();//idUser
    var username = $($row).children("td:nth-child(2)").text();
    var usercompletname = $($row).children("td:nth-child(3)").text();
    var usermail = $($row).children("td:nth-child(4)").text();
    var userperfilname = $($row).children("td:nth-child(5)").text();
    var perfile = $($row).children("td:nth-child(6)").text();

    $("#nombreUsuario").text(username.toUpperCase())
    $('.Users option[value=' + id + ']').attr('selected', true)
    $('.Users').attr('disabled', 'disabled')
    $('#idProfileAccount option[value=' + perfile + ']').attr('selected', true)
    $("#hdfUserid").val(id);
    GetInfoByIdUser(id);

    $('#divUsetoWork').css("display", "block");
    $('#divSearchGral').css("display", "none");

}


$('.btn_backUser').on('click', function () {
    $('#divUsetoWork').css("display", "none");
    $('#divSearchGral').css("display", "block");

});

function getFillUsers() {
    $.ajax({
        method: "GET"
        , url: Configuration.getUsersToFill
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $(".Users").empty();
            var $dropdownuser = $(".Users");
            if (items != null) {
                $.each(items, function (key, value) {
                    $dropdownuser.append($("<option />").val(value.value).text(value.text));
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getFillProfile() {
    $.ajax({
        method: "GET"
        , url: Configuration.getProfileToFill
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $(".ProfileAccount").empty();
            var $dropdownprofile = $(".ProfileAccount");
            if (items != null) {
                $.each(items, function (key, value) {
                    $dropdownprofile.append($("<option />").val(value.value).text(value.text));
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getFillCompany() {
    $.ajax({
        method: "GET"
        , url: Configuration.getCompaniesToFill
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $("#conSelAll").empty();
            $("#Companies").empty();
            if (items != null) {
                $('<input />', { type: 'checkbox', id: 'ccball_1', value: 'Seleccionar todos', class: 'myinput large' }).appendTo($("#conSelAll"));
                $('<label />', { 'for': 'ccball_1', text: 'Seleccionar todos' }).appendTo($("#conSelAll"));
                $.each(items, function (key, value) {
                    $("#Companies").append("<div class='row margin-right-lg margin-top-sm'>"
                        + "<input type='checkbox' class='Companies myinput large' id='ccb" + value.value + "' value='" + value.value + "' />"
                        + "<label for='ccb" + value.value + "'>" + value.text + "</label>"
                        + "</div>");
                    $("#ccb" + value.value).change(function () {
                        var $all = $(this)
                        if ($all[0].checked == true) {
                            $("input#" + $all[0].id).attr('checked', 'checked');
                        } else {
                            $("input#" + $all[0].id).removeAttr('checked');
                        }
                    });
                });
                $("#ccball_1").change(function () {
                    var $all = $(this)
                    var $check = $('input:checkbox.Companies');
                    if ($all[0].checked == true) {
                        $('input:checkbox.Companies').attr('checked', 'checked');
                        $.each($check, function (key, value) {
                            value.checked = true;
                        });
                    } else {
                        $('input:checkbox.Companies').removeAttr('checked');
                        $.each($check, function (key, value) {
                            value.checked = false;
                        });
                    }
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getFillAccount() {
    $.ajax({
        method: "GET"
        , url: Configuration.getAccountToFill
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $("#accSelAll").empty();
            $("#par").empty();
            $("#impar").empty();
            if (items != null) {
                $('<input />', { type: 'checkbox', id: 'acball_1', value: 'Seleccionar todos', class: 'myinput large' }).appendTo($("#accSelAll"));
                $('<label />', { 'for': 'acball_1', text: 'Seleccionar todos' }).appendTo($("#accSelAll"));
                $.each(items, function (key, value) {
                    var num = key + 1;
                    if (key % 2 === 0) {
                        $("#par").append("<div class='row margin-right-lg' id='account" + value.idCompany + "'>"
                            + "<div class='row margin-right-lg'><label>" + num + ".- Cuentas empresa " + value.companyName + "</label></div>"
                            + "</div>");
                        $.each(value.formSelectModel, function (j, valor) {
                            $("#account" + value.idCompany).append("<div class='row margin-right-lg'>"
                                + "<input type='checkbox' class='Accounts myinput large' id='acb" + valor.value + "' value='" + valor.value + "' />"
                                + "<label for='acb" + valor.value + "'>" + valor.text + "</label>"
                                + "</div>");

                            $("#acb" + valor.value).change(function () {
                                var $all = $(this)
                                if ($all[0].checked == true) {
                                    $("#" + $all[0].id).attr('checked', 'checked');
                                } else {
                                    $("#" + $all[0].id).removeAttr('checked');
                                }
                            });
                        });

                    }
                    else {
                        $("#impar").append("<div class='row margin-right-lg' id='account" + value.idCompany + "'>"
                            + "<div class='row margin-right-lg'><label>" + num + ".- Cuentas empresa " + value.companyName + "</label></div>"
                            + "</div>");
                        $.each(value.formSelectModel, function (j, valor) {
                            $("#account" + value.idCompany).append("<div class='row margin-right-lg'>"
                                + "<input type='checkbox' class='Accounts myinput large' id='acb" + valor.value + "' value='" + valor.value + "' />"
                                + "<label for='acb" + valor.value + "'>" + valor.text + "</label>"
                                + "</div>");

                            $("#acb" + valor.value).change(function () {
                                var $all = $(this)
                                if ($all[0].checked == true) {
                                    $("#" + $all[0].id).attr('checked', 'checked');
                                } else {
                                    $("#" + $all[0].id).removeAttr('checked');
                                }
                            });
                        });

                    }



                });
                $("#acball_1").change(function () {
                    var $all = $(this)
                    var $check = $('input:checkbox.Accounts');
                    if ($all[0].checked == true) {
                        $('input:checkbox.Accounts').attr('checked', 'checked');
                        $.each($check, function (key, value) {
                            value.checked = true;
                        });
                    } else {
                        $('input:checkbox.Accounts').removeAttr('checked');
                        $.each($check, function (key, value) {
                            value.checked = false;
                        });
                    }
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getCompanyByIdUser(UserId) {
    getDocumentRequest = $.ajax({
        method: "POST"
        , url: Configuration.getCompaniesByIdUser
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
    return getDocumentRequest;
}

function getAccountByIdUser(UserId) {
    getDocumentRequest = $.ajax({
        method: "POST"
        , url: Configuration.getAccountByIdUser
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
    return getDocumentRequest;
}

function getPermissionsByIdUser(UserId) {
    getDocumentRequest = $.ajax({
        method: "POST"
        , url: Configuration.getPermissionsByIdUser
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {


        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
    return getDocumentRequest;
}

function GetInfoByIdUser(id) {
    jsPromiseGetCompanyById = Promise.resolve(getCompanyByIdUser(id));
    jsPromiseGetCompanyById.then(function (response) {
        var items = response.data;

        if (items != null) {
            $.each(items, function (key, value) {
                $.each($(".Companies"), function (k, valor) {
                    var idCtrl = "#" + valor.id
                    if (idCtrl == "#ccb" + value.idcompany && value.usercompanyactive == true) {
                        $("#" + valor.id).attr('checked', 'checked');
                    }
                });
            });
        }

        jsPromiseGetAccountBy = Promise.resolve(getAccountByIdUser(id));

    }).then(response => jsPromiseGetAccountBy)
        .then(response => {
            var items = response.data;

            if (items != null) {
                $.each(items, function (key, value) {
                    $.each($(".Accounts"), function (k, valor) {
                        var idCtrl = "#" + valor.id
                        if (idCtrl == "#acb" + value.idBAccount && value.userbaccountActive == true) {
                            $("#" + valor.id).attr('checked', 'checked');
                        }
                    });
                });
            }

            jsPromiseGetPermissionsBy = Promise.resolve(getPermissionsByIdUser(id));
        }).then(response => jsPromiseGetPermissionsBy)
        .then(response => {
            // Restarting Checked
            $('.chkPermissionsClass').prop({
                indeterminate: false,
                checked: false
            });
            // restarting Label
            $('.lblPermissionsClass')
                .removeClass('custom-checked custom-unchecked custom-indeterminate')
                .addClass('custom-unchecked');

            var items = response.data;

            if (items != null) {
                $.each(items, function (key, value) {
                    /* $.each($(".Permissions"), function (k, valor) {
                        var idCtrl = "#" + valor.id
                        if (idCtrl == "#pcb" + value.idPermission && value.userpermissionActive == true) {
                            $("#" + valor.id).attr('checked', 'checked');
                        }
                    }); */
                    // Setting value checkeds

                    var name = "#pcb" + value.idpermission;
                    $(name).prop({
                        indeterminate: false,
                        checked: value.userpermissionactive
                    });

                    // Setting Class cheched
                    var nameLabel = "#lbl" + value.idpermission;
                    if (value.userpermissionactive == true) {
                        $(nameLabel).removeClass('custom-checked custom-unchecked custom-indeterminate')
                            .addClass('custom-checked');
                    }
                });
            }

        })
        .catch(function (error) {
            console.log("Failed Request! ->", error);
        });
}

function saveInfoUser() {

    if ($("#idProfileAccount").val() != 0) {
        var Users = {
            idUser: $("#hdfUserid").val(),
            idProfile: $("#idProfileAccount").val(),
            password: $("#password").val(),
            passwordconfirm: $("#passwordconfirm").val()
        };
        getDocumentRequest = $.ajax({
            method: "POST"
            , url: Configuration.saveInfoUserBy
            , datatype: 'json'
            , data: { user: Users }
        })
            .done(function (data, textStatus, jqXHR) {

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert("error");
            });
    } else {
        getDocumentRequest = 'Debe Seleccionar un perfil de usuario.';
    }

    return getDocumentRequest;
}

function saveCompanies() {

    var $check = $(".Companies");
    var Companies = [];
    if ($check.is(":checked") == false) {
        getDocumentRequest = "Debe seleccionar empresas para asignar al usuario.";
    }
    else {
        $.each($check, function (key, value) {
            var company = {}
            company.iduser = $("#hdfUserid").val(),
                company.idcompany = value.value,
                company.usercompanyactive = value.checked
            Companies.push(company)

        });

        getDocumentRequest = $.ajax({
            method: "POST"
            , url: Configuration.saveCompaniesBy
            , datatype: 'json'
            , data: { companies: Companies }
        })
            .done(function (data, textStatus, jqXHR) {

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert("error");
            });
    }
    return getDocumentRequest;
}

function saveAccounts() {
    var $check = $(".Accounts");
    var Accounts = [];
    if ($check.is(":checked") == false) {
        getDocumentRequest = "Debe seleccionar cuentas para asignar al usuario.";
    } else {
        $.each($check, function (key, value) {
            var account = {}
            account.idUser = $("#hdfUserid").val(),
                account.idBAccount = value.value,
                account.userbaccountActive = value.checked
            Accounts.push(account)

        });

        getDocumentRequest = $.ajax({
            method: "POST"
            , url: Configuration.saveAccountsBy
            , datatype: 'json'
            , data: { accounts: Accounts }
        })
            .done(function (data, textStatus, jqXHR) {


            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert("error");
            });
    }
    return getDocumentRequest;
}

function savePermissions() {

    // var $check = $(".Permissions");
    var $checked = $('.custom-checked');
    var $unchecked = $('.custom-unchecked');

    var Permissions = [];
    if ($checked.length == 0) {
        getDocumentRequest = "Debe seleccionar permisos para asignar al usuario.";
    } else {
        $.each($checked, function (key, value) {
            var permission = {}
            permission.iduser = $("#hdfUserid").val(),
                permission.idpermission = value.id.slice(3),
                permission.userpermissionactive = true
            Permissions.push(permission)

        });

        $.each($unchecked, function (key, value) {
            var permission = {}
            permission.iduser = $("#hdfUserid").val(),
                permission.idpermission = value.id.slice(3),
                permission.userpermissionactive = false
            Permissions.push(permission)

        });

        getDocumentRequest = $.ajax({
            method: "POST"
            , url: Configuration.savePermissionsBy
            , datatype: 'json'
            , data: { permissions: Permissions }
        })
            .done(function (data, textStatus, jqXHR) {

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert("error");
            });
    }
    return getDocumentRequest;
}

var _html = [];
function loadPermissionsAll() {
    var jsPromiseGetPermissions = Promise.resolve(PrepareRequestGetPermissionsAll());
    jsPromiseGetPermissions.then(function (response) {
        html = [];
        GeneratePermissionsAll(response.data, 0);
        $('#page-wrap').append(_html.join(''));
        $('input[type="checkbox"]').change(checkboxChanged); // Activate all cheackboxes action

    })
        .catch(function (error) {
            console.log("Failed Request! loadPermissionsAll ->", error);
            notifyMessageGral("No se pueden cargar la lista de permisos.", "error", null, null);
        });
}

function PrepareRequestGetPermissionsAll() {

    var getPermissionsAllRequest = $.ajax({
        method: "GET"
        , url: Configuration.getPermissionsToFill
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getPermissionsAllRequest;
}

function GeneratePermissionsAll(data, parent) {


    if (parent == 0) { _html.push('<ul class="treeview">'); } else { _html.push('<ul class="">'); }

    $.each(data, function (i, val) {

        var name = "pcb" + val.idPermission;
        var nameLabel = "lbl" + val.idPermission;
        _html.push('<li> <input type="checkbox" id="' + name + '" name ="' + name + '" value="" class="' + "chkPermissionsClass" + ' " > <label id="' + nameLabel + '" name="' + nameLabel + '" value="' + val.idPermission + '" for="' + name + '" title="' + val.permissionDescription + '" class="custom-unchecked lblPermissionsClass">' + val.permissionTitle + '</label>');

        if (val.subpermisionmodel) {
            GeneratePermissionsAll(val.subpermisionmodel, val.idPermission)
        }
        _html.push('</li>');
    });
    _html.push('</ul>');
};

function checkboxChanged() {
    // alert("ecercerc");
    var $this = $(this),
        checked = $this.prop("checked"),
        container = $this.parent(),
        siblings = container.siblings();

    container.find('input[type="checkbox"]')
        .prop({
            indeterminate: false,
            checked: checked
        })
        .siblings('label')
        .removeClass('custom-checked custom-unchecked custom-indeterminate')
        .addClass(checked ? 'custom-checked' : 'custom-unchecked');

    checkSiblings(container, checked);
}

function checkSiblings($el, checked) {
    var parent = $el.parent().parent(),
        all = true,
        indeterminate = false;

    $el.siblings().each(function () {
        return all = ($(this).children('input[type="checkbox"]').prop("checked") === checked);
    });

    if (all && checked) {
        parent.children('input[type="checkbox"]')
            .prop({
                indeterminate: false,
                checked: checked
            })
            .siblings('label')
            .removeClass('custom-checked custom-unchecked custom-indeterminate')
            .addClass(checked ? 'custom-checked' : 'custom-unchecked');

        checkSiblings(parent, checked);
    }
    else if (all && !checked) {
        indeterminate = parent.find('input[type="checkbox"]:checked').length > 0;

        parent.children('input[type="checkbox"]')
            .prop("checked", checked)
            .prop("indeterminate", indeterminate)
            .siblings('label')
            .removeClass('custom-checked custom-unchecked custom-indeterminate')
            .addClass(indeterminate ? 'custom-indeterminate' : (checked ? 'custom-checked' : 'custom-unchecked'));

        checkSiblings(parent, checked);
    }
    else {
        $el.parents("li").children('input[type="checkbox"]')
            .prop({
                indeterminate: true,
                checked: false
            })
            .siblings('label')
            .removeClass('custom-checked custom-unchecked custom-indeterminate')
            .addClass('custom-indeterminate');
    }
}

function getPermissions() {
    loadPermissionsAll();
}