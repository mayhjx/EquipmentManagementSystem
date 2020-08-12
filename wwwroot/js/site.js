// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



$(document).ready(function () {

    if (localStorage) {
        // Sidebar状态
        var open = "";
        var close = "sidebar-collapse";
        $('ul.navbar-nav a[data-widget="pushmenu"]').on('click', function () {
            var nextPosition = $("body").hasClass(close) ? open : close;
            localStorage.setItem('sidebarPosition', nextPosition);
        });
        var sidebarPosition = localStorage.getItem("sidebarPosition");
        if (sidebarPosition) {
            $("body").addClass(sidebarPosition);
        }

        // 当前选择的主侧边栏选项
        $('aside a.nav-link').on('click', function () {
            localStorage.setItem('activeMenu', $(this).attr('href'));
        });
        // 当前选择的导航栏选项，当用户单击上边的导航栏时覆盖activeMenu的值
        $('ul.navbar-nav a.nav-link').on('click', function () {
            localStorage.setItem('activeMenu', $(this).attr('href'));
        });
        var activeMenu = localStorage.getItem('activeMenu');
        if (activeMenu) {
            $('li.nav-item a[href="' + activeMenu + '"]').tab('show');
            $('li.nav-item a[href="' + activeMenu + '"]').parent().parent().parent().addClass('menu-open');
            $('li.nav-item a[href="' + activeMenu + '"]').parent().parent().parent().children("a.nav-link").addClass('active');
        }
    }
    else {
        console.log("当前浏览器不支持LocalStorage");
    }

    // 格式Index页面的table
    $('table#index').DataTable({
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        orderMulti: true,
        info: true,
        autoWidth: false,
        responsive: true,
        processing: true,
        stateSave: true,
        deferRender: true,
        language: {
            "emptyTable": "没有数据", //没有数据时要显示的字符串
            "info": "当前 _START_ 条到 _END_ 条 共 _TOTAL_ 条",//左下角的信息，变量可以自定义，到官网详细查看
            "infoEmpty": "无记录",//当没有数据时，左下角的信息
            "infoFiltered": "(从 _MAX_ 条记录过滤)",//当表格过滤的时候，将此字符串附加到主要信息
            "infoPostFix": "",//在摘要信息后继续追加的字符串
            "thousands": ",",//千分位分隔符
            "lengthMenu": "每页 _MENU_ 条记录",//用来描述分页长度选项的字符串
            "loadingRecords": "加载中...",//用来描述数据在加载中等待的提示字符串 - 当异步读取数据的时候显示
            "processing": "处理中...",//用来描述加载进度的字符串
            "search": "搜索：",//用来描述搜索输入框的字符串
            "zeroRecords": "没有找到",//当没有搜索到结果时，显示
            "paginate": {
                "first": "首页",
                "previous": "上一页",
                "next": "下一页",
                "last": "尾页"
            }
        },
    });

    // 下拉列表格式
    $("select.select2").select2({
        theme: 'bootstrap4'
    });


});
