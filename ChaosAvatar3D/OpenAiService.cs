using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChaosAvatar3D
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public OpenAiService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            _httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            }
        }

        public async Task<string> GetChatCompletionAsync(object[] messages)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "Error: OPENAI_API_KEY environment variable not set.";
            }

            var requestBody = new
            {
                model = "gpt-4o",
                messages
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic? responseObject = JsonConvert.DeserializeObject(responseString);
                    string? assistantReply = responseObject?.choices[0].message.content;
                    return assistantReply ?? "No response from AI.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode} - {errorContent}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
