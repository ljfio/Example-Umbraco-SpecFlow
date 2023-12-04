Feature: Membership

    Scenario: Creating a new member
        Given there are 0 members
        And the register page exists
        When I am on the register page
        And I create the following members
          | Username | Name | Email            | Password       |
          | anne     | Anne | anne@example.org | I_Love_Bob_23! |
          | bob      | Bob  | bob@example.com  | Umbraco4Life$  |
        Then there should be 2 members