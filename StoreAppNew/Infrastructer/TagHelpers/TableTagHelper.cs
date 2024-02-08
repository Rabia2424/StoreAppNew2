using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreAppNew2.Infrastructer.TagHelpers
{
	[HtmlTargetElement("table")]
	public class TableTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.SetAttribute("class", "table table-hover");
		}
	}
}
