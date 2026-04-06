// Мобильное меню
const hamburger = document.querySelector('.hamburger');
const navMenu = document.querySelector('.nav-menu');

if (hamburger) {
    hamburger.addEventListener('click', () => {
        hamburger.classList.toggle('active');
        navMenu.classList.toggle('active');
    });
}

document.querySelectorAll('.nav-link').forEach(link => {
    link.addEventListener('click', () => {
        if (hamburger) hamburger.classList.remove('active');
        if (navMenu) navMenu.classList.remove('active');
    });
});

// Активная навигация при скролле
window.addEventListener('scroll', () => {
    const sections = document.querySelectorAll('section');
    const navLinks = document.querySelectorAll('.nav-link');
    
    let current = '';
    sections.forEach(section => {
        const sectionTop = section.offsetTop;
        const sectionHeight = section.clientHeight;
        if (scrollY >= sectionTop - 200) {
            current = section.getAttribute('id');
        }
    });
    
    navLinks.forEach(link => {
        link.classList.remove('active');
        if (link.getAttribute('href') === `#${current}`) {
            link.classList.add('active');
        }
    });
});

// Табы модулей
const tabBtns = document.querySelectorAll('.tab-btn');
const tabPanes = document.querySelectorAll('.tab-pane');

tabBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const tabId = btn.getAttribute('data-tab');
        
        tabBtns.forEach(b => b.classList.remove('active'));
        tabPanes.forEach(p => p.classList.remove('active'));
        
        btn.classList.add('active');
        const activePane = document.getElementById(tabId);
        if (activePane) activePane.classList.add('active');
    });
});

// Словарь для демо перевода
const translations = {
    'en-ru': {
        'hello': 'привет',
        'world': 'мир',
        'how are you': 'как дела',
        'good morning': 'доброе утро',
        'thank you': 'спасибо',
        'goodbye': 'до свидания',
        'welcome': 'добро пожаловать'
    },
    'ru-en': {
        'привет': 'hello',
        'мир': 'world',
        'как дела': 'how are you',
        'доброе утро': 'good morning',
        'спасибо': 'thank you',
        'до свидания': 'goodbye',
        'добро пожаловать': 'welcome'
    },
    'en-fr': {
        'hello': 'bonjour',
        'world': 'monde',
        'how are you': 'comment allez-vous',
        'thank you': 'merci'
    },
    'fr-en': {
        'bonjour': 'hello',
        'monde': 'world',
        'comment allez-vous': 'how are you',
        'merci': 'thank you'
    }
};

function translateDemo() {
    const sourceText = document.getElementById('sourceText');
    const sourceLang = document.getElementById('sourceLang');
    const targetLang = document.getElementById('targetLang');
    const resultDiv = document.getElementById('translatedText');
    
    if (!sourceText || !sourceLang || !targetLang || !resultDiv) return;
    
    const text = sourceText.value;
    const src = sourceLang.value;
    const tgt = targetLang.value;
    
    if (!text.trim()) {
        resultDiv.innerHTML = 'Введите текст для перевода...';
        return;
    }
    
    let translated = text;
    const langPair = `${src}-${tgt}`;
    
    if (translations[langPair]) {
        const dict = translations[langPair];
        let lowerText = text.toLowerCase();
        for (const [orig, trans] of Object.entries(dict)) {
            if (lowerText.includes(orig)) {
                const regex = new RegExp(`\\b${orig}\\b`, 'gi');
                translated = translated.replace(regex, trans);
            }
        }
    }
    
    resultDiv.innerHTML = translated;
    
    resultDiv.style.opacity = '0';
    setTimeout(() => {
        resultDiv.style.opacity = '1';
    }, 100);
}

function swapLanguages() {
    const sourceLang = document.getElementById('sourceLang');
    const targetLang = document.getElementById('targetLang');
    const sourceText = document.getElementById('sourceText');
    const translatedText = document.getElementById('translatedText');
    
    if (!sourceLang || !targetLang || !sourceText || !translatedText) return;
    
    const tempLang = sourceLang.value;
    sourceLang.value = targetLang.value;
    targetLang.value = tempLang;
    
    const tempText = sourceText.value;
    sourceText.value = translatedText.innerHTML;
    translatedText.innerHTML = tempText;
}

function clearDemo() {
    const sourceText = document.getElementById('sourceText');
    const translatedText = document.getElementById('translatedText');
    
    if (sourceText) sourceText.value = '';
    if (translatedText) translatedText.innerHTML = 'Перевод появится здесь...';
}

function showDemo() {
    const demoSection = document.getElementById('demo');
    if (demoSection) {
        demoSection.scrollIntoView({ behavior: 'smooth' });
    }
}

// Плавный скролл для всех якорных ссылок
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function(e) {
        e.preventDefault();
        const targetId = this.getAttribute('href');
        if (targetId === '#') return;
        
        const target = document.querySelector(targetId);
        if (target) {
            target.scrollIntoView({ behavior: 'smooth' });
        }
    });
});

// Анимация появления элементов при загрузке
document.addEventListener('DOMContentLoaded', () => {
    const fadeElements = document.querySelectorAll('.feature-card, .module-item, .doc-card');
    fadeElements.forEach((el, index) => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        el.style.transition = 'all 0.5s';
        
        setTimeout(() => {
            el.style.opacity = '1';
            el.style.transform = 'translateY(0)';
        }, index * 100);
    });
    
    // Установка языков по умолчанию
    const sourceLang = document.getElementById('sourceLang');
    const targetLang = document.getElementById('targetLang');
    if (sourceLang) sourceLang.value = 'auto';
    if (targetLang) targetLang.value = 'ru';
});
