var nowDate = new Date();
var EditPostCategory = {
    init: function () {
        EditPostCategory.registerEvents();
    },
    registerEvents: function () {
        var id = $('#hiddenID').val();
        EditPostCategory.GetDescription(id);
        //CKEditor
        var editor = CKEDITOR.replace('txtDescription',
            {
                customConfig: '/Assets/Admin/plugins/ckeditor/config.js'

            });
        $('#txtModifiedDate').datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), nowDate.getHours(), nowDate.getMinutes(), 0, 0)
        });
        //Sửa loại tin tức
        $('#btnEdit').on('click', function () {
            EditPostCategory.EditPostCategory();
        });
        //Trở lại
        $('#btnBack').on('click', function () {
            window.location.href = '/Admin/PostCategory/Index';
        });
    },
    EditPostCategory: function () {
        var id = $('#hiddenID').val();
        var txtName = $('#txtName').val();
        var txtDescription = CKEDITOR.instances['txtDescription'].getData();
        var txtModifiedDate = $('#txtModifiedDate').val();
        var txtModifiedBy = $('#txtModifiedBy').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var ckHomeFlag = $('#ckHomeFlag').prop('checked');
        var postCategory = {
            ID: id,
            Name: txtName,
            Description: txtDescription,
            ModifiedDate: txtModifiedDate,
            ModifiedBy: txtModifiedBy,
            Status: ckStatus,
            HomeFlag: ckHomeFlag
        };
        $.ajax({
            url: '/Admin/PostCategory/EditPostCategory',
            type: 'POST',
            dataType: 'json',
            data: { postCategory: JSON.stringify(postCategory) },
            success: function (res) {
                if (res) {
                    alertify.success("Sửa thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/PostCategory/Index';
                    }, 500);
                }
                else {
                    alertify.error("Đã có lỗi");
                }
            }
        });
    },
    GetDescription: function (id) {
        $.ajax({
            url: '/Admin/PostCategory/GetDescription',
            type: 'GET',
            dataType: 'json',
            data: { id: id },
            success: function (res) {
                CKEDITOR.instances['txtDescription'].setData(res);
            }
        });
    }
}
EditPostCategory.init();