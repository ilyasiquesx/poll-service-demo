// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function()
{
    let i = 1;
    $('#add').click(function() {
        if(i < 9)
        {
            i++;
            $('#dynamic-field').append('<tr id="row-'+i+'"><td><span class="field-validation-valid" data-valmsg-for="AddOptionModels['+i+'].Name" data-valmsg-replace="true"></span><input class="form-control" placeholder="Введите вариант ответа" type="text" data-val="true" data-val-required="Поле обязательно к заполнению" id="AddOptionModels_'+i+'__Name" name="AddOptionModels['+i+'].Name" value=""></td><td class="align-middle"><a id="'+i+'" style="color: red" class="btn-remove"><i class="fas fa-minus-circle fa-lg"></i></a></td></tr>')
        }
    });
    $(document).on('click', '.btn-remove', function() {
        let buttonId = $ (this).attr("id");
        $('#row-'+buttonId+'').remove();
        i--;
    });
});

$('#myForm').submit(function () {
    sendContactForm();
    return false;
});