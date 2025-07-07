namespace AI.PullRequest.Prompts
{
    public static class BranchPrompts
    {
        // Give example how this const will be used
        // string prompt = string.Format(BranchPrompts.CreateBranchPromptTemplate, pullRequestUrl);
        public static readonly string CreateBranchPromptTemplate = @"
          Using available functions, navigate to URL {0} to get extracted branch name.
          Save is as 'branch-name'. 
          From 'branch-name' extract Jira ticket number.
          url is {0}. Tell me what is Jira ticket number?
        ";
    }
}