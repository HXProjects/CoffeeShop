using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using Microsoft.SemanticKernel;
using AI.PullRequest.Services;
using AI.PullRequest.Models;
using System.Text.RegularExpressions;

namespace AI.PullRequest.Plugins
{
    public class JiraPlugins
    {
        private readonly IAppConfigurationService _configService;
        public JiraPlugins(IAppConfigurationService configService)
        {
            _configService = configService;
        }

        [KernelFunction("find_jira_ticket_by_name")]
        [Description("Finds the Jira ticket by specified given Jira ticket name.Should be ignored if Jira Ticket name does not look like Jira ticket name.")]
        [return: Description("Jira ticket model.")]
        public JiraTicket FindJiraTicket(string jiraTicketName)
        {
            var directoryPath = _configService.JiraTicketsDirectory;
            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(jiraTicketName))
                throw new ArgumentException("Directory path and Jira project key must be provided.");

            var pattern = $"{jiraTicketName}.md";
            var files = Directory.EnumerateFiles(directoryPath, pattern, SearchOption.AllDirectories);

            var file = files.FirstOrDefault();
            if (file == null)
                throw new FileNotFoundException("Jira ticket file not found.", pattern);

            var jiraContent = File.ReadAllText(file);

            return new JiraTicket
            {
                Id = jiraTicketName,
                Name = ExtractJiraTicketKey(jiraContent) ?? jiraTicketName,
                Description = ExtractJiraTicketDescription(jiraContent),
                Summary = ExtractJiraTicketSummary(jiraContent),
                LinkedTestCases = ExtractLinkedTestCases(jiraContent),
            };
        }

        private string ExtractJiraTicketKey(string jiraContent)
        {
            var pattern = @"#\s*([A-Z]+-\d+)";
            var match = Regex.Match(jiraContent, pattern);
            return match.Success ? match.Groups[1].Value : "";
        }

        private string ExtractJiraTicketSummary(string jiraContent)
        {
            var pattern = @"\*\*Summary:\*\*\s*(.+)";
            var match = Regex.Match(jiraContent, pattern, RegexOptions.Multiline);
            return match.Success ? match.Groups[1].Value : "";
        }

        private string ExtractJiraTicketDescription(string jiraContent)
        {
            var pattern = @"\*\*Description:\*\*\s*(.+)";
            var match = Regex.Match(jiraContent, pattern, RegexOptions.Multiline);
            return match.Success ? match.Groups[1].Value : "";
        }
        private List<string> ExtractLinkedTestCases(string jiraContent)
        {
            var testCases = new List<string>();
            var pattern = @"\*\*Linked Test Cases:\*\*\s*((?:\r?\n-\s*[^\r\n]+)+)";
            var caseMatchesPattern = @"-\s*([^\r\n]+)";
            var match = Regex.Match(jiraContent, pattern, RegexOptions.Multiline);
            if (match.Success)
            {
                var casesBlock = match.Groups[1].Value;
                foreach (Match caseMatch in Regex.Matches(casesBlock, caseMatchesPattern))
                {
                    testCases.Add(caseMatch.Groups[1].Value.Trim());
                }
            }
            return testCases;
        }
    }
}