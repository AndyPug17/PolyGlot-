using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyGlot.Analytics
{
    /// <summary>
    /// Модуль 8: Сервис аналитики и отчетности
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        private readonly List<TranslationEvent> _events;
        
        public AnalyticsService()
        {
            _events = new List<TranslationEvent>();
        }
        
        public async Task RecordTranslationEventAsync(TranslationEventData eventData)
        {
            var evt = new TranslationEvent
            {
                Id = Guid.NewGuid(),
                UserId = eventData.UserId,
                SourceLanguage = eventData.SourceLanguage,
                TargetLanguage = eventData.TargetLanguage,
                TextLength = eventData.TextLength,
                ProcessingTimeMs = eventData.ProcessingTimeMs,
                Success = eventData.Success,
                Timestamp = DateTime.UtcNow
            };
            
            _events.Add(evt);
            await Task.CompletedTask;
        }
        
        public async Task<DashboardData> GetDashboardDataAsync(DateTime startDate, DateTime endDate)
        {
            var filteredEvents = _events.Where(e => e.Timestamp >= startDate && e.Timestamp <= endDate).ToList();
            
            var data = new DashboardData
            {
                TotalTranslations = filteredEvents.Count,
                UniqueUsers = filteredEvents.Select(e => e.UserId).Distinct().Count(),
                PopularLanguagePairs = GetPopularLanguagePairs(filteredEvents),
                AverageResponseTime = filteredEvents.Average(e => e.ProcessingTimeMs),
                SuccessRate = filteredEvents.Count(e => e.Success) / (double)filteredEvents.Count,
                PeriodStart = startDate,
                PeriodEnd = endDate
            };
            
            return await Task.FromResult(data);
        }
        
        public async Task<Report> GenerateReportAsync(ReportRequest request)
        {
            var data = await GetDashboardDataAsync(request.StartDate, request.EndDate);
            
            var report = new Report
            {
                Title = $"Translation Report {request.StartDate:yyyy-MM-dd} to {request.EndDate:yyyy-MM-dd}",
                GeneratedAt = DateTime.UtcNow,
                DashboardData = data,
                Format = request.Format
            };
            
            return report;
        }
        
        private Dictionary<string, int> GetPopularLanguagePairs(List<TranslationEvent> events)
        {
            return events
                .GroupBy(e => $"{e.SourceLanguage}->{e.TargetLanguage}")
                .ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(kv => kv.Value)
                .Take(10)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
    
    public interface IAnalyticsService
    {
        Task RecordTranslationEventAsync(TranslationEventData eventData);
        Task<DashboardData> GetDashboardDataAsync(DateTime startDate, DateTime endDate);
        Task<Report> GenerateReportAsync(ReportRequest request);
    }
    
    public class TranslationEventData
    {
        public Guid UserId { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public int TextLength { get; set; }
        public long ProcessingTimeMs { get; set; }
        public bool Success { get; set; }
    }
    
    public class TranslationEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public int TextLength { get; set; }
        public long ProcessingTimeMs { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }
    
    public class DashboardData
    {
        public int TotalTranslations { get; set; }
        public int UniqueUsers { get; set; }
        public Dictionary<string, int> PopularLanguagePairs { get; set; }
        public double AverageResponseTime { get; set; }
        public double SuccessRate { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
    
    public class ReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Format { get; set; } = "PDF";
    }
    
    public class Report
    {
        public string Title { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DashboardData DashboardData { get; set; }
        public string Format { get; set; }
    }
}
