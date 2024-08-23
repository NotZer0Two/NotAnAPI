using Exiled.API.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NotAnAPI.API.Translator
{
    public class GoogleTranslator
    {
        public const int MaxLengthToTranslate = 3900;

        public static readonly Dictionary<Languages, string> _languageCodes = new Dictionary<Languages, string>
        {
            { Languages.Auto, "auto" },
            { Languages.Afrikaans, "af" },
            { Languages.Albanian, "sq" },
            { Languages.Amharic, "am" },
            { Languages.Arabic, "ar" },
            { Languages.Armenian, "hy" },
            { Languages.Azerbaijani, "az" },
            { Languages.Basque, "eu" },
            { Languages.Belarusian, "be" },
            { Languages.Bengali, "bn" },
            { Languages.Bosnian, "bs" },
            { Languages.Bulgarian, "bg" },
            { Languages.Catalan, "ca" },
            { Languages.Cebuano, "ceb" },
            { Languages.Chichewa, "ny" },
            { Languages.ChineseSimplified, "zh-cn" },
            { Languages.ChineseTraditional, "zh-tw" },
            { Languages.Corsican, "co" },
            { Languages.Croatian, "hr" },
            { Languages.Czech, "cs" },
            { Languages.Danish, "da" },
            { Languages.Dutch, "nl" },
            { Languages.English, "en" },
            { Languages.Esperanto, "eo" },
            { Languages.Estonian, "et" },
            { Languages.Filipino, "tl" },
            { Languages.Finnish, "fi" },
            { Languages.French, "fr" },
            { Languages.Frisian, "fy" },
            { Languages.Galician, "gl" },
            { Languages.Georgian, "ka" },
            { Languages.German, "de" },
            { Languages.Greek, "el" },
            { Languages.Gujarati, "gu" },
            { Languages.HaitianCreole, "ht" },
            { Languages.Hausa, "ha" },
            { Languages.Hawaiian, "haw" },
            { Languages.Hebrew, "iw" },
            { Languages.Hindi, "hi" },
            { Languages.Hmong, "hmn" },
            { Languages.Hungarian, "hu" },
            { Languages.Icelandic, "is" },
            { Languages.Igbo, "ig" },
            { Languages.Indonesian, "id" },
            { Languages.Irish, "ga" },
            { Languages.Italian, "it" },
            { Languages.Japanese, "ja" },
            { Languages.Javanese, "jw" },
            { Languages.Kannada, "kn" },
            { Languages.Kazakh, "kk" },
            { Languages.Khmer, "km" },
            { Languages.Korean, "ko" },
            { Languages.KurdishKurmanji, "ku" },
            { Languages.Kyrgyz, "ky" },
            { Languages.Lao, "lo" },
            { Languages.Latin, "la" },
            { Languages.Latvian, "lv" },
            { Languages.Lithuanian, "lt" },
            { Languages.Luxembourgish, "lb" },
            { Languages.Macedonian, "mk" },
            { Languages.Malagasy, "mg" },
            { Languages.Malay, "ms" },
            { Languages.Malayalam, "ml" },
            { Languages.Maltese, "mt" },
            { Languages.Maori, "mi" },
            { Languages.Marathi, "mr" },
            { Languages.Mongolian, "mn" },
            { Languages.MyanmarBurmese, "my" },
            { Languages.Nepali, "ne" },
            { Languages.Norwegian, "no" },
            { Languages.Pashto, "ps" },
            { Languages.Persian, "fa" },
            { Languages.Polish, "pl" },
            { Languages.Portuguese, "pt" },
            { Languages.Punjabi, "pa" },
            { Languages.Romanian, "ro" },
            { Languages.Russian, "ru" },
            { Languages.Samoan, "sm" },
            { Languages.ScotsGaelic, "gd" },
            { Languages.Serbian, "sr" },
            { Languages.Sesotho, "st" },
            { Languages.Shona, "sn" },
            { Languages.Sindhi, "sd" },
            { Languages.Sinhala, "si" },
            { Languages.Slovak, "sk" },
            { Languages.Slovenian, "sl" },
            { Languages.Somali, "so" },
            { Languages.Spanish, "es" },
            { Languages.Sundanese, "su" },
            { Languages.Swahili, "sw" },
            { Languages.Swedish, "sv" },
            { Languages.Tajik, "tg" },
            { Languages.Tamil, "ta" },
            { Languages.Telugu, "te" },
            { Languages.Thai, "th" },
            { Languages.Turkish, "tr" },
            { Languages.Ukrainian, "uk" },
            { Languages.Urdu, "ur" },
            { Languages.Uzbek, "uz" },
            { Languages.Vietnamese, "vi" },
            { Languages.Welsh, "cy" },
            { Languages.Xhosa, "xh" },
            { Languages.Yiddish, "yi" },
            { Languages.Yoruba, "yo" },
            { Languages.Zulu, "zu" },
            { Languages.FilipinoAlt, "fil" },
            { Languages.HebrewAlt, "he" }
        };

        public static string Request(string message, Languages target, Languages source)
        {
            if (target == source) return message;

            string plainText;
            var tags = ExtractTags(message, out plainText);

            string url = "https://deep-translator-api.azurewebsites.net/google/";

            string jsonData = $@"
                {{
                    ""source"": ""{source.GetLanguage()}"",
                    ""target"": ""{target.GetLanguage()}"",
                    ""text"": ""{plainText}"",
                    ""proxies"": []
                }}";

            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";

                    //Don't ask me, why it needs to be cleaned but i lost 2 hourse only for this so lol
                    string response = CleanResponse(client.UploadString(url, "POST", jsonData).Replace('\"', ' '));

                    Log.Info(response);

                    var translationResponse = JsonConvert.DeserializeObject<TranslationResponse>(response);

                    Log.Info(translationResponse.Translation);

                    if (!string.IsNullOrEmpty(translationResponse.Translation))
                    {
                        return ReinsertTags(translationResponse.Translation, tags);
                    }
                    else
                    {
                        return $"Error: {translationResponse.Error}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error Exc: {ex.Message}";
            }
        }

        private static string CleanResponse(string response)
        {
            string cleaned = response.Replace("'", "\"");

            cleaned = Regex.Replace(cleaned, @"(\w+)\s*:", @"$1:");
            cleaned = Regex.Replace(cleaned, @":\s*([^,}]+)(?=[,}])", match => $": \"{match.Groups[1].Value.Trim()}\"");

            return cleaned;
        }

        private static Dictionary<int, string> ExtractTags(string input, out string plainText)
        {
            var tags = new Dictionary<int, string>();
            var regex = new Regex(@"<[^>]+>");

            plainText = regex.Replace(input, match =>
            {
                tags[match.Index] = match.Value;
                return $".TAG{match.Index}.";
            });

            return tags;
        }

        private static string ReinsertTags(string translatedText, Dictionary<int, string> tags)
        {
            foreach (var tag in tags)
            {
                translatedText = translatedText.Replace($".TAG{tag.Key}.", tag.Value);
            }

            return translatedText;
        }
    }
}
