
$(document).ready(function () {
    $("#project").on("change", function () {
        var project = $(this).val();
        $("#instrumentId").removeAttr("disabled");
        $("#instrumentId").empty();
        $.getJSON(`?handler=InstrumentFilter&projectName=${project}`, (data) => {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#instrumentId").append(`<option value='${item}'>${item}</option>`);
                    $(":radio").removeAttr("disabled");
                });
            }
            else {
                $("#instrumentId").empty();
                $("#instrumentId").append("<option value=''>无可选设备</option>");
            }
        });
    });

    $("#instrumentId").on("change", function () {
        // 删除已生成的维护内容选项
        $(":checkbox").parent().remove();
        // 隐藏临时维护内容输入框
        $("div#TemporarilyContent").attr("hidden", "hidden");
    });

    $("input:radio").on("change", function (e) {
        var type = e.target.value;
        var instrument = $("select#instrumentId").val();
        // 删除已生成的维护内容选项
        $(":checkbox").parent().remove();
        // 隐藏临时维护内容输入框
        $("div#TemporarilyContent").attr("hidden", "hidden");

        if (type == "临时维护") {
            $("div#TemporarilyContent").removeAttr("hidden");
        }
        else {
            $.getJSON(`?handler=MaintenanceContents&instrument=${instrument}&maintenanceType=${type}`, (data) => {
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        var html = `<div class="form-check">
<input id="Content-${i}" type="checkbox" name="MaintenanceContent" value="${item}" checked />
<label class="form-check-label" for="Content-${i}">${item}</label></div>`;
                        $(e.target).parent().after(html);
                    })
                }
            });
        }
    });
});
