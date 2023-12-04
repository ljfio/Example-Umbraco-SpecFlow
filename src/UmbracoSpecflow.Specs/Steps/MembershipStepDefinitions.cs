using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UmbracoSpecflow.Specs.Drivers;

namespace UmbracoSpecflow.Specs.Steps;

[Binding]
public sealed class MembershipStepDefinitions
{
    private readonly AnglesharpWebDriver _webDriver;
    private readonly UmbracoDriver _umbracoDriver;
    
    public MembershipStepDefinitions(
        AnglesharpWebDriver webDriver,
        UmbracoDriver umbracoDriver)
    {
        _webDriver = webDriver;
        _umbracoDriver = umbracoDriver;
    }

    [Given(@"the register page exists")]
    public void GivenTheRegisterPageExists()
    {
        _umbracoDriver.EnsureRegisterPageCreated();
    }
    
    [Given(@"there are ([0-9]+) members")]
    [Then(@"there should be ([0-9]+) members")]
    public void ThereShouldBeNMembers(int numberOfMembers)
    {
        _umbracoDriver.EnsureNumberOfMembers(numberOfMembers);
    }

    [When(@"I am on the register page")]
    public async Task WhenIAmOnTheRegisterPage()
    {
        await _webDriver.VisitPageAsync("/register/");
    }

    [When(@"I create the following members")]
    public async Task WhenICreateTheFollowingMembers(Table table)
    {
        foreach (var row in table.Rows)
        {
            _webDriver.InputValue("RegisterModel.Name", row["Name"]);
            _webDriver.InputValue("RegisterModel.Email", row["Email"]);
            _webDriver.InputValue("RegisterModel.Password", row["Password"]);
            _webDriver.InputValue("RegisterModel.ConfirmPassword", row["Password"]);

            await _webDriver.SubmitFormAsync();
        }
    }
}