var nowDate = new Date();
var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var loadPage = false;
var txtSearch = "";
var page = $('#txtPage').val();
var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        //Định dạng lịch
        $('#txtCreatedDate').datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), nowDate.getHours(), nowDate.getMinutes(), 0, 0)
        });
        $('#txtModifiedDate').datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), nowDate.getHours(), nowDate.getMinutes(), 0, 0)
        });

        //Thêm ảnh
        $('#btnChooseImg').on('click', function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $('#txtImage').val(fileUrl);
            }
            finder.popup();
        });

        //Chọn ảnh chi tiết
        $('#btnChooseMoreImg').off('click').on('click', function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                var html = `<div style="position: relative;display: inline-block;">
                            <img class="mb-1 mr-1" src="${fileUrl}" alt="" width="85">
                            <a href="#" class="mdi mdi-delete-outline text-danger font-24 btnDelMoreImg" style="position: absolute;top: 0px;right: 5px;"></a>
                        </div>`;
                $('.listImages').append(html);
                $('.btnDelMoreImg').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            }
            finder.popup();
        });

        //Chọn hiển thị số hàng
        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            page = 1;
            product.LoadData();
        });

        //Test chức năng
        $('#btnTest').on('click', function () {
            alertify.success('test');
        });


        //Thêm sản phẩm
        $('#btnAdd').on('click', function () {
            $('#myModalLabel').html('Thêm sản phẩm');
            $('#btnSave').html('Thêm');
            $('#txtModifiedBy').attr('disabled', 'disabled');
            $('#txtModifiedDate').attr('disabled', 'disabled');
            product.ResetForm();
        });

        //Lưu thông tin sản phẩm
        $('#btnSave').on('click', function () {
            if (product.IsValid()) {
                product.SaveProduct();
            }
        });

        //Xem thông tin sản phẩm
        $('.btnInfo').off('click').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            product.GetInfo(id);
        });

        //Sửa sản phẩm
        $('.btnEdit').off('click').on('click', function (e) {
            e.preventDefault();
            $('#myModalLabel').html('Sửa sản phẩm');
            $('#btnSave').html('Sửa');
            $('#txtModifiedBy').removeAttr('disabled');
            $('#txtModifiedDate').removeAttr('disabled');
            product.ResetForm();
            var id = parseInt($(this).data('id'));
            product.EditProduct(id);
        });

        //Xóa sản phẩm
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            swal({
                title: 'Xóa bản ghi này ?',
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
                        product.DeleteProduct(id);
                    }
                }
            });
        });

        //Tìm kiếm
        $('#btnSearch').on('click', function (e) {
            e.preventDefault();
            txtSearch = $('#txtSearch').val();
            product.LoadData();
        });
    },
    //Xem chi tiết
    GetInfo: function (id) {
        var txtNameInfo = $('#txtNameInfo');
        var txtDescriptionInfo = $('#txtDescriptionInfo');
        var txtImageInfo = $('#txtImageInfo');
        var txtFrameInfo = $('#txtFrameInfo');
        var txtRimsInfo = $('#txtRimsInfo');
        var txtTiresInfo = $('#txtTiresInfo');
        var txtWeightInfo = $('#txtWeightInfo');
        var txtWeightLimitInfo = $('#txtWeightLimitInfo');
        var txtPriceInfo = $('#txtPriceInfo');
        var txtPromotionInfo = $('#txtPromotionInfo');
        var txtQuantityInfo = $('#txtQuantityInfo');
        var txtCategoryInfo = $('#txtCategoryInfo');
        var txtSupplierInfo = $('#txtSupplierInfo');
        var txtCreatedByInfo = $('#txtCreatedByInfo');
        var txtCreatedDateInfo = $('#txtCreatedDateInfo');
        var txtModifiedByInfo = $('#txtModifiedByInfo');
        var txtModifiedDateInfo = $('#txtModifiedDateInfo');
        var txtViewCountInfo = $('#txtViewCountInfo');
        $('#lstImage').html('');
        $.ajax({
            url: '/Admin/Product/GetByIdInfo',
            type: 'GET',
            dataType: 'json',
            data: { id: id },
            success: function (data) {
                txtNameInfo.html(data.obj.Name);
                txtDescriptionInfo.html(data.obj.Description);
                txtImageInfo.prop('src', data.obj.Image);
                $.each(data.lstImage, function (index, value) {
                    var html = `<a class="" href="${value}" title="">
                            <div class="img-fluid d-inline mx-auto">
                                <img src="${value}" alt="" width="85">
                            </div>
                        </a>`;
                    $('#lstImage').append(html);
                });
                txtFrameInfo.html(data.obj.Frame);
                txtRimsInfo.html(data.obj.Rims);
                txtTiresInfo.html(data.obj.Tires);
                txtWeightInfo.html(data.obj.Weight);
                txtWeightLimitInfo.html(data.obj.Weightlimit);
                txtPriceInfo.html(data.obj.StrPrice);
                txtPromotionInfo.html(data.obj.StrPromotion);
                txtQuantityInfo.html(data.obj.Quantity);
                txtCategoryInfo.html(data.obj.StrCategory);
                txtSupplierInfo.html(data.obj.StrSupplier);
                txtCreatedByInfo.html(data.obj.CreatedBy);
                txtCreatedDateInfo.html(data.obj.CreatedDate);
                txtModifiedByInfo.html(data.obj.ModifiedBy);
                txtModifiedDateInfo.html(data.obj.ModifiedDate);
                txtViewCountInfo.html(data.obj.ViewCount);

            }
        });
    },
    //Thêm + Sửa sản phẩm
    SaveProduct: function () {
        var id = parseInt($('#hiddenID').val());
        var txtName = $('#txtName').val();
        var txtImage = $('#txtImage').val();
        var txtMoreImages = product.GetMoreImages();
        var txtDescription = $('#txtDescription').val();
        var txtFrame = $('#txtFrame').val();
        var txtRims = $('#txtRims').val();
        var txtTires = $('#txtTires').val();
        var txtWeight = $('#txtWeight').val();
        var txtWeightLimit = $('#txtWeightLimit').val();
        var txtPrice = $('#txtPrice').val();
        var txtPromotion = $('#txtPromotion').val();
        var txtQuantity = $('#txtQuantity').val();
        var slCategory = $('#slCategory').val();
        var slSupplier = $('#slSupplier').val();
        var txtCreatedBy = $('#txtCreatedBy').val();
        var txtCreatedDate = $('#txtCreatedDate').val();
        var txtModifiedBy = $('#txtModifiedBy').val();
        var txtModifiedDate = $('#txtModifiedDate').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var ckHomeFlag = $('#ckHomeFlag').prop('checked');
        var ckHotFlag = $('#ckHotFlag').prop('checked');
        var obj = {
            ID: id,
            Name: txtName,
            Image: txtImage,
            MoreImages: txtMoreImages,
            Description: txtDescription,
            Frame: txtFrame,
            Rims: txtRims,
            Tires: txtTires,
            Weight: txtWeight,
            Weightlimit: txtWeightLimit,
            Price: txtPrice,
            Promotion: txtPromotion,
            Quantity: txtQuantity,
            CategoryID: slCategory,
            SupplierID: slSupplier,
            CreatedBy: txtCreatedBy,
            CreatedDate: txtCreatedDate,
            ModifiedBy: txtModifiedBy,
            ModifiedDate: txtModifiedDate,
            Status: ckStatus,
            HomeFlag: ckHomeFlag,
            HotFlag: ckHotFlag
        };
        $.ajax({
            url: '/Admin/Product/SaveProduct',
            type: 'POST',
            dataType: 'json',
            data: {
                product: JSON.stringify(obj)
            },
            success: function (result) {
                if (result) {
                    if (id == 0)
                        alertify.success("Thêm thành công");
                    else
                        alertify.success("Sửa thành công");
                    $('#modalAddEdit').modal('hide');
                    loadPage = true;
                    setTimeout(function () {
                        product.LoadData();
                    }, 700);
                }
                else {
                    if (id == 0)
                        alertify.error("Thêm không thành công");
                    else
                        alertify.error("Sửa không thành công");
                }
            }
        });
    },
    //Sửa
    EditProduct: function (id) {
        var txtName = $('#txtName');
        var txtImage = $('#txtImage');
        var txtDescription = $('#txtDescription');
        var txtFrame = $('#txtFrame');
        var txtRims = $('#txtRims');
        var txtTires = $('#txtTires');
        var txtWeight = $('#txtWeight');
        var txtWeightLimit = $('#txtWeightLimit');
        var txtPrice = $('#txtPrice');
        var txtPromotion = $('#txtPromotion');
        var txtQuantity = $('#txtQuantity');
        var slCategory = $('#slCategory');
        var slSupplier = $('#slSupplier');
        var txtCreatedBy = $('#txtCreatedBy');
        var txtCreatedDate = $('#txtCreatedDate');
        var txtModifiedBy = $('#txtModifiedBy');
        var txtModifiedDate = $('#txtModifiedDate');
        var ckStatus = $('#ckStatus');
        var ckHomeFlag = $('#ckHomeFlag');
        var ckHotFlag = $('#ckHotFlag');
        $.ajax({
            url: '/Admin/Product/GetByIdEdit',
            type: 'GET',
            dataType: 'json',
            data: { id: id },
            success: function (response) {
                if (response != null) {
                    $('#hiddenID').val(id);
                    txtName.val(response.obj.Name);
                    txtImage.val(response.obj.Image);
                    txtDescription.val(response.obj.Description);
                    txtFrame.val(response.obj.Frame);
                    txtRims.val(response.obj.Rims);
                    txtTires.val(response.obj.Tires);
                    txtWeight.val(response.obj.Weight);
                    txtWeightLimit.val(response.obj.Weightlimit);
                    txtPrice.val(response.obj.Price);
                    txtPromotion.val(response.obj.Promotion);
                    txtQuantity.val(response.obj.Quantity);
                    slCategory.val(response.obj.CategoryID);
                    slSupplier.val(response.obj.SupplierID);
                    txtCreatedBy.val(response.obj.CreatedBy);
                    txtCreatedDate.val(response.obj.CreatedDate);
                    txtModifiedBy.val(response.obj.ModifiedBy);
                    txtModifiedDate.val(response.obj.ModifiedDate);
                    ckStatus.prop('checked', response.obj.Status);
                    ckHomeFlag.prop('checked', response.obj.HomeFlag);
                    ckHotFlag.prop('checked', response.obj.HotFlag);
                    $.each(response.lstImage, function (index, value) {
                        var html = `<div style="position: relative;display: inline-block;">
                            <img class="mb-1 mr-1" src="${value}" alt="" width="85">
                            <a href="#" class="mdi mdi-delete-outline text-danger font-24 btnDelMoreImg" style="position: absolute;top: 0px;right: 5px;"></a>
                        </div>`;
                        $('.listImages').append(html);
                        $('.btnDelMoreImg').off('click').on('click', function (e) {
                            e.preventDefault();
                            $(this).parent().remove();
                        });
                    });
                    product.IsValid();
                }
            }
        });
    },
    //Xóa
    DeleteProduct: function (id) {
        $.ajax({
            url: '/Admin/Product/DeleteProduct',
            type: 'POST',
            dataType: 'json',
            data: { id: id },
            success: function (result) {
                if (result) {
                    alertify.success("Xóa thành công");
                    loadPage = true;
                    setTimeout(function () {
                        product.LoadData();
                    }, 700);
                }
                else
                    alertify.error("Xóa không thành công");
            }
        });
    },
    GetMoreImages: function () {
        var srcImages = [];
        var host = "http://localhost:53411";
        $.each($('.listImages img'), function (index, value) {
            var src = $(value).prop('src');
            if (src.indexOf(host) >= 0) {
                src = src.replace(host, '');
            }
            srcImages.push(src);
        });
        return JSON.stringify(srcImages);
    },
    IsValid: function () {
        var flag = true;
        var notnull = $('.notnull');
        for (var i = 0; i < notnull.length; i++) {
            if (notnull[i].value == null || notnull[i].value == '') {
                notnull[i].classList.add('parsley-error');
                notnull[i].nextElementSibling.innerHTML = '<i class="mdi mdi-close-circle"></i> Không được để trống';
                flag = false;
            }
            else {
                notnull[i].classList.remove('parsley-error');
                notnull[i].nextElementSibling.innerHTML = '';
            }
            notnull[i].addEventListener('change', function () {
                if (this.value == null || this.value == '') {
                    this.classList.add('parsley-error');
                    this.nextElementSibling.innerHTML = '<i class="mdi mdi-close-circle"></i> Không được để trống';
                }
                else {
                    this.classList.remove('parsley-error');
                    this.nextElementSibling.innerHTML = '';
                }
            });
        }
        var txtQuantity = $('#txtQuantity');
        if (parseInt(txtQuantity.val()) < 0 || txtQuantity.val() == '') {
            txtQuantity.val(1);
        }
        return flag;
    },
    ResetForm: function () {
        $('#hiddenID').val(0);
        $('#txtName').val('');
        $('#txtImage').val('');
        $('#txtDescription').val('');
        $('#txtFrame').val('');
        $('#txtRims').val('');
        $('#txtTires').val('');
        $('#txtWeight').val('');
        $('#txtWeightLimit').val('');
        $('#txtPrice').val('');
        $('#txtPromotion').val('');
        $('#txtQuantity').val(1);
        $('#slCategory').val('');
        $('#slSupplier').val('');
        $('#txtCreatedBy').val('');
        $('#txtCreatedDate').val('');
        $('#txtModifiedBy').val('');
        $('#txtModifiedDate').val('');
        $('#ckStatus').prop('checked', false);
        $('#ckHomeFlag').prop('checked', false);
        $('#ckHotFlag').prop('checked', false);
        $('.listImages').html('');
        var notnull = $('.notnull');
        for (var i = 0; i < notnull.length; i++) {
            notnull[i].classList.remove('parsley-error');
            notnull[i].nextElementSibling.innerHTML = '';
        }
    },
    LoadData: function () {
        window.location.replace("/Admin/Product?page=" + page + "&size=" + pageSize + "&loadPage=" + loadPage + "&searchName=" + txtSearch);
    }
}
product.init();