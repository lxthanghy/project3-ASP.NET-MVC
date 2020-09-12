var nowDate = new Date();
var AddPostCategory = {
    init: function () {
        AddPostCategory.registerEvents();
    },
    registerEvents: function () {
        //CKEditor
        var editor = CKEDITOR.replace('txtDescription',
            {
                customConfig: '/Assets/Admin/plugins/ckeditor/config.js'
            });
        //Định dạng lịch
        $('#txtCreatedDate').datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), nowDate.getHours(), nowDate.getMinutes(), 0, 0)
        });

        //Thêm loại tin tức
        $('#btnAdd').on('click', function () {
            AddPostCategory.AddPostCategory();
        });
        //Trở lại
        $('#btnBack').on('click', function () {
            window.location.href = '/Admin/PostCategory/Index';
        });
    },
    AddPostCategory: function () {
        var txtName = $('#txtName').val();
        var txtDescription = CKEDITOR.instances['txtDescription'].getData();
        var txtCreatedDate = $('#txtCreatedDate').val();
        var txtCreatedBy = $('#txtCreatedBy').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var ckHomeFlag = $('#ckHomeFlag').prop('checked');
        var postCategory = {
            Name: txtName,
            Description: txtDescription,
            CreatedDate: txtCreatedDate,
            CreatedBy: txtCreatedBy,
            Status: ckStatus,
            HomeFlag: ckHomeFlag
        };
        $.ajax({
            url: '/Admin/PostCategory/AddPostCategory',
            type: 'POST',
            dataType: 'json',
            data: {
                postCategory: JSON.stringify(postCategory)
            },
            success: function (res) {
                if (res) {
                    alertify.success("Thêm thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/PostCategory/Index';
                    }, 500);
                }
                else {
                    alertify.error("Đã có lỗi");
                }
            },
            error: function (err) {
                alertify.error("Đã có lỗi");
            }
        });
    }
}
AddPostCategory.init();