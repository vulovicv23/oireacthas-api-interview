using System;
using System.ComponentModel;
using System.Threading.Tasks;
using OireachtasAPI.Services;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace OireachtasAPI.Commands
{
    public class FilterBillsByLastUpdatedCommand : BaseAsyncCommand<FilterBillsByLastUpdatedCommandSettings>
    {
        private readonly IFilterDataService _filterDataService;

        public FilterBillsByLastUpdatedCommand(IFilterDataService filterDataService)
        {
            _filterDataService = filterDataService;
        }

        public override async Task<int> ExecuteAsync(CommandContext context,
            FilterBillsByLastUpdatedCommandSettings settings)
        {
            Log.Debug("Received since date {sinceDate} and until date {untilDate}", settings.Since, settings.Until);

            var bills = await _filterDataService.FilterBillsByLastUpdated((DateTime)settings.Since, settings.Until);

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

        public override ValidationResult Validate(CommandContext context,
            FilterBillsByLastUpdatedCommandSettings settings)
        {
            if (settings.Since == null)
            {
                return ValidationResult.Error("Since date cannot be null. Please add it via -s or --since");
            }

            return base.Validate(context, settings);
        }
    }

    public sealed class FilterBillsByLastUpdatedCommandSettings : CommandSettings
    {
        [Description("Since date")]
        [CommandOption("-s|--since")]
        public DateTime? Since { get; set; }

        [Description("Until date")]
        [CommandOption("-u|--until")]
        public DateTime? Until { get; set; }
    }
}