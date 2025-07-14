namespace AI.PullRequest.Prompts
{
  public static class BranchPrompts
  {
    public static readonly string CreateBranchPromptTemplate = @"
          Using appropriate function, navigate to URL {0} to get extracted branch name.
          Save is as 'branch-name'. 
          From 'branch-name' extract Jira ticket number.
          url is {0}. Tell me what is branch name and wait for next instructions.
        ";
          public static readonly string ExtractJiraTicketNameTemplate = @"
          Given branch name 'branch-name' which you defined in previous steps (Your answer was:' {0}'), extract Jira ticket name.
          There is no function for that, because you have your ability to extract jira ticket number from branch name without any help, but using your knowledge about how Jira-number is formed.
          Save is as 'jira-ticket-name'. 
          As response, tell me what is jira ticket name and wait for next instructions.
        ";
    }
}