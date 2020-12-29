using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.TagHelpers
{
    public class AuditTrailGroupingByPKTagHelper : TagHelper
    {
        public IEnumerable<IGrouping<string, AuditTrailLog>> Logs { get; set; }
        public string ModalId { get; set; }
        public string EmptyValue { get; set; } = "/";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            //output.Attributes.SetAttribute("class", );

            output.Content.SetHtmlContent($@"
                <button type='button' class='btn btn-outline-secondary' data-toggle='modal' data-target='#{ModalId}' style='margin-right:10px;margin-left:10px; '>
                    日志
                </button> ");

            output.Content.AppendHtml($@"
                <div class='modal fade' id='{ModalId}' style='display:none;' aria-hidden='true'>
                   <div class='modal-dialog modal-xl modal-dialog-scrollable'>
                    <div class='modal-content'>
                        <div class='modal-header'>
                            <h4 class='modal-title'>操作日志</h4>
                            <button type = 'button' class='close' data-dismiss='modal' aria-label='Close'>
                                <span aria-hidden='true'>×</span>
                            </button>
                        </div>
                        <div class='modal-body'>");

            if (!Logs.ToList().Any())
            {
                output.Content.AppendHtml("<p>暂无操作日志</p>");
            }

            foreach (var logGroup in Logs)
            {
                output.Content.AppendHtml($@"<table>
                                        <thead><tr><td colspan='2' class='bg-secondary'>记录ID：{logGroup.Key}</td></tr></thead><tbody>");

                foreach (var log in logGroup)
                {
                    if (log.Action == EntityState.Added.ToString())
                    {
                        var action = "新建";
                        output.Content.AppendHtml($@"<tr><td>
                                                    <table>
                                                    <thead><tr><th>时间</th><th>用户</th><th>操作</th></tr></thead>
                                                    <tbody><tr><td>{log.DateChanged}</td><td>{log.UserName}</td><td>{action}</td></tr></tbody>
                                                    </table></td>");

                        output.Content.AppendHtml("<td>");
                        output.Content.AppendHtml("<table><thead><tr><th>字段</th><th>值</th></tr></thead><tbody>");

                        var newValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.CurrentValue);
                        foreach (var kvp in newValueDic)
                        {
                            var value = string.IsNullOrEmpty(kvp.Value) ? EmptyValue : kvp.Value;
                            output.Content.AppendHtml($@"<tr><td class='text-left'>{kvp.Key}</td><td>{value}</td></tr>");
                        }

                        output.Content.AppendHtml("</tbody></table>");
                        output.Content.AppendHtml("</td></tr>");
                    }
                    else if (log.Action == EntityState.Deleted.ToString())
                    {
                        var action = "删除";
                        output.Content.AppendHtml($@"<tr><td>
                                                    <table>
                                                    <thead><tr><th>时间</th><th>用户</th><th>操作</th></tr></thead>
                                                    <tbody><tr><td>{log.DateChanged}</td><td>{log.UserName}</td><td>{action}</td></tr></tbody>
                                                    </table></td>");

                        output.Content.AppendHtml("<td>");
                        output.Content.AppendHtml("<table><thead><tr><th>字段</th><th>值</th></tr></thead><tbody>");

                        var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.OriginalValue);
                        foreach (var kvp in oldValueDic)
                        {
                            var value = string.IsNullOrEmpty(kvp.Value) ? EmptyValue : kvp.Value;
                            output.Content.AppendHtml($@"<tr><td class='text-left'>{kvp.Key}</td><td>{value}</td></tr>");
                        }

                        output.Content.AppendHtml("</tbody></table>");
                        output.Content.AppendHtml("</td></tr>");
                    }
                    else if (log.Action == EntityState.Modified.ToString())
                    {
                        var action = "编辑";
                        output.Content.AppendHtml($@"<tr><td>
                                                    <table>
                                                    <thead><tr><th>时间</th><th>用户</th><th>操作</th></tr></thead>
                                                    <tbody><tr><td>{log.DateChanged}</td><td>{log.UserName}</td><td>{action}</td></tr></tbody>
                                                    </table></td>");

                        output.Content.AppendHtml("<td>");
                        output.Content.AppendHtml("<table><thead><tr><th>字段</th><th>旧值</th><th>新值</th></tr></thead><tbody>");

                        var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.OriginalValue);
                        var newValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.CurrentValue);
                        foreach (var kvp in oldValueDic)
                        {
                            var prop = kvp.Key;
                            var oldValue = string.IsNullOrEmpty(kvp.Value) ? EmptyValue : kvp.Value;
                            var newValue = string.IsNullOrEmpty(newValueDic.GetValueOrDefault(kvp.Key)) ? EmptyValue : newValueDic.GetValueOrDefault(kvp.Key);

                            output.Content.AppendHtml($@"<tr><td class='text-left'>{kvp.Key}</td><td>{oldValue}</td><td>{newValue}</td></tr>");
                        }

                        output.Content.AppendHtml("</tbody></table>");
                        output.Content.AppendHtml("</td></tr>");
                    }
                }
                output.Content.AppendHtml("</tbody></table>");
            }

            output.Content.AppendHtml($@"</div>
                        <div class='modal-footer'>
                            <button type='button' class='btn btn-secondary' data-dismiss='modal'>关闭</button>
                        </div>
                    </div>
                </div>
            </div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
