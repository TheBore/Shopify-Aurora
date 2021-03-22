$(document).ready(function () {

    $('#clothesTable').DataTable();

    $('input[type=radio]').on('change', function () {
        $("#clothesForm").submit();
    });

    $('#selectedSize').on('change', function() {
        $("#clothesForm").submit();
    });

    $('#selectedBrand').on('change', function () {
        $("#clothesForm").submit();
    });

    $('#selectedType').on('change', function () {
        $("#clothesForm").submit();
    });

    $('#myRange').on('change', function () {
        $("#clothesForm").submit();
    });

    $('#quantity').on('change', function() {
        $('#clothesForm').submit();
    });



    /*var slider = document.getElementById("myRange");
    var output = document.getElementById("amount");
    output.innerHTML = slider.value; // Display the default slider value

    // Update the current slider value (each time you drag the slider handle)
    slider.oninput = function () {
        output.innerHTML = this.value;
    }*/
});

