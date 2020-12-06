
$("#add-ColumnType").on("click", function () {
    $(this).before(`<div><input name="ColumnTypes" class="" required /><button class="remove-input" type="button">X</button></div>`);
});

$("#add-MobilePhase").on("click", function () {
    $(this).before(`<div><input name="MobilePhases" class="" required /><button class="remove-input" type="button">X</button></div>`);
});

$("#add-IonSource").on("click", function () {
    $(this).before(`<div><input name="IonSources" class="" required /><button class="remove-input" type="button">X</button></div>`);
});

$("#add-Detector").on("click", function () {
    $(this).before(`<div><input name="Detectors" class="" required /><button class="remove-input" type="button">X</button></div>`);
});

$("div.form-group").on("click", "button.remove-input", function () {
    $(this).parent().remove();
})