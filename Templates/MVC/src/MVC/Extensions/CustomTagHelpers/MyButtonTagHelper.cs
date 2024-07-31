using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.Extensions.CustomTagHelpers;

[HtmlTargetElement("my-button", Attributes = "btn-type, route-id")]
public class MyButtonTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _contextAccessor;
    
    private string? ActionName;
    private string? ClassName;
    private string? ClassSpanIcon;

    [HtmlAttributeName("btn-type")]
    public ButtonType ButtonTypeSelected { get; set; }

    [HtmlAttributeName("route-id")]
    public int RouteId { get; set; }
    public MyButtonTagHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        switch (ButtonTypeSelected)
        {
            case ButtonType.Details:
                ActionName = "Details";
                ClassName = "btn btn-info";
                ClassSpanIcon = "fa fa-search";
                break;
            case ButtonType.Edit:
                ActionName = "Edit";
                ClassName = "btn btn-warning";
                ClassSpanIcon = "fa fa-pencil-alt";
                break;
            case ButtonType.Delete:
                ActionName = "Delete";
                ClassName = "btn btn-danger";
                ClassSpanIcon = "fa fa-trash";
                break;
        }

        var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

        output.TagName = "a";
        output.Attributes.SetAttribute("href", $"{controller}/{ActionName}/{RouteId}");
        output.Attributes.SetAttribute("class", ClassName);

        var span = new TagBuilder("span");
        span.AddCssClass(ClassSpanIcon);

        output.Content.AppendHtml(span);
    }
}

public enum ButtonType
{
    Details = 1,
    Edit,
    Delete
}
