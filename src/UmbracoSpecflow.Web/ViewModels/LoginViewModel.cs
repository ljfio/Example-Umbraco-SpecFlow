using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Models;

namespace UmbracoSpecflow.Web.ViewModels;

public class LoginViewModel : PublishedContentWrapped
{
    public LoginViewModel(
        IPublishedContent content,
        IPublishedValueFallback publishedValueFallback) :
        base(
            content,
            publishedValueFallback)
    {
    }
    
    public LoginModel? LoginModel { get; set; }
}