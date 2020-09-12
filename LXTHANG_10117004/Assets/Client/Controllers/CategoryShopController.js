var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var searchName = $('#txtSearch').val();
var sort = $('#txtSort').val();
var txtAlias = $('#txtAlias').val();
var id = $('#txtID').val();
$('#slSort').val(sort);
var CategoryShop = {
    init: function () {
        CategoryShop.registerEvents();
    },
    registerEvents: function () {
        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            CategoryShop.LoadData();
        });
        $('#btnSearch').on('click', function (e) {
            e.preventDefault();
            searchName = $('#txtSearch').val();
            CategoryShop.LoadData();
        });
        $('#slSort').on('change', function () {
            sort = $('#slSort').val();
            CategoryShop.LoadData();
        });
    },
    LoadData: function () {
        window.location.replace("/loai/" + txtAlias + "/" + id + "?page=1&size=" + pageSize + "&searchName=" + searchName + "&sort=" + sort);
    }
}
CategoryShop.init();