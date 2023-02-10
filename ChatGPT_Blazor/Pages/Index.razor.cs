using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http.Headers;
using OpenAI_API;
using System.Threading.Channels;

namespace ChatGPT_Blazor.Pages
{
    public partial class Index:ComponentBase
    {
        public string message { get; set; }
        public string generatedText { get; set; } = "Answer will be displayed here.";
        private readonly HttpClient _httpClient = new HttpClient();

        private async Task GetResponseFromGPT3()
        {
            generatedText = "Finding Answer...";
            try
            {
                string apiKey = "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";
                string answer = string.Empty;
                var openai = new OpenAIAPI(apiKey);
                CompletionRequest completion = new CompletionRequest();
                completion.Prompt = message;
                completion.Model = OpenAI_API.Model.DavinciText;
                completion.MaxTokens = 4000;
                var result = await openai.Completions.CreateCompletionAsync(completion);

                if (result != null)
                {
                    foreach (var item in result.Completions)
                    {
                        GPT3Response.Response = item.Text;
                    }
                    generatedText = GPT3Response.Response;
                }

                StateHasChanged();
            }
             catch (Exception ex)
            {

                throw ex;
            }
        }
        


        public static class GPT3Response
        {
            public static string Response { get; set; }
        }

    }
}
