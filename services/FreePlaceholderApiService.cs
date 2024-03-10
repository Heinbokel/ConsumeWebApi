using System.Text.Json;
using ConsumeWebApi.exceptions;
using ConsumeWebApi.models.responses;

namespace ConsumeWebApi.services
{

    public class FreePlaceholderApiService
    {
        private static readonly string BASE_URL = "https://jsonplaceholder.typicode.com";

        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<Todo?> RetrieveTodoById(int todoId)
        {
            Console.WriteLine("CALLING FOR TODO");
            HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/todos/{todoId}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("DONE CALLING FOR TODO");
                return JsonSerializer.Deserialize<Todo>(responseBody);
            }
            else
            {
                throw new ExternalServiceException($"Error occured calling for TODO's. {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

    }

}