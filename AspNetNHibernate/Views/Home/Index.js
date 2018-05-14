$(function () {
    //Variáveis
    var CustomerDialog = new puidialog('#CustomerDialog', 500, [], 'center');

    //Funções
    function ReloadCustomer() {
        $('#CustomerDataTable').puidatatable({
            columns: [
                { field: 'Name', headerText: 'Nome', bodyStyle: 'text-align:center' },
                { field: 'CPF', headerText: 'CPF', bodyStyle: 'text-align:center' }
            ],
            paginator: {
                rows: 5
            },
            datasource: function (callback) {
                $.ajax({
                    type: 'post',
                    data: $('#SearchForm').serialize(),
                    url: '/Home/QueryCustomers',
                    context: this,
                    success: function (response) {
                        callback.call(this, response);
                    }
                });
            },
            selectionMode: 'single'
        });
    }

    //Eventos
    $('#SearchForm_Button').click(function (e) {
        e.preventDefault();

        ReloadCustomer();
    });

    $('#CustomerDataTable_New').click(function (e) {
        e.preventDefault();
        CustomerDialog.showDialog();
        $('#CustomerDialog')[0].reset();
    });
    $('#CustomerDataTable_Edit').click(function (e) {
        e.preventDefault();
        var data = $('#CustomerDataTable').puidatatable('getSelection')[0];
        if (data != null) {

            $('#CustomerDialog_Id').val(data.Id);
            $('#CustomerDialog_Name').val(data.Name);
            $('#CustomerDialog_CPF').val(data.CPF);
            CustomerDialog.showDialog();

        } else {
            alert("Nenhum cliente selecionado!");
        }

    });
    $('#CustomerDataTable_Remove').click(function (e) {
        e.preventDefault();
        var data = $('#CustomerDataTable').puidatatable('getSelection')[0];
        if (data != null) {
            $.ajax({
                type: 'post',
                data: { customerId : data.Id },
                url: '/Home/RemoveCustomer',
                dataType: 'json',
                success: function (response) {
                    ReloadCustomer();
                    alert("Cliente Removido");
                },
                error: function (response) {
                    alert("Error!");
                }
            });
        } else {
            alert("Nenhum cliente selecionado!");
        }
    });

    $('#CustomerDialog_Save').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'post',
            data: $('#CustomerDialog').serialize(),
            url: '/Home/SaveCustomer',
            dataType: 'json',
            success: function (response) {
                ReloadCustomer();
                CustomerDialog.hideDialog();
                alert("Cliente Salvo");
            },
            error: function (response) {
                alert("Error!");
            }
        });
    });
    $('#CustomerDialog_Cancel').click(function (e) {
        e.preventDefault();
        CustomerDialog.hideDialog();
    });
    //Inicial
    ReloadCustomer();
})