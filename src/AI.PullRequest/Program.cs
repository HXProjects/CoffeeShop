using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

Console.WriteLine("Starting the agent");
var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(
    modelId: "mathstral-7b-v0.1",
    serviceId: "mathstral-7b-v0.1", // Explicitly set serviceId
    endpoint: new Uri("http://localhost:1234/v1"),
    apiKey: "");

builder.Plugins.AddFromType<PullRequestPlugin>();

var kernel = builder.Build();
PromptExecutionSettings settings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };
var chat = kernel.GetRequiredService<IChatCompletionService>();
var pullRequestUrl = "https://github.com/HXProjects/CoffeeShop/pull/2";


var instructionA = @$"
 url is {pullRequestUrl}. Tell me what is Jira ticket number?.
";
var instructionB = @$"
You are a Pull request agent.
As the agent you perform tasks connected to pull requests on GitHub.
You need to know that on the project we have Jira tickets.
On project we use ticket abbreviation like 'PROJ-' followed by some numeric value, It is Jira ticket number.
step by step execute next tasks:
 Navigate to URL to get extracted branch name {{$pullRequestUrl}}. Save is as {{branch-name}}. From {{branch-name}} extract Jira ticket number.
 

 url is {pullRequestUrl}. Tell me what is Jira ticket number?
";
var handlebarsTemplate = """
<message role="system">
You are a Pull request agent.
As the agent you perform tasks connected to pull requests on GitHub.
You need to know that on the project we have Jira tickets.
On project we use ticket abbreviation like 'PROJ-' followed by some numeric value, It is Jira ticket number.
step by step execute next tasks:
 Navigate to URL to get extracted branch name {{$pullRequestUrl}}. Save is as {{branch-name}}. From {{branch-name}} extract Jira ticket number.
</message>
<message role="user">
{{instructionA}}
</message>
""";

var arguments = new KernelArguments() {
    {"instructionA", instructionA
    },
    {
        "pullRequestUrl", pullRequestUrl
    }
};
var executionSetting = new OpenAIPromptExecutionSettings
{
    Temperature = 0.1
};

var templateFactory = new HandlebarsPromptTemplateFactory();
var promptTemplateConfig = new PromptTemplateConfig()
{
    Template = handlebarsTemplate,
    TemplateFormat = "handlebars",
    Name = "PullRequestReviewTemplate",
    ExecutionSettings = new Dictionary<string, PromptExecutionSettings>
    {
        // Use the same modelId as in AddOpenAIChatCompletion
        { "mathstral-7b-v0.1", executionSetting }
    }
};
var promptTemplate = templateFactory.Create(promptTemplateConfig);
var renderedPrompt = await promptTemplate.RenderAsync(kernel, arguments);//

//var function = kernel.CreateFunctionFromPrompt(promptTemplateConfig, templateFactory);
//var response = await kernel.InvokeAsync(function, arguments);
//var history = new ChatHistory(instructionB);
//var result = await chat.GetChatMessageContentAsync(
//    history,
//    executionSetting
//);
var resultInvocation = await kernel.InvokePromptAsync(instructionB, new(settings));
//Console.WriteLine(response);
Console.WriteLine(resultInvocation);
Console.ReadLine();

