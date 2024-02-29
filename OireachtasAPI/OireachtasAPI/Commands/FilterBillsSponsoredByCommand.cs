using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using OireachtasAPI.Models;
using OireachtasAPI.Services;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace OireachtasAPI.Commands
{
    public sealed class FilterBillsSponsoredByCommand : BaseAsyncCommand<FilterBillsSponsoredByCommandSettings>
    {
        private readonly IFilterDataService _filterDataService;

        public FilterBillsSponsoredByCommand(IFilterDataService filterDataService)
        {
            _filterDataService = filterDataService;
        }

        public override async Task<int> ExecuteAsync(CommandContext context,
            FilterBillsSponsoredByCommandSettings settings)
        {
            Log.Debug("Received pId {pId}", settings.PId);

            var bills = await _filterDataService.FilterBillsSponsoredBy(settings.PId);

            if (bills.Count > 0)
            {
                PresentTable(bills);
            }
            else
            {
                AnsiConsole.Markup("[yellow]No bills found[/]");
            }

            return 0;
        }

        public override ValidationResult Validate(CommandContext context, FilterBillsSponsoredByCommandSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.PId))
            {
                return ValidationResult.Error("Argument pId is null or empty, add it by adding -p or --pId");
            }
            
            return base.Validate(context, settings);
        }
    }

    public sealed class FilterBillsSponsoredByCommandSettings : CommandSettings
    {
        [Description("PiD Of the user")]
        [CommandOption("-p|--pId")]
        public string PId { get; set; }
    }
}