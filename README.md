 PolyGlot - Интеллектуальная система перевода с автоматическим определением языка


📋 Оглавление
Описание проекта

Возможности программного продукта

Архитектура системы

Модули программного комплекса

Клиентская часть и пользовательский интерфейс

Технические требования

Установка и настройка

Использование

Справочное руководство

Примеры кода

Лицензия

🎯 Описание проекта
PolyGlot - это современная многофункциональная система автоматического перевода текстов, оснащенная интеллектуальным механизмом определения языка исходного текста из числа доступных в системе языковых пакетов. Проект разработан с использованием модульной архитектуры, что обеспечивает высокую гибкость, масштабируемость и возможность интеграции с различными сервисами перевода.

Ключевые особенности:
🔍 Автоматическое определение языка текста (без участия пользователя)

🌍 Поддержка 100+ языков через интеграцию с популярными API

⚡ Асинхронная обработка для максимальной производительности

💾 Кэширование результатов для оптимизации повторных запросов

🔐 Безопасное хранение ключей API и пользовательских данных

📱 Адаптивный интерфейс для всех типов устройств

✨ Возможности программного продукта
Базовый функционал:
Автоматическое определение языка входного текста

Перевод текста между 100+ языками

Поддержка массового перевода (пакетная обработка)

История переводов с возможностью поиска

Словарь терминов для пользовательских словарей

Транслитерация для неподдерживаемых языков

Озвучивание текста (Text-to-Speech)

Распознавание текста с изображений (OCR)

Синхронизация между устройствами

Экспорт переводов в различные форматы (TXT, DOCX, PDF)

Расширенные возможности:
📊 Статистика использования и аналитика

🔄 Автоматическое обновление языковых пакетов

🎨 Настройка тем оформления (светлая/темная)

⌨️ Горячие клавиши для быстрого доступа

🌐 Режим офлайн с базовыми языковыми пакетами
Модули программного комплекса
Модуль 1: Модуль аутентификации и управления пользователями (Authentication Module)
Назначение: Обеспечение безопасного доступа пользователей к системе, управление профилями и разграничение прав доступа.

Функциональные возможности:

Регистрация и авторизация пользователей (email/пароль, социальные сети)

Двухфакторная аутентификация (2FA)

Восстановление пароля через email/SMS

Управление сессиями и токенами доступа (JWT)

Хранение истории действий пользователя

Интерфейсы:

registerUser(UserData userData) -> AuthResponse

loginUser(credentials) -> SessionToken

logoutUser(sessionId) -> boolean

verify2FA(code, sessionId) -> boolean

resetPassword(email) -> boolean

Зависимости: Модуль БД, Модуль шифрования

Пример кода:

csharp
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IEmailService _emailService;
    
    public async Task<AuthResponse> RegisterUserAsync(UserRegistrationDto userDto)
    {
        // Валидация входных данных
        if (!IsValidEmail(userDto.Email))
            throw new ValidationException("Invalid email format");
        
        // Проверка существования пользователя
        if (await _userRepository.ExistsByEmailAsync(userDto.Email))
            throw new UserAlreadyExistsException("User already exists");
        
        // Хеширование пароля
        var hashedPassword = _passwordHasher.HashPassword(userDto.Password);
        
        // Создание пользователя
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            IsEmailVerified = false
        };
        
        // Сохранение в БД
        await _userRepository.CreateAsync(user);
        
        // Отправка подтверждения email
        var verificationToken = _tokenGenerator.GenerateEmailToken(user.Id);
        await _emailService.SendVerificationEmailAsync(user.Email, verificationToken);
        
        // Генерация JWT токена
        var accessToken = _tokenGenerator.GenerateAccessToken(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        
        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600,
            UserId = user.Id
        };
    }
}
Модуль 2: Модуль определения языка текста (Language Detection Module)
Назначение: Автоматическое определение языка исходного текста на основе статистических и нейросетевых алгоритмов.

Функциональные возможности:

Определение языка по n-граммам символов

Использование нейросетевого классификатора

Поддержка 150+ языков

Определение смешанных языков в тексте

Вычисление confidence score для каждого языка

Интерфейсы:

detectLanguage(text: string) -> LanguageResult

getAvailableLanguages() -> List<Language>

addCustomLanguage(languageModel) -> boolean

getLanguageConfidence(text, language) -> float

Зависимости: Модуль языковых моделей, Модуль машинного обучения

Пример кода:

python
class LanguageDetector:
    def __init__(self, model_path: str, ngram_size: int = 3):
        self.ngram_size = ngram_size
        self.model = self._load_model(model_path)
        self.language_profiles = self._load_language_profiles()
        self.cld3_detector = CLD3Detector()  # Google Compact Language Detector
        
    def detect_language(self, text: str, min_length: int = 50) -> LanguageDetectionResult:
        """
        Определяет язык текста с высокой точностью
        
        Args:
            text: Входной текст для анализа
            min_length: Минимальная длина для достоверного определения
            
        Returns:
            LanguageDetectionResult с информацией о языке и уверенностью
        """
        if len(text) < min_length:
            # Для коротких текстов используем более чувствительный метод
            return self._detect_short_text(text)
        
        # Очистка текста от спецсимволов
        cleaned_text = self._preprocess_text(text)
        
        # Извлечение n-грамм
        ngrams = self._extract_ngrams(cleaned_text, self.ngram_size)
        
        # Получение вероятностей от нейросетевой модели
        cld3_result = self.cld3_detector.find_language(text)
        
        # Сравнение с профилями языков
        scores = {}
        for lang_code, profile in self.language_profiles.items():
            score = self._calculate_ngram_similarity(ngrams, profile)
            scores[lang_code] = score
            
        # Комбинирование результатов
        final_result = self._combine_results(cld3_result, scores)
        
        return LanguageDetectionResult(
            language=final_result.language_code,
            language_name=self._get_language_name(final_result.language_code),
            confidence=final_result.confidence,
            alternative_languages=final_result.alternatives[:3]
        )
    
    def _detect_short_text(self, text: str) -> LanguageDetectionResult:
        """Специальный метод для коротких текстов (менее 50 символов)"""
        # Используем символьные триграммы и char-based CNN
        char_tensor = self._text_to_char_tensor(text)
        prediction = self.char_cnn_model.predict(char_tensor)
        return self._parse_prediction(prediction)
Модуль 3: Модуль перевода текста (Translation Module)
Назначение: Обеспечение высококачественного перевода текста между поддерживаемыми языками.

Функциональные возможности:

Интеграция с Google Translate API

Интеграция с DeepL API

Интеграция с Yandex Translate API

Интеграция с Microsoft Translator

Выбор оптимального API на основе качества и стоимости

Асинхронная обработка больших текстов

Интерфейсы:

translateText(text, sourceLang, targetLang) -> TranslationResult

translateBatch(texts, sourceLang, targetLang) -> List<TranslationResult>

getSupportedLanguagePairs() -> List<LanguagePair>

estimateCost(text, sourceLang, targetLang) -> CostEstimate

Зависимости: Модуль определения языка, Модуль кэширования

Пример кода:

java
@Service
public class TranslationService {
    @Autowired
    private List<TranslationProvider> providers;
    
    @Autowired
    private CacheManager cacheManager;
    
    @Autowired
    private LanguageDetector languageDetector;
    
    @Autowired
    private RateLimiter rateLimiter;
    
    public TranslationResult translate(String text, String targetLang, String sourceLang) 
            throws TranslationException {
        
        // Автоопределение исходного языка, если не указан
        if (sourceLang == null || sourceLang.isEmpty()) {
            LanguageDetectionResult detection = languageDetector.detectLanguage(text);
            sourceLang = detection.getLanguage();
            
            // Логируем определение языка
            Logger.info("Auto-detected source language: {} (confidence: {})", 
                       sourceLang, detection.getConfidence());
        }
        
        // Проверка кэша
        String cacheKey = generateCacheKey(text, sourceLang, targetLang);
        TranslationResult cachedResult = cacheManager.get(cacheKey);
        if (cachedResult != null) {
            Logger.debug("Cache hit for key: {}", cacheKey);
            return cachedResult;
        }
        
        // Rate limiting
        rateLimiter.acquire();
        
        // Выбор оптимального провайдера
        TranslationProvider bestProvider = selectBestProvider(sourceLang, targetLang);
        
        // Выполнение перевода с ретраями
        TranslationResult result = executeWithRetry(() -> 
            bestProvider.translate(text, sourceLang, targetLang)
        );
        
        // Сохранение в кэш
        cacheManager.put(cacheKey, result);
        
        // Асинхронное сохранение в историю
        saveToHistoryAsync(text, result, sourceLang, targetLang);
        
        return result;
    }
    
    private TranslationProvider selectBestProvider(String sourceLang, String targetLang) {
        return providers.stream()
            .filter(p -> p.supportsLanguagePair(sourceLang, targetLang))
            .max(Comparator.comparing(TranslationProvider::getQualityScore))
            .orElseThrow(() -> new NoProviderAvailableException());
    }
    
    @Retryable(maxAttempts = 3, backoff = @Backoff(delay = 1000))
    private TranslationResult executeWithRetry(Supplier<TranslationResult> translationSupplier) {
        return translationSupplier.get();
    }
}
Модуль 4: Модуль кэширования и оптимизации (Cache & Optimization Module)
Назначение: Повышение производительности системы за счет кэширования результатов переводов и оптимизации запросов.

Функциональные возможности:

In-memory кэширование (Redis/Memcached)

LRU (Least Recently Used) политика вытеснения

TTL (Time-To-Live) для устаревания кэша

Сжатие данных в кэше

Предзагрузка часто используемых фраз

Интерфейсы:

get(key: CacheKey) -> Optional<TranslationResult>

put(key: CacheKey, value: TranslationResult, ttl: Duration)

invalidate(key: CacheKey) -> boolean

getStats() -> CacheStatistics

preloadFrequentPhrases(userId) -> void

Зависимости: Модуль БД, Модуль аналитики

Пример кода:

go
package cache

import (
    "context"
    "time"
    "github.com/go-redis/redis/v8"
    "github.com/vmihailenco/msgpack/v5"
)

type TranslationCache struct {
    client    *redis.Client
    lru       *lru.Cache
    maxSize   int
    defaultTTL time.Duration
    metrics   *CacheMetrics
}

type CacheKey struct {
    Text       string
    SourceLang string
    TargetLang string
}

type CachedTranslation struct {
    TranslatedText string
    DetectedLang   string
    Timestamp      time.Time
    Provider       string
    Confidence     float64
}

func NewTranslationCache(redisAddr string, maxSize int) *TranslationCache {
    client := redis.NewClient(&redis.Options{
        Addr:         redisAddr,
        Password:     "",
        DB:           0,
        PoolSize:     100,
        MinIdleConns: 10,
    })
    
    lruCache, _ := lru.New(maxSize)
    
    return &TranslationCache{
        client:      client,
        lru:         lruCache,
        maxSize:     maxSize,
        defaultTTL:  24 * time.Hour,
        metrics:     &CacheMetrics{},
    }
}

func (c *TranslationCache) Get(ctx context.Context, key CacheKey) (*CachedTranslation, bool) {
    start := time.Now()
    defer func() {
        c.metrics.RecordLatency(time.Since(start))
    }()
    
    // Проверка LRU кэша (быстрее)
    if val, ok := c.lru.Get(key); ok {
        c.metrics.RecordHit("lru")
        return val.(*CachedTranslation), true
    }
    
    // Проверка Redis (распределенный кэш)
    cacheKey := c.generateRedisKey(key)
    data, err := c.client.Get(ctx, cacheKey).Bytes()
    
    if err == redis.Nil {
        c.metrics.RecordMiss("redis")
        return nil, false
    } else if err != nil {
        Logger.Error("Redis get error: %v", err)
        c.metrics.RecordError()
        return nil, false
    }
    
    // Десериализация
    var result CachedTranslation
    if err := msgpack.Unmarshal(data, &result); err != nil {
        Logger.Error("Deserialization error: %v", err)
        return nil, false
    }
    
    // Сохранение в LRU для быстрого доступа
    c.lru.Add(key, &result)
    c.metrics.RecordHit("redis")
    
    return &result, true
}

func (c *TranslationCache) Put(ctx context.Context, key CacheKey, value *CachedTranslation, ttl time.Duration) error {
    if ttl == 0 {
        ttl = c.defaultTTL
    }
    
    // Сохранение в LRU
    c.lru.Add(key, value)
    
    // Сохранение в Redis
    data, err := msgpack.Marshal(value)
    if err != nil {
        return fmt.Errorf("serialization error: %w", err)
    }
    
    cacheKey := c.generateRedisKey(key)
    err = c.client.Set(ctx, cacheKey, data, ttl).Err()
    if err != nil {
        return fmt.Errorf("redis set error: %w", err)
    }
    
    c.metrics.RecordWrite()
    return nil
}
Модуль 5: Модуль управления пользовательскими словарями (User Dictionary Module)
Назначение: Предоставление пользователям возможности создавать и использовать собственные словари терминов и специализированной лексики.

Функциональные возможности:

Создание/редактирование/удаление пользовательских словарей

Импорт/экспорт словарей в CSV/JSON форматах

Приоритизация переводов из пользовательских словарей

Совместное использование словарей между пользователями

Автоматическое предложение добавления терминов

Интерфейсы:

createDictionary(name, language) -> Dictionary

addTerm(dictionaryId, sourceTerm, targetTerm, context) -> boolean

getCustomTranslation(text, sourceLang, targetLang) -> Optional<String>

mergeDictionaries(sourceDictId, targetDictId) -> Dictionary

exportDictionary(dictionaryId, format) -> File

Зависимости: Модуль БД, Модуль перевода

Пример кода:

typescript
class UserDictionaryService {
    private dictionaryRepository: DictionaryRepository;
    private translationService: TranslationService;
    private cacheService: CacheService;
    
    async translateWithDictionary(
        text: string, 
        sourceLang: string, 
        targetLang: string,
        userId: string
    ): Promise<TranslationResult> {
        // Разбиваем текст на слова и фразы
        const tokens = this.tokenizeText(text);
        let translatedTokens: string[] = [];
        
        for (const token of tokens) {
            // Проверяем в пользовательских словарях
            const customTranslation = await this.findInUserDictionaries(
                token, sourceLang, targetLang, userId
            );
            
            if (customTranslation) {
                translatedTokens.push(customTranslation);
                continue;
            }
            
            // Проверяем в глобальных словарях
            const globalTranslation = await this.findInGlobalDictionaries(
                token, sourceLang, targetLang
            );
            
            if (globalTranslation) {
                translatedTokens.push(globalTranslation);
                continue;
            }
            
            // Используем стандартный перевод
            const standardTranslation = await this.translationService.translate(
                token, sourceLang, targetLang
            );
            translatedTokens.push(standardTranslation.getText());
        }
        
        return new TranslationResult(translatedTokens.join(' '));
    }
    
    private async findInUserDictionaries(
        term: string, 
        sourceLang: string, 
        targetLang: string,
        userId: string
    ): Promise<string | null> {
        const cacheKey = `dict:${userId}:${sourceLang}:${targetLang}:${term}`;
        const cached = await this.cacheService.get(cacheKey);
        
        if (cached) {
            return cached as string;
        }
        
        const dictionaries = await this.dictionaryRepository.findByUser(userId);
        
        for (const dict of dictionaries) {
            if (dict.sourceLang === sourceLang && dict.targetLang === targetLang) {
                const term = await this.dictionaryRepository.findTerm(dict.id, term);
                if (term && term.priority > 0) {
                    await this.cacheService.set(cacheKey, term.translation, 3600);
                    return term.translation;
                }
            }
        }
        
        return null;
    }
    
    async suggestTerm(
        sourceText: string,
        suggestedTranslation: string,
        context: string,
        userId: string
    ): Promise<void> {
        // Анализ частоты использования термина
        const frequency = await this.analyzeTermFrequency(sourceText, userId);
        
        if (frequency > 5) { // Часто используемый термин
            const suggestion = new TermSuggestion({
                sourceTerm: sourceText,
                suggestedTranslation: suggestedTranslation,
                context: context,
                userId: userId,
                frequency: frequency,
                timestamp: new Date()
            });
            
            await this.suggestionRepository.save(suggestion);
            await this.notifyModerators(suggestion);
        }
    }
}
