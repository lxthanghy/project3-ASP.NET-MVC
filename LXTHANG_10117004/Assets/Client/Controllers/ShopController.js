var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var searchName = $('#txtSearch').val();
var sort = $('#txtSort').val();
$('#slSort').val(sort);
var Shop = {
    init: function () {
        Shop.registerEvents();
    },
    registerEvents: function () {
        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            Shop.LoadData();
        });
        $('#btnSearch').on('click', function (e) {
            e.preventDefault();
            searchName = $('#txtSearch').val();
            Shop.LoadData();
        });
        $('#slSort').on('change', function () {
            sort = $('#slSort').val();
            Shop.LoadData();
        });
    },
    LoadData: function () {
        window.location.replace("/xe-dap?page=1&size=" + pageSize + "&searchName=" + searchName + "&sort=" + sort);
    }
}
Shop.init();