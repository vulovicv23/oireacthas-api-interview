using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OireachtasAPI.Commands;
using OireachtasAPI.Services;
using OireachtasAPI.Services.LoadData;
using Serilog;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;

namespace OireachtasAPI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = CreateAndBuildServiceProvider();

            var app = new CommandApp(new DependencyInjectionRegistrar(serviceProvider));

            app.Configure(config =>
            {
                config.AddCommand<FilterBillsSponsoredByCommand>("filterBillsSponsoredBy");
                config.AddCommand<FilterBillsByLastUpdatedCommand>("filterBillsByLastUpdated");
            });

            await app.RunAsync(args);
        }

        private static IServiceCollection CreateAndBuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient<IHttpMeanService, HttpMeanService>();
            serviceCollection.AddScoped<ILocalFileMeanService, LocalFileMeanService>();
            serviceCollection.AddScoped<ILoadDataService, LoadDataService>(srv =>
                new LoadDataService(srv.GetRequiredService<IHttpMeanService>(),
                    srv.GetRequiredService<ILocalFileMeanService>(),
                    bool.Parse(ConfigurationManager.AppSettings["useLocalFiles"]),
                    ConfigurationManager.AppSettings["oireachtasApi"]));
            serviceCollection.AddScoped<IFilterDataService, FilterDataService>();

            serviceCollection.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(dispose: true); });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Debug.txt")
                .CreateLogger();

            return serviceCollection;
        }
    }
}