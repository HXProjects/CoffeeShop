using Microsoft.SemanticKernel;
using AI.PullRequest.Prompts;
using Microsoft.SemanticKernel.ChatCompletion;

Console.WriteLine("Starting the agent");
var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(
    modelId: "mathstral-7b-v0.1",
    serviceId: "mathstral-7b-v0.1",
    endpoint: new Uri("http://localhost:1234/v1"),
    apiKey: "");

builder.Plugins.AddFromType<PullRequestPlugin>();
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

var identifiedBranch = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel: kernel);

Console.WriteLine(identifiedBranch);
chatHistory.AddAssistantMessage(identifiedBranch.Content);

//var linkedTestCasesPrompt = string.Format(JiraPrompts.FindLinkedTestCases, identifiedBranch);
//chatHistory.Add(new ChatMessageContent(AuthorRole.User, linkedTestCasesPrompt));
//
//var linkedTestCases = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel: kernel);
//Console.WriteLine(linkedTestCases);

Console.ReadLine();

