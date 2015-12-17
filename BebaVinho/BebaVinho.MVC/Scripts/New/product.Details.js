var productDetails = {

    divProductPrice: null,
    divTitulo: null,
    divRegion: null,
    inputQuant: null,
    ImgProductImage: null,
    divSmallDescription: null,
    divDescription: null,
    getQueryStringValue: null,

    initialize: function () {
        productDetails.divProductPrice = $('.product-price');
        productDetails.divTitulo = $('#title');
        productDetails.divRegion = $('#region');
        productDetails.inputQuant = $('#input-quant');
        productDetails.divSmallDescription = $('#small-description');
        productDetails.divDescription = $('#description');
        productDetails.ImgProductImage = $('#product-image');
        var productId = productDetails.getQueryStringValue('productId');
        productDetails.getDetailsPageData(productId);
    },

    getQueryStringValue: function (name) {
        var url = window.location.href;
        var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(url);
        if (!results) {
            return undefined;
        }
        return results[1] || undefined;
    },

    bindDetailsPageData: function (json) {
        var quant = productDetails.getQueryStringValue('productId');
        var dvOldPrice = productDetails.divProductPrice.find('div[class="from"]');
        var dvPrice = productDetails.divProductPrice.find('div[class="for"]');

        productDetails.divTitulo.text(json.Title);
        productDetails.divRegion.text(json.Region);

        if (json.OldPrice == '') {
            dvOldPrice.find('span[class="from-price"]').text('-');
        }
        else {
            dvOldPrice.find('span[class="from-price"]').text(json.OldPrice);
        }

        dvPrice.find('span[class="for-price"]').text('R$' + json.Price);

        var imagePath = '/Content' + json.PDImagePath1;

        productDetails.ImgProductImage.attr('src', imagePath);
        productDetails.inputQuant.val(quant);
        productDetails.divSmallDescription.find('span[class="Static_15"]').text(json.SmallDetail);
        productDetails.divDescription.text(json.Detail);
    },

    getDetailsPageData: function (productId) {
        var productIdInt = parseInt(productId);
        $.ajax({
            type: 'GET',
            dataType: 'json',
            data: { 'id': productIdInt },
            url: '/New/GetProductProductDetailsById',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.message == '') {
                    if (result.data == null || result.data.length == 0) {
                        return;
                    }

                    productDetails.bindDetailsPageData(result.data);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (result) {
                alert("Desculpe, ocorreu um erro ao buscar os detalhes produtos.");
            }
        });
    }
}

$(document).ready(function () {
    productDetails.initialize();
}); 