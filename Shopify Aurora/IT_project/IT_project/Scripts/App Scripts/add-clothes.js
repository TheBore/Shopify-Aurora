$(document).ready(function () {
    $('#more').on('click', function () {
        var input = "<input type='color' name='colors' value='#ff0000'>";
        var cbinput = "<input type='checkbox'/>";
        $("#colorsList").append(cbinput);
        $("#colorsList").append(input);
        $("#colorsListCreate").append(input);
    });

    $('#less').on('click', function() {
        document.getElementsByName("colors")[0].remove();
    });
});