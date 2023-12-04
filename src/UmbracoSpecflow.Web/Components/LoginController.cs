using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Models;
using UmbracoSpecflow.Web.ViewModels;

namespace UmbracoSpecflow.Web.Components;

public class LoginController : RenderController
{
    private readonly IPublishedValueFallback _publishedValueFallback;
    
    public LoginController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor, 
        IPublishedValueFallback publishedValueFallback) :
        base(
            logger,
            compositeViewEngine,
            umbracoContextAccessor)
    {
        _publishedValueFallback = publishedValueFallback;
    }

    public override IActionResult Index()
    {
        string? redirectUrl = Request.Query["redirectUrl"];
        
        var model = new LoginViewModel(CurrentPage!, _publishedValueFallback)
        {
            LoginModel = new LoginModel()
            {
                RedirectUrl = redirectUrl,
            } 
        };
        
        return CurrentTemplate(model);
    }
}