using Microsoft.SemanticKernel;
using AI.PullRequest.Prompts;
using Microsoft.SemanticKernel.ChatCompletion;
using AI.PullRequest.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AI.PullRequest.Plugins;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register configuration service and interface
        services.AddSingleton<IAppConfigurationService, AppConfigurationService>();
       // services.AddSingleton<JiraPlugins>((host) => new JiraPlugins(host.GetRequiredService<IAppConfigurationService>()));

    })
    .Build();
    
var config = host.Services.GetRequiredService<IAppConfigurationService>();
var jiraPlugin = new JiraPlugins(config);

//var file = jiraPlugin.FindJiraTicket("PROJ-2");
//Console.ReadLine();
Console.WriteLine("Starting the agent");
var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(
    modelId: "mathstral-7b-v0.1",
    serviceId: "mathstral-7b-v0.1",
    endpoint: new Uri("http://localhost:1234/v1"),
    apiKey: "");


builder.Plugins.AddFromType<PullRequestPlugin>();
//builder.Plugins.AddFromType<JiraPlugins>();
// Manually instantiate JiraPlugins with config and register as plugin instance
//var jiraPlugin = new JiraPlugins(config);
builder.Plugins.AddFromObject(jiraPlugin);
var kernel = builder.Build();

PromptExecutionSettings settings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
};

string pullRequestUrl = "https://github.com/HXProjects/CoffeeShop/pull/4";

var findBranchInstruction = string.Format(BranchPrompts.CreateBranchPromptTemplate, pullRequestUrl);

ChatHistory chatHistory = [];
chatHistory.AddSystemMessage(BasicPrompts.RoleDefinition);
chatHistory.AddSystemMessage(BasicPrompts.TaskDefinition);
chatHistory.AddUserMessage(findBranchInstruction);



//var executionSetting = new OpenAIPromptExecutionSettings
//{
//    Temperature = 0.1
//};
// Get the chat completion service from the kernel
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

var branchName = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel: kernel);

Console.WriteLine(branchName);
chatHistory.AddAssistantMessage(branchName.ToString());

var extractJiraTicketNamePrompt = string.Format(BranchPrompts.ExtractJiraTicketNameTemplate, branchName);
chatHistory.Add(new ChatMessageContent(AuthorRole.User, extractJiraTicketNamePrompt));
var jiraTicketName = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel: kernel);
Console.WriteLine(jiraTicketName);
chatHistory.Add(new ChatMessageContent(AuthorRole.Assistant, jiraTicketName.ToString()));

var linkedTestCasesPrompt = string.Format(JiraPrompts.FindLinkedTestCases, jiraTicketName);
chatHistory.Add(new ChatMessageContent(AuthorRole.User, linkedTestCasesPrompt));

var linkedTestCases = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel: kernel);
Console.WriteLine(linkedTestCases);

Console.ReadLine();

