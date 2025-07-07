using System.Collections.Generic;

namespace AI.PullRequest.Models
{
    public class JiraTicket
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<string> LinkedTestCases { get; set; } = new List<string>();
    }
}