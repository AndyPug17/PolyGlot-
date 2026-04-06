<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>polyglot-documentation</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            line-height: 1.6;
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            color: #333;
        }
        h1, h2, h3 { color: #2c3e50; }
        table { border-collapse: collapse; width: 100%; margin: 20px 0; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; }
        pre { background: #f5f5f5; padding: 15px; overflow-x: auto; }
        code { background: #f5f5f5; padding: 2px 5px; }
    </style>
</head>
<body>
<h1 id="core-services">Ядро системы</h1>
<p>Часть описывает основные сервисы системы перевода.</p>
<h2 id="authentication-module">Модуль 1: Аутентификация и управление
пользователями</h2>
<h3 id="auth-overview">Обзор</h3>
<p>Модуль обеспечивает безопасный доступ к системе, регистрацию,
авторизацию и управление сессиями пользователей.</p>
<div class="note">
<p>Для работы модуля требуется наличие JWT секрета в конфигурационном
файле.</p>
</div>
<h3 id="auth-classes">Классы и интерфейсы</h3>
AuthenticationService
public class AuthenticationService : IAuthenticationService
public async Task&lt;AuthResult&gt;
RegisterUserAsync
UserRegistrationDto
dto
public async Task&lt;AuthResult&gt;
LoginAsync
LoginCredentials
credentials
<h3 id="auth-examples">Примеры использования</h3>
<div class="sourceCode" id="cb1"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb1-1"><a href="#cb1-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> service <span class="op">=</span> <span class="kw">new</span> <span class="fu">AuthenticationService</span><span class="op">();</span></span>
<span id="cb1-2"><a href="#cb1-2" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> result <span class="op">=</span> await service<span class="op">.</span><span class="fu">RegisterUserAsync</span><span class="op">(</span><span class="kw">new</span> UserRegistrationDto </span>
<span id="cb1-3"><a href="#cb1-3" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb1-4"><a href="#cb1-4" aria-hidden="true" tabindex="-1"></a>    Email <span class="op">=</span> <span class="st">&quot;<a href="/cdn-cgi/l/email-protection" class="__cf_email__" data-cfemail="136660766153766b727e637f763d707c7e">[email&#160;protected]</a>&quot;</span><span class="op">,</span></span>
<span id="cb1-5"><a href="#cb1-5" aria-hidden="true" tabindex="-1"></a>    Password <span class="op">=</span> <span class="st">&quot;SecurePass123&quot;</span><span class="op">,</span></span>
<span id="cb1-6"><a href="#cb1-6" aria-hidden="true" tabindex="-1"></a>    UserName <span class="op">=</span> <span class="st">&quot;JohnDoe&quot;</span></span>
<span id="cb1-7"><a href="#cb1-7" aria-hidden="true" tabindex="-1"></a><span class="op">});</span></span>
<span id="cb1-8"><a href="#cb1-8" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb1-9"><a href="#cb1-9" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>result<span class="op">.</span><span class="fu">Success</span><span class="op">)</span> </span>
<span id="cb1-10"><a href="#cb1-10" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb1-11"><a href="#cb1-11" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Пользователь зарегистрирован: {result.UserId}&quot;</span><span class="op">);</span></span>
<span id="cb1-12"><a href="#cb1-12" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Токен доступа: {result.Token}&quot;</span><span class="op">);</span></span>
<span id="cb1-13"><a href="#cb1-13" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb1-14"><a href="#cb1-14" aria-hidden="true" tabindex="-1"></a>                    </span></code></pre></div>
<div class="sourceCode" id="cb2"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb2-1"><a href="#cb2-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> credentials <span class="op">=</span> <span class="kw">new</span> LoginCredentials </span>
<span id="cb2-2"><a href="#cb2-2" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb2-3"><a href="#cb2-3" aria-hidden="true" tabindex="-1"></a>    Email <span class="op">=</span> <span class="st">&quot;<a href="/cdn-cgi/l/email-protection" class="__cf_email__" data-cfemail="7d080e180f3d18051c100d1118531e1210">[email&#160;protected]</a>&quot;</span><span class="op">,</span></span>
<span id="cb2-4"><a href="#cb2-4" aria-hidden="true" tabindex="-1"></a>    Password <span class="op">=</span> <span class="st">&quot;SecurePass123&quot;</span></span>
<span id="cb2-5"><a href="#cb2-5" aria-hidden="true" tabindex="-1"></a><span class="op">};</span></span>
<span id="cb2-6"><a href="#cb2-6" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb2-7"><a href="#cb2-7" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> result <span class="op">=</span> await service<span class="op">.</span><span class="fu">LoginAsync</span><span class="op">(</span>credentials<span class="op">);</span></span>
<span id="cb2-8"><a href="#cb2-8" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>result<span class="op">.</span><span class="fu">Success</span><span class="op">)</span></span>
<span id="cb2-9"><a href="#cb2-9" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb2-10"><a href="#cb2-10" aria-hidden="true" tabindex="-1"></a>    <span class="co">// Сохраняем токен для последующих запросов</span></span>
<span id="cb2-11"><a href="#cb2-11" aria-hidden="true" tabindex="-1"></a>    _tokenStorage<span class="op">.</span><span class="fu">SaveToken</span><span class="op">(</span>result<span class="op">.</span><span class="fu">Token</span><span class="op">);</span></span>
<span id="cb2-12"><a href="#cb2-12" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb2-13"><a href="#cb2-13" aria-hidden="true" tabindex="-1"></a>                    </span></code></pre></div>
<h2 id="language-detection-module">Модуль 2: Определение языка
текста</h2>
<h3 id="langdetect-overview">Обзор</h3>
<p>Модуль автоматически определяет язык текста с использованием
статистических алгоритмов и нейросетевых моделей.</p>
<div class="important">
<p>Для достоверного определения рекомендуется передавать текст длиной не
менее 50 символов.</p>
</div>
<h3 id="langdetect-classes">Классы и интерфейсы</h3>
LanguageDetectionService
public class LanguageDetectionService : ILanguageDetectionService
public
DetectLanguage
string
text
int
minLength
<h3 id="langdetect-examples">Примеры использования</h3>
<div class="sourceCode" id="cb3"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb3-1"><a href="#cb3-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> detector <span class="op">=</span> <span class="kw">new</span> <span class="fu">LanguageDetectionService</span><span class="op">();</span></span>
<span id="cb3-2"><a href="#cb3-2" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> text <span class="op">=</span> <span class="st">&quot;Hello, world! This is an English text.&quot;</span><span class="op">;</span></span>
<span id="cb3-3"><a href="#cb3-3" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb3-4"><a href="#cb3-4" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> result <span class="op">=</span> detector<span class="op">.</span><span class="fu">DetectLanguage</span><span class="op">(</span>text<span class="op">);</span></span>
<span id="cb3-5"><a href="#cb3-5" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Определён язык: {result.Language}&quot;</span><span class="op">);</span></span>
<span id="cb3-6"><a href="#cb3-6" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Уверенность: {result.Confidence:P2}&quot;</span><span class="op">);</span></span>
<span id="cb3-7"><a href="#cb3-7" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb3-8"><a href="#cb3-8" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>result<span class="op">.</span><span class="fu">AlternativeLanguages</span> <span class="op">!=</span> <span class="kw">null</span><span class="op">)</span></span>
<span id="cb3-9"><a href="#cb3-9" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb3-10"><a href="#cb3-10" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span><span class="st">&quot;Альтернативные варианты:&quot;</span><span class="op">);</span></span>
<span id="cb3-11"><a href="#cb3-11" aria-hidden="true" tabindex="-1"></a>    <span class="kw">foreach</span> <span class="op">(</span><span class="dt">var</span> alt <span class="kw">in</span> result<span class="op">.</span><span class="fu">AlternativeLanguages</span><span class="op">)</span></span>
<span id="cb3-12"><a href="#cb3-12" aria-hidden="true" tabindex="-1"></a>    <span class="op">{</span></span>
<span id="cb3-13"><a href="#cb3-13" aria-hidden="true" tabindex="-1"></a>        Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;  {alt.Key}: {alt.Value:P2}&quot;</span><span class="op">);</span></span>
<span id="cb3-14"><a href="#cb3-14" aria-hidden="true" tabindex="-1"></a>    <span class="op">}</span></span>
<span id="cb3-15"><a href="#cb3-15" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb3-16"><a href="#cb3-16" aria-hidden="true" tabindex="-1"></a>                    </span></code></pre></div>
<h2 id="translation-module">Модуль 3: Перевод текста</h2>
<h3 id="translation-overview">Обзор</h3>
<p>Модуль обеспечивает высококачественный перевод текста между
поддерживаемыми языками с использованием нескольких провайдеров (Google,
DeepL, Yandex, Microsoft).</p>
<h3 id="translation-classes">Классы и интерфейсы</h3>
TranslationService
public class TranslationService : ITranslationService
public async Task&lt;TranslationResult&gt;
TranslateAsync
string
text
string
targetLanguage
string
sourceLanguage
<h3 id="translation-examples">Примеры использования</h3>
<div class="sourceCode" id="cb4"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb4-1"><a href="#cb4-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> translator <span class="op">=</span> <span class="kw">new</span> <span class="fu">TranslationService</span><span class="op">();</span></span>
<span id="cb4-2"><a href="#cb4-2" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> result <span class="op">=</span> await translator<span class="op">.</span><span class="fu">TranslateAsync</span><span class="op">(</span></span>
<span id="cb4-3"><a href="#cb4-3" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Hello, how are you?&quot;</span><span class="op">,</span> </span>
<span id="cb4-4"><a href="#cb4-4" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;ru&quot;</span><span class="op">,</span> </span>
<span id="cb4-5"><a href="#cb4-5" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;auto&quot;</span></span>
<span id="cb4-6"><a href="#cb4-6" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb4-7"><a href="#cb4-7" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb4-8"><a href="#cb4-8" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>result<span class="op">.</span><span class="fu">Success</span><span class="op">)</span></span>
<span id="cb4-9"><a href="#cb4-9" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb4-10"><a href="#cb4-10" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Перевод: {result.TranslatedText}&quot;</span><span class="op">);</span></span>
<span id="cb4-11"><a href="#cb4-11" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Язык оригинала: {result.SourceLanguage}&quot;</span><span class="op">);</span></span>
<span id="cb4-12"><a href="#cb4-12" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Время выполнения: {result.ProcessingTimeMs} мс&quot;</span><span class="op">);</span></span>
<span id="cb4-13"><a href="#cb4-13" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb4-14"><a href="#cb4-14" aria-hidden="true" tabindex="-1"></a>                    </span></code></pre></div>
<div class="sourceCode" id="cb5"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb5-1"><a href="#cb5-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> texts <span class="op">=</span> <span class="kw">new</span> List<span class="op">&lt;</span><span class="dt">string</span><span class="op">&gt;</span> </span>
<span id="cb5-2"><a href="#cb5-2" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb5-3"><a href="#cb5-3" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Hello world&quot;</span><span class="op">,</span></span>
<span id="cb5-4"><a href="#cb5-4" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Good morning&quot;</span><span class="op">,</span></span>
<span id="cb5-5"><a href="#cb5-5" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Thank you&quot;</span></span>
<span id="cb5-6"><a href="#cb5-6" aria-hidden="true" tabindex="-1"></a><span class="op">};</span></span>
<span id="cb5-7"><a href="#cb5-7" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb5-8"><a href="#cb5-8" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> results <span class="op">=</span> await translator<span class="op">.</span><span class="fu">TranslateBatchAsync</span><span class="op">(</span>texts<span class="op">,</span> <span class="st">&quot;es&quot;</span><span class="op">);</span></span>
<span id="cb5-9"><a href="#cb5-9" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb5-10"><a href="#cb5-10" aria-hidden="true" tabindex="-1"></a><span class="kw">foreach</span> <span class="op">(</span><span class="dt">var</span> result <span class="kw">in</span> results<span class="op">)</span></span>
<span id="cb5-11"><a href="#cb5-11" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb5-12"><a href="#cb5-12" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;{result.OriginalText} -&gt; {result.TranslatedText}&quot;</span><span class="op">);</span></span>
<span id="cb5-13"><a href="#cb5-13" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb5-14"><a href="#cb5-14" aria-hidden="true" tabindex="-1"></a>                    </span></code></pre></div>
<h1 id="processing-optimization">Обработка и оптимизация</h1>
<h2 id="cache-module">Модуль 4: Кэширование и оптимизация</h2>
<h3 id="cache-overview">Обзор</h3>
<p>Модуль реализует многоуровневое кэширование для повышения
производительности системы.</p>
<table>
<caption>Уровни кэширования</caption>
<thead>
<tr class="header">
<th>Уровень</th>
<th>Тип</th>
<th>Время жизни</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>L1</td>
<td>In-Memory (LRU)</td>
<td>1 час</td>
</tr>
<tr class="even">
<td>L2</td>
<td>Redis</td>
<td>24 часа</td>
</tr>
</tbody>
</table>
<h3 id="cache-examples">Примеры использования</h3>
<div class="sourceCode" id="cb6"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb6-1"><a href="#cb6-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> cache <span class="op">=</span> <span class="kw">new</span> <span class="fu">CacheService</span><span class="op">(</span>maxSize<span class="op">:</span> <span class="dv">1000</span><span class="op">);</span></span>
<span id="cb6-2"><a href="#cb6-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb6-3"><a href="#cb6-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Сохранение в кэш</span></span>
<span id="cb6-4"><a href="#cb6-4" aria-hidden="true" tabindex="-1"></a>await cache<span class="op">.</span><span class="fu">SetAsync</span><span class="op">(</span><span class="st">&quot;key&quot;</span><span class="op">,</span> data<span class="op">,</span> TimeSpan<span class="op">.</span><span class="fu">FromHours</span><span class="op">(</span><span class="dv">1</span><span class="op">));</span></span>
<span id="cb6-5"><a href="#cb6-5" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb6-6"><a href="#cb6-6" aria-hidden="true" tabindex="-1"></a><span class="co">// Получение из кэша</span></span>
<span id="cb6-7"><a href="#cb6-7" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> cached <span class="op">=</span> await cache<span class="op">.</span><span class="fu">GetAsync</span><span class="op">&lt;</span>MyData<span class="op">&gt;(</span><span class="st">&quot;key&quot;</span><span class="op">);</span></span>
<span id="cb6-8"><a href="#cb6-8" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>cached <span class="op">!=</span> <span class="kw">null</span><span class="op">)</span></span>
<span id="cb6-9"><a href="#cb6-9" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb6-10"><a href="#cb6-10" aria-hidden="true" tabindex="-1"></a>    <span class="co">// Используем кэшированные данные</span></span>
<span id="cb6-11"><a href="#cb6-11" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb6-12"><a href="#cb6-12" aria-hidden="true" tabindex="-1"></a><span class="kw">else</span></span>
<span id="cb6-13"><a href="#cb6-13" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb6-14"><a href="#cb6-14" aria-hidden="true" tabindex="-1"></a>    <span class="co">// Загружаем данные из источника</span></span>
<span id="cb6-15"><a href="#cb6-15" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb6-16"><a href="#cb6-16" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h2 id="dictionary-module">Модуль 5: Пользовательские словари</h2>
<h3 id="dictionary-overview">Обзор</h3>
<p>Позволяет пользователям создавать собственные словари терминов для
улучшения качества перевода.</p>
<h3 id="dictionary-examples">Примеры использования</h3>
<div class="sourceCode" id="cb7"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb7-1"><a href="#cb7-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> dictService <span class="op">=</span> <span class="kw">new</span> <span class="fu">UserDictionaryService</span><span class="op">();</span></span>
<span id="cb7-2"><a href="#cb7-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb7-3"><a href="#cb7-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Создание словаря</span></span>
<span id="cb7-4"><a href="#cb7-4" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> dictionary <span class="op">=</span> await dictService<span class="op">.</span><span class="fu">CreateDictionaryAsync</span><span class="op">(</span></span>
<span id="cb7-5"><a href="#cb7-5" aria-hidden="true" tabindex="-1"></a>    userId<span class="op">,</span> </span>
<span id="cb7-6"><a href="#cb7-6" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Technical Terms&quot;</span><span class="op">,</span> </span>
<span id="cb7-7"><a href="#cb7-7" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;en&quot;</span><span class="op">,</span> </span>
<span id="cb7-8"><a href="#cb7-8" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;ru&quot;</span></span>
<span id="cb7-9"><a href="#cb7-9" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb7-10"><a href="#cb7-10" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb7-11"><a href="#cb7-11" aria-hidden="true" tabindex="-1"></a><span class="co">// Добавление термина</span></span>
<span id="cb7-12"><a href="#cb7-12" aria-hidden="true" tabindex="-1"></a>await dictService<span class="op">.</span><span class="fu">AddTermAsync</span><span class="op">(</span></span>
<span id="cb7-13"><a href="#cb7-13" aria-hidden="true" tabindex="-1"></a>    dictionary<span class="op">.</span><span class="fu">Id</span><span class="op">,</span> </span>
<span id="cb7-14"><a href="#cb7-14" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;API&quot;</span><span class="op">,</span> </span>
<span id="cb7-15"><a href="#cb7-15" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Интерфейс программирования приложений&quot;</span><span class="op">,</span></span>
<span id="cb7-16"><a href="#cb7-16" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;technical documentation&quot;</span></span>
<span id="cb7-17"><a href="#cb7-17" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb7-18"><a href="#cb7-18" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb7-19"><a href="#cb7-19" aria-hidden="true" tabindex="-1"></a><span class="co">// Использование при переводе</span></span>
<span id="cb7-20"><a href="#cb7-20" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> translation <span class="op">=</span> await dictService<span class="op">.</span><span class="fu">GetCustomTranslationAsync</span><span class="op">(</span></span>
<span id="cb7-21"><a href="#cb7-21" aria-hidden="true" tabindex="-1"></a>    userId<span class="op">,</span> <span class="st">&quot;API&quot;</span><span class="op">,</span> <span class="st">&quot;en&quot;</span><span class="op">,</span> <span class="st">&quot;ru&quot;</span></span>
<span id="cb7-22"><a href="#cb7-22" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb7-23"><a href="#cb7-23" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h2 id="ocr-module">Модуль 6: Распознавание текста на изображениях</h2>
<h3 id="ocr-examples">Примеры использования</h3>
<div class="sourceCode" id="cb8"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb8-1"><a href="#cb8-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> ocr <span class="op">=</span> <span class="kw">new</span> <span class="fu">OCRService</span><span class="op">();</span></span>
<span id="cb8-2"><a href="#cb8-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb8-3"><a href="#cb8-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Загрузка изображения</span></span>
<span id="cb8-4"><a href="#cb8-4" aria-hidden="true" tabindex="-1"></a><span class="dt">byte</span><span class="op">[]</span> imageData <span class="op">=</span> File<span class="op">.</span><span class="fu">ReadAllBytes</span><span class="op">(</span><span class="st">&quot;document.jpg&quot;</span><span class="op">);</span></span>
<span id="cb8-5"><a href="#cb8-5" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb8-6"><a href="#cb8-6" aria-hidden="true" tabindex="-1"></a><span class="co">// Извлечение текста</span></span>
<span id="cb8-7"><a href="#cb8-7" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> result <span class="op">=</span> await ocr<span class="op">.</span><span class="fu">ExtractTextFromImageAsync</span><span class="op">(</span>imageData<span class="op">,</span> <span class="st">&quot;rus&quot;</span><span class="op">);</span></span>
<span id="cb8-8"><a href="#cb8-8" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb8-9"><a href="#cb8-9" aria-hidden="true" tabindex="-1"></a><span class="kw">if</span> <span class="op">(</span>result<span class="op">.</span><span class="fu">Success</span><span class="op">)</span></span>
<span id="cb8-10"><a href="#cb8-10" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb8-11"><a href="#cb8-11" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Распознанный текст: {result.ExtractedText}&quot;</span><span class="op">);</span></span>
<span id="cb8-12"><a href="#cb8-12" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Уверенность: {result.Confidence:P2}&quot;</span><span class="op">);</span></span>
<span id="cb8-13"><a href="#cb8-13" aria-hidden="true" tabindex="-1"></a>    </span>
<span id="cb8-14"><a href="#cb8-14" aria-hidden="true" tabindex="-1"></a>    <span class="co">// Перевод распознанного текста</span></span>
<span id="cb8-15"><a href="#cb8-15" aria-hidden="true" tabindex="-1"></a>    <span class="dt">var</span> translated <span class="op">=</span> await translator<span class="op">.</span><span class="fu">TranslateAsync</span><span class="op">(</span></span>
<span id="cb8-16"><a href="#cb8-16" aria-hidden="true" tabindex="-1"></a>        result<span class="op">.</span><span class="fu">ExtractedText</span><span class="op">,</span> </span>
<span id="cb8-17"><a href="#cb8-17" aria-hidden="true" tabindex="-1"></a>        <span class="st">&quot;en&quot;</span></span>
<span id="cb8-18"><a href="#cb8-18" aria-hidden="true" tabindex="-1"></a>    <span class="op">);</span></span>
<span id="cb8-19"><a href="#cb8-19" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb8-20"><a href="#cb8-20" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h1 id="ui-analytics">Пользовательский интерфейс и аналитика</h1>
<h2 id="tts-module">Модуль 7: Озвучивание текста</h2>
<h3 id="tts-examples">Примеры использования</h3>
<div class="sourceCode" id="cb9"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb9-1"><a href="#cb9-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> tts <span class="op">=</span> <span class="kw">new</span> <span class="fu">TextToSpeechService</span><span class="op">();</span></span>
<span id="cb9-2"><a href="#cb9-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb9-3"><a href="#cb9-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Получение доступных голосов</span></span>
<span id="cb9-4"><a href="#cb9-4" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> voices <span class="op">=</span> await tts<span class="op">.</span><span class="fu">GetAvailableVoicesAsync</span><span class="op">(</span><span class="st">&quot;ru-RU&quot;</span><span class="op">);</span></span>
<span id="cb9-5"><a href="#cb9-5" aria-hidden="true" tabindex="-1"></a><span class="kw">foreach</span> <span class="op">(</span><span class="dt">var</span> voice <span class="kw">in</span> voices<span class="op">)</span></span>
<span id="cb9-6"><a href="#cb9-6" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb9-7"><a href="#cb9-7" aria-hidden="true" tabindex="-1"></a>    Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Голос: {voice.Name} ({voice.Gender})&quot;</span><span class="op">);</span></span>
<span id="cb9-8"><a href="#cb9-8" aria-hidden="true" tabindex="-1"></a><span class="op">}</span></span>
<span id="cb9-9"><a href="#cb9-9" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb9-10"><a href="#cb9-10" aria-hidden="true" tabindex="-1"></a><span class="co">// Синтез речи</span></span>
<span id="cb9-11"><a href="#cb9-11" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> settings <span class="op">=</span> <span class="kw">new</span> VoiceSettings </span>
<span id="cb9-12"><a href="#cb9-12" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb9-13"><a href="#cb9-13" aria-hidden="true" tabindex="-1"></a>    VoiceId <span class="op">=</span> <span class="st">&quot;ru-RU-Dariya&quot;</span><span class="op">,</span></span>
<span id="cb9-14"><a href="#cb9-14" aria-hidden="true" tabindex="-1"></a>    Speed <span class="op">=</span> <span class="fl">1.0f</span><span class="op">,</span></span>
<span id="cb9-15"><a href="#cb9-15" aria-hidden="true" tabindex="-1"></a>    Pitch <span class="op">=</span> <span class="dv">0</span>f</span>
<span id="cb9-16"><a href="#cb9-16" aria-hidden="true" tabindex="-1"></a><span class="op">};</span></span>
<span id="cb9-17"><a href="#cb9-17" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb9-18"><a href="#cb9-18" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> audioData <span class="op">=</span> await tts<span class="op">.</span><span class="fu">SynthesizeSpeechAsync</span><span class="op">(</span></span>
<span id="cb9-19"><a href="#cb9-19" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;Привет, мир!&quot;</span><span class="op">,</span> </span>
<span id="cb9-20"><a href="#cb9-20" aria-hidden="true" tabindex="-1"></a>    <span class="st">&quot;ru-RU&quot;</span><span class="op">,</span> </span>
<span id="cb9-21"><a href="#cb9-21" aria-hidden="true" tabindex="-1"></a>    settings</span>
<span id="cb9-22"><a href="#cb9-22" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb9-23"><a href="#cb9-23" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb9-24"><a href="#cb9-24" aria-hidden="true" tabindex="-1"></a><span class="co">// Сохранение в файл</span></span>
<span id="cb9-25"><a href="#cb9-25" aria-hidden="true" tabindex="-1"></a>await File<span class="op">.</span><span class="fu">WriteAllBytesAsync</span><span class="op">(</span><span class="st">&quot;output.mp3&quot;</span><span class="op">,</span> audioData<span class="op">);</span></span>
<span id="cb9-26"><a href="#cb9-26" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h2 id="analytics-module">Модуль 8: Аналитика и отчётность</h2>
<h3 id="analytics-examples">Примеры использования</h3>
<div class="sourceCode" id="cb10"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb10-1"><a href="#cb10-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> analytics <span class="op">=</span> <span class="kw">new</span> <span class="fu">AnalyticsService</span><span class="op">();</span></span>
<span id="cb10-2"><a href="#cb10-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb10-3"><a href="#cb10-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Регистрация события</span></span>
<span id="cb10-4"><a href="#cb10-4" aria-hidden="true" tabindex="-1"></a>await analytics<span class="op">.</span><span class="fu">RecordTranslationEventAsync</span><span class="op">(</span><span class="kw">new</span> TranslationEventData</span>
<span id="cb10-5"><a href="#cb10-5" aria-hidden="true" tabindex="-1"></a><span class="op">{</span></span>
<span id="cb10-6"><a href="#cb10-6" aria-hidden="true" tabindex="-1"></a>    UserId <span class="op">=</span> currentUserId<span class="op">,</span></span>
<span id="cb10-7"><a href="#cb10-7" aria-hidden="true" tabindex="-1"></a>    SourceLanguage <span class="op">=</span> <span class="st">&quot;en&quot;</span><span class="op">,</span></span>
<span id="cb10-8"><a href="#cb10-8" aria-hidden="true" tabindex="-1"></a>    TargetLanguage <span class="op">=</span> <span class="st">&quot;ru&quot;</span><span class="op">,</span></span>
<span id="cb10-9"><a href="#cb10-9" aria-hidden="true" tabindex="-1"></a>    TextLength <span class="op">=</span> text<span class="op">.</span><span class="fu">Length</span><span class="op">,</span></span>
<span id="cb10-10"><a href="#cb10-10" aria-hidden="true" tabindex="-1"></a>    ProcessingTimeMs <span class="op">=</span> <span class="dv">150</span><span class="op">,</span></span>
<span id="cb10-11"><a href="#cb10-11" aria-hidden="true" tabindex="-1"></a>    Success <span class="op">=</span> <span class="kw">true</span></span>
<span id="cb10-12"><a href="#cb10-12" aria-hidden="true" tabindex="-1"></a><span class="op">});</span></span>
<span id="cb10-13"><a href="#cb10-13" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb10-14"><a href="#cb10-14" aria-hidden="true" tabindex="-1"></a><span class="co">// Получение данных дашборда</span></span>
<span id="cb10-15"><a href="#cb10-15" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> dashboard <span class="op">=</span> await analytics<span class="op">.</span><span class="fu">GetDashboardDataAsync</span><span class="op">(</span></span>
<span id="cb10-16"><a href="#cb10-16" aria-hidden="true" tabindex="-1"></a>    DateTime<span class="op">.</span><span class="fu">Now</span><span class="op">.</span><span class="fu">AddDays</span><span class="op">(-</span><span class="dv">30</span><span class="op">),</span> </span>
<span id="cb10-17"><a href="#cb10-17" aria-hidden="true" tabindex="-1"></a>    DateTime<span class="op">.</span><span class="fu">Now</span></span>
<span id="cb10-18"><a href="#cb10-18" aria-hidden="true" tabindex="-1"></a><span class="op">);</span></span>
<span id="cb10-19"><a href="#cb10-19" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb10-20"><a href="#cb10-20" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Всего переводов: {dashboard.TotalTranslations}&quot;</span><span class="op">);</span></span>
<span id="cb10-21"><a href="#cb10-21" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Уникальных пользователей: {dashboard.UniqueUsers}&quot;</span><span class="op">);</span></span>
<span id="cb10-22"><a href="#cb10-22" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Среднее время ответа: {dashboard.AverageResponseTime} мс&quot;</span><span class="op">);</span></span>
<span id="cb10-23"><a href="#cb10-23" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h2 id="sync-module">Модуль 9: Синхронизация и резервное
копирование</h2>
<h3 id="sync-examples">Примеры использования</h3>
<div class="sourceCode" id="cb11"><pre
class="sourceCode csharp"><code class="sourceCode cs"><span id="cb11-1"><a href="#cb11-1" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> sync <span class="op">=</span> <span class="kw">new</span> <span class="fu">SyncService</span><span class="op">();</span></span>
<span id="cb11-2"><a href="#cb11-2" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb11-3"><a href="#cb11-3" aria-hidden="true" tabindex="-1"></a><span class="co">// Синхронизация данных</span></span>
<span id="cb11-4"><a href="#cb11-4" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> syncResult <span class="op">=</span> await sync<span class="op">.</span><span class="fu">SyncUserDataAsync</span><span class="op">(</span>userId<span class="op">,</span> deviceId<span class="op">);</span></span>
<span id="cb11-5"><a href="#cb11-5" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Синхронизировано элементов: {syncResult.SyncedItems}&quot;</span><span class="op">);</span></span>
<span id="cb11-6"><a href="#cb11-6" aria-hidden="true" tabindex="-1"></a></span>
<span id="cb11-7"><a href="#cb11-7" aria-hidden="true" tabindex="-1"></a><span class="co">// Создание резервной копии</span></span>
<span id="cb11-8"><a href="#cb11-8" aria-hidden="true" tabindex="-1"></a><span class="dt">var</span> backup <span class="op">=</span> await sync<span class="op">.</span><span class="fu">CreateBackupAsync</span><span class="op">(</span>userId<span class="op">,</span> BackupType<span class="op">.</span><span class="fu">Full</span><span class="op">);</span></span>
<span id="cb11-9"><a href="#cb11-9" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Резервная копия создана: {backup.BackupId}&quot;</span><span class="op">);</span></span>
<span id="cb11-10"><a href="#cb11-10" aria-hidden="true" tabindex="-1"></a>Console<span class="op">.</span><span class="fu">WriteLine</span><span class="op">(</span>$<span class="st">&quot;Размер: {backup.Size / 1024} KB&quot;</span><span class="op">);</span></span>
<span id="cb11-11"><a href="#cb11-11" aria-hidden="true" tabindex="-1"></a>                </span></code></pre></div>
<h2 id="ui-module">Модуль 10: Пользовательский интерфейс</h2>
<h3 id="ui-api">API компонентов UI</h3>
<table>
<caption>Компоненты UI</caption>
<thead>
<tr class="header">
<th>Компонент</th>
<th>Описание</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>TranslatorUIComponent</td>
<td>Основной компонент перевода</td>
</tr>
<tr class="even">
<td>LanguageSelector</td>
<td>Выбор языка перевода</td>
</tr>
<tr class="odd">
<td>HistoryPanel</td>
<td>Панель истории переводов</td>
</tr>
<tr class="even">
<td>DictionaryManager</td>
<td>Управление словарями</td>
</tr>
</tbody>
</table>
<h2 id="appendix-error-codes">Коды ошибок</h2>
<table>
<caption>Таблица кодов ошибок</caption>
<thead>
<tr class="header">
<th>Код</th>
<th>Название</th>
<th>Описание</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>1001</td>
<td>InvalidEmailFormat</td>
<td>Неверный формат email адреса</td>
</tr>
<tr class="even">
<td>1002</td>
<td>UserAlreadyExists</td>
<td>Пользователь уже существует</td>
</tr>
<tr class="odd">
<td>2001</td>
<td>LanguageNotSupported</td>
<td>Язык не поддерживается</td>
</tr>
<tr class="even">
<td>3001</td>
<td>TranslationFailed</td>
<td>Ошибка выполнения перевода</td>
</tr>
</tbody>
</table>
<h2 id="appendix-glossary">Глоссарий</h2>
<h2></h2>
JWT
<p>JSON Web Token - стандарт для создания токенов доступа.</p>
N-грамма
<p>Последовательность из N символов, используемая для анализа
текста.</p>
OCR
<p>Optical Character Recognition - оптическое распознавание
символов.</p>
TTS
<p>Text-to-Speech - технология синтеза речи.</p>

<script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script></body>
</html>
