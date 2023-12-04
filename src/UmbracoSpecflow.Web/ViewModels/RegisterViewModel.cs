using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Website.Models;

namespace UmbracoSpecflow.Web.ViewModels;

public class RegisterViewModel : PublishedContentWrapped
{
    public RegisterViewModel(
        IPublishedContent content,
        IPublishedValueFallback publishedValueFallback) :
        base(
            content,
            publishedValueFallback)
    {
    }
    
    public RegisterModel? RegisterModel { get; set; }
}