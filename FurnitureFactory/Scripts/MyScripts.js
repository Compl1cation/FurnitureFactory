var list = [];
var postList = [];
var FurnitureId, Name, PricePerUnit, Quantity;

//Populate sessinStorage
$(document).on('click', '.addToCart', function () {

    var id = $(this).attr("data-prod-id");
    var name = $(this).attr("data-prod-name");
    var pricePerUnit = $(this).attr("data-prod-pricePerUnit");
    AddToCart(id, name, pricePerUnit);
})

//Post Order
$(document).on('click', '.postButton', function () {
    var prods = [];
    prods = sessionStorage.getItem("postOrder")
    PostOrder(prods);
});
//Post Order
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
        }
    });
}

//Add Product and increase quantity of sessionStorage
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
}

//Append products
function PopulateCart() {
    var prods = [];
    prods = JSON.parse(sessionStorage.getItem("order"));
    AppendProducts(prods);
}
//Append products
function AppendProducts(prods)
{
    var total = 0;
    if (prods !== null) {
        for (var k = 0; k < prods.length; k++)
        {
           var row = $("<tr>"+
           '<td>' + prods[k].Name + '</td>' +
           '<td>' + prods[k].Quantity + '</td>' +
           '<td>' + ((prods[k].PricePerUnit) * (prods[k].Quantity)) + '</td>' +
           '</tr>');
           total += ((prods[k].PricePerUnit) * (prods[k].Quantity));
           $(".cart").append(row);
        }
        var rowTotal = $("<tr>" +
        "<td>" + "</td>" +
        "<td>" + "</td>" +
        "<td>" + "Total: "+ total +"</td>" +
        "</tr>")
        $(".cart").append(rowTotal);
    }
}

//Check if product is in cart
function containsObject(obj, list) {
        var i;
        for (i = 0; i < list.length; i++) {
            if (list[i].FurnitureId === obj) {
                return true;
            }
        }
    return false;
}

//Autocomplete
$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-prods-target"));
            $target.replaceWith(data);
        });
        return false;
    };
    var createAutocomplete = function () {
        var $input = $(this);

        var options =
        {
            source: $input.attr("data-autocomplete-source")
        };

        $input.autocomplete(options);
    };
    $("form[data-prods-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-autocomplete-source]").each(createAutocomplete);
});

//Sorting Info
//$(document).ready(function () {
//    $(".header").click(function (evt) {
//        debugger;
//        var sortfield = $(evt.target).data("sortfield");
//        if ($("#SortField").val() == sortfield) {
//            if ($("#SortDirection").val() == "Ascending") {
//                $("#SortDirection").val("Descending");
//            }
//            else {
//                $("#SortDirection").val("Ascending");
//            }
//        }
//        else {
//            $("#SortField").val(sortfield);
//            $("#SortDirection").val("Ascending");
//        }
//    });

    //clicking on page number
    //$(".pager").click(function (evt) {
    //    debugger;
    //    var pageindex = $(evt.target).data("pageindex");
    //    $("#CurrentPageIndex").val(pageindex);
    //    evt.preventDefault();
    //    $("form").submit();
    //});

//function GetPage()
//{
//    debugger;
//    var sortInfo = JSON.stringify(getFields());
//         $.ajax({
//          type: "POST",
//          url: "BrowseFurniture/Index",
//          contentType: "application/json; charset=UTF-8",
//          data: sortInfo,
//          dataType: "json"
//      });
//}

//function getFields()
//{
//    debugger;
//    var sortInfo = {
//        SortField : document.getElementById("SortField").value,
//        SortDirection : document.getElementById("SortDirection").value,
//        PageCount : document.getElementById("PageCount").value,
//        PageSize : document.getElementById("PageSize").value,
//        CurrentPageIndex : document.getElementById("CurrentPageIndex").value,
//    };
//    return sortInfo;
//}

    //clicking next
    //$(".next").click(function (evt) {
    //    var pageindex = $("#CurrentPageIndex").val();
    //    $("#CurrentPageIndex").val(Number(pageindex)+Number(1));
    //    evt.preventDefault();
    //    $("form").submit();
    //});

    ////clicking previous
    //$(".previous").click(function (evt) {
    //    var pageindex = $("#CurrentPageIndex").val();
   
    //    $("#CurrentPageIndex").val(Number(pageindex) - Number(1));
    //    evt.preventDefault();
    //    $("form").submit();
    //});
//});
