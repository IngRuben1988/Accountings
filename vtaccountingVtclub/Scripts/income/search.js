/********************************************************************************************************************/
/********************************************************************************************************************/
/******************************************** I N C O M E   S E A R C H *********************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/

// Working Search
function HideworkingSearch(index) {
    $(".workingSearch").hide();
    incomeObject = incomesObjectsList[index];
    // Get DOcuments type to Attach
    prepareEdit('#incomeForm', incomeObject);
    ShowworkingIncome();


    setTimeout(function () {
        EditJSGriIncomeMovements(incomeObject.item)
    }, 2000);
}

function ShowworkingSearch() {
    $(".workingSearch").show();
    SegmentCompanyEdit = false;
}
// Working Invoice
function HideworkingIncome() {
    $(".divIncomeGral").hide();
    ShowworkingSearch();
    document.title = "VTA - Edición ingreso";
    // clearAllData();
    incomeObject = {};
}

function ShowworkingIncome() {
    $(".divIncomeGral").show();
}

$('#btnAdvanceSearch').click(function () {
    printgridIncomes();
});

function printgridIncomes() {
    OpenModalProcessing(".processingModal");
    deleteDataTableIncomesTR();
    var searchHead = {
        id: $('#id').val(),
        number: $("#number").val(),
        company: $("#company").val(),
        ammountIni: $("#ammountIni").val(),
        ammountEnd: $("#ammountEnd").val(),
        applicationDateIni: $("#applicationDateIni").val(),
        applicationDateFin: $("#applicationDateFin").val(),
        creationDateIni: $("#creationDateIni").val(),
        creationDateFin: $("#creationDateFin").val()
    }
    axios.post("/Income/GetIncomes",
        { id: searchHead.id, number: searchHead.number, company: searchHead.company, ammountIni: searchHead.ammountIni, ammountEnd: searchHead.ammountEnd, applicationDateIni: searchHead.applicationDateIni, applicationDateFin: searchHead.applicationDateFin, creationDateIni: searchHead.creationDateIni, creationDateFin: searchHead.creationDateFin })
        .then(function (response) {
            incomesObjectsList = response.data.data;
            printDataTableIncomesTR(incomesObjectsList);
            // console.log(incomesObjectsList);
        }).catch(function (error) {
            console.error("No se puede obtener la lista de los ingresos " + parent);
            notifyMessageGral("No se puede obtener la lista de ingresos.", 'error', 1500, '.info');
        }).finally(function () {
            ForceCloseModal(".processingModal");
        });

}

function printDataTableIncomesTR(data) {
    var tr = "";
    var endtr = "</tr>";
    var resulttr = "";
    $('#tblIncomes > tbody').empty();
    if (data.length != 0) {
        $.each(data, function (key, value) {
            tr = "<tr class='incomes_class_tr' id='incomes_" + value.item + "'>";
            // if (value.length != 0) {
            resulttr = (tr + printDataTableIncomesTD(value) + endtr);
            $('#tblIncomes > tbody:last-child').append(resulttr);
            // }
        });
    }
}

function printDataTableIncomesTD(data) {
    var actions = "", editar = "", eliminar = "";
    if (incomeRegisterEdit == true) {
        editar = "<input type='button' class='tablegrid-button-edit tablegrid-edit-button' data-toggle='tooltip' data-placement='top' title='Editar' onClick='HideworkingSearch(" + data.index + ")'>";          
    }
    if (incomeDelete == true) {
        eliminar = "<input type='button' class='tablegrid-button tablegrid-delete-button' data-toggle='tooltip' data-placement='top' title='Borrar' onclick='deleteIncomeOnly(" + data.index + ")' >";
    }
    actions = "<td style='width: 100px;'>" + editar + eliminar + "</td>";
    var result =
        "<td style='text-align: center !important;'>" + data.row + "</td>" +
        "<td style='text-align: center !important;'>" + data.identifier + " </td>" +
        "<td style='text-align: right; padding-right: 4rem !important;'>" + data.coststring + "</td>" +
        "<td style='text-align: center !important;'>" + data.creationdatestring + "</td>" +
        "<td style='text-align: center !important;'>" + data.applicationdatestring + "</td>" + actions;
    return result;
}

function deleteDataTableIncomesTR() {
    $('.incomes_class_tr').remove();
}