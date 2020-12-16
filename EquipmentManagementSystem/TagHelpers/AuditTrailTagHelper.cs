using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EquipmentManagementSystem.TagHelpers
{
    public class AuditTrailTagHelper : TagHelper
    {
        public IList<AuditTrailLog> Logs { get; set; }
        public string ModalId { get; set; }

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

            foreach (var log in Logs)
            {
                output.Content.AppendHtml($@"<table class='table table-bordered table-sm table-striped'>
                                            <tbody>
                                            <tr class='text-center'>
                                            <td>时间：{log.DateChanged}</td>
                                            <td>动作：{log.Action}</td>
                                            <td>用户ID：{log.UserId}，用户名：{log.UserName}</td>
                                            <td>记录ID：{log.PrimaryKeyValue}</td></tr>");
                //< tr class='text-center bg-secondary'><td colspan='2'>旧值</td><td colspan='2'>新值</td>");

                output.Content.AppendHtml("<tr><td colspan='2' width='50%'>");

                if (log.OriginalValue?.Length > 0)
                {
                    var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.OriginalValue);
                    output.Content.AppendHtml("<ul>旧值：");
                    foreach (var kvp in oldValueDic)
                    {
                        output.Content.AppendHtml($@"<li>{kvp.Key}：{kvp.Value}</li>");
                    }
                    output.Content.AppendHtml("</ul>");
                }

                output.Content.AppendHtml("</td>");
                output.Content.AppendHtml("<td colspan='2' width='50%'>");

                if (log.CurrentValue?.Length > 0)
                {
                    var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.CurrentValue);
                    output.Content.AppendHtml("<ul>新值：");
                    foreach (var kvp in oldValueDic)
                    {
                        output.Content.AppendHtml($@"<li>{kvp.Key}：{kvp.Value}</li>");
                    }
                    output.Content.AppendHtml("</ul>");
                }

                output.Content.AppendHtml("</td></tr>");
            }
            output.Content.AppendHtml("</tbody></table>");

            output.Content.AppendHtml($@"</div>
                        <div class='modal-footer'>
                            <button type='button' class='btn btn-primary float-right' data-dismiss='modal'>关闭</button>
                        </div>
                    </div>
                </div>
            </div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
