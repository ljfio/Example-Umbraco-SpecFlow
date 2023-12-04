using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using UmbracoSpecflow.Core.Models;
using UmbracoSpecflow.Specs.Context;

namespace UmbracoSpecflow.Specs.Drivers;

public class UmbracoDriver
{
    private readonly IContentService _contentService;
    private readonly IMemberService _memberService;

    public UmbracoDriver(IntegrationWebContext webContext)
    {
        _contentService = webContext.Services.GetRequiredService<IContentService>();
        _memberService = webContext.Services.GetRequiredService<IMemberService>();
    }

    public IContent EnsureHomePageCreated()
    {
        // Locate the home node at the root
        var foundHomeNode = _contentService.GetRootContent()
            .FirstOrDefault(f => f.ContentType.Name == Home.ModelTypeAlias);

        if (foundHomeNode is not null)
            return foundHomeNode;

        // Create a new home node at the root
        var homeNode = _contentService.Create("Home", Constants.System.Root, Home.ModelTypeAlias);

        var saveHomeResult = _contentService.SaveAndPublish(homeNode);
        saveHomeResult.Success.Should().BeTrue();

        return homeNode;
    }

    public IContent EnsureRegisterPageCreated()
    {
        var homePage = EnsureHomePageCreated();

        // Locate the register node under home
        var foundRegisterNode = _contentService.GetByLevel(homePage.Level + 1)
            .FirstOrDefault(f => f.ParentId == homePage.ParentId && f.ContentType.Alias == Register.ModelTypeAlias);

        if (foundRegisterNode is not null)
            return foundRegisterNode;

        // Create a new register node under the home
        var registerNode = _contentService.Create("Register", homePage, Register.ModelTypeAlias);

        var saveRegisterResult = _contentService.SaveAndPublish(registerNode);
        saveRegisterResult.Success.Should().BeTrue();

        return registerNode;
    }

    public void EnsureNumberOfMembers(int numberOfMembers)
    {
        _memberService.Count().Should().Be(numberOfMembers);
    }
}