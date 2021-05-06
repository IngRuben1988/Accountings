function VTAComboBoxDinamic(parameters, urls, methods, components, asyncs) {
    var ComboBox = $(components);
    $.ajax({
        async:  asyncs,
        method: methods,
        url: urls,
        datatype: "json",
        data: parameters,
    })
        .done(function (data, textStatus, jqXHR) {
            $.each(data.data, function (key, value) {
                ComboBox.append($("<option></option>").attr("value", value.value));
            });
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log('Problemas en el Servcio');
        });
}

function populateaccountl1() {
    $.ajax({
        method: "GET",
        url: accountlevels.level1,
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            $.each(data.data, function (key, value) {
                $('#accountl1').append($("<option></option>").attr("value", value.value).text(value.text));
            });
            $("#accountl1 option[value='1']").remove();
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
}

function populateaccountl2(selected, id) {
    $.ajax({
        method: "GET",
        url: accountlevels.level2,
        data: { id: id },
        datatype: 'json',
    })
        .done(function (data, textStatus, jqXHR) {
            $("#accountl2").removeAttr("disabled");
            $('#accountl2').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#accountl2').append($("<option></option>").attr("value", value.value).text(value.text));
            });
            if (selected) {
                $('#accountl2').val(selected);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    return true;
}

function populateAccuntL3(selected, id) {
    $.ajax({
        method: "GET",
        url: accountlevels.level3,
        data: { id: id },
        datatype: 'json',
    })
        .done(function (data, textStatus, jqXHR) {
            $("#category").removeAttr("disabled");
            $('#category').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#category').append($("<option></option>").attr("value", value.value).text(value.text));
            });

            if (selected) {
                $('#category').val(selected);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
}

function updateTypeCallBack(selected, category, Company, accl1) {
    $.ajax({
        url: accountlevels.level4,
        type: "GET",
        datatype: 'json',
        data: { id: category, Company: Company, accl1: accl1 },
        success: function (data, textStatus, jqXHR) {
            $("#Type").removeAttr("disabled");
            $('#Type').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#Type').append($("<option></option>").attr("value", value.value).text(value.text));
            });
            if (selected) {
                $("#Type").val(selected);
            }
        },
        error: function () {
            $("#info").notify("No se puede obtener los tipos de gastos.", {
                position: "bottom right",
                className: "error",
                hideDuration: 500
            });
        }
    });
}



function VTAGridviewDinamic(parameter, urls, heads, updater, eraser,hidencolumn) {
    GridView.Head.empty();
    GridView.Body.empty();
    var tblbody = "";
    var hidecol = hidencolumn.length;
    var coslpan = hidecol > 0 ? heads.length - hidecol : heads.length;
    $.each(heads, function (key, value) {
        GridView.Head.append($("<td></td>").append(value));
    });
    $.ajax({
        url: urls,
        type: "GET",
        datatype: "json",
        data: { parameters: parameter },
        success: function (data, textStatus, jqXHR) {
            if (data.data != null) {
                if (data.data.length > 0) {
                    $.each(data.data, function (key, value) {
                        tblbody += '<tr class="tblfield' + key + '">';
                        $.each(value, function (key, value) {
                            tblbody += '<td class="' + key + '">' + value + '</td>';
                        });

                        if (updater != "" || updater != null) {
                            tblbody +=
                                '<td>' +
                                '<center>' +
                                '<button id="tblupdate' + key + '" class="updater tblupdate' + key + '" style=" background: none; border: none; width: 30px;" OnClick="' + updater + '">' +
                                '<img src="../Content/fontawesome/svgs/regular/edit.svg"/>' +
                                '</button>' +
                                '</center>' +
                                '</td>';
                        }
                        if (eraser != null) {
                            tblbody +=
                                '<td>' +
                                '<center>' +
                                '<button id="tbleraser' + key + '" class="eraser  tbleraser' + key + '" style=" background: none; border: none; width: 30px;" OnClick="' + eraser + '">' +
                                '<img src="../Content/fontawesome/svgs/regular/trash-alt.svg"/>' +
                                '</button>' +
                                '</center>' +
                                '</td>';
                        }
                        tblbody += '</tr>';
                    });
                    GridView.Body.append(tblbody);

                    $.each(hidencolumn, function (key, value) {
                        $("td." + value).hide();
                    });
                } else {
                    tblbody = "<tr><td colspan='" + coslpan +"' style='text-align:center;'>Sin datos</td></tr>"
                    GridView.Body.append(tblbody);
                }
            } else {
                tblbody = "<tr><td colspan='" + coslpan +"' style='text-align:center;'>Sin datos</td></tr>"
                GridView.Body.append(tblbody);
            }
        }
        ,
        error: function (error) {
            console.log("Algo salio mal ERROR");
        }
    })
        //.finally(function () {

        //});

}

function reloadGrid(gridId) {
    $(gridId).jsGrid("loadData");
}