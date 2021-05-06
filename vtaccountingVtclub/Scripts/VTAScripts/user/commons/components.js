var $pagination = $('#pagination'),
    totalRecords = 0,
    records = [],
    displayRecords = [],
    recPerPage = 10,
    page = 1,
    totalPages = 0;
var jsPromiseSaveDocument;
var jsPromiseSaveDocumentEmpresas;
var jsPromiseSaveDocumentAccounts;
var jsPromiseSaveDocumentPermissions;
var getDocumentRequest;
var _dataObject = {};

var ConfigUsers = {}
ConfigUsers.identity   = $("#idUser");
ConfigUsers.btncall    = $("#btnAdvanceSearch");
ConfigUsers.btnuser    = $("#btnSaveUser");
ConfigUsers.btncompany = $("#btnSaveCompany");
ConfigUsers.btnaccount = $("#btnSaveAccount");
ConfigUsers.btnpermits = $("#btnSavePermissions");
ConfigUsers.btngeneral = $("#btnSaveGeneral");
ConfigUsers.tooltip    = $('[data-toggle="tooltip"]');

var TblHeadUsers = ["Perfil VTA", "Numero Usuario", "Nombre Completo", "Correo Electronico", "Nombre de Usuario", "Perfil", "Configuracion"]; 
  


