namespace AI.PullRequest.Prompts
{
    public static class JiraPrompts
    {
        public const string FindLinkedTestCases = @"Previous result, you found Jira ticket number {0}.
 Now find all test cases linked to this Jira ticket.";
    }
}