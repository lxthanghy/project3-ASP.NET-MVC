var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var searchName = $('#txtSearch').val();
var sort = $('#txtSort').val();
var txtCategoryAlias = $('#txtCategoryAlias').val();
var txtSupplierName = $('#txtSupplierName').val();
var IDCate = $('#txtIDCate').val();
var IDSupp = $('#txtIDSupp').val();
$('#slSort').val(sort);
var CSShop = {
    init: function () {
        CSShop.registerEvents();
    },
    registerEvents: function () {
        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            CSShop.LoadData();
        });
        $('#btnSearch').on('click', function (e) {
            e.preventDefault();
            searchName = $('#txtSearch').val();
            CSShop.LoadData();
        });
        $('#slSort').on('change', function () {
            sort = $('#slSort').val();
            CSShop.LoadData();
        });
    },
    LoadData: function () {
        window.location.replace("/" + txtCategoryAlias + "/" + txtSupplierName + "/" + IDCate + "/" + IDSupp + "?page=1&size=" + pageSize + "&searchName=" + searchName + "&sort=" + sort);
    }
}
CSShop.init();