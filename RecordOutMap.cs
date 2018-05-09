using CsvHelper.Configuration;

namespace Truss
{
    public class RecordOutMap : ClassMap<RecordOut>
    {
        public RecordOutMap()
        {
            // format as ISO-8601
            Map(m => m.Timestamp).TypeConverterOption.Format("o");

            Map(m => m.Address);
            Map(m => m.ZIP);
            Map(m => m.FullName);
            Map(m => m.FooDuration);
            Map(m => m.BarDuration);
            Map(m => m.TotalDuration);
            Map(m => m.Notes);
        }
    }
}