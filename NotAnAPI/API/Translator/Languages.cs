﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.API.Translator
{
    public enum Languages
    {
        Auto,
        Afrikaans,
        Albanian,
        Amharic,
        Arabic,
        Armenian,
        Azerbaijani,
        Basque,
        Belarusian,
        Bengali,
        Bosnian,
        Bulgarian,
        Catalan,
        Cebuano,
        Chichewa,
        ChineseSimplified,
        ChineseTraditional,
        Corsican,
        Croatian,
        Czech,
        Danish,
        Dutch,
        English,
        Esperanto,
        Estonian,
        Filipino,
        Finnish,
        French,
        Frisian,
        Galician,
        Georgian,
        German,
        Greek,
        Gujarati,
        HaitianCreole,
        Hausa,
        Hawaiian,
        Hebrew,
        Hindi,
        Hmong,
        Hungarian,
        Icelandic,
        Igbo,
        Indonesian,
        Irish,
        Italian,
        Japanese,
        Javanese,
        Kannada,
        Kazakh,
        Khmer,
        Korean,
        KurdishKurmanji,
        Kyrgyz,
        Lao,
        Latin,
        Latvian,
        Lithuanian,
        Luxembourgish,
        Macedonian,
        Malagasy,
        Malay,
        Malayalam,
        Maltese,
        Maori,
        Marathi,
        Mongolian,
        MyanmarBurmese,
        Nepali,
        Norwegian,
        Pashto,
        Persian,
        Polish,
        Portuguese,
        Punjabi,
        Romanian,
        Russian,
        Samoan,
        ScotsGaelic,
        Serbian,
        Sesotho,
        Shona,
        Sindhi,
        Sinhala,
        Slovak,
        Slovenian,
        Somali,
        Spanish,
        Sundanese,
        Swahili,
        Swedish,
        Tajik,
        Tamil,
        Telugu,
        Thai,
        Turkish,
        Ukrainian,
        Urdu,
        Uzbek,
        Vietnamese,
        Welsh,
        Xhosa,
        Yiddish,
        Yoruba,
        Zulu,
        FilipinoAlt, // For 'fil'
        HebrewAlt   // For 'he'
    }


    public static class LanguagesExtensions
    {
        public static string GetLanguage(this Languages lang)
        {
            return GoogleTranslator._languageCodes[lang];
        }
    }
}
