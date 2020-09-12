var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var searchName = $('#txtSearch').val();
var sort = $('#txtSort').val();
$('#slSort').val(sort);
var SaleShop = {
    init: function () {
        SaleShop.registerEvents();
    },
    registerEvents: function () {
        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            SaleShop.LoadData();
        });
        $('#btnSearch').on('click', function (e) {
            e.preventDefault();
            searchName = $('#txtSearch').val();
            SaleShop.LoadData();
        });
        $('#slSort').on('change', function () {
            sort = $('#slSort').val();
            SaleShop.LoadData();
        });
    },
    LoadData: function () {
        window.location.replace("/sale-shop?page=1&size=" + pageSize + "&searchName=" + searchName + "&sort=" + sort);
    }
}
SaleShop.init();