namespace AI.PullRequest.Prompts
{
  public static class BranchPrompts
  {
    public static readonly string CreateBranchPromptTemplate = @"
          Using appropriate function, navigate to URL {0} to get extracted branch name.
          Save is as 'branch-name'. 
          From 'branch-name' extract Jira ticket number.
          url is {0}. Tell me what is branch name?
        ";
          public static readonly string ExtractJiraTicketNameTemplate = @"
          Given branch name 'branch-name' which you defined is {0}, extract Jira ticket name.
          Save is as 'jira-ticket-name'. 
          Tell me what is jira ticket name?
        ";
    }
}