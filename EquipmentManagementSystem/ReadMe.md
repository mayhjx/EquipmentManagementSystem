

** 发布时不能删除已存在的文件和文件夹，因为里面有用户上传的文件 **


Done:
EquipmentContext中仪器和故障部分分开
新建工单时更新故障设备的状态
使用SqlServer数据库
故障工单或需要确认的模块已确认的话则不能进行编辑
修改工单删除逻辑
优化禁止访问页面
一般用户只能新建所属项目组设备的故障工单
故障工单某模块确认不能再编辑，需优化页面逻辑
在故障详情界面进行确认
权限分级
修改密码
导入用户信息
附件保存到物理磁盘，数据库仅保存路径
使用登记导出功能
维护登记导出功能
操作日志
使用记录显示总时长，总标本量，总batch样品量
看板待维护设备显示季度和年度维护

TODO:
忘记密码
报表显示每月的使用率，故障率
记住上次选择的仪器编号
系统日志

20201221
设备主任最高权限，其他主任一般
组长以上密码随机设置

Tips:
Git bash: $git reset --soft HEAD~1
Usage: Undo the most recent commit while keeping the changes made in that commit to staging.

css:
设置width: 100%; box-sizing: border-box;可以让元素在调整td的width时自动适应；
如果table的table-layout: fixed，且thead中有跨越所有列的tr的话，可以设置table的<colgroup><col />...</colgroup?>的宽度调整每一列的宽度；

https://www.smashingmagazine.com/2015/01/designing-for-print-with-css/
table-layout:https://css-tricks.com/almanac/properties/t/table-layout/

要实现跟踪编辑操作的话，要先获取对应的记录，然后使用TryUpdateModelAsync等方法更新记录，才能通过ChangeTracker获取真正的旧值和新值。
http://blog.stoverud.no/posts/how-to-unit-test-asp-net-core-authorizationhandler/