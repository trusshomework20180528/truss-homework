using System;

namespace Truss
{
    public class RecordIn
    {
        public DateTime Timestamp { get; set; }
        public string Address { get; set; }
        public string ZIP { get; set; }
        public string FullName { get; set; }
        public string FooDuration { get; set; }
        public string BarDuration { get; set; }
        public string Notes { get; set; }
    }
}