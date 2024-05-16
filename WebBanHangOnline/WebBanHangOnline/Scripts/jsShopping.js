$(document).ready(function () {
    ShowCount();
    

    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var size = $('#showSize').val().trim(); // Lấy giá trị size từ input và loại bỏ khoảng trắng thừa
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity, size: size },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });
    //$('body').on('click', '.btnAddToCart', function (e) {
    //    e.preventDefault();
    //    var id = $(this).data('id');
    //    var quantity = 1;
    //    var size = $('.showSize').text(); // Lấy giá trị size từ input
    //    var tQuantity = $('#quantity_value').text();
    //    if (tQuantity != '') {
    //        quantity = parseInt(tQuantity);
    //    }

    //    $.ajax({
    //        url: '/shoppingcart/addtocart',
    //        type: 'POST',
    //        data: { id: id, quantity: quantity, size: size }, // Thêm trường size vào dữ liệu gửi đi
    //        success: function (rs) {
    //            if (rs.Success) {
    //                $('#checkout_items').html(rs.Count);
    //                alert(rs.msg);
    //            }
    //        }
    //    });
    //});

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();

        // Kiểm tra nếu số lượng nhập vào là số âm, thì sẽ cập nhật thành 1
        if (parseInt(quantity) <= 0) {
            quantity = 1;
            $('#Quantity_' + id).val(quantity); // Cập nhật giá trị hiển thị trên input thành 1
        }

        Update(id, quantity);
    });

    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Đồng ý xóa hết?');
        //debugger;
        if (conf == true) {
            DeleteAll();
            location.reload();
        }

    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Đồng ý xóa?');
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();
                    }
                }
            });
        }

    });
});



function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}
