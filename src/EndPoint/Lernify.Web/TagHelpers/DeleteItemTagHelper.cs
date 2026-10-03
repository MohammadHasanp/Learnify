using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Learnify.Web.TagHelpers
{
    [HtmlTargetElement("delete-item")]
    public class DeleteItemTagHelper : TagHelper
    {
        public string Url { get; set; } = null!;
        public string Description { get; set; } = "";
        public string Class { get; set; } = "btn btn-danger btn-sm";
        public bool IsButtonTag { get; set; } = false;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsButtonTag)
                output.TagName = "button";
            else
                output.TagName = "a";

            output.Attributes.Add("onClick", $"DeleteItem('{Url}','{Description}')");
            output.Attributes.Add("class", Class);
            base.Process(context, output);
        }
    }
}
