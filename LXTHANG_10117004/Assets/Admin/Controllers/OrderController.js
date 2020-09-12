var Order = {
    init: function () {
        Order.registerEvents();
    },
    registerEvents: function () {
        $('.btnInfo').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            Order.ViewDetail(id);
        });
        //Chọn trạng thái đơn hàng
        $('.slStatus').off('change').on('change', function () {
            var status = parseInt($(this).val());
            var id = $(this).data('id');
            if (id != -1) {
                Order.UpdateStatus(id, status);
            }
        });
        //Xóa đơn hàng
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            Swal.fire({
                title: 'Bạn chắc chắn chứ?',
                text: "Thao tác không thể phục hồi bạn có muốn tiếp tục ?",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Xóa',
                cancelButtonText: 'Bỏ qua'
            }).then(function (response) {
                if (response.value) {
                    if (id != '') {
                        Order.DeleteOrder(id);
                    }
                }
            });
        });
    },
    //Cập nhật trạng thái đơn hàng
    UpdateStatus: function (id, status) {
        $.ajax({
            url: '/Admin/Order/UpdateStatus',
            type: 'POST',
            dataType: 'json',
            data: {
                id: id,
                status: status
            },
            success: function (res) {
                if (res) {
                    alertify.success("Cập nhật thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/Order/Index';
                    }, 500);
                }
                else {
                    alertify.success("Đã có lỗi");
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },
    DeleteOrder: function (id) {
        $.ajax({
            url: '/Admin/Order/DeleteOrder',
            type: 'POST',
            dataType: 'json',
            data: { id: id },
            success: function (res) {
                if (res) {
                    alertify.success("Xóa thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/Order/Index';
                    }, 500);
                }
                else {
                    alertify.success("Đã có lỗi");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    //Xem chi tiết đơn hàng
    ViewDetail: function (id) {
        var txtName = $('#txtName');
        var txtAddress = $('#txtAddress');
        var txtEmail = $('#txtEmail');
        var txtPhone = $('#txtPhone');
        var txtCreatedDate = $('#txtCreatedDate');
        var lstCart = $('#lstCart');
        var txtTotalMoney = $('#txtTotalMoney');
        var txtNotes = $('#txtNotes');
        lstCart.html('');
        $.ajax({
            url: '/Admin/Order/ViewDetail',
            type: 'GET',
            dataType: 'json',
            data: { idOrder: id },
            success: function (res) {
                txtName.html(res.OrderName);
                txtAddress.html(res.OrderAddress);
                txtEmail.html(res.OrderEmail);
                txtPhone.html(res.OrderPhone);
                txtCreatedDate.html(res.CreatedDate);
                txtTotalMoney.html(res.TotalMoney);
                txtNotes.html(res.OrderNotes);
                $.each(res.lstOrderDetail, function (index, value) {
                    var html = `<tr>
                                    <th scope="row">${value.Name}</th>
                                    <td><img class="" src="${value.Image}" alt="" height="60"></td>
                                    <td>${value.Quantity}</td>
                                    <td>${value.Price}</td>
                                    <td>${value.TotalMoney}</td>
                                </tr>`;
                    lstCart.append(html);
                });
            }
        });
    }
}
Order.init();