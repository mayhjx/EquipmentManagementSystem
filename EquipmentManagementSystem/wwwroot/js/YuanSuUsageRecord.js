$(document).ready(function () {
    // 重新激活上次选择的Tab
    if (localStorage) {
        $('div.card-title a[data-toggle="pill"').on('click', function () {
            localStorage.setItem('activeTab', $(this).attr('href'));
        });
        var activeTab = localStorage.getItem('activeTab');
        if (activeTab) {
            $('.nav-record a[href="' + activeTab + '"]').tab('show');
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
});

$(".delete-usage-record-btn").click(function () {
    if (!confirm("确认删除该条记录？")) {
        return false;
    }

    let id = $(this).attr("data-id");
    let record = $(this).closest("tr"); // 调用ajax后，this就不再是delete-btn了，所以需要先获取

    var options = {};
    options.url = "Records/UsageRecordsOfYuanSu/Delete?id=" + id;
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