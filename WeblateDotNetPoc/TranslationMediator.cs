using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace WeblateDotNetPoc
{
    public interface ITranslationMediator
    {
        void InitializeLanguages(string languageCode);
        string GetTranslationByKey(string key, string languageCode = null);
        Dictionary<string, string>GetTranslationsByLanguage(string languageCode = null);
        string CreateTranslation(string value);
    }
    
    public class TranslationMediator : ITranslationMediator
    {
        private const string BASE_URL = "http://localhost:80/api/translations/drake/angular-js";
        private static Dictionary<string, LanguageKeys> translateCache = new Dictionary<string, LanguageKeys>();

        public void InitializeLanguages(string languageCode)
        {
            string baseUrl = $"{BASE_URL}/{languageCode}/file/?format=json";
            
            using (HttpClient client = new HttpClient())
            {
                if (translateCache.Count == 0 || !translateCache.ContainsKey(languageCode))
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync(baseUrl).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;

                            if (result != null && !string.IsNullOrEmpty(result))
                            {
                                var languageKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                                translateCache.Add(languageCode, new LanguageKeys { Translations = languageKeys });
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Erro na requisição: {e.Message}");
                    }
                }
                
            }
        }

        public string GetTranslationByKey(string key, string languageCode = null)
        {
            var result = String.Empty;
            
            if (languageCode == null)
            {
                languageCode = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            }

            if (translateCache.TryGetValue(languageCode, out LanguageKeys translations))
            {
                if (translations.Translations.TryGetValue(key, out string value))
                {
                    result = value;
                    return result;
                }

                CreateTranslation(key);

                return GetTranslationByKey(key, languageCode);
            }

            return key;
        }

        public Dictionary<string, string> GetTranslationsByLanguage(string languageCode = null)
        {
            if (languageCode == null)
            {
                languageCode = "pt";
            }

            var result = new Dictionary<string, string>();

            if (translateCache.TryGetValue(languageCode, out LanguageKeys language))
            {
                result = language.Translations;
            }
            else
            {
                InitializeLanguages(languageCode);
                
            }

            return result;
        }

        public string CreateTranslation(string value)
        {
            string baseUrl = $"{BASE_URL}/pt/units/";
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var requestBody = new { key = value, value = new[] { value }};

                    string json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(baseUrl, content).Result;
                    response.EnsureSuccessStatusCode();

                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na criação da tradução: {e.Message}");
                    return null;
                }
            }
        }
    }

    public class LanguageKeys
    {
        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();
    }
}