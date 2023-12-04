using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io.Network;
using FluentAssertions;
using UmbracoSpecflow.Specs.Context;

namespace UmbracoSpecflow.Specs.Drivers;

public class AnglesharpWebDriver
{
    private readonly IBrowsingContext _browsingContext;
    private readonly Url _baseUrl;
    
    private IDocument _document;

    public AnglesharpWebDriver(IntegrationWebContext webContext)
    {
        var httpClient = webContext.Client;
        
        _baseUrl = new Url(httpClient.BaseAddress!.ToString());

        var requester = new HttpClientRequester(httpClient);

        var configuration = Configuration.Default
            .With(requester)
            .WithDefaultLoader();
        
        _browsingContext = BrowsingContext.New(configuration);
    }

    public async Task VisitPageAsync(string path)
    {
        var url = new Url(_baseUrl, path);
        _document = await _browsingContext.OpenAsync(url);
    }

    public void InputValue(string field, string value)
    {
        var form = _document.Forms.FirstOrDefault();
        
        form.Should().NotBeNull();

        var input = form!
            .QuerySelectorAll<IHtmlInputElement>("input")
            .FirstOrDefault(i => i.Name == field);
        
        input.Should().NotBeNull();

        input!.Value = value;
    }

    public async Task SubmitFormAsync()
    {
        var form = _document.Forms.FirstOrDefault();
        
        form.Should().NotBeNull();
        
        _document = await form!.SubmitAsync();
    }
}