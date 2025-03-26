using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace WeblateDotNetPoc
{
    public static class TranslationMediator
    {
        private const string BASE_URL = "http://localhost:80/api/translations/drake/angular-js";
        private const string APP_TOKEN = "Token wlu_HCBOOlwNOd1WATtAgp61nuxOljjtC7paeu4s";
        private static Dictionary<string, TranslationKeys> _translationsByLanguages = new Dictionary<string, TranslationKeys>();

        public static string GetTranslationByKey(string key, string languageCode = null)
        {
            if (languageCode == null)
            {
                languageCode = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            }

            if (_translationsByLanguages.TryGetValue(languageCode, out TranslationKeys translations))
            {
                if (translations.Translations.TryGetValue(key, out string value))
                {
                    return value;
                }

                CreateTranslation(key);
                return GetTranslationByKey(key, languageCode);
            }
            
            InitializeLanguage(languageCode);
            return GetTranslationByKey(key, languageCode);
        }

        private static void CreateTranslation(string value)
        {
            string baseUrl = $"{BASE_URL}/pt/units/";
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", APP_TOKEN);
                    
                    var requestBody = new { key = value, value = new[] { value }};
                    string json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    
                    HttpResponseMessage response = client.PostAsync(baseUrl, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReloadAllLanguages();
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na criação da tradução: {e.Message}");
                }
            }
        }
        
        private static void InitializeLanguage(string languageCode)
        {
            if (!_translationsByLanguages.ContainsKey(languageCode))
            {
                SetTranslationsOnCache(languageCode);
            }
        }

        private static void ReloadAllLanguages()
        {
            var languageCodes = _translationsByLanguages.Keys.ToList();

            foreach (var languageCode in languageCodes)
            {
                SetTranslationsOnCache(languageCode);
            }
        }

        private static void SetTranslationsOnCache(string languageCode)
        {
            string baseUrl = $"{BASE_URL}/{languageCode}/file/?format=json";
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(baseUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        client.DefaultRequestHeaders.Add("Authorization", APP_TOKEN);
                        var result = response.Content.ReadAsStringAsync().Result;

                        if (result != null && !string.IsNullOrEmpty(result))
                        {
                            var languageKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                            if (_translationsByLanguages.ContainsKey(languageCode))
                            {
                                _translationsByLanguages[languageCode].Translations = languageKeys;
                            }
                            else
                            {
                                _translationsByLanguages.Add(languageCode, new TranslationKeys { Translations = languageKeys });
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                    
                }
            }
        }
    }

    public class TranslationKeys
    {
        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();
    }
}