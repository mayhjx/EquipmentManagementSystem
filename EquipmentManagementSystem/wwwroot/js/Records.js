// 使用记录
$(document).ready(function () {
    // 重新激活上次选择的Tab
    if (localStorage) {
        $('div.card-title a[data-toggle="pill"').on('click', function () {
            localStorage.setItem('activeTab', $(this).attr('href'));
        });
        var activeTab = localStorage.getItem('activeTab');
        if (activeTab) {
            $('.nav-usageRecord a[href="' + activeTab + '"]').tab('show');
        }
    }
    else {
        console.log("当前浏览器不支持LocalStorage");
    };

    $('#date').on('change', function () {
        $('form#search').submit();
    });

    $('#instrumentId').on('change', function () {
        $('form#search').submit();
    });

    // 更新新建行的色谱柱编号
    let project = $("select.select-project:last").val();
    let instrumentId = $("select#instrumentId").val();
    getLatestRecord(project, instrumentId);

    // 更新新建行的色谱柱编号
    $(".select-project:last").on("change", function () {
        let project = $(this).val();
        let instrumentId = $("select#instrumentId").val();
        getLatestRecord(project, instrumentId);
    });
})

$(".edit-btn").click(function () {
    // 将其他已在编辑状态的行取消编辑
    $(".cancel-btn").click();
    var RowOfViewRecord = $(this).closest("tr");
    // 选定行的下一行是编辑行
    var RowOfEditRecord = RowOfViewRecord.next("tr");
    RowOfViewRecord.hide();
    RowOfEditRecord.show();
    RowOfEditRecord.find(":disabled").prop("disabled", false);
});

$(".cancel-btn").click(function () {
    var RowOfEditRecord = $(this).closest("tr");
    var RowOfViewRecord = RowOfEditRecord.prev("tr");
    RowOfEditRecord.hide();
    RowOfEditRecord.find("input,button,textarea,select").prop("disabled", true);
    RowOfViewRecord.show();
});

$(".delete-usage-record-btn").click(function () {
    if (!confirm("确认删除该条记录？")) {
        return false;
    }

    let id = $(this).attr("data-id");
    let record = $(this).closest("tr"); // 调用ajax后，this就不再是delete-btn了，所以需要先获取

    var options = {};
    options.url = "Records/UsageRecords/Delete?id=" + id;
    options.type = "post";
    options.dataType = "json";
    options.beforeSend = function (xhr) {
        xhr.setRequestHeader("MY-XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
    };
    options.success = function (msg) {
        if (msg.indexOf("成功") != -1) {
            record.next("tr.update-row").remove(); // 删除对应的编辑行
            record.remove(); // 删除行
        }
        else {
            alert(msg);
        }
    };
    options.error = function (msg) {
        alert(msg);
    };

    $.ajax(options);
});

// 获取最新一条记录
function getLatestRecord(project, instrumentId) {
    $.get(`Records/UsageRecords/AjaxHelper?handler=LatestRecordOfProject&project=${project}&instrumentId=${instrumentId}`, function (data) {
        //console.log(data);
        setInitialValue(data);
    });
};

// 初始化新使用记录的值
function setInitialValue(data) {
    if (data != null) {
        // 色谱柱编号
        $("#UsageRecord_SystemOneColumnNumber").val(data.systemOneColumnNumber);
        $("#UsageRecord_SystemTwoColumnNumber").val(data.systemTwoColumnNumber);

        // 色谱柱，真空度单位
        $("#UsageRecord_LowVacuumDegreeUnit").val(data.lowVacuumDegreeUnit);
        $("#UsageRecord_HighVacuumDegreeUnit").val(data.highVacuumDegreeUnit);
        $("#UsageRecord_SystemOneColumnPressureUnit").val(data.systemOneColumnPressureUnit);
        $("#UsageRecord_SystemTwoColumnPressureUnit").val(data.systemTwoColumnPressureUnit);
    }
    else {// 重置
        $("#UsageRecord_SystemOneColumnNumber").val("");
        $("#UsageRecord_SystemTwoColumnNumber").val("");

        $("#UsageRecord_LowVacuumDegreeUnit")[0].selectedIndex = 0;
        $("#UsageRecord_HighVacuumDegreeUnit")[0].selectedIndex = 0;
        $("#UsageRecord_SystemOneColumnPressureUnit")[0].selectedIndex = 0;
        $("#UsageRecord_SystemTwoColumnPressureUnit")[0].selectedIndex = 0;
    }
}
