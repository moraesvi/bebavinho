var product = {

    dvIndexDataContainer: null,
    btnHomeContainer: null,

    initialize: function () {
        dvIndexDataContainer = $('#dvIndexData');
        btnHomeContainer = $('#btnHome');
        product.events();
        product.bindProductsWithPathImage('Default/GetProductsWithPathImage');
    },

    events: function () {
        btnHomeContainer.click(function () {
            
        });
    },

    bindingProducts: function (arrayJsonData) {
        var divData = '';
        var div = '';
        for (var index = 0; index < arrayJsonData.length; index++) {
            var obj = arrayJsonData[index];
            div = '<div class="col-lg-3 col-md-4 col-xs-6 thumb">';
            div += '    <a class="thumbnail" href="#">';
            div += '        <img class="img-responsive" style="width: 400px; heigth: 300px;" src=' + obj.ImagePath + ' alt="">';
            div += '    </a>';
            div += '</div>';
            divData += div;
        }
        $(divData).appendTo(dvIndexDataContainer);
    },

    bindProductsWithPathImage: function (url) {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: url,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.message == '') {
                    product.bindingProducts(result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                alert("Desculpe, ocorreu um erro ao buscar os produtos.");
            }
        });
    }
}

product.initialize();
