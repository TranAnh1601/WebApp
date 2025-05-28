
$('#btn-search-product').on('click', function (e) {
    e.preventDefault()
    var keyWord = $('#txt-search-product').val();
    location.href = '/product/index?keyWord=' + keyWord
})

var _model = new Object();
_model.PageSize = 9
_model.PageIndex = 1
_model.CategoryId = $('#txt-categoryId').text();
// functions

function loadProduct(model, isLoadMore = false) {
    $.ajax({
        type: 'post',
        url: '/product/ProductListPartial',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            console.log("AJAX response received:", data);
            if (!isLoadMore) {
                $("#product-list-partial").html(data);
            }
            else {
                $("#product-list-partial").append(data);
            }           
        }
    });
}
function init() {
    _model.CategoryId = $('#txt-categoryId').text();
    loadProduct(_model)
}

function filterCategory() {
    _model.PageIndex = 1
    _model.CategoryId = $(this).val();
    loadProduct(_model)
}
init();
$('body').on('change', '.input-select', filterCategory);
$('body').on('change', '#category-select', filterCategory);