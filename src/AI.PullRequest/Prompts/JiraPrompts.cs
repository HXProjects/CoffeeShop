namespace AI.PullRequest.Prompts
{
    public static class JiraPrompts
    {
        public const string FindLinkedTestCases = @"Great, you have found the Jira ticket number, (you said {0}).
        Your new task is by using available function find linked test cases to this Jira ticket. 
        and wait for next instructions.";
    }
}