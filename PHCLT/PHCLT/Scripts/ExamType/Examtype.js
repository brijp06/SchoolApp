jQuery(document).ready(function ($) {
    $('#btnsave').on('click', function () {
        var examtype = $("#examtype").val();
        $.ajax({
            type: 'POST',
            url: '/ExamTypeMaster/AddExamtype',
            data: { examtype: examtype },
            success: function (result) {
                Toast.fire({
                    icon: 'success',
                    title: 'Save.'
                })
                $("#examtype").val('');
            },
            error: function (error) {
                // Handle the error response
                console.log(error);
            }
        });
    });
    $('#btnviewlist').on('click', function () {
        const url = `/ExamTypeMaster/ExamtypeMasterList`;
        window.location.href = url;

    });
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
});