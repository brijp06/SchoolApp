jQuery(document).ready(function ($) {
    $('#btnsave').on('click', function () {
        var subjectname = $("#subjectname").val();
        var stdname = $('#Standard option:selected').text();
        $.ajax({
            type: 'POST',
            url: '/SubjectMaster/Addsubject',
            data: { subjectname: subjectname, stdname: stdname },
            success: function (result) {
                Toast.fire({
                    icon: 'success',
                    title: 'Save.'
                })
                $("#subjectname").val('');
            },
            error: function (error) {
                // Handle the error response
                console.log(error);
            }
        });
    });
    $('#btnviewlist').on('click', function () {
        const url = `/SubjectMaster/SubjectMasterList`;
        window.location.href = url;
       
    });
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
});