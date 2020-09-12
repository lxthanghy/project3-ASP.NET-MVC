var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        //Tìm kiếm home
        $('#btnSearchHome').on('click', function (e) {
            e.preventDefault();
            var txtSearchHome = $('#txtSearchHome').val();
            product.SearchHome(txtSearchHome);
        });
        $('#txtSearchHome').keyup(function () {
            $('#result').html('');
            var searchField = $('#txtSearchHome').val();
            $.ajax({
                url: '/Product/SearchKeyup',
                type: 'GET',
                dataType: 'json',
                data: {
                    search: searchField
                },
                success: function (res) {
                    $.each(res, function (index, value) {
                        var link = `/chi-tiet/${value.Alias}/${value.ID}`;
                        var html = `<div class="list-group-item">
                                        <img class="mr-1" src="${value.Image}" alt="" height="40">
                                        <a href="${link}" class="font-weight-bold">${value.Name}</a>
                                        <span>${value.SupplierName}</span>
                                    </div>`;
                        $('#result').append(html);
                    });
                }
            });
        });

    },
    SearchHome: function (txtSearchHome) {
        window.location.href = "/Product/SearchProduct?searchName=" + txtSearchHome;
    }
}
product.init();