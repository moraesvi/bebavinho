var product = {

    productCollectionContainer: null,
    dataTemplateContainer: null,
    btnHomeContainer: null,
    dvRowContainer: null,
    paginationContainer: null,
    solicitarPedidoContainer: null,
    dvDataSolicitationContainer: null,
    pagination: {
        pagina: 0,
        take: 0,
        totalRegisters: 0
    },    

    totalCart: 0,
    fTotalCartContainer: null,
    inputCleanContainer: null,
    confirmationDiagologContainer: null,
    dialogFormContainer: null,
    contentContainer: null,
    shoppingCart: {},
    arrayShoppingCart: [],

    initialize: function () {
        product.paginationContainer = $('#pagination');
        product.productCollectionContainer = $('.product-collection');
        product.dataTemplateContainer = $('#data-template');
        product.productBlockContainer = $('#dvData');
        product.btnHomeContainer = $('#btnHome');
        product.dvRowContainer = product.productCollectionContainer.find('div');
        product.fTotalCartContainer = $('#fTotalCart');
        product.inputCleanContainer = $('#inputClean');
        product.dialogFormContainer = $('#dialog-form');
        product.solicitarPedidoContainer = $('#solicitar-pedido');
        product.dvDataSolicitationContainer = $('#dvDataSolicitation')
        product.initializeShoppingCartModel();
        product.events();
        product.pagination.pagina = 1;
        product.pagination.totalRegisters = 0;
        product.getProducts();
    },

    events: function () {
        var dialog;

        dialog = product.dialogFormContainer.dialog({
            autoOpen: false,
            height: 'auto',
            width: '50%',
            modal: true,
            close: function () {
                $(this).dialog("close");
            }
        });

        product.inputCleanContainer.click(function () {
            product.clearLabelTotalCart();
        });

        product.solicitarPedidoContainer.click(function () {
            if (product.arrayShoppingCart.length == 0) {
                alert('Não existem itens no carrinho.');
                return;
            }
            product.setSolicitationOrBuyData();
            dialog.dialog("open");
        });

        $(window).load(function () {
            product.getShoppingCartSession();
        });

        $(window).bind('beforeunload', function () {
            if (product.arrayShoppingCart.length == 0) {
                return;
            }
            product.saveShoppingCartSession(product.arrayShoppingCart);
        });

        $(document).on({
            ajaxStart: function () {
                //$('body').addClass("loading");
            },
            ajaxStop: function () {
                //$('body').removeClass("loading");
            }
        });
    },

    bidingEventosCompra: function () {
        product.productBlockContainer.find('button[class="btn-comprar"]').each(function () {
            $(this).click(function () {
                var box = $(this).parent();
                var dvText = box.find('h2').find('font[class="quantidade"]');
                var span = dvText.find('span')
                var spanCountCart = parseInt(span.text());
                spanCountCart += 1;
                span.text(spanCountCart);

                product.totalCart += 1;
                product.updateLabelTotalCart();
                product.setShoppingCartModel(box, span.text());
            });
        });
        product.productBlockContainer.find('button[class="btn-comprar-def-quant"]').each(function () {
            $(this).click(function () {
                var actionType = $(this).val();
                var box = $(this);
                var button = box.parent().find('button[class="btn-comprar"]')
                if (actionType.toLowerCase() == 'editar') {
                    box.find('span').removeClass('glyphicon-pencil').addClass('glyphicon-ok');
                    var dvText = box.parent().find('h2').find('font[class="quantidade"]');
                    var span = dvText.find('span');
                    $('<input type="number" style="width: 40px; margin-right: 3px; height: 38px; font-size: 17px;" min="0" max="50" value="' + span.text() + '"></input>').insertBefore(box).bind('keyup', function () {
                        var total = 0;
                        if ($(this).val() != '' && $(this).val() != undefined) {
                            total = parseInt($(this).val());
                        }
                        if (total > 50 || total == undefined) {
                            $(this).val('');
                        }
                        product.totalCart = parseInt($(this).val());
                    });
                    button.hide();
                    $(this).css({ 'title': 'ok' });
                    $(this).val('ok');
                }
                else if (actionType.toLowerCase() == 'ok') {
                    var countCart = $(this).parent().find('input[type="number"]').val();
                    countCart = countCart == '' ? 0 : countCart;
                    box.find('span').removeClass('glyphicon-ok ').addClass('glyphicon-pencil');
                    box.parent().find('input[type="number"]').remove();
                    var dvText = box.parent().find('h2').find('font[class="quantidade"]');
                    var span = dvText.find('span');
                    span.text(parseInt(countCart));
                    box.css({ 'title': 'Editar' });
                    box.val('editar');
                    button.show();
                    product.setShoppingCartModel(box.parent(), countCart);
                    product.updateLabelTotalCart();
                }
            });
        });
    },

    bindPaginationEvent: function () {
        var total = Math.ceil(product.pagination.totalRegisters / 8)
        $('#pagination-top').bootpag({
            total: product.pagination.totalRegisters,
            total: total,
            maxVisible: 5
        }).on('page', function (event, num) {
            product.pagination.pagina = num;
            product.getProductsByPagination();
        });
    },

    updateLabelTotalCart: function () {
        product.totalCart = 0;
        product.productBlockContainer.find('h2').find('font[class="quantidade"]').each(function () {
            product.totalCart += parseInt($(this).find('span').text());
        });
        product.fTotalCartContainer.text(product.totalCart).hide().fadeIn(200);
    },

    setShoppingCartModel: function (box, count) {
        product.shoppingCart = {};
        var productId = box.find('input[type="hidden"]').val();

        var shoppingCartExists = $.grep(product.arrayShoppingCart, function (shoppingCart, index) {
            return shoppingCart.productId == productId
        });

        if (shoppingCartExists.length > 0) {
            shoppingCartExists[0].count = parseInt(count);
            shoppingCartExists[0].total = parseInt(product.fTotalCartContainer.text());
        }
        else {
            product.shoppingCart.productId = productId;
            product.shoppingCart.count = parseInt(count);

            product.arrayShoppingCart.push(product.shoppingCart);
        }
    },

    clearLabelTotalCart: function () {
        product.totalCart = 0;
        product.productBlockContainer.find('h2').find('font[class="quantidade"]').each(function () {
            $(this).find('span').text('0');
        });

        product.fTotalCartContainer.text(product.totalCart);
        product.arrayShoppingCart = [];
        product.clearShoppingCartSession();
    },

    bindPaginationData: function (arrayJson) {
        var div = '';
        var dvData = $('#dvData').html();
        var dataTemplate = product.dataTemplateContainer.html()
        $('#dvData').html('');
        product.pagination.totalRegisters = arrayJson[0].TotalRegisters;
        for (var index = 0; index < arrayJson.length; index++) {
            var templateData = product.dataTemplateContainer.find('div[class="product-block"]').parent();
            var jsonObject = arrayJson[index];
            var idDvData = 'dvData_' + (index + 1);
            templateData.attr('id', idDvData);
            $(product.dataTemplateContainer.html()).appendTo($('#dvData'));

            $('#dvData').find('div[id="' + idDvData + '"]').css({ display: 'block' });

            var imagePath = 'Content' + jsonObject.ImagePath;

            var productBlock = $('#' + idDvData);
            var href = $(productBlock).find('a[role="linkToProduct"]').attr('href') + '?productId=' + jsonObject.Id;

            $(productBlock).find('a[role="productName"]').text(jsonObject.Name);
            $(productBlock).find('a[role="linkToProduct"]').attr('href', href);
            $(productBlock).find('img[class="img-responsive"]').attr('src', imagePath)
            $(productBlock).find('h2[class="h2Regiao"]').text(jsonObject.Region);
            $(productBlock).find('input[class="product-id"]').attr('value', jsonObject.Id)

            var h3 = $(productBlock).find('h3');
            if (jsonObject.OldPrice > 0) {
                var aee = '<s>R$' + jsonObject.OldPrice + '</s> ' + 'R$' + jsonObject.Price;
                $(h3).html(aee);
            }
            else {
                $(h3).text('R$' + jsonObject.Price);
            }
            //productData.append($(productData.html()));
        }
    },

    initializeShoppingCartModel: function () {

        product.shoppingCartModel = {
            productId: 0,
            count: 0,
            productName: '',
        };

    },

    saveShoppingCartSession: function (arrayShoppingCart) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: 'New/SaveShoppingCartSession',
            async: true,
            data: JSON.stringify({ 'arrayShoppingCart': arrayShoppingCart }),
            success: function (result) {
                if (result.message == '') {
                    console.log('Log gerado: ' + result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                //alert("Desculpe, ocorreu um erro ao buscar os produtos.");
            }
        });
    },

    setSolicitationOrBuyData: function () {
        product.shoppingCart = {};
        var index = 1;

        product.arrayShoppingCart.forEach(function () {
            var box = product.dvDataSolicitationContainer.find('div[class="col-md-3 box1"]').first().clone();

            var oldIndex = index - 1;

            if (index == 1) {
                product.dvDataSolicitationContainer.html('');
                oldIndex = 1;
            }

            box.find('font[id="titulo-' + oldIndex + '"]').attr('id', 'titulo-' + index + '');
            box.find('span[id="regiao-' + oldIndex + '"]').attr('id', 'regiao-' + index + '');
            box.find('span[id="preco-unidade-' + oldIndex + '"]').attr('id', 'preco-unidade-' + index + '');
            box.find('span[id="total-calculado-' + oldIndex + '"]').attr('id', 'total-calculado-' + index + '');
            box.find('span[id="total-' + oldIndex + '"]').attr('id', 'total-' + index + '');

            $(box).appendTo(product.dvDataSolicitationContainer);

            index++;
        });

        index = 1; 

        product.productBlockContainer.find('div[class="product-block"]').each(function () {
            var box = $(this);
            var boxId = box.parent().attr('id');
            var productId = box.find('input[type="hidden"]').val();

            var shoppingCartExists = $.grep(product.arrayShoppingCart, function (shoppingCart, index) {
                return shoppingCart.productId == productId
            });
            
            if (shoppingCartExists.length == 0) {
                return;
            }

            box = $('#' + boxId);

            var productName = box.find('a[role="productName"]').text();
            var region = box.find('h2[class="h2Regiao"]').text();
            var price = box.find('h3').text();
            var count = box.find('h2').find('font[class="quantidade"]').find('span').text();

            var titleContainer = product.dvDataSolicitationContainer.find('font[id="titulo-' + index + '"]');
            var regionContainer = product.dvDataSolicitationContainer.find('span[id="regiao-' + index + '"]');
            var unitPriceContainer = product.dvDataSolicitationContainer.find('span[id="preco-unidade-' + index + '"]');
            var totalCalc = product.dvDataSolicitationContainer.find('span[id="total-calculado-' + index + '"]');
            var countContainer = product.dvDataSolicitationContainer.find('span[id="total-' + index + '"]');

            titleContainer.text(productName);
            regionContainer.text(region);
            unitPriceContainer.text(price);
            countContainer.text(count);

            index++;
        });      
    },

    getShoppingCartSession: function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/New/GetShoppingCartSession',
            async: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(),
            success: function (result) {
                if (result.message == '') {
                    if (result.data == null || result.data.length == 0) {
                        return;
                    }

                    product.bindShoppingCartSession(result.data);
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

    clearShoppingCartSession: function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/New/ClearShoppingCartSession',
            async: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(),
            success: function (result) {
                if (result.message == '') {
                    if (result.data == null || result.data.length == 0) {
                        return;
                    }

                    product.bindShoppingCartSession(result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                console.log("Ocorreu um erro ao limpar a sessão");
            }
        });
    },

    bindShoppingCartSession: function (jsonShoppingCart) {
        var totalCart = 0;
        product.arrayShoppingCart = [];
        product.productBlockContainer.find('div[class="product-block"]').each(function () {
            var box = $(this);
            product.shoppingCart = {};
            var productId = box.find('input[type="hidden"]').val();
            var boxId = box.parent().attr('id');

            var shoppingCart = $.grep(jsonShoppingCart, function (shoppingCart, index) {
                return shoppingCart.ProductId == productId
            });

            if (shoppingCart.length == 0)
                return;

            box = $('#' + boxId);

            box.find('font[class="quantidade"]').find('span').text(shoppingCart[0].Count);

            product.shoppingCart.productId = productId;
            product.shoppingCart.count = shoppingCart[0].Count;

            product.arrayShoppingCart.push(product.shoppingCart);
            totalCart += shoppingCart[0].Count;
        });
        product.totalCart = totalCart;
        product.fTotalCartContainer.text(product.totalCart).hide().fadeIn(200);
    },

    bindingProducts: function (arrayJsonData) {
        var div = '';
        var dvData = $('#dvData').html();
        $('#dvData').html('');
        for (var index = 0; index < arrayJsonData.length; index++) {
            var templateData = product.dataTemplateContainer.find('div[class="product-block"]')
            var jsonObject = arrayJsonData[index];
            var idDvData = 'dvData_' + (index + 1);
            templateData.attr('id', idDvData);
            $(dvDataId).appendTo(templateData);

            var productBlock = $('#'+idDvData);
            $(productBlock).find('a[role="productName"]').text(jsonObject.Name);
            $(productBlock).find('h2[class="h2Regiao"]').text(jsonObject.Region);

            var h3 = $(productBlock).find('h3');
            if (jsonObject.OldPrice > 0) {
                var aee = '<s>R$' + jsonObject.OldPrice + '</s> ' + 'R$' + jsonObject.Price;
                $(h3).html(aee);
            }
            else {
                $(h3).text('R$' + jsonObject.Price);
            }
            //productData.append($(productData.html()));
        }
        //$(divData).appendTo(dvIndexDataContainer);
    },

    getProductsWithPathImage: function (url) {
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
    },

    getProducts: function (url) {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            data: { 'skip': product.pagination.pagina },
            url: '/New/GetProductsByPagination',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.message == '') {
                    product.bindPaginationData(result.data);
                    product.bindPaginationEvent();
                    product.bidingEventosCompra();
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

    getByIds: function (url) {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            data: { 'skip': product.pagination.pagina },
            url: '/New/GetByIds',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.message == '') {
                    product.bindPaginationData(result.data);
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

    getProductsByPagination: function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            data: { 'skip': product.pagination.pagina },
            url: '/New/GetProductsByPagination',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.message == '') {
                    product.bindPaginationData(result.data);
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
