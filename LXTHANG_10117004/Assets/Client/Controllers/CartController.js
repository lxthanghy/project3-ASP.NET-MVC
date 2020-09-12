var Cart = {
    init: function () {
        Cart.LoadCart();
        Cart.CheckQuantity();
        Cart.registerEvents();
    },
    registerEvents: function () {
        //Thêm vào giỏ hàng
        $('.addToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            Cart.AddToCart(id, 1);
        });
        //Thêm vào giỏ hàng (Trang chi tiết sản phẩm)
        $('#addToCartV').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var quantity = $('#txtQuantity').val();
            Cart.AddToCart(id, quantity);
        });
        //Chức năng cho ViewCart
        //Xóa sản phẩm trong giỏ
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            Cart.DeleteCart(id);
            $(this).parent().parent().remove();

        });
        //Cập nhật giỏ hàng
        $('#btnUpdate').on('click', function (e) {
            e.preventDefault();
            var cartItems = $('.txtQuantity');
            var cartList = [];
            $.each(cartItems, function (index, value) {
                cartList.push({
                    ID: $(value).data('id'),
                    Quantity: $(value).val()
                });
            });
            Cart.UpdateCart(cartList);
        });
        //Xóa toàn bộ sản phẩm trong giỏ
        $('#btnDeleteAll').on('click', function (e) {
            e.preventDefault();
            bootbox.confirm({
                size: "small",
                message: "<b class='text-danger'>Xóa sạch giỏ hàng ?</b>",
                callback: function (res) {
                    if (res) {
                        Cart.DeleteAll();
                    }
                }
            });
        });
        //Tiếp tục mua hàng
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/trang-chu";
        });
    },
    //Load giỏ hàng
    LoadCart: function () {
        var countCart = $('#countCart');
        var cartList = $('#cartList');
        cartList.html('');
        $.ajax({
            url: '/Cart/LoadCart',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.count > 0) {
                    countCart.html(res.count);
                    $.each(res.data, function (index, value) {
                        var html = `<li class="single-shopping-cart" data-id="${value.ID}">
                                                <div class="shopping-cart-img">
                                                    <a href="#"><img alt="" src="${value.Image}"></a>
                                                    <div class="item-close">
                                                        <a href="#"><i class="sli sli-close"></i></a>
                                                    </div>
                                                </div>
                                                <div class="shopping-cart-title">
                                                    <h4><a href="#">${value.Name}</a></h4>
                                                    <span>${value.strPrice}</span>
                                                </div>
                                                <div class="shopping-cart-delete">
                                                    <a href="#" class="deleteItem"><i class="la la-trash"></i></span>
                                                </div>
                                            </li>`;
                        cartList.append(html);
                    });
                    cartList.append(`<div class="shopping-cart-bottom">
                                            <div class="shopping-cart-total">
                                                <h4>Tổng tiền <span class="shop-total">${res.money}</span></h4>
                                            </div>
                                            <div class="shopping-cart-btn btn-hover default-btn text-center">
                                                <a class="black-color" href="/xem-gio-hang">Xem giỏ hàng</a>
                                                <a class="black-color" href="/thanh-toan">Thanh toán</a>
                                            </div>
                                        </div>`);
                    cartList.find('.deleteItem').off('click').on('click', function (e) {
                        e.preventDefault();
                        var id = $(this).parent().parent().data('id');
                        Cart.DeleteCart(id);
                        $(this).parent().parent().remove();
                    });
                }
                else {
                    countCart.html(0);
                    cartList.append(`<li><span class="text-danger font-weight-bold">Không có sản phẩm nào!</span></li>`);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    //Thêm giỏ hàng
    AddToCart: function (id, quantity) {
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            dataType: 'json',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                if (res) {
                    alertify.success("Thêm thành công");
                    Cart.LoadCart();
                }
                else {
                    alertify.error("Thêm thất bại");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    //Xóa giỏ hàng
    DeleteCart: function (id) {
        $.ajax({
            url: '/Cart/DeleteCart',
            type: 'POST',
            dataType: 'json',
            data: { id: id },
            success: function (res) {
                if (res) {
                    alertify.success("Xóa thành công");
                    window.setTimeout(function () {
                        window.location.href = "/xem-gio-hang";
                    }, 500);
                }
                else {
                    alertify.error("Xóa không thành công");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    //Cập nhật giỏ hàng
    UpdateCart: function (cartList) {
        $.ajax({
            url: '/Cart/UpdateCart',
            type: 'POST',
            dataType: 'json',
            data: { cartList: JSON.stringify(cartList) },
            success: function (res) {
                if (res == 0) {
                    alertify.error("Bạn chưa thay đổi");
                }
                if (res == 1) {
                    alertify.success("Cập nhật thành công");
                    window.setTimeout(function () {
                        window.location.href = "/xem-gio-hang";
                    }, 1000);
                }
                if (res == 2) {
                    alertify.error("Cập nhật không thành công");
                }
            }
        })
    },
    //Xóa toàn bộ sản phẩm trong giỏ
    DeleteAll: function () {
        $.ajax({
            url: '/Cart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res) {
                    Cart.LoadCart();
                    var html = `<div class="p-3 mb-2 bg-danger text-white">Không có sản phẩm nào!</div>`;
                    $('#lstCart').html(html);
                    alertify.success("Đã xóa sạch");
                }
                else {
                    alertify.error("Đã có lỗi");
                }
            }
        });
    },
    CheckQuantity: function () {
        $('.txtQuantity').off('change').on('change', function () {
            var id = $(this).data('id');
            var sl = $(this);
            var tmp = 0;
            if (sl == '' || parseInt(sl.val()) <= 0) {
                $(this).val(1);
            }
            else {
                $.ajax({
                    url: '/Cart/CheckQuantity',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        id: id,
                        quantity: sl.val()
                    },
                    success: function (res) {
                        if (res.status) {
                            alertify.error("Số lượng yêu cầu không có sẵn");
                            sl.val(res.qt);
                        }
                    }
                });
            }
        });
        $('#txtQuantity').off('change').on('change', function () {
            var sl = $(this);
            var id = $(this).data('id');
            if (sl == '' || parseInt(sl.val()) <= 0) {
                $(this).val(1);
            }
            else {
                $.ajax({
                    url: '/Cart/CheckQuantity1',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        id: id,
                        quantity: sl.val()
                    },
                    success: function (res) {
                        if (res) {
                            alertify.error("Số lượng yêu cầu không có sẵn");
                            sl.val(1);
                        }
                    }
                });
            }
        });
    }
}
Cart.init();