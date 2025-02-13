$("#btnsave").click(function () {
    var token = $("#token").val();
    var msg = $("#messgae").val();

    $.ajax({
        url: "/AddNotification/Sendmessage",
        type: "POST",
        data: {
            token: token,
            msg: msg,
            deviceToken: "es-hZ3IoAspQRlB-TMAUtU:APA91bGxSvy_lj4kxdGgxeEUqRign6h8VYLSBd8KnPFc_1nO14PV6M42Zt3cuLgdDTRFSIyKYt9aQ3wi5GqfpD-BaRn6a6XbAzj86KOa4w4VY5GeJzEUX_U"
        },
        success: function (response) {
            console.log(response);
            alert(response.message);
            Toast.fire({
               icon: 'success',
                title: 'Send.'
           })
        },
        error: function (err) {
            console.log(err);
            alert("Error sending notification!");
        }
    });
    //$.ajax({
    //    url: "/AddNotification/Sendmessage",
    //    type: "POST",
    //    contentType: "application/json",
    //    data: JSON.stringify({ token: token,msg:msg }),
    //    success: function (response) {
    //        //alert(response.message);
    //        Toast.fire({
    //            icon: 'success',
    //            title: 'Save.'
    //        })
           
    //    },
    //    error: function () {
    //        alert("An error occurred while saving data.");
    //    }
    //});
});
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});