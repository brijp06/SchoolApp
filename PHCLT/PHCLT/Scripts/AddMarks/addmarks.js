$(document).ready(function () {
    // Load students when standard dropdown changes
    $("#standardDropdown").change(function () {
        loadStudentData();
        loadSubjectdata();
    });
    $("#divDropdown").change(function () {
        loadStudentData();
    });
    loadSubjectdata();
    loadExamdata();

    // Load students from the server
    function loadSubjectdata() {
        var selectedStandard = $("#standardDropdown").val();
        var selecteddiv = $("#divDropdown").val();

        $.ajax({
            url: "/AddMarks/GetSubject",
            type: "GET",
            data: { standard: selectedStandard, Div: selecteddiv }, // Pass selected standard
            success: function (data) {
                $('#SubjectDropdown').empty();

                if (data && data.length > 0) {
                    $('#SubjectDropdown').append('<option value="0">Please Select a Subject</option>');
                    data.forEach(function (Subject) {
                        $('#SubjectDropdown').append('<option value="' + Subject.Id + '">' + Subject.Subjectname + '</option>');
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
    function loadExamdata() {
        var selectedStandard = $("#standardDropdown").val();
        var selecteddiv = $("#divDropdown").val();

        $.ajax({
            url: "/AddMarks/GetExamtype",
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

    // Load students from the server
    function loadStudentData() {
        var selectedStandard = $("#standardDropdown").val();
        var selecteddiv = $("#divDropdown").val();

        $.ajax({
            url: "/AddMarks/GetStudents",
            type: "GET",
            data: { standard: selectedStandard, Div: selecteddiv }, // Pass selected standard
            success: function (data) {
                $("#productTable tbody").empty(); // Clear existing rows
                $.each(data, function (index, student) {
                    var row = `
                           <tr>
                                <td style="display:none" class="student-id">${student.Id}</td>
                                <td>${student.Name}</td>
                                     <td>
             <input type="number" class="student-input" value="${student.Marks}" style="width: 65px; height: 30px;" />
                </td>
                            </tr>`;
                    $("#productTable tbody").append(row);

                });

                // Add event listener for checkbox changes
                
            },
            error: function () {
                alert("Failed to load data.");
            }
        });
    }

    // Load data on page load
    loadStudentData();

    // Save students using AJAX
    $("#saveStudents").click(function () {
        var selectedStandard = $("#standardDropdown").val();
        var selecteddiv = $("#divDropdown").val();
        var selecteexamtype = $("#ExamtypeDropdown option:selected").text();
        var selectesubject = $("#SubjectDropdown option:selected").text();
        var exmid = $("#ExamtypeDropdown").val();
        var subid = $("#SubjectDropdown").val(); 
        var selectemarks = $("#Passingmark").val();

        var students = [];
        $("#productTable tbody tr").each(function () {
            var studentId = $(this).find(".student-id").text().trim(); // Get student ID
            var studentName = $(this).find("td:eq(1)").text().trim(); // Get student name
            var marks = $(this).find(".student-input").val(); // Get checkbox status

            students.push({
                Id: studentId, // Send student ID
                Name: studentName,
                Marks: marks,
                Standard: selectedStandard, // Include selected standard
                Div: selecteddiv,
                exmtype: selecteexamtype,
                subname: selectesubject,
                passmark: selectemarks,
                subid: subid,
                exmid: exmid
            });
        });

        $.ajax({
            url: "/AddMarks/SaveMarks",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ students: students }),
            success: function (response) {
                //alert(response.message);
                Toast.fire({
                    icon: 'success',
                    title: 'Save.'
                })
                loadStudentData(); // Reload data after save
                loadSubjectdata();
                loadExamdata();
            },
            error: function () {
                alert("An error occurred while saving data.");
            }
        });
    });

    // Select all checkboxes when the "Select All" button is clicked
    $("#selectAll").click(function () {
        $(".student-checkbox").prop("checked", true).closest("tr").css("background-color", ""); // Reset background color
    });
});
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});