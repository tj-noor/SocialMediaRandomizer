using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvRandomizer.Models;
using McMaster.Extensions.CommandLineUtils;

namespace CsvRandomizer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Set Up App Info
            var app = new CommandLineApplication();
            app.Name = "Social Media Randomizer - CSV";
            app.Description = "This tool will pick a random entry from a csv file.";
            app.HelpOption("-?|-h|--help");

            // Set up Options/Parameters
            CommandOption entriesOption = app.Option<string>("-e|--entries <filePath>", "This is the path where the CSV file with the entries are located.",
                CommandOptionType.SingleValue);

            CommandOption configOption = app.Option<string>("-c|--config <filePath>", "This is the path where the CSV file with the configuration for entry types & weights are located.",
                CommandOptionType.SingleValue);

            CommandOption shuffleCountOption = app.Option<int>("-s|--shuffle <count>", "This is number of times times the list will be shuffled before an entry is chosen. Default is 10.",
                CommandOptionType.SingleValue);

            // Set up what happens when the App Runs
            app.OnExecuteAsync(async cancellationToken =>
            {
                // Check for Options/Parameters
                if (!entriesOption.HasValue() || !File.Exists(entriesOption.Value()))
                {
                    Utilities.Log("❌ Please provide a valid file for the list of entries.", ConsoleColor.Red);
                    return 400;
                }

                if (!configOption.HasValue() || !File.Exists(configOption.Value()))
                {
                    Utilities.Log("❌ Please provide a valid file for your configuration.", ConsoleColor.Red);
                    return 400;
                }

                int shuffleCount = shuffleCountOption.HasValue() ? Convert.ToInt32(shuffleCountOption.Value()) : 10;

                // Get Config & Data from CSV Files
                List<EntryWeight> config = CsvParser.GetWeights(configOption.Value());
                List<EntryRecord> entries = CsvParser.GetEntries(entriesOption.Value());

                // Apply Weight to Entries
                var weightedEntries = new List<EntryRecord>();

                foreach (EntryRecord entry in entries)
                {
                    int? multiplier = config.FirstOrDefault(c => c.Id == entry.EntryTypeId)?.NumberOfEntries;

                    if (multiplier == null)
                    {
                        Utilities.Log($"❌ No Config Entry Found for Id: {entry.EntryTypeId}", ConsoleColor.Red);
                        Utilities.LogWithKeyInput("Enter any key to exit ...", ConsoleColor.Gray);
                        return 400;
                    }

                    weightedEntries.AddRange(Enumerable.Range(0, multiplier.Value).Select(e => entry).ToList());
                }

                // Shuffle Entries
                Utilities.Log($"Shuffling {weightedEntries.Count} Entries\n", ConsoleColor.DarkGreen);

                for (int i = 0; i < shuffleCount; i++)
                {
                    weightedEntries = weightedEntries.Shuffle().ToList();
                }

                // Pick Random Entry
                EntryRecord winner = weightedEntries.PickRandom(1).Single();
                EntryWeight winnerSource = config.First(c => c.Id == winner.EntryTypeId);
                
                Utilities.Log("--------------------------------------------------", ConsoleColor.DarkCyan);
                Utilities.Log(">>                 Winner Found                 <<", ConsoleColor.DarkCyan);
                Utilities.Log($">>{" ",46}<<", ConsoleColor.DarkCyan);
                Utilities.Log($">>  Winner       : {winner.Participant,-28} <<", ConsoleColor.DarkCyan);
                Utilities.Log($">>  Entry Source : {winnerSource.Source,-28} <<", ConsoleColor.DarkCyan);
                Utilities.Log($">>  Entry Method : {winnerSource.TypeOfEntry,-28} <<", ConsoleColor.DarkCyan);
                Utilities.Log($">>{" ",46}<<", ConsoleColor.DarkCyan);
                Utilities.Log("--------------------------------------------------\n", ConsoleColor.DarkCyan);

                return 200;
            });

            // Run the App
            try
            {
                Utilities.Log(">> Initiating the Randomizer\n", ConsoleColor.DarkGreen);
                await app.ExecuteAsync(args);
                Utilities.LogWithKeyInput("Enter any key to exit ...", ConsoleColor.Gray);
            }
            catch (CommandParsingException ex)
            {
                Utilities.Log($"❌ An error has occurred with your command line arguments: {ex.Message}", ConsoleColor.Red);
                Utilities.LogWithKeyInput("Enter any key to exit ...", ConsoleColor.Gray);
            }
            catch (Exception ex)
            {
                Utilities.Log($"❌ An exception was thrown: {ex.Message}", ConsoleColor.Red);
                Utilities.LogWithKeyInput("Enter any key to exit ...", ConsoleColor.Gray);
            }
        }
    }
}
