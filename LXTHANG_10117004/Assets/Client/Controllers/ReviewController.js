var Review = {
    init: function () {
        Review.registerEvents();
    },
    registerEvents: function () {
        Review.LoadReviews();
        //Click đánh giá
        $('#btnReview').off('click').on('click', function (e) {
            e.preventDefault();
            var idProduct = $(this).data('id');
            var txtReview = $('#txtReview').val();
            Review.AddReview(idProduct, txtReview);
        });
    },
    AddReview: function (idProduct, txtReview) {
        $.ajax({
            url: '/Review/AddReview',
            type: 'POST',
            dataType: 'json',
            data: {
                idProduct: idProduct,
                txtReview: txtReview
            },
            success: function (res) {
                if (res == -1) {
                    alertify.error("Đánh giá không được để trống");
                }
                else if (res == 0) {
                    alertify.error("Bạn đã đánh giá");
                }
                else if (res == 1) {
                    alertify.success("Đánh giá thành công");
                    Review.LoadReviews();
                    $('#txtReview').val('');
                }
                else
                    alertify.error("Bạn chưa đăng nhập");
            },
            error: function (err) {
                alertify.error("Đã có lỗi");
            }
        });
    },
    LoadReviews: function () {
        var idProduct = $('#btnReview').data('id');
        var Reviews = $('#reviews');
        Reviews.html('');
        $.ajax({
            url: '/Review/GetReviews',
            type: 'GET',
            dataType: 'json',
            data: { idProduct: idProduct },
            success: function (res) {
                if (parseInt(res.count) > 0) {
                    $.each(res.reviews, function (index, value) {
                        var html = `<div class="dec-review-wrap mb-50">
                                    <div class="row">
                                        <div class="col-xl-4 col-lg-4 col-md-5">
                                            <div class="dec-review-img-wrap">
                                                <div class="dec-review-img">
                                                    <img src="/Assets/Client/images/man.png" alt="review">
                                                </div>
                                                <div class="dec-client-name">
                                                    <h4>${value.Name}</h4>
                                                    <div class="dec-client-rating">
                                                        <i class="la la-star"></i>
                                                        <i class="la la-star"></i>
                                                        <i class="la la-star"></i>
                                                        <i class="la la-star"></i>
                                                        <i class="la la-star-half-o"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-7 col-lg-8 col-md-7">
                                            <div class="dec-review-content">
                                                <p>${value.Content}</p>
                                                <div class="review-content-bottom">
                                                    <div class="review-like">
                                                        <span><i class="la la-heart-o"></i> 24 Likes</span>
                                                    </div>
                                                    <div class="review-date">
                                                        <span>${value.CreatedDate}</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>`;
                        Reviews.append(html);
                    });
                }
                else {
                    var html = `<span class="text-info">Chưa có nhận xét nào!</span>`;
                    Reviews.append(html);
                }
            }
        });
    }
}
Review.init();