var nowDate = new Date();
var Post = {
    init: function () {
        Post.registerEvents();
    },
    registerEvents: function () {
        var id = $('#hiddenID').val();
        Post.GetContent(id);
        //CKEditor
        var editor = CKEDITOR.replace('txtContent',
            {
                customConfig: '/Assets/Admin/plugins/ckeditor/config.js'
            });
        //Định dạng lịch
        $('#txtModifiedDate').datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), nowDate.getHours(), nowDate.getMinutes(), 0, 0)
        });
        //Sửa loại tin tức
        $('#btnEdit').on('click', function () {
            Post.EditPost();
        });
        //Trở lại
        $('#btnBack').on('click', function () {
            window.location.href = '/Admin/Post/Index';
        });
    },
    EditPost: function () {
        var id = $('#hiddenID').val();
        var txtName = $('#txtName').val();
        var txtContent = CKEDITOR.instances['txtContent'].getData();
        var postcategory = $('#slPostCategory').val();
        var txtCreatedDate = $('#txtCreatedDate').val();
        var txtCreatedBy = $('#txtCreatedBy').val();
        var ckStatus = $('#ckStatus').prop('checked');
        var post = {
            ID: id,
            Name: txtName,
            CategoryID: postcategory,
            Content: txtContent,
            CreatedDate: txtCreatedDate,
            CreatedBy: txtCreatedBy,
            Status: ckStatus
        };
        $.ajax({
            url: '/Admin/Post/EditPost',
            type: 'POST',
            dataType: 'json',
            data: { post: JSON.stringify(post) },
            success: function (res) {
                if (res) {
                    alertify.success("Sửa thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/Post/Index';
                    }, 500);
                }
                else {
                    alertify.error("Đã có lỗi");
                }
            }
        });
    },
    GetContent: function (id) {
        $.ajax({
            url: '/Admin/Post/GetContent',
            type: 'GET',
            dataType: 'json',
            data: { id: id },
            success: function (res) {
                CKEDITOR.instances['txtContent'].setData(res);
            }
        })
    }
}
Post.init();