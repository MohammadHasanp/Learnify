using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Learnify.Web.TagHelpers
{
    [HtmlTargetElement("submit", TagStructure = TagStructure.WithoutEndTag)]
    public class SubmitButton : TagHelper
    {
        public string Text { get; set; } = "ثبت اطلاعات";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("class", "btn btn-success shadow");
            output.Attributes.Add("type", "submit");
            output.Attributes.Add("value", Text);
        }
    }
}
