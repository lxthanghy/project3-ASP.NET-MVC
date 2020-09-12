var Search = {
    init: function () {
        Search.registerEvents();
    },
    registerEvents: function () {
        $('#btnHomeSearch').on('click', function (e) {
            e.preventDefault();
            var txtHomeSearch = $('#txtHomeSearch').val();
            Search.SearchHome(txtHomeSearch);
        });
    },
    SearchHome: function (txtSearchHome) {
        window.location.href = "/Product/SearchProduct?searchName=" + txtSearchHome;
    }
}
Search.init();