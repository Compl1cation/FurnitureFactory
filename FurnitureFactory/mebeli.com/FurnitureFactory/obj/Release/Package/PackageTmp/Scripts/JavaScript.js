var list = [];
var postList = [];
var FurnitureId, Name, PricePerUnit, Quantity;

$(document).on('click', '.addToCart', function () {

    var id = $(this).attr("data-prod-id");
    var name = $(this).attr("data-prod-name");
    var pricePerUnit = $(this).attr("data-prod-pricePerUnit");
    AddToCart(id, name, pricePerUnit);
})


$(document).on('click', '.postButton', function () {

    var prods = [];
    prods = sessionStorage.getItem("postOrder")
    alert(prods);
    PostOrder(prods);
})

function PostOrder(prods)
{
    $.ajax({
        type: "POST",
        url: "http://localhost:55049/api/Order",
        data: prods,
        contentType: "application/json",
        complete: function () {
            alert("Sucessfully placed your order!");
            window.location.href = '/BrowseFurniture/Index';
            //Reset session storage after sucessful order placement
            sessionStorage.clear();
        },
        error: function () { alert("error") }
    });
}
function AddToCart(id, name, pricePerUnit) {

    if (sessionStorage.getItem("order") === null)
    {
        list = [];
        postList = [];
    }
    else
    {
        list = JSON.parse(sessionStorage.getItem("order"));
        postList = JSON.parse(sessionStorage.getItem("postOrder"));
    }
    FurnitureId = parseInt(id);
    Name = name;
    PricePerUnit = pricePerUnit;
    if (list === null || !containsObject(FurnitureId, list)) {
        list.push({ FurnitureId: parseInt(FurnitureId), Name: Name, PricePerUnit: PricePerUnit, Quantity: 1 })
        postList.push({ FurnitureId: FurnitureId, Quantity: 1 })
        sessionStorage.setItem("order", JSON.stringify(list));
        sessionStorage.setItem("postOrder", JSON.stringify(postList));
        alert("Added product", FurnitureId);
    }
    else {
        for (var i = 0; i < list.length; i++) {
            if (list[i].FurnitureId === parseInt(FurnitureId)) {
                list[i].Quantity++;
                postList[i].Quantity++;
                sessionStorage.setItem("order", JSON.stringify(list));
                sessionStorage.setItem("postOrder", JSON.stringify(postList));
            }
            alert("Increased quantity by 1", FurnitureId);
        }
    }
    alert(sessionStorage.getItem("postOrder"));
}

function PopulateCart() {
    var prods = [];
    prods = JSON.parse(sessionStorage.getItem("order"));
    AppendProducts(prods);
}

function AppendProducts(prods)
{
    if (prods !== null) {
        for (var k = 0; k < prods.length; k++) {
            var row = $("<tr>"+
      '<td>' + prods[k].Name + '</td>' +
      '<td>' + prods[k].Quantity + '</td>' +
      '<td>' + ((prods[k].PricePerUnit) * (prods[k].Quantity)) + '</td>' +
      '</tr>');
      $(".shopping").append(row);
        }
    }
}

function containsObject(obj, list) {
        var i;
        for (i = 0; i < list.length; i++) {
            if (list[i].FurnitureId === obj) {
                return true;
            }
        }
    return false;
}



//TODO NOT WORKING:
$(function () {
    var ajaxFormSubmit = function () {
        var form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-prods-target"));
            var $newHtml = $(data);
            $target.replaceWith(data);
            $newHtml.effect("highlight");
        });
        return false;
    };

    $("form[data-prods-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-autocomplete-source]").each(createAutocomplete);
});

function createAutocomplete() {
    var $input = $(this);
    var options =
    {
        source: $input.attr("data-prods-autocomplete")
    };
};