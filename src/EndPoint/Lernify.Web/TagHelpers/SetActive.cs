//using Microsoft.AspNetCore.Razor.TagHelpers;

//namespace Eshop_RazorPage.TagHelpers
//{
//    public class SetActive:TagHelper
//    {
//        public string Url { get; set; }
//        public string Class { get; set; } = "btn btn-sm btn-warning mt-1";
//        public string Description { get; set; } = "";
//        public string Alert { get; set; }
//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            output.TagName = "button";
//            output.Attributes.Add("class",Class);
//            output.Attributes.Add("onClick", $"SetActive('{Url}','{Description}','{Alert}')");
//            base.Process(context, output);
//        }
//    }
//}
