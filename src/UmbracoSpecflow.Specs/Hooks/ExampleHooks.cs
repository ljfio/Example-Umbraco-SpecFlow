using TechTalk.SpecFlow;

namespace UmbracoSpecflow.Specs.Hooks;

[Binding]
public class ExampleHooks
{
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
    }
    
    [BeforeFeature]
    [Scope(Feature = "Membership")]
    public static void BeforeMembershipFeature()
    {
    }
    
    [AfterFeature]
    [Scope(Feature = "Membership")]
    public static void AfterMembershipFeature()
    {
    }

    [BeforeScenario]
    [Scope(Feature = "Membership", Scenario = "Creating a new member")]
    public void BeforeCreatingANewMemberScenario()
    {
    }
    
    [AfterScenario]
    [Scope(Feature = "Membership", Scenario = "Creating a new member")]
    public void AfterCreatingANewMemberScenario()
    {
    }
}