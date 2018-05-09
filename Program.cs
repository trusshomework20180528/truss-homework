using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.TypeConversion;
using NodaTime;

namespace Truss
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var recordsIn = ReadRecords();
            var recordsOut = ProcessRecords(recordsIn);
            WriteRecords(recordsOut);
        }

        public static List<RecordIn> ReadRecords()
        {
            var result = new List<RecordIn>();

            using (var csv = new CsvReader(Console.In))
            {
                while (csv.Read())
                {
                    try
                    {
                        var recordIn = csv.GetRecord<RecordIn>();
                        result.Add(recordIn);
                    }
                    catch (Exception ex)
                    {
                        // write to stderr and drop row if replacement makes data invalid
                        Console.Error.WriteLine($"Unable to process record: {ex.Message}");
                    }
                }
            }

            return result;
        }

        private static List<RecordOut> ProcessRecords(List<RecordIn> records)
        {
            var result = new List<RecordOut>();

            var pacific = DateTimeZoneProviders.Tzdb["US/Pacific"];
            var eastern = DateTimeZoneProviders.Tzdb["US/Eastern"];

            foreach (var recordIn in records)
            {
                // convert US/Pacific time to US/Eastern time
                var localDateTime = LocalDateTime.FromDateTime(recordIn.Timestamp);
                var pacificDateTime = pacific.AtStrictly(localDateTime);
                var easternDateTime = pacificDateTime.WithZone(eastern);

                var fooSplit = recordIn.FooDuration.Split(":");
                var fooSplitSeconds = fooSplit[2].Split(".");
                var fooDuration = new TimeSpan(0, int.Parse(fooSplit[0]), int.Parse(fooSplit[1]), int.Parse(fooSplitSeconds[0]), int.Parse(fooSplitSeconds[1]));

                var barSplit = recordIn.BarDuration.Split(":");
                var barSplitSeconds = barSplit[2].Split(".");
                var barDuration = new TimeSpan(0, int.Parse(fooSplit[1]), int.Parse(barSplit[1]), int.Parse(barSplitSeconds[0]), int.Parse(barSplitSeconds[1]));

                var recordOut = new RecordOut
                {
                    Timestamp = easternDateTime.ToOffsetDateTime(),

                    // pass through address
                    Address = recordIn.Address,

                    // format zip code as 5 digits
                    ZIP = recordIn.ZIP.PadLeft(5, '0'),

                    // convert all names to uppercase
                    FullName = recordIn.FullName.ToUpperInvariant(),

                    // convert to floating point seconds
                    FooDuration = fooDuration.TotalSeconds,
                    BarDuration = barDuration.TotalSeconds,

                    Notes = recordIn.Notes
                };

                recordOut.TotalDuration = recordOut.FooDuration + recordOut.BarDuration;

                result.Add(recordOut);
            }

            return result;
        }

        private static void WriteRecords(List<RecordOut> records)
        {
            using (var csv = new CsvWriter(Console.Out))
            {
                csv.Configuration.RegisterClassMap<RecordOutMap>();
                csv.WriteRecords(records);
            }            
        }
    }
}