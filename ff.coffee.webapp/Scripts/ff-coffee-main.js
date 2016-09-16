"use strict";

$(document).ready(function () {

    /*----------------------------Delete Section---------------------------*/

    $("#btnDeleteRestaurant").click(function(){
        var okDelete = confirm("Are you sure to delete it ?"),
            restID = $("#restID").val();
        if (okDelete === true) {
            document.location = "/Setting/DeleteRestaurant/" + restID;
        }
    });

    $("#btnDeleteCoffeeTable").click(function () {
        var okDelete = confirm("Are you sure to delete it ?"),
            ctID = $("#coffeeID").val();
        if (okDelete === true) {
            document.location = "/Setting/SoftDeleteCoffeeTable/" + ctID;
        }
    });
    
    $("#btnDeleteProductCat").click(function () {
        var okDelete = confirm("Are you sure to delete it ?"),
            pcID = $("#proCatID").val();
        if (okDelete === true) {
            document.location = "/Store/SoftDeleteProductCat/" + pcID;
        }
    });
    
    $("#btnDeleteProduct").click(function () {
        var okDelete = confirm("Are you sure to delete it ?"),
            prodID = $("#prodID").val();
        if (okDelete === true) {
            document.location = "/Store/SoftDeleteProduct/" + prodID;
        }
    });

    $("#btnDeleteStaff").click(function () {
        var okDelete = confirm("Are you sure to delete it ?"),
            userName = $("#userName").val();
        if (okDelete === true) {
            document.location = "/Account/SoftDeleteUser/?userName=" + userName;
        }
    });

    /*-----------------------------------------------------------------*/

    $("#txtSearchProduct").keyup(function () {

        if (this.value !== "") {
            loadingToggle(true);
            var url = "Order/SearchProduct?keyword=" + this.value;
            $("#ctnResult").load(url, loadingToggle(false));
        }
        else {
            $("#ctnResult").hide();
        }

    });

    $("#lsbProductCat").change(function () {
        loadingToggle(true);
        var url = "Order/SearchProductCat?category=" + this.value;
        $("#ctnResult").load(url, loadingToggle(false));
    });

    $("#txtDiscount").keyup(function (e) {
        var regNumber = /^[0-9]+$/;
        if (regNumber.test(this.value)) {
            var strDiscount = this.value,
                strTotal = $("#txtTotal").val(),
                discount = Number.parseFloat(strDiscount);
            if (discount > 50) {
                return;
            }
            var total = Number.parseFloat(strTotal),
                finalTotal = total - (total * (discount / 100));

            $("#lblTotal").html(finalTotal.toString().toCurrencyFormat("đ"));
        } else {
            this.value = 0;
        }
    });

    if ($("#saleReportDateFrom").datepicker) {
        $("#saleReportDateFrom").datepicker();
    }

    if ($("#saleReportDateTo").datepicker) {
        $("#saleReportDateTo").datepicker();
    }

    function loadingToggle(isDisplay) {
        if (isDisplay) {
            $("#ctnResult").hide();
            $("#gifLoading").show();
        } else {
            $("#ctnResult").show();
            $("#gifLoading").hide();
        }
    }

    String.prototype.toCurrencyFormat = function (currency) {
        var n = this + " ";
        return n.replace(/(\d)(?=(\d{3})+ )/g, "$1,") + currency;
    }

    String.prototype.toNumberFormat = function () {
        return this.replace(/[^0-9]+/g, "");
    }
});