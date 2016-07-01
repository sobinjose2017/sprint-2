$(document).ready(function(){
    //$("#login").click(function(){
    //    var email = $('#txtemail').val(),
    //       password = $('#txtpassword').val();
    //    $.ajax({
    //        url: "http://api.baabtra.com/LoginService/login.php",
    //        data: { email: email, password: password },
    //        success: function (response) {
    //            var result = JSON.parse(response);
    //            //console.log(result);
                
    //            if (result[0].ResponseCode=="200") {
    //                window.location.href = "Home/sucess?message=" + result[0].vchr_first_name + " &&photo=" + result[0].vchr_prof_pic + "";
                
    //            }
    //            else if (result[0].ResponseCode == "500" && result[0].Msg == "Password Incorrect!") {
    //                window.location.href = "Home/error?msg=" + result[0].Msg + " &&photo=" + result[0].vchr_prof_pic + "";
    //               // console.log(result[0].vchr_user_name);

    //            }
    //            else {
    //                window.location.href = "Home/error?message="+" &&photo=" + result[0].vchr_prof_pic + "";
    //            }
    //        }
    //    });

    //    //$.post("http://api.baabtra.com/LoginService/login.php", { email: email, password: password })
    //    //.done(function (d) {
    //    //    console.log(d)
    //    //});
    //});

    $("#search").change(function () {
        var like = $(this).val();
        $.ajax({
            url: "/Home/showusers",
            type: "POST",
            data: { like: like },
            success: function (data) {
                console.log(data)
                var result = JSON.parse(data);
                console.log(result.vchr_fname);

            },
            error: function (response) {
                alert(response);

            }
        });

    });

});
function showusers(like) {
    $.ajax({
        url: "/Home/showusers",
        type: "POST",
        data: { like: like },
        success: function (data) {
            console.log(data)
            return data;

        },
        error: function (response) {
            alert(response);

        }
    });
}
