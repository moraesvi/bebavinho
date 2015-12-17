var product = {

    dvIndexDataContainer: null,
    btnHomeContainer: null,
    dvDataContainer: null,
    totalCart: 0,
    fTotalCartContainer: null,
    inputCleanContainer: null,
    confirmationDiagologContainer: null,
    dialogFormContainer: null,
    shoppingCart: {},

    initialize: function () {
        product.dvIndexDataContainer = $('#dvIndexData');
        product.btnHomeContainer = $('#btnHome');
        product.dvDataContainer = $('#dvData');
        product.fTotalCartContainer = $('#fTotalCart');
        product.inputCleanContainer = $('#inputClean');
        product.confirmationDiagologContainer = $('#open-confirmation-dialog');
        product.dialogFormContainer = $('#dialog-form');
        product.initializeShoppingCartModel();
        product.events();
        //product.bindProductsWithPathImage('Default/GetProductsWithPathImage');
    },

    events: function () {
        var dialog;

        dialog = product.dialogFormContainer.dialog({
            autoOpen: false,
            height: 650,
            width: 850,
            modal: true,
            close: function () {
                $(this).dialog("close");
            }
        });

        product.confirmationDiagologContainer.click(function () {
            dialog.dialog("open");
        });

        product.inputCleanContainer.click(function () {
            product.clearLabelTotalCart();
        });

        product.dvDataContainer.find('button[title="Carrinho"]').each(function () {
            $(this).click(function () {
                var box = $(this).parent();
                var dvText = box.find('div[class="text"]');
                var span = dvText.find('span[class="count-cart"]')
                var spanCountCart = parseInt(span.text());
                spanCountCart += 1;
                span.text(spanCountCart);

                product.totalCart += 1;
                product.updateLabelTotalCart();
                product.setShoppingCartModel(box, span.text());
            });
        });

        product.dvDataContainer.find('button[class="actions-cart"]').each(function () {
            $(this).click(function () {
                var actionType = $(this).val();
                var box = $(this).parent().parent().parent();
                if (actionType.toLowerCase() == 'editar') {
                    $(this).find('span').removeClass('glyphicon-pencil').addClass('glyphicon-ok');
                    var span = $(this).parent().find('span[class="count-cart"]');
                    span.removeClass('count-cart').addClass('hide');
                    $('<input type="number" style="width: 40px" min="1" max="50"></input>').insertAfter(span).bind('keyup', function () {
                        var total = 0;
                        if ($(this).val() != '' && $(this).val() != undefined) {
                            total = parseInt($(this).val());
                        }
                        if (total > 50 || total == undefined) {
                            $(this).val('');
                        }
                        product.totalCart = total;
                        product.updateLabelTotalCart();
                    });
                    $(this).css({ 'title': 'ok' });
                    $(this).val('ok')
                }
                else if (actionType.toLowerCase() == 'ok') {
                    var countCart = $(this).parent().find('input[type="number"]').val();
                    countCart = countCart == '' ? 0 : countCart;
                    $(this).find('span').removeClass('glyphicon-ok ').addClass('glyphicon-pencil');
                    var span = $(this).parent().find('span[class="hide"]');
                    span.removeClass('hide').addClass('count-cart').text(countCart);
                    $(this).parent().find('input[type="number"]').remove();
                    $(this).css({ 'title': 'Editar' });
                    $(this).val('editar');
                    product.setShoppingCartModel(box, countCart);
                }
            });
        });
    },

    updateLabelTotalCart: function () {
        product.fTotalCartContainer.text(product.totalCart);
    },

    setShoppingCartModel: function (box, count) {
        product.shoppingCart.productId = box.find('input[type="hidden"]').val();
        product.shoppingCart.count = parseInt(count);
        product.shoppingCart.productName = box.find('div[class="title"]').find('font').text();

        product.saveShoppingCartSession(product.shoppingCart);
    },

    clearLabelTotalCart: function () {
        product.totalCart = 0;
        product.dvDataContainer.find('span[class="count-cart"]').each(function () {
            $(this).text('0');
        });

        product.fTotalCartContainer.text(product.totalCart);
    },

    initializeShoppingCartModel: function () {

        product.shoppingCartModel = {
            productId: 0,
            count: 0,
            productName: '',
        };

    },

    saveShoppingCartSession: function (shoppingCart) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: 'Default/SaveShoppingCartSession',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(shoppingCart),
            success: function (result) {
                if (result.message == '') {
                    console.log('Log gerado: ' + result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                alert("Desculpe, ocorreu um erro ao buscar os produtos.");
            }
        });
    },

    getShoppingCartSession: function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: 'Default/GetShoppingCartSession',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(shoppingCart),
            success: function (result) {
                if (result.message == '') {
                    console.log('Log gerado: ' + result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                alert("Desculpe, ocorreu um erro ao buscar os produtos.");
            }
        });
    },

    bindShoppingCartSession: function (jsonShoppingCart) {
    
        product.dvDataContainer.each(function () {



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
