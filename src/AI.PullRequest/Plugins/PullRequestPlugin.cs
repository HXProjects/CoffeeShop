using System.ComponentModel;
using Microsoft.SemanticKernel;
using System.Text.RegularExpressions;
public class PullRequestPlugin
{
    [KernelFunction("navigate_url_to_extract_branch_name")]
    [Description("Navigates to url and extracts the branch name from a pull request")]
    [return: Description("branch name")]
    public string GetBranchNameFromPullRequest(string pullRequestUrl)
    {
        var result = "none"; // Default return value if no description is found


        if (string.IsNullOrEmpty(pullRequestUrl))
        {
            return result;
        }
        var httpClient = new System.Net.Http.HttpClient();
        try
        {
            // Example logic to fetch pull request description from a URL
            var response = httpClient.GetStringAsync(pullRequestUrl).Result;
            // Assuming the response contains the description in a specific format
            string pattern = @"feature/PROJ-\d+_[\w-]+";
            var matches = Regex.Match(response, pattern);
            if (!matches.Success)
            {
                return result; // Return 'none' if no match is found
            }
            return matches.Value;
        } // Return the matched branch name
        catch (Exception)
        {
            return "none"; // Return 'none' if there's an error fetching the description
        }
    }
}