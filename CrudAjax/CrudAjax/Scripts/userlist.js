$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "Json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.EmpId + '</td>';
                html += '<td>' + item.EmpName + '</td>';
                html += '<td>' + item.EmpAge + '</td>';
                html += '<td>' + item.EmpState + '</td>';
                html += '<td>' + item.EmpCountry + '</td>';

                html += '<td><a href="#" onclick="return GetById(' + item.EmpId + ')">Edit</a> | <a href="#" onclick="Delele(' + item.EmpId + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var EmpId = $('#EmpId').val();
    var EmpName = $('#EmpName').val();
    var EmpAge = $('#EmpAge').val();
    var EmpState = $('#EmpState').val();
    var EmpCountry = $('#EmpCountry').val();

    // Create an object to hold the data
    var emp = {
        EmpId: EmpId,
        EmpName: EmpName,
        EmpAge: EmpAge,
        EmpState: EmpState,
        EmpCountry: EmpCountry,
    };

    $.ajax({
        url: "/Home/Create",
        type: "POST",
        data: JSON.stringify(emp),
        contentType: "application/json",
        success: function (result) {
            // Handle success, if needed

            // Reload or update your data as required
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert("Error: " + errormessage.responseText);
        }
    });
}


function GetById(ID) {
    $('#EmpId').css('border-color', 'lightgrey');
    $('#EmpName').css('border-color', 'lightgrey');
    $('#EmpAge').css('border-color', 'lightgrey');
    $('#EmpState').css('border-color', 'lightgrey');
    $('#EmpCountry').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/GetById/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#EmpId').val(result.EmpId);
            $('#EmpName').val(result.EmpName);
            $('#EmpAge').val(result.EmpAge);
            $('#EmpState').val(result.EmpState);
            $('#EmpCountry').val(result.EmpCountry);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var UserEdit = {
        EmpId: $('#EmpId').val(),
        EmpName: $('#EmpName').val(),
        EmpAge: $('#EmpAge').val(),
        EmpState: $('#EmpState').val(),
        EmpCountry: $('#EmpCountry').val(),
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(UserEdit),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#EmpId').val("");
            $('#EmpName').val("");
            $('#EmpAge').val("");
            $('#EmpState').val("");
            $('#EmpCountry').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function Delele(EmpId) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + EmpId,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}


function clearTextBox() {
    $('#EmpId').val("");
    $('#EmpName').val("");
    $('#EmpAge').val("");
    $('#EmpState').val("");
    $('#EmpCountry').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#EmpId').css('border-color', 'lightgrey');
    $('#EmpName').css('border-color', 'lightgrey');
    $('#EmpAge').css('border-color', 'lightgrey');
    $('#EmpState').css('border-color', 'lightgrey');
    $('#EmpCountry').css('border-color', 'lightgrey');
}

function validate() {
    var isValid = true;
    if ($('#EmpName').val().trim() == "") {
        $('#EmpName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmpName').css('border-color', 'lightgrey');
    }
    if ($('#EmpAge').val().trim() == "") {
        $('#EmpAge').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmpAge').css('border-color', 'lightgrey');
    }
    if ($('#EmpState').val().trim() == "") {
        $('#EmpState').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmpState').css('border-color', 'lightgrey');
    }
    if ($('#EmpCountry').val().trim() == "") {
        $('#EmpCountry').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmpCountry').css('border-color', 'lightgrey');
    } 
    return isValid;
}