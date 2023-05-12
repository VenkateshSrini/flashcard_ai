using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechCards.Services.ServiceCollection;
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
     .ConfigureServices((hostBuilderContext, services) =>
     {
         services.AddChatServices(hostBuilderContext.Configuration["ConnectionString"],
             hostBuilderContext.Configuration["OpenAIKey"]);
         services.AddLogging();
     })
    .Build();

host.Run();
