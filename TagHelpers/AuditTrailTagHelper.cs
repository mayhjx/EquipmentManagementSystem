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
            output.Attributes.SetAttribute("class", "table table-bordered table-sm");

            output.Content.SetHtmlContent($@"<tbody>");

            foreach (var log in logs)
            {
                output.Content.AppendHtml($@"
                                            <tr class='bg-secondary text-center'>
                                            <td>时间：{log.DateChanged}</td>
                                            <td>动作：{log.Action}</td>
                                            <td>用户ID：{log.UserId}，用户名：{log.UserName}</td>
                                            <td>记录ID：{log.PrimaryKeyValue}</td></tr>
                                            <tr class='text-center bg-secondary'><td colspan='2'>旧值</td><td colspan='2'>新值</td>");

                output.Content.AppendHtml("<tr><td colspan='2'>");

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
                output.Content.AppendHtml("<td colspan='2'>");

                if (log.CurrentValue?.Length > 0)
                {
                    var oldValueDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(log.CurrentValue);
                    output.Content.AppendHtml("<ul>");
                    foreach (var kvp in oldValueDic)
                    {
                        output.Content.AppendHtml($@"<li>{kvp.Key}：{kvp.Value}</li>");
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
