$(document).ready(function () {
     loadExamdata();
    $("#ExamtypeDropdown").change(function () {
        showresult();
    });
    
    function loadExamdata() {
        var selectedStandard = $("#standardDropdown").val();
        var selecteddiv = $("#divDropdown").val();

        $.ajax({
            url: "/ShowResult/GetExamtype",
            type: "GET",
            data: { standard: selectedStandard, Div: selecteddiv }, // Pass selected standard
            success: function (data) {
                $('#ExamtypeDropdown').empty();

                if (data && data.length > 0) {
                    $('#ExamtypeDropdown').append('<option value="0">Please Select a Exam Type</option>');
                    data.forEach(function (Examtypes) {
                        $('#ExamtypeDropdown').append('<option value="' + Examtypes.Id + '">' + Examtypes.Examtype + '</option>');
                    });
                } else {
                    //cmbTaluka.append('<option value="0">No Village found</option>');
                }
            },
            error: function () {
                alert("Failed to load data.");
            }
        });
    }
    function showresult() {
        var examtype = $("#ExamtypeDropdown").val();

        $.ajax({
            url: "/ShowResult/GetResult",
            type: "GET",
            data: { examtype: examtype }, // Pass selected standard
            success: function (data) {
                $("#productTable tbody").empty(); // Clear existing rows
                let totalMarks = 0;
                let totalSubjects = 0;

                $.each(data, function (index, Result) {
                    var row = `
                           <tr>
                                <td style="display:none" class="student-id">${Result.Id}</td>
                                <td>${Result.Subjectname}</td>
                                <td>${Result.marks}</td>
                                <td style="font-weight:bold; color: ${Result.marks <= Result.PassingMark ? 'green' : 'red'};">
        ${Result.marks <= Result.PassingMark ? 'Pass' : 'Fail'}
    </td>
                            </tr>`;
                    $("#productTable tbody").append(row);
                    let marks = Number(Result.marks);
                    totalMarks += marks;
                    totalSubjects++;

                });
                $("#totalMarks").text(totalMarks);
                $("#totalPercentage").text((totalMarks / (totalSubjects * 100) * 100).toFixed(2) + "%");
                // Add event listener for checkbox changes

            },
            error: function () {
                alert("Failed to load data.");
            }
        });
    }
});
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});