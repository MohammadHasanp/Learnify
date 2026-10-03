$(document).ready(function() {
    $('[data-toggle="tooltip"]').tooltip();

    $("select").change(function() {
        $("#filter_Form").submit();
    });

});
