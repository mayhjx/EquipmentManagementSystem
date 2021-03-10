// 选择类型后自动选中或不选中对应的维护内容
$('div > input:checkbox').on("click", function () {
    let isChecked = $(this).is(":checked");
    let inputValue = $(this).val();
    let ingredients = $(`ul#${inputValue}`).find("input");

    for (var i = 0; i < ingredients.length; i++) {
        ingredients[i].checked = isChecked;
    }
})

// 选中或不取消选中维护内容时更新类型checkbox的状态
$("ul").on("change", "li input:checkbox", function () {
    let parentul = $(this).closest("ul");
    let ulid = parentul.attr("id");

    let checkedCount = 0;
    let ingredients = parentul.find("input");
    for (var i = 0; i < ingredients.length; i++) {
        if (ingredients[i].checked) {
            checkedCount++;
        }
    }

    let TypeCheckbox = $(`input:checkbox[value=${ulid}]`);

    if (checkedCount === 0) {
        TypeCheckbox.prop("checked", false);
        TypeCheckbox.prop("indeterminate", false);
    } else if (checkedCount === ingredients.length) {
        TypeCheckbox.prop("checked", true);
        TypeCheckbox.prop("indeterminate", false);
    } else {
        TypeCheckbox.prop("checked", false);
        TypeCheckbox.prop("indeterminate", true);
    }
})

function getMaintenanceContext(instrumentId) {
    // 删除已生成的维护内容选项
    // 获取维护内容，填写至对应的维护类型下
    DeleteAllExistsContent();
    getContentOfInstrument(instrumentId);
}

function DeleteAllExistsContent() {
    $("input:checkbox.content").parent().remove();
}

function getContentOfInstrument(instrumentId) {
    $.getJSON(`AjaxHelper?handler=MaintenanceContents&instrument=${instrumentId}`, (data) => {
        if (Array.isArray(data)) {
            $.each(data, function (i, item) {
                // 为了分开保存不同类型的维护内容
                let name = "";
                switch (item.type) {
                    case "日常维护":
                        name = "daily";
                        break;
                    case "周维护":
                        name = "weekly";
                        break;
                    case "月度维护":
                        name = "monthly";
                        break;
                    case "季度维护":
                        name = "quarterly";
                        break;
                    case "半年维护":
                        name = "halfYearly";
                        break;
                    case "年度维护":
                        name = "yearly";
                        break;
                    case "临时维护":
                        name = "temporary";
                        break;
                }

                $(`ul#${item.type}`).append(`<li>
                                            <input type="checkbox" id="Content-${i}" class="content" 
                                                    name="${name}MaintenanceContent" value="${item.text}" />
                                            <label class="form-check-label" for="Content-${i}">${item.text}</label>
                                             </li>`)
            });
        }
        else {
            alert(data);
        }

        // 删除没有维护内容的类型
        document.querySelectorAll("ul:empty").forEach(ul => ul.parentElement.style = "display:none;");
    });
}

$(".delete-maintenance-record-btn").click(function () {
    if (!confirm("确认删除该条记录？")) {
        return false;
    }

    let id = $(this).attr("data-id");

    var options = {};
    options.url = "Records/MaintenanceRecords/Delete?id=" + id;
    options.type = "post";
    options.dataType = "json";
    options.beforeSend = function (xhr) {
        xhr.setRequestHeader("MY-XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
    };
    options.success = function (msg) {
        if (msg.indexOf("成功") != -1) {
            location.reload();
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
