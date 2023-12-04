using Microsoft.AspNetCore.Mvc.Razor;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSpecflow.Web.Views;

public abstract class BlockListItemView<TContent, TSettings> : RazorPage<BlockListItem<TContent, TSettings>>
    where TContent : IPublishedElement
    where TSettings : IPublishedElement
{
    public TContent Content => Model.Content;

    public TSettings Settings => Model.Settings;
}

public abstract class BlockListItemView<TContent> : BlockListItemView<TContent, IPublishedElement>
    where TContent : IPublishedElement
{
}