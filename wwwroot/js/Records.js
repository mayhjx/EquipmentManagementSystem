
$(document).ready(function () {

    // 根据所选项目获取仪器下拉列表
    $("#project").on("change", function () {
        var project = $(this).val();
        $("#instrumentId").removeAttr("disabled");
        $("#instrumentId").empty();
        $.getJSON(`?handler=InstrumentFilter&projectName=${project}`,
            (data) => {
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        $("#instrumentId").append(`<option value='${item}'>${item}</option>`);
                        $(":radio").removeAttr("disabled");
                    });
                    // 维护内容
                    getMaintenanceContext(data[0]);
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

    $('input:radio').on('click', function (e) {
        $('input:checkbox').prop("checked", false);
        $('input:checkbox').prop("disabled", true);
        var type = e.target.value;
        $(`input:checkbox.${type}`).prop('disabled', false);
        $(`input:checkbox.${type}`).prop('checked', true);
    });

    //    $("input:radio").on("click", function (e) {
    //        var type = e.target.value;
    //        var instrument = $("#instrumentId").val();
    //        // 删除已生成的维护内容选项
    //        $("input:checkbox").parent().remove();
    //        // 隐藏临时维护内容输入框
    //        $("div#TemporarilyContent").attr("hidden", "hidden");

    //        if (type == "临时维护") {
    //            $("div#TemporarilyContent").removeAttr("hidden");
    //        }
    //        else {
    //            $.getJSON(`?handler=MaintenanceContents&instrument=${instrument}&maintenanceType=${type}`, (data) => {
    //                if (data.length > 0) {
    //                    $.each(data, function (i, item) {
    //                        var html = `<div class="form-check">
    //<input id="Content-${i}" type="checkbox" name="MaintenanceContent" value="${item}" checked />
    //<label class="form-check-label" for="Content-${i}">${item}</label></div>`;
    //                        $(e.target).parent().after(html);
    //                    })
    //                }
    //            });
    //        }
    //    });

    $(document).ajaxError(function () {
        console.log("ajax Error");
    });
});

// 函数放在$(document).ready()外面就可以跨文件调用
function getMaintenanceContext(instrumentId) {
    // 删除已生成的维护内容选项
    $("input:checkbox").parent().remove();
    $.getJSON(`?handler=MaintenanceContents&instrument=${instrumentId}&maintenanceType=""`, (data) => {
        if (data.length > 0) {
            console.log("获取维护内容成功!");
            $.each(data, function (i, item) {
                var html = `<div class="form-check">
                                <input id="Content-${i}" class="${item.type}" type="checkbox" name="MaintenanceContent" value="${item.text}" disabled />
                                <label class="form-check-label" for="Content-${i}">${item.text}</label></div>`;
                $(`input:radio[value='${item.type}']`).parent().after(html);
            });
            $("div#TemporarilyContent").removeAttr("hidden");
        }
    });
}
