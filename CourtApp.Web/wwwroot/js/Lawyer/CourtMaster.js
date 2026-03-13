$(document).ready(function () {
    $("#CourtTypeId").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#StateCode").select2({
        placeholder: "Select a state",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtDistrictId").select2({
        placeholder: "Select a district court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#CourtComplexId").select2({
        placeholder: "Select a complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
});
$("#CourtTypeId").on("change", function () {
    if ($("#CourtTypeId option:selected").text().toUpperCase() === "High Court".toUpperCase()) {
        $('#c_district').addClass('div_hide');
        $('#d_court_Complex').addClass('div_hide');
        $('#d_high_court_name').removeClass('div_hide');        
        IsBenchExists().then(function (exists) {
            if (exists) {
                Swal.fire("Error!", "Selected relationship already exist, please make entry of new court in by editing existing data!", "error");
                $('#form-modal').modal('hide');
            } 
        });        
    }
    else {        
        $('#c_district').addClass('div_hide');
        $('#d_court_Complex').addClass('div_hide');
        $('#c_district').removeClass('div_hide');
        $('#d_court_Complex').removeClass('div_hide');
        $('#d_high_court_name').addClass('div_hide');               
    }
    $.getJSON("/LawyerDiary/CourtMaster/LoadCourtDistrictByState?StateId=" + $("#StateCode").val(), function (data) {
        $("#CourtDistrictId").empty();
        $.each(data.Data, function (i, item) {
            $("#CourtDistrictId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
});
$("#DistrictCode").on("change", function () {
    $.getJSON("/LawyerDiary/CourtMaster/LoadCourtDistrict?DistrictId=" + $("#DistrictCode").val(), function (data) {
        $("#CourtDistrictId").empty();
        $.each(data.Data, function (i, item) {
            $("#CourtDistrictId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
});

$("#CourtDistrictId").on("change", function () {
    $.getJSON("/LawyerDiary/CourtMaster/LoadCourtComplex?CDistrictId=" + $("#CourtDistrictId").val(), function (data) {
        $("#CourtComplexId").empty();
        $.each(data.Data, function (i, item) {
            $("#CourtComplexId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
});
$("#CourtComplexId").on("change", function () {
    IsComplexCourtExist().then(function (exists) {       
        if (exists) {
            Swal.fire("Error!", "Selected relationship already exist, please make entry of new court in by editing existing data!", "error");
            $('#form-modal').modal('hide');
        }
    });
});
function IsBenchExists() {
    const url = "/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() +
        "&StateId=" + $("#StateCode").val() +
        "&ComplexId=00000000-0000-0000-0000-000000000000" +
        "&CourtDistrict=00000000-0000-0000-0000-000000000000";

    return $.getJSON(url).then(function (data) {        
        return Array.isArray(data.Data) && data.Data.length > 0;
    });
}

IsComplexCourtExist = function () {
    const url = "/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() +
        "&StateId=" + $("#StateCode").val() +
        "&ComplexId=" + $("#CourtComplexId").val() +
        "&CourtDistrict=" + $("#CourtDistrictId").val();

    return $.getJSON(url).then(function (data) {
        return Array.isArray(data.Data) && data.Data.length > 0;
    });
}

var btn_delete = '<button type="button" onclick="removeKolom($(this))" class="btn btn-warning"><i class="fa fa-trash" aria-hidden="true"></i></button>';
var btn_add = '<button class="add-btn-repeat btn btn-success" onclick="addElement($(this))" type="button"><i class="fa fa-plus" aria-hidden="true"></i></button>';

function removeKolom(e) {
    e.parents('.kolom').remove();
}

function addElement(e) {
    var trlength = $('#tblCourtBench tbody tr').length;
    var clonedRow = $('#tblCourtBench tbody tr:last').clone();
    clonedRow.find('input').each(function (i) {
        var BName = "", BenchId;
        if (i === 0) {
            NameAtr = "CourtBenches[" + trlength + "].CourtBench_En";
            IdAtr = "CourtBenches_" + trlength + "__Address";
        }
        if (i === 1) {
            NameAtr = "CourtBenches[" + trlength + "].Address";
            IdAtr = "CourtBenches_" + trlength + "__Address";
        }
        $(this).attr('name', NameAtr).attr('id', IdAtr);
    });
    clonedRow.find('button').find('button').replaceWith(btn_add);
    clonedRow.find('button.add-btn-repeat').replaceWith(btn_delete);
    clonedRow.find("input").val("");
    $('#tblCourtBench tbody').append(clonedRow);
}