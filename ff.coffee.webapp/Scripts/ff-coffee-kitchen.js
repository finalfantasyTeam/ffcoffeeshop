"use strict";
var FFCoffeeJs = FFCoffeeJs || {};

FFCoffeeJs.Kitchen = function() {
    
}();

function updatedulieu() {
    //alert("updatedulieu");
    $new = false;

    if ($new)
        $("#notifyAudio")[0].play();

}

function xacnhan(a) {

    //	alert('a');
    //	clearInterval(refreshId);
    $str = 'bepxacnhan.php' + a.attr('id');
    //alert($str);
    $.ajax({
        type: 'POST',
        url: $str,
        success: function () {
            window.location.replace("cook.php");
        }
    });
    $("#hide").load($url);
}

function xacnhancaban_thoigian(a) {

    //	alert('a');
    //	clearInterval(refreshId);
    $str = 'bepxacnhancaban_thoigian.php' + a.attr('id');
    //	alert($str);
    $.ajax({
        type: 'POST',
        url: $str,
        success: function () {
            //alert('bep da xac nhan');
            window.location.replace("cook.php");
        }
    });
    $("#hide").load($url);
}


function huyxacnhan(a) {
    //	clearInterval(refreshId);
    $str = 'bephuyxacnhan.php' + a.attr('id');
    //alert($str);
    $.ajax({
        type: 'POST',
        url: $str,
        success: function () {
            //alert('bep da huy xac nhan');
            window.location.replace("cook.php");

        }
    });
    $("#hide").load($url);
}
function huyxacnhancaban_thoigian(a) {
    //	clearInterval(refreshId);
    $str = 'bephuyxacnhancaban_thoigian.php' + a.attr('id');
    //	alert($str);
    $.ajax({
        type: 'POST',
        url: $str,
        success: function () {
            //alert('bep da huy xac nhan');
            window.location.replace("cook.php");

        }
    });
    $("#hide").load($url);
}