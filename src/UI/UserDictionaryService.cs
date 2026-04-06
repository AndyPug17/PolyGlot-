using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyGlot.UI
{
    /// <summary>
    /// Модуль 5: Сервис пользовательских словарей
    /// </summary>
    public class UserDictionaryService : IUserDictionaryService
    {
        private readonly Dictionary<Guid, UserDictionary> _dictionaries;
        
        public UserDictionaryService()
        {
            _dictionaries = new Dictionary<Guid, UserDictionary>();
        }
        
        public async Task<UserDictionary> CreateDictionaryAsync(Guid userId, string name, string sourceLang, string targetLang)
        {
            var dictionary = new UserDictionary
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name,
                SourceLanguage = sourceLang,
                TargetLanguage = targetLang,
                Terms = new List<DictionaryTerm>(),
                CreatedAt = DateTime.UtcNow
            };
            
            _dictionaries[dictionary.Id] = dictionary;
            
            return await Task.FromResult(dictionary);
        }
        
        public async Task<bool> AddTermAsync(Guid dictionaryId, string sourceTerm, string targetTerm, string context = null)
        {
            if (!_dictionaries.ContainsKey(dictionaryId))
                return false;
            
            var term = new DictionaryTerm
            {
                Id = Guid.NewGuid(),
                SourceTerm = sourceTerm,
                TargetTerm = targetTerm,
                Context = context,
                UsageCount = 0,
                AddedAt = DateTime.UtcNow
            };
            
            _dictionaries[dictionaryId].Terms.Add(term);
            
            return await Task.FromResult(true);
        }
        
        public async Task<string> GetCustomTranslationAsync(Guid userId, string text, string sourceLang, string targetLang)
        {
            var dictionary = _dictionaries.Values
                .FirstOrDefault(d => d.UserId == userId && 
                                    d.SourceLanguage == sourceLang && 
                                    d.TargetLanguage == targetLang);
            
            if (dictionary == null)
                return null;
            
            var term = dictionary.Terms
                .FirstOrDefault(t => t.SourceTerm.Equals(text, StringComparison.OrdinalIgnoreCase));
            
            if (term != null)
            {
                term.UsageCount++;
                return term.TargetTerm;
            }
            
            return null;
        }
    }
    
    public interface IUserDictionaryService
    {
        Task<UserDictionary> CreateDictionaryAsync(Guid userId, string name, string sourceLang, string targetLang);
        Task<bool> AddTermAsync(Guid dictionaryId, string sourceTerm, string targetTerm, string context = null);
        Task<string> GetCustomTranslationAsync(Guid userId, string text, string sourceLang, string targetLang);
    }
    
    public class UserDictionary
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public List<DictionaryTerm> Terms { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class DictionaryTerm
    {
        public Guid Id { get; set; }
        public string SourceTerm { get; set; }
        public string TargetTerm { get; set; }
        public string Context { get; set; }
        public int UsageCount { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
