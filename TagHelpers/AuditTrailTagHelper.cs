using System.Collections.Generic;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace EquipmentManagementSystem.TagHelpers
{
    public class AuditTrailTagHelper : TagHelper
    {
        public IList<AuditTrailLog> logs { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.SetAttribute("class", "table table-bordered");

            output.Content.SetHtmlContent($@"
                                            <thead class='text-center'>
                                            <tr>
                                            <th>时间</th>
                                            <th>动作</th>
                                            <th>操作者ID</th>
                                            <th>操作者</th>
                                            <th>记录ID</th>
                                            <th>旧值</th>
                                            <th>新值</th>
                                            </tr>
                                            </thead><tbody>");

            foreach (var log in logs)
            {
                output.Content.AppendHtml($@"
                                            <tr>
                                            <td>{log.DateChanged}</td>
                                            <td>{log.Action}</td>
                                            <td>{log.UserId}</td>
                                            <td>{log.UserName}</td>
                                            <td>{log.PrimaryKeyValue}</td>");

                output.Content.AppendHtml("<td>");

                if (log.OriginalValue?.Length > 0)
                {
                    var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.OriginalValue);
                    output.Content.AppendHtml("<ul>");
                    foreach (var kvp in oldValueDic)
                    {
                        output.Content.AppendHtml($@"<li>{kvp.Key}：{kvp.Value}</li>");
                    }
                    output.Content.AppendHtml("</ul>");
                }

                output.Content.AppendHtml("</td>");
                output.Content.AppendHtml("<td>");

                if (log.CurrentValue?.Length > 0)
                {
                    var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.CurrentValue);
                    output.Content.AppendHtml("<ul>");
                    foreach (var kvp in oldValueDic)
                    {
                        output.Content.AppendHtml($@"<li>{kvp.Key}: {kvp.Value}</li>");
                    }
                    output.Content.AppendHtml("</ul>");
                }

                output.Content.AppendHtml("</td></tr>");
            }
            output.Content.AppendHtml("</tbody>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
