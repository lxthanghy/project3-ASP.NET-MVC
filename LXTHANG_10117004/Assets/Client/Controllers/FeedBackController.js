var Feedback = {
    init: function () {
        Feedback.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function (e) {
            e.preventDefault();
            if (Feedback.IsValid()) {
                var email = $('#txtEmail').val();
                var checkemail = /^([\w\.])+@([a-zA-Z0-9\-]{2,4})+\.([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$/;
                if (email.search(checkemail) == -1) {
                    alertify.error("Email không hợp lệ");
                }
                else {
                    Feedback.Send();
                }
            }
        });
    },
    //Gửi Feedback
    Send: function () {
        var name = $('#txtName').val();
        var email = $('#txtEmail').val();
        var address = $('#txtAddress').val();
        var phone = $('#nbPhone').val();
        var message = $('#txtMessage').val();
        $.ajax({
            url: '/FeedBack/AddFeedback',
            type: 'POST',
            dataType: 'json',
            data: {
                name: name,
                email: email,
                address: address,
                phone: phone,
                message: message
            },
            success: function (res) {
                if (res) {
                    alertify.success("Gửi thành công");
                    Feedback.ResetForm();
                }
                else {
                    alertify.error("Gửi thất bại");
                }
            }
        });
    },
    //Reset form
    ResetForm: function () {
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtAddress').val('');
        $('#nbPhone').val('');
        $('#txtMessage').val('');
    },
    IsValid: function () {
        var notnull = $('.notnull');
        var flag = true;
        $.each(notnull, function (index, value) {
            if (value.value == '') {
                value.parentElement.getElementsByClassName('text-danger')[0].innerHTML = 'Không được để trống!';
                flag = false;
            }
            else
                value.parentElement.getElementsByClassName('text-danger')[0].innerHTML = '';
            $(value).on('change', function () {
                if (value.value == '') {
                    value.parentElement.getElementsByClassName('text-danger')[0].innerHTML = 'Không được để trống!';
                    flag = false;
                }
                else
                    value.parentElement.getElementsByClassName('text-danger')[0].innerHTML = '';
            });
        });
        return flag;
    }
}
Feedback.init();