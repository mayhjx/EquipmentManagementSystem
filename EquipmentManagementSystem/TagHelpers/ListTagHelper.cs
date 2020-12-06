using System.Collections.Generic;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace EquipmentManagementSystem.TagHelpers
{
    public class ListTagHelper:TagHelper
    {
        public List<string> List { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";

            foreach(var element in List)
            {
                output.Content.AppendHtml($"<li>{element}</li>");
            }

            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
