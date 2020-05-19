using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvRandomizer.Models;

namespace CsvRandomizer
{
    public class CsvParser
    {
        public static List<EntryWeight> GetWeights(string path)
        {
            var weights = new List<EntryWeight>();
            using (var reader = new StreamReader(path))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.RegisterClassMap<EntryWeightClassMap>();
                var isHeader = true;
                while (csv.Read())
                {
                    if (isHeader)
                    {
                        csv.ReadHeader();
                        isHeader = false;
                        continue;
                    }

                    try
                    {
                        weights.Add(csv.GetRecord<EntryWeight>());
                    }
                    catch (Exception e)
                    {
                        Utilities.Log($"Error parsing EntryWeight - {e}", ConsoleColor.DarkRed);
                    }
                }
            }

            return weights;
        }

        public static List<EntryRecord> GetEntries(string path)
        {
            var records = new List<EntryRecord>();
            using (var reader = new StreamReader(path))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.RegisterClassMap<EntryRecordClassMap>();
                var isHeader = true;
                while (csv.Read())
                {
                    if (isHeader)
                    {
                        csv.ReadHeader();
                        isHeader = false;
                        continue;
                    }

                    try
                    {
                        records.Add(csv.GetRecord<EntryRecord>());
                    }
                    catch (Exception e)
                    {
                        Utilities.Log($"Error parsing EntryRecord - {e}", ConsoleColor.DarkRed);
                    }
                }
            }

            return records;
        }
    }
}