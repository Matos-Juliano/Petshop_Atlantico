$('document').ready(function () {

    GetHealthStatusComboBox();
    GetAnimalList(1);

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    }); 

    $('#Search').click(function (e) {
        GetAnimalList(1);
    })
})

function GetAnimalList(pageIndex) {
    pageSize = 10;
    FormData = $('#SearchFilters').serialize() + pageSize + pageIndex;
    
    $.ajax({
        url: 'animals/getanimallist',
        type: 'POST',
        data: FormData,
        success: function (response) {
            $('#AnimalList').html(response)
        }
    })
}

function GetHealthStatusComboBox() {
    $.ajax({
        url: 'animals/GetHealthStatusComboBox',
        success: function (response) {
            html = []
            html.push('<option value="">Selecione</options>')
            response.forEach(p => html.push(`<option value="${p.value}">${p.name}</options>`))
            $('#HealthStatus').html(html)
            console.log(html)
        }
    })
}