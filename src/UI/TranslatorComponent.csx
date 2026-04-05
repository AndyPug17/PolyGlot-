// Модуль 10: React компонент интерфейса переводчика
// Этот файл имитирует React компонент на C# для демонстрации

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolyGlot.UI
{
    /// <summary>
    /// Модуль 10: Компонент пользовательского интерфейса переводчика
    /// </summary>
    public class TranslatorUIComponent
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageDetectionService _detectionService;
        private readonly ITextToSpeechService _ttsService;
        
        public TranslatorUIComponent(
            ITranslationService translationService,
            ILanguageDetectionService detectionService,
            ITextToSpeechService ttsService)
        {
            _translationService = translationService;
            _detectionService = detectionService;
            _ttsService = ttsService;
        }
        
        public async Task<UIState> HandleTextInputAsync(string sourceText, string targetLang)
        {
            var state = new UIState { SourceText = sourceText, IsLoading = true };
            
            try
            {
                var translation = await _translationService.TranslateAsync(sourceText, targetLang);
                
                state.TranslatedText = translation.TranslatedText;
                state.DetectedLanguage = translation.SourceLanguage;
                state.Confidence = translation.Confidence;
                state.IsLoading = false;
                state.Success = true;
                
                // Добавление в историю
                AddToHistory(sourceText, translation.TranslatedText, targetLang);
            }
            catch (Exception ex)
            {
                state.Error = ex.Message;
                state.IsLoading = false;
                state.Success = false;
            }
            
            return state;
        }
        
        private void AddToHistory(string source, string target, string lang)
        {
            // Сохранение в историю
            Console.WriteLine($"[UI] Added to history: {source} -> {target}");
        }
    }
    
    public class UIState
    {
        public string SourceText { get; set; }
        public string TranslatedText { get; set; }
        public string DetectedLanguage { get; set; }
        public double Confidence { get; set; }
        public bool IsLoading { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<HistoryItem> History { get; set; } = new List<HistoryItem>();
        public UISettings Settings { get; set; } = new UISettings();
    }
    
    public class HistoryItem
    {
        public string SourceText { get; set; }
        public string TargetText { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public DateTime Timestamp { get; set; }
    }
    
    public class UISettings
    {
        public string Theme { get; set; } = "light";
        public bool AutoDetectLanguage { get; set; } = true;
        public bool EnableTTS { get; set; } = true;
        public int HistorySize { get; set; } = 50;
        public bool AutoCopy { get; set; } = false;
    }
}
