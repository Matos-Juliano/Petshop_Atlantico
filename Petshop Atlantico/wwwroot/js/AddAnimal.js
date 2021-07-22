$('document').ready(function () {
    GetHealthStatusComboBox();
    GetLodgingList()

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    }); 
})

function GetLodgingList() {
    var htmlString = $('#Lodge_Id').html();
    $.ajax({
        url: '../lodgings/getunnocupiedlodginglist',
        success: function (response) {
            var htmlArr = []
            htmlArr.push(htmlString);
            response.forEach(p => htmlArr.push(`<option value="${p}">Vaga ${p}</option>`))
            $('#Lodge_Id').html(htmlArr)
        }
    })
}

function GetHealthStatusComboBox() {
    $.ajax({
        url: 'GetHealthStatusComboBox',
        success: function (response) {
            html = []
            response.forEach(p => html.push(`<option value="${p.value}">${p.name}</options>`))
            $('#HealthStatus').html(html)
        }
    })
}