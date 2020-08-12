
$(document).ready(function () {

    // 根据所选项目获取仪器下拉列表
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

    // 使用记录页面色谱柱压力输入
    $("button#showSystemOne").click(function (e) {
        // 显示系统一色谱柱压力输入框
        e.preventDefault();
        $("button#showSystemOne").attr("hidden", "");
        $("div#systemOne").removeAttr("hidden");
        $("button#showSystemTwo").removeAttr("hidden");
    })

    var unit = $("select#SystemOnePressureUnit").html();
    var checkvalue = $("select#SystemOnePressureUnit").val();
    $("select#SystemTwoPressureUnit").empty();
    $("select#SystemTwoPressureUnit").append(unit);
    $("select#SystemTwoPressureUnit").val(checkvalue);

    $("button#showSystemTwo").click(function (e) {
        // 显示系统二色谱柱压力输入框
        e.preventDefault();
        $("div#systemTwo").removeAttr("hidden");
        $("button#showSystemTwo").attr("hidden", "");
        // 系统二色谱柱压力单位与系统一同步
        var checkvalue = $("select#SystemOnePressureUnit").val();
        var unit = $("select#SystemOnePressureUnit").html();
        $("select#SystemTwoPressureUnit").empty();
        $("select#SystemTwoPressureUnit").append(unit);
        $("select#SystemTwoPressureUnit").val(checkvalue);
    })

    $("select#SystemOnePressureUnit").change(function () {
        // 更新系统二色谱柱压力单位
        var checkvalue = $(this).val();
        $("select#SystemTwoPressureUnit").val(checkvalue);
    })

    $("select#SystemTwoPressureUnit").change(function () {
        // 更新系统一色谱柱压力单位
        var checkvalue = $(this).val();
        $("select#SystemOnePressureUnit").val(checkvalue);
    })


    // 维护记录页面
    $("#project").on("change", function () {
        // 取消选中
        $("input:radio").prop("checked", false);
        // 删除已生成的维护内容选项
        $("input:checkbox").parent().remove();
        // 隐藏临时维护内容输入框
        $("div#TemporarilyContent").attr("hidden", "hidden");
    });

    $("#instrumentId").on("change", function () {
        // 取消选中
        $("input:radio").prop("checked", false);
        // 删除已生成的维护内容选项
        $("input:checkbox").parent().remove();
        // 隐藏临时维护内容输入框
        $("div#TemporarilyContent").attr("hidden", "hidden");
    });

    $("input:radio").on("click", function (e) {
        var type = e.target.value;
        var instrument = $("#instrumentId").val();
        // 删除已生成的维护内容选项
        $("input:checkbox").parent().remove();
        // 隐藏临时维护内容输入框
        $("div#TemporarilyContent").attr("hidden", "hidden");

        if (type == "临时维护") {
            $("div#TemporarilyContent").removeAttr("hidden");
            $("div#TemporarilyContent textarea").val("");
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

    $(document).ajaxSuccess(function () {
        console.log("ajax Success");
    })

    $(document).ajaxError(function () {
        console.log("ajax Error");
    })
});
