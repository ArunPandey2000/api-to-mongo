namespace test.Models
{
    
    public class FrontEndErrorsResponse
    {
        public FEData data { get; set; }
        public List<Error> errors { get; set; }
    }

    public class FEData
    {
        public FEActor actor { get; set; }
    }

    public class FEActor
    {
        public FEAccount account { get; set; }
    }

    public class FEAccount
    {
        public FENrql nrql { get; set; }
    }

    public class FENrql
    {
        public string embeddedChartUrl { get; set; }
        public string nrql { get; set; }
        public object otherResult { get; set; }
        public RawResponse rawResponse { get; set; }
        public string staticChartUrl { get; set; }
        public object totalResult { get; set; }
    }

    public class RawResponse
    {
        public Metadata metadata { get; set; }
        public PerformanceStats performanceStats { get; set; }
        public QueryUsage queryUsage { get; set; }
        public List<Result> results { get; set; }
    }

    public class Metadata
    {
        public List<int> accounts { get; set; }
        public string beginTime { get; set; }
        public long beginTimeMillis { get; set; }
        public List<Column> contents { get; set; }
        public string endTime { get; set; }
        public long endTimeMillis { get; set; }
        public string eventType { get; set; }
        public List<string> eventTypes { get; set; }
        public string guid { get; set; }
        public List<string> messages { get; set; }
        public bool openEnded { get; set; }
        public string rawCompareWith { get; set; }
        public string rawSince { get; set; }
        public string rawUntil { get; set; }
        public string routerGuid { get; set; }
    }

    public class Column
    {
        public List<string> columns { get; set; }
        public string function { get; set; }
        public int limit { get; set; }
        public Order order { get; set; }
    }

    public class Order
    {
        public string column { get; set; }
        public bool descending { get; set; }
    }

    public class PerformanceStats
    {
        public bool exceedsRetentionWindow { get; set; }
        public int inspectedCount { get; set; }
        public int matchCount { get; set; }
        public int omittedCount { get; set; }
        public int wallClockTime { get; set; }
    }

    public class QueryUsage
    {
        public string billableMetric { get; set; }
        public double ccu { get; set; }
        public int inspectedCount { get; set; }
        public int scannedEvents { get; set; }
    }

    public class FEResult
    {
        public List<Event> events { get; set; }
    }

    public class Event
    {
        public string BlockedURI { get; set; }
        public string CSPContext { get; set; }
        public long ColumnNumber { get; set; }
        public string HashPath { get; set; }
        public long LineNumber { get; set; }
        public string Location { get; set; }
        public string Sample { get; set; }
        public string SourceFile { get; set; }
        public string UserName { get; set; }
        public string ViolatedDirective { get; set; }
        public long timestamp { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public List<string> path { get; set; }
        public Extensions extensions { get; set; }
        public List<Location> locations { get; set; }
    }

    public class Extensions
    {
        public string errorClass { get; set; }
        public string errorCode { get; set; }
    }

    public class Location
    {
        public int line { get; set; }
        public int column { get; set; }
    }

}
