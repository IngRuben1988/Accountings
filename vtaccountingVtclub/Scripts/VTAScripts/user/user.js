$(document).ready(function () {
    //getAllUsers();
    //getFillUsers();
    //getFillProfile();
    //getFillCompany();
    //getFillAccount();
    //loadPermissionsAll();

    //users gridview  
    VTAGridviewDinamic(null, Configuration.getUsers, TblHeadUsers, "edit()",null);

    ConfigUsers.identity.on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    ConfigUsers.btncall.click(function (event) {
        event.preventDefault();
        searchUser();
    });

    ConfigUsers.btnuser.click(function (event) {
        event.preventDefault();
        jsPromiseSaveDocument = Promise.resolve(saveInfoUser());
        jsPromiseSaveDocument.then(function (response) {
            _dataObject = response.data;
            var frmUser = $("#formUser");
            if (response.success === true) {
                frmUser.notify("Se ha guardado la información. " + response.message, {
                    position: "top right",
                    className: "success",
                    hideDuration: 500
                });
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

    });

    ConfigUsers.btncompany.click(function (event) {
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

    ConfigUsers.btnaccount.click(function (event) {
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

    ConfigUsers.btnpermits.click(function (event) {
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

    ConfigUsers.btngeneral.click(function (event) {
        event.preventDefault();

        jsPromiseSaveDocument = Promise.resolve(saveInfoUser());

        jsPromiseSaveDocument.then(function (response) {
            _dataObject = response.data;
            var frmUser = $("#formUser");
            if (response.success === true) {
                frmUser.notify("Se ha guardado la información. " + response.message, {
                    position: "top right",
                    className: "success",
                    hideDuration: 500
                });
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

    });

    ConfigUsers.tooltip.tooltip();

    ConfigUsers.btnback.on('click', function () {
        $('#divUsetoWork').css("display", "none");
        $('#divSearchGral').css("display", "block");

    });
});

function edit() {
    alert('success');
}

function searchUser() {
    $.ajax({
        method: "GET"
        , url: "/users/searchUsers"
        , datatype: 'json'
        , data: { id: ConfigUsers.identity.val(), userName: $("#userName").val() }
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;

            if (items.length < 10) {
                $("#tblAllUsers tbody").empty();
                $.each(items, function (key, value) {
                    $("#tblAllUsers tbody").append("<tr class='jsgrid-row'>"
                        + "<td class='jsgrid-cell'>" + value.idUser + "</td>"
                        + "<td class='jsgrid-cell'>" + value.userName + "</td>"
                        + "<td class='jsgrid-cell'>" + value.userCompledName + "</td>"
                        + "<td class='jsgrid-cell'>" + value.userEmail + "</td>"
                        + "<td class='jsgrid-cell'>" + value.userProfile + "</td>"
                        + "<td class='jsgrid-cell'><i id='user_" + value.idUser + "' class='fa fa-edit fa-lg' data-toggle='tooltip' data-original-title='Editar' style='cursor: pointer;'></td>"
                        + "</tr>");
                    $("#user_" + value.idUser).click(function () {
                        $('#divUsetoWork').css("display", "block");
                        $('#divSearchGral').css("display", "none");
                        // $("#btnBusqueda").css("display", "none");
                        // $("#ctrlBusqueda").css("display", "none");
                        // $("#infoUsuarios").css("display", "none");
                        $("#nombreUsuario").text(value.userCompledName);
                        // $("#infoUser").css("display", "block");
                        // $("#infoCompany").css("display", "block");
                        // $("#infoAccount").css("display", "block");
                        // $("#infoPermissions").css("display", "block");
                        // $("#btnActions").css("display", "block");
                        $("#exTab1").css("display", "block");
                        $("select#idUser option[value='" + value.idUser + "']").attr("selected", true);
                        $("select#idUser").prop('disabled', 'disabled');
                        $("#hdfUserid").val(value.idUser);
                        if (value.idProfile != 0) {
                            $("select#idProfileAccount option[value='" + value.idProfile + "']").attr("selected", true);
                        }
                        getCompanyByIdUser(value.idUser);
                        getAccountByIdUser(value.idUser);
                        getPermissionsByIdUser(value.idUser);
                    });
                });
            } else {
                $('#pagination').empty();
                $('#pagination').removeData("twbs-pagination");
                $('#pagination').unbind("page");
                records = data.data;
                totalRecords = records.length;
                totalPages = Math.ceil(totalRecords / recPerPage);
                apply_pagination();
                //generate_table();
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getAllUsers() {
    $.ajax({
        method: "GET"
        , url: "/users/getAllUsers"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            records = data.data;
            totalRecords = records.length;
            totalPages = Math.ceil(totalRecords / recPerPage);
            apply_pagination();


        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function apply_pagination() {
    $pagination.twbsPagination({
        totalPages: totalPages,
        visiblePages: 6,
        first: 'Primera',
        prev: 'Anterior',
        next: 'Siguiente',
        last: 'Última',
        onPageClick: function (event, page) {
            displayRecordsIndex = Math.max(page - 1, 0) * recPerPage;
            endRec = (displayRecordsIndex) + recPerPage;

            displayRecords = records.slice(displayRecordsIndex, endRec);
            generate_table();
        }
    });
}

function generate_table() {
    $("#tblAllUsers tbody").empty();
    if (displayRecords != null) {
        $.each(displayRecords, function (key, value) {
            $("#tblAllUsers tbody").append("<tr class='jsgrid-row'>"
                + "<td class='jsgrid-cell'>" + value.idUser + "</td>"
                + "<td class='jsgrid-cell'>" + value.userName + "</td>"
                + "<td class='jsgrid-cell'>" + value.userCompledName + "</td>"
                + "<td class='jsgrid-cell'>" + value.userEmail + "</td>"
                + "<td class='jsgrid-cell'>" + value.userProfile + "</td>"
                + "<td class='jsgrid-cell'><i id='user_" + value.idUser + "' class='fa fa-edit fa-lg' data-toggle='tooltip' data-original-title='Editar' style='cursor: pointer;'></td>"
                + "</tr>");
            $("#user_" + value.idUser).click(function () {
                $('#divUsetoWork').css("display", "block");
                $('#divSearchGral').css("display", "none");
                // $("#btnBusqueda").css("display", "none");
                // $("#ctrlBusqueda").css("display", "none");
                // $("#infoUsuarios").css("display", "none");
                $("#nombreUsuario").text(value.userCompledName + " | " + value.userName + " | " + value.idUser);
                // $("#infoUser").css("display", "block");
                // $("#infoCompany").css("display", "block");
                // $("#infoAccount").css("display", "block");
                // $("#infoPermissions").css("display", "block");
                // $("#btnActions").css("display", "block");
                $("#exTab1").css("display", "block");
                $("select#idUser option[value='" + value.idUser + "']").attr("selected", true);
                $("select#idUser").prop('disabled', 'disabled');
                $("#hdfUserid").val(value.idUser);
                if (value.idProfile != 0) {
                    $("select#idProfileAccount option[value='" + value.idProfile + "']").attr("selected", true);
                }
                getCompanyByIdUser(value.idUser);
                getAccountByIdUser(value.idUser);
                getPermissionsByIdUser(value.idUser);
            });
        });
    }
}

function getFillUsers() {
    $.ajax({
        method: "GET"
        , url: "/users/getUserToFillSelect"
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
        , url: "/users/getToFillProfile"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $("#idProfileAccount").empty();
            var $dropdownprofile = $("#idProfileAccount");
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
        , url: "/users/getToFillCompanies"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $("#conSelAll").empty();
            $("#Companies").empty();
            if (items != null) {
                $('<input />', { type: 'checkbox', id: 'ccball_1', value: 'Seleccionar todos' }).appendTo($("#conSelAll"));
                $('<label />', { 'for': 'ccball_1', text: 'Seleccionar todos' }).appendTo($("#conSelAll"));
                $.each(items, function (key, value) {
                    $("#Companies").append("<div class='row'>"
                        + "<input type='checkbox' class='Companies' id='ccb" + value.value + "' value='" + value.value + "' />"
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
        , url: "/users/getToFillAccount"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;
            $("#accSelAll").empty();
            $("#par").empty();
            $("#impar").empty();
            if (items != null) {
                $('<input />', { type: 'checkbox', id: 'acball_1', value: 'Seleccionar todos' }).appendTo($("#accSelAll"));
                $('<label />', { 'for': 'acball_1', text: 'Seleccionar todos' }).appendTo($("#accSelAll"));
                $.each(items, function (key, value) {
                    var num = key + 1;
                    if (key % 2 === 0) {
                        $("#par").append("<div class='row' id='account" + value.idCompany + "'>"
                            + "<div class='row'><label>" + num + ".- Cuentas empresa " + value.companyName + "</label></div>"
                            + "</div>");
                        $.each(value.formSelectModel, function (j, valor) {
                            $("#account" + value.idCompany).append("<div class='row'>"
                                + "<input type='checkbox' class='Accounts' id='acb" + valor.value + "' value='" + valor.value + "' />"
                                + "<label for='acb" + valor.value + "'>" + valor.text + "</label>"
                                + "</div>");
                        });
                        $("#acb" + value.value).change(function () {
                            var $all = $(this)
                            if ($all[0].checked == true) {
                                $("#" + $all[0].id).attr('checked', 'checked');
                            } else {
                                $("#" + $all[0].id).removeAttr('checked');
                            }
                        });
                    }
                    else {
                        $("#impar").append("<div class='row' id='account" + value.idCompany + "'>"
                            + "<div class='row'><label>" + num + ".- Cuentas empresa " + value.companyName + "</label></div>"
                            + "</div>");
                        $.each(value.formSelectModel, function (j, valor) {
                            $("#account" + value.idCompany).append("<div class='row'>"
                                + "<input type='checkbox' class='Accounts' id='acb" + valor.value + "' value='" + valor.value + "' />"
                                + "<label for='acb" + valor.value + "'>" + valor.text + "</label>"
                                + "</div>");
                        });
                        $("#acb" + value.value).change(function () {
                            var $all = $(this)
                            if ($all[0].checked == true) {
                                $("#" + $all[0].id).attr('checked', 'checked');
                            } else {
                                $("#" + $all[0].id).removeAttr('checked');
                            }
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

function getFillPermissions() {
    $.ajax({
        method: "GET"
        , url: "/users/getToFillPermissions"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;

            $("#perSelAll").empty();
            $("#Permissions").empty();
            if (items != null) {

                $('<input />', { type: 'checkbox', id: 'pcball_1', value: 'Seleccionar todos' }).appendTo($("#perSelAll"));
                $('<label />', { 'for': 'pcball_1', text: 'Seleccionar todos' }).appendTo($("#perSelAll"));
                /*
                $.each(items, function (key, value) {
                    var num = key + 1;
                    $("#Permissions").append("<div class='row' id='content" + value.idPermission + "'><div class='row'><label>" + num + ".- Permisos " + value.permissionTitle + "</label></div><div class='form-group margin-right-md'><div class='tooltipermissions'>"
                     + "<input type='checkbox' class='Permissions' id='pcb" + value.idPermission + "' value='" + value.idPermission + "' />"
                     + "<label for='pcb" + value.idPermission + "'>" + value.permissionTitle + "</label>"
                     + "<span class='tooltipermissionstext'>" + value.permissionDescription + "</span>"
                     + "</div></div></div>");
                    $.each(value.formSelectModel, function (j, valor) {
                        $("#content" + value.idPermission).append("<div class='form-group margin-right-md'><div class='tooltipermissions'>"
                     + "<input type='checkbox' class='Permissions' id='pcb" + valor.value + "' value='" + valor.value + "' />"
                     + "<label for='pcb" + valor.value + "'>" + valor.text + "</label>"
                     + "<span class='tooltipermissionstext'>" + valor.shortText + "</span>"
                     + "</div></div>");
                    });
                    $("#pcb" + value.idPermission).change(function () {
                        var $all = $(this)
                        if ($all[0].checked == true) {
                            $("#" + $all[0].id).attr('checked', 'checked');
                        } else {
                            $("#" + $all[0].id).removeAttr('checked');
                        }
                    });
                });
                */
                $("#pcball_1").change(function () {
                    var $all = $(this);
                    var $check = $('input:checkbox.Permissions');
                    if ($all[0].checked == true) {
                        $('input:checkbox.Permissions').attr('checked', 'checked');
                        $.each($check, function (key, value) {
                            value.checked = true;
                        });
                    } else {
                        $('input:checkbox.Permissions').removeAttr('checked');
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
    $.ajax({
        method: "POST"
        , url: "/users/getCompaniesByIdUser"
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;

            if (items != null) {
                $.each(items, function (key, value) {
                    $.each($(".Companies"), function (k, valor) {
                        var idCtrl = "#" + valor.id
                        if (idCtrl == "#ccb" + value.idCompany && value.userCompanyActive == true) {
                            $("#" + valor.id).attr('checked', 'checked');
                        }
                    });
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getAccountByIdUser(UserId) {
    $.ajax({
        method: "POST"
        , url: "/users/getAccountByIdUser"
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {
            var items = data.data;

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

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function getPermissionsByIdUser(UserId) {
    $.ajax({
        method: "POST"
        , url: "/users/getPermissionsByIdUser"
        , datatype: 'json'
        , data: { idUser: UserId }
    })
        .done(function (data, textStatus, jqXHR) {
            // Restarting Checked
            $('.chkPermissionsClass').prop({
                indeterminate: false,
                checked: false
            });
            // restarting Label
            $('.lblPermissionsClass')
                .removeClass('custom-checked custom-unchecked custom-indeterminate')
                .addClass('custom-unchecked');


            var items = data.data;

            if (items != null) {
                $.each(items, function (key, value) {
                    /* $.each($(".Permissions"), function (k, valor) {
                        var idCtrl = "#" + valor.id
                        if (idCtrl == "#pcb" + value.idPermission && value.userpermissionActive == true) {
                            $("#" + valor.id).attr('checked', 'checked');
                        }
                    }); */
                    // Setting value checkeds

                    var name = "#pcb" + value.idPermission;
                    $(name).prop({
                        indeterminate: false,
                        checked: value.userpermissionActive
                    });

                    // Setting Class cheched
                    var nameLabel = "#lbl" + value.idPermission;
                    if (value.userpermissionActive == true) {
                        $(nameLabel).removeClass('custom-checked custom-unchecked custom-indeterminate')
                            .addClass('custom-checked');
                    }
                });
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        });
}

function saveInfoUser() {

    if ($("#idProfileAccount").val() != 0) {
        var Users = {
            idUser: $("#hdfUserid").val(),
            idProfile: $("#idProfileAccount").val(),
        };
        getDocumentRequest = $.ajax({
            method: "POST"
            , url: "/users/saveInfoUserByUser"
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
        getDocumentRequest = "Debe Seleccionar empresas para el usuario.";
    }
    else {
        $.each($check, function (key, value) {
            var company = {}
            company.idUser = $("#hdfUserid").val(),
                company.idCompany = value.value,
                company.usercompanyActive = value.checked
            Companies.push(company)

        });

        getDocumentRequest = $.ajax({
            method: "POST"
            , url: "/users/saveCompaniesByUser"
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
        getDocumentRequest = "Debe Seleccionar cuentas para el usuario.";
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
            , url: "/users/saveAccountsByUser"
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
        getDocumentRequest = "Debe Seleccionar permisos para el usuario.";
    } else {
        $.each($checked, function (key, value) {
            var permission = {}
            permission.idUser = $("#hdfUserid").val(),
                permission.idPermission = value.id.slice(3),
                permission.userpermissionActive = true
            Permissions.push(permission)

        });

        $.each($unchecked, function (key, value) {
            var permission = {}
            permission.idUser = $("#hdfUserid").val(),
                permission.idPermission = value.id.slice(3),
                permission.userpermissionActive = false
            Permissions.push(permission)

        });

        getDocumentRequest = $.ajax({
            method: "POST"
            , url: "/users/savePermissionsByUser"
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

//
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
        , url: "/users/getToFillPermissions"
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
        // var _classid = "idClass";
        // var _classChild = "parentClass" + val.idPermission;
        // var _classParent = "parentClass" + parent;
        // var _class = render == 0 ? "parentClass" + 0 : "parentClass" + render;
        var name = "pcb" + val.idPermission;
        var nameLabel = "lbl" + val.idPermission;
        _html.push('<li> <input type="checkbox" id="' + name + '" name ="' + name + '" value="" class="' + "chkPermissionsClass" + ' " > <label id="' + nameLabel + '" name="' + nameLabel + '" value="' + val.idPermission + '" for="' + name + '" title="' + val.permissionDescription + '" class="custom-unchecked lblPermissionsClass">' + val.permissionTitle + '</label>');
        // _html.push('<input type="checkbox" id="pcb' + val.idPermission + '" value="Seleccionar todos"/>');

        if (val.subpermisionmodel) {
            // render == 0 ? GeneratePermissionsAll(val.subpermisionmodel, 0) : GeneratePermissionsAll(val.subpermisionmodel, val.idPermission);
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