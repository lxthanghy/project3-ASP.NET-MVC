var pageSize = $('#txtPageSize').val();
$('#slPageSize').val(pageSize);
var loadPage = false;
var nowDate = new Date();
var txtSearch = "";
var page = $('#txtPage').val();
var supplier = {
    init: function () {
        supplier.registerEvents();
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


        $('#slPageSize').on('change', function () {
            pageSize = $('#slPageSize').val();
            page = 1;
            supplier.LoadData();
        });

        $('#btnAdd').off('click').on('click', function () {
            supplier.ResetForm();
            $('#myModalLabel').text('Thêm mới');
            $('#btnSave').html('Thêm');
            $('#txtModifiedBy').attr('disabled', 'disabled');
            $('#txtModifiedDate').attr('disabled', 'disabled');
        });
        $('.btnEdit').off('click').on('click', function (e) {
            e.preventDefault();
            supplier.ResetForm();
            var id = $(this).data('id');
            supplier.GetById(id);
            $('#myModalLabel').text('Sửa thông tin');
            $('#btnSave').html('Sửa');
            $('#txtModifiedBy').removeAttr('disabled');
            $('#txtModifiedDate').removeAttr('disabled');
        });
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
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
                        supplier.DeleteSupplier(id);
                    }
                }
            });
        });
        $('#btnSave').off('click').on('click', function () {
            if (supplier.IsValid()) {
                supplier.SaveSupplier();
            }
        });
        //Tìm kiếm nhà cung cấp
        $('#btnSearch').off('click').on('click', function (e) {
            e.preventDefault();
            txtSearch = $('#txtSearch').val();
            supplier.LoadData();
        });
        $('#btnTest').on('click', function () {
            console.log(supplier.IsValid());
        });
        $('#btnExportExcel').on('click', function () {
            window.location = "/Admin/Supplier/ExportExcel?pageSize=" + pageSize;
        });
    },
    SaveSupplier: function () {
        var id = parseInt($('#hiddenID').val());
        var txtName = $('#txtName').val();
        var txtAddress = $('#txtAddress').val();
        var txtEmail = $('#txtEmail').val();
        var txtPhone = $('#txtPhone').val();
        var txtCreatedBy = $('#txtCreatedBy').val();
        var txtCreatedDate = $('#txtCreatedDate').val();
        var txtModifiedBy = $('#txtModifiedBy').val();
        var txtModifiedDate = $('#txtModifiedDate').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var sl = {
            ID: id,
            Name: txtName,
            Address: txtAddress,
            Email: txtEmail,
            Mobile: txtPhone,
            CreatedBy: txtCreatedBy,
            CreatedDate: txtCreatedDate,
            ModifiedBy: txtModifiedBy,
            ModifiedDate: txtModifiedDate,
            Status: ckStatus
        };
        $.ajax({
            url: '/Admin/Supplier/SaveSupplier',
            type: 'POST',
            dataType: 'json',
            data: { supplier: JSON.stringify(sl) },
            success: function (response) {
                if (response) {
                    if (id == 0)
                        alertify.success("Thêm thành công");
                    else
                        alertify.success("Sửa thành công");
                    loadPage = true;
                    $('#myModal').modal('hide');
                    setTimeout(function () {
                        supplier.LoadData();
                    }, 700);
                }
                else {
                    if (id == 0)
                        alertify.error("Thêm không thành công");
                    else
                        alertify.error("Sửa không thành công");
                }
            }
        })
    },
    GetById: function (id) {
        $.ajax({
            url: '/Admin/Supplier/GetById',
            type: 'GET',
            dataType: 'json',
            data: { id: id },
            success: function (response) {
                if (response != null) {
                    $('#hiddenID').val(id);
                    $('#txtName').val(response.Name);
                    $('#txtAddress').val(response.Address);
                    $('#txtEmail').val(response.Email);
                    $('#txtPhone').val(response.Mobile);
                    $('#txtCreatedBy').val(response.CreatedBy);
                    $('#txtCreatedDate').val(response.CreatedDate);
                    $('#txtModifiedBy').val(response.ModifiedBy);
                    $('#txtModifiedDate').val(response.ModifiedDate);
                    $('#ckStatus').prop('checked', response.Status);
                }
            }
        })
    },
    DeleteSupplier: function (id) {
        $.ajax({
            url: '/Admin/Supplier/DeleteSupplier',
            type: 'POST',
            dataType: 'json',
            data: { id: id },
            success: function (response) {
                if (response == true) {
                    alertify.success("Xóa thành công");
                    loadPage = true;
                    setTimeout(function () {
                        supplier.LoadData();
                    }, 700);
                }
                else
                    alertify.error("Xóa không thành công");
            }
        })
    },
    IsValid: function () {
        var flag = true;
        var notnul = $('.notnull');
        for (var i = 0; i < notnul.length; i++) {
            if (notnul[i].value == null || notnul[i].value == '') {
                notnul[i].classList.add('parsley-error');
                notnul[i].nextElementSibling.innerHTML = '<i class="mdi mdi-close-circle"></i> Không được để trống';
                flag = false;
            }
            else {
                notnul[i].classList.remove('parsley-error');
                notnul[i].nextElementSibling.innerHTML = '';
            }
            notnul[i].addEventListener('change', function () {
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
        return flag;
    },
    ResetForm: function () {
        $('#txtName').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#txtPhone').val('');
        $('#txtCreatedBy').val('');
        $('#txtCreatedDate').val('');
        $('#txtModifiedBy').val('');
        $('#txtModifiedDate').val('');
        $('#ckStatus').prop('checked', false);
        var notnul = $('.notnull');
        for (var i = 0; i < notnul.length; i++) {
            notnul[i].classList.remove('parsley-error');
            notnul[i].nextElementSibling.innerHTML = '';
        }
    },
    LoadData: function () {
        window.location.replace("/Admin/Supplier?page=" + page + "&size=" + pageSize + "&loadPage=" + loadPage + "&searchName=" + txtSearch);
    }
}
supplier.init();