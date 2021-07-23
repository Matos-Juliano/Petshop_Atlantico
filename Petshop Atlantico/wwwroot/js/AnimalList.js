$('document').ready(function () {

    GetHealthStatusComboBox();
    GetAnimalList(1);

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