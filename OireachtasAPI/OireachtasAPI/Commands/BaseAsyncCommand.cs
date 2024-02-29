using System.Collections.Generic;
using OireachtasAPI.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace OireachtasAPI.Commands
{
    public abstract class BaseAsyncCommand<TCommand> : AsyncCommand<TCommand> where TCommand : CommandSettings
    {
        protected void PresentTable(List<Bill> list)
        {
            var table = new Table();
            table.AddColumn("Bill Number");
            table.AddColumn("Bill Year");
            table.AddColumn("Bill Type");
            table.AddColumn("Last Updated");

            foreach (var l in list)
            {
                table.AddRow(l.BillNo, l.BillYear, l.BillType, l.LastUpdated.ToString());
            }

            AnsiConsole.Write(table);
        }
    }
}