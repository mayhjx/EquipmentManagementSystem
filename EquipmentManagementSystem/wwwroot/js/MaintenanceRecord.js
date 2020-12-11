//// 使用记录
//$(".edit-btn").click(function () {
//    // 将其他已在编辑状态的行取消编辑
//    $(".cancel-btn").click();
//    var RowOfViewRecord = $(this).closest("tr");
//    // 选定行的下一行是编辑行
//    var RowOfEditRecord = RowOfViewRecord.next("tr");
//    RowOfViewRecord.hide();
//    RowOfEditRecord.show();
//    RowOfEditRecord.find("td").children().prop("disabled", false);
//});

//$(".cancel-btn").click(function () {
//    var RowOfEditRecord = $(this).closest("tr");
//    var RowOfViewRecord = RowOfEditRecord.prev("tr");
//    RowOfEditRecord.hide();
//    RowOfEditRecord.find("td").children().prop("disabled", true);
//    RowOfViewRecord.show();
//});

//$(".delete-btn").click(function () {
//    if (!confirm("确认删除该条记录？")) {
//        return false;
//    }

//    let id = $(this).attr("data-id");
//    let record = $(this).closest("tr"); // 调用ajax后，this就不再是delete-btn了，所以需要先获取

//    var options = {};
//    options.url = "Records/UsageRecords/Delete?id=" + id;
//    options.type = "post";
//    options.dataType = "json";
//    options.beforeSend = function (xhr) {
//        xhr.setRequestHeader("MY-XSRF-TOKEN",
//            $('input:hidden[name="__RequestVerificationToken"]').val());
//    };
//    options.success = function (msg) {
//        record.next("tr.update-row").remove(); // 删除对应的编辑行
//        record.remove(); // 删除行
//        console.log(msg);
//    };
//    options.error = function (msg) {
//        alert(msg);
//    };

//    $.ajax(options);
//});

//$("#select-project").on("change", getLatestRecord);

//// 初始化使用记录
//function getLatestRecord() {
//    var project = $("#select-project").val();
//    $.get(`Records/Index?handler=LatestRecordOfProject&project=${project}`, function (data) {
//        setInitialValue(data);
//    });
//};

//function setInitialValue(data) {
//    if (data != null) {
//        //console.log(data);
//        // 色谱柱编号
//        $("#UsageRecord_SystemOneColumnNumber").val(data.systemOneColumnNumber);
//        $("#UsageRecord_SystemTwoColumnNumber").val(data.systemTwoColumnNumber);

//        // 色谱柱，真空度单位
//        $("#UsageRecord_HighVacuumDegreeUnit").val(data.highVacuumDegreeUnit);
//        $("#UsageRecord_LowVacuumDegreeUnit").val(data.lowVacuumDegreeUnit);
//        $("#UsageRecord_SystemOneColumnPressureUnit").val(data.systemOneColumnPressureUnit);
//        $("#UsageRecord_SystemTwoColumnPressureUnit").val(data.systemTwoColumnPressureUnit);
//    }
//    else {
//        // 重置
//        $("#UsageRecord_SystemOneColumnNumber").val("");
//        $("#UsageRecord_SystemTwoColumnNumber").val("");

//        $("#UsageRecord_HighVacuumDegreeUnit")[0].selectedIndex = 0;
//        $("#UsageRecord_LowVacuumDegreeUnit")[0].selectedIndex = 0;
//        $("#UsageRecord_SystemOneColumnPressureUnit")[0].selectedIndex = 0;
//        $("#UsageRecord_SystemTwoColumnPressureUnit")[0].selectedIndex = 0;
//    }
//}

//$(document).ready(function () {
//    // 重新激活上次选择的Tab
//    if (localStorage) {
//        $('div.card-title a[data-toggle="pill"').on('click', function () {
//            localStorage.setItem('activeTab', $(this).attr('href'));
//        });
//        var activeTab = localStorage.getItem('activeTab');
//        if (activeTab) {
//            $('.nav-usageRecord a[href="' + activeTab + '"]').tab('show');
//        }
//    }
//    else {
//        console.log("当前浏览器不支持LocalStorage");
//    };

//    $('#date').on('change', function () {
//        $('form#search').submit();
//    });

//    $('#instrumentId').on('change', function () {
//        $('form#search').submit();
//    });

//    getLatestRecord();
//})

$(document).ready(function () {
    $("select#instrument").on("change", function () {
        let instrumentId = $(this).val();
        getMaintenanceContext(instrumentId);
    });

    $('input:radio').on('click', function (e) {
        $('input:checkbox').prop("checked", false);
        $('input:checkbox').prop("disabled", true);
        var type = e.target.value;
        if (type == "临时维护") {
            $("textarea#other").prop('disabled', false);
        }
        else {
            $("textarea#other").prop('disabled', true);
        }
        $(`input:checkbox.${type}`).prop('disabled', false);
        $(`input:checkbox.${type}`).prop('checked', true);
    });

    //$(document).ajaxError(function () {
    //    console.log("ajax Error");
    //});
});

function getMaintenanceContext(instrumentId) {
    // 删除已生成的维护内容选项
    // 获取维护内容，填写至对应的维护类型下
    // enable所有单选框
    // 将临时维护的其他输入框移动到选项后面
    DeleteAllExistsContent();
    getContentOfInstrument(instrumentId);
    enableAllRatio();
    AddTemporaryTextArea();
}

function DeleteAllExistsContent() {
    $("input:checkbox").parent().remove();
}

function getContentOfInstrument(instrumentId) {
    $.getJSON(`?handler=MaintenanceContents&instrument=${instrumentId}`, (data) => {
        if (Array.isArray(data)) {
            $.each(data, function (i, item) {
                let content = `<div class="form-check">
                                <input id="Content-${i}" class="${item.type}" type="checkbox" name="maintenanceContent" value="${item.text}" disabled />
                                <label class="form-check-label" for="Content-${i}">${item.text}</label></div>`;
                $(`input:radio[value='${item.type}']`).parent().after(content);
            });
        }
        else {
            alert(data);
        }
    });
}

function enableAllRatio() {
    $(":radio").removeAttr("disabled");
}

function AddTemporaryTextArea() {
    let temporaryTextArea = `<div class="form-group pl-3" id="otherContent">
                                <label for="other">其他：</label>
                                <textarea id="other" name="otherMaintenanceContent" class="form-control" rows="3" disabled></textarea>
                            </div>`;

    $('input:radio[value="临时维护"]').parent().last().after(temporaryTextArea);
}
