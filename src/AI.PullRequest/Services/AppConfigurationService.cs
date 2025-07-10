using Microsoft.Extensions.Configuration;

namespace AI.PullRequest.Services
{
    public interface IAppConfigurationService
    {
        string? JiraTicketsDirectory { get; }
    }
    
    public class AppConfigurationService : IAppConfigurationService
    {
        private readonly IConfigurationRoot _configuration;

        public AppConfigurationService()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string? JiraTicketsDirectory =>
            _configuration["JiraTicketsDirectory"];
    }
}