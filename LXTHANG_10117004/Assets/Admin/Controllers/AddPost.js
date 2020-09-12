var nowDate = new Date();
var Post = {
    init: function () {
        Post.registerEvents();
    },
    registerEvents: function () {
        //CKEditor
        var editor = CKEDITOR.replace('txtContent',
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
        //Thêm tin tức
        $('#btnAdd').on('click', function () {
            Post.AddPost();
        });
        //Trở lại
        $('#btnBack').on('click', function () {
            window.location.href = '/Admin/Post/Index';
        });
    },
    AddPost: function () {
        var txtName = $('#txtName').val();
        var txtContent = CKEDITOR.instances['txtContent'].getData();
        var postcategory = $('#slPostCategory').val();
        var txtCreatedDate = $('#txtCreatedDate').val();
        var txtCreatedBy = $('#txtCreatedBy').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var post = {
            Name: txtName,
            CategoryID: postcategory,
            Content: txtContent,
            CreatedDate: txtCreatedDate,
            CreatedBy: txtCreatedBy,
            Status: ckStatus
        };
        $.ajax({
            url: '/Admin/Post/AddPost',
            type: 'POST',
            dataType: 'json',
            data: {
                post: JSON.stringify(post)
            },
            success: function (res) {
                if (res) {
                    alertify.success("Thêm thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/Post/Index';
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
Post.init();