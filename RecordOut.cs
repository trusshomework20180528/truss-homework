using System;
using NodaTime;

namespace Truss
{
    public class RecordOut
    {
        public OffsetDateTime Timestamp { get; set; }
        public string Address { get; set; }
        public string ZIP { get; set; }
        public string FullName { get; set; }
        public double FooDuration { get; set; }
        public double BarDuration { get; set; }
        public double TotalDuration { get; set; }
        public string Notes { get; set; }
    }
}