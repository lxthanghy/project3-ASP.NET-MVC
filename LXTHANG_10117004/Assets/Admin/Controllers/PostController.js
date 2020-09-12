var Post = {
    init: function () {
        Post.registerEvents();
    },
    registerEvents: function () {
        //Xóa tin tức
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
                        Post.DeletePost(id);
                    }
                }
            });
        });
    },
    DeletePost: function (id) {
        $.ajax({
            url: '/Admin/Post/DeletePost',
            type: 'POST',
            dataType: 'json',
            data: { id: id },
            success: function (res) {
                if (res) {
                    alertify.success("Xóa thành công");
                    setTimeout(function () {
                        window.location.href = '/Admin/Post/Index';
                    }, 500);
                }
                else {
                    alertify.error("Đã có lỗi");
                }
            }
        });
    }
}
Post.init();