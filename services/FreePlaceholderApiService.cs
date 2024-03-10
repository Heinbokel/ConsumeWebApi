using System.Text.Json;
using ConsumeWebApi.exceptions;
using ConsumeWebApi.models.responses;

namespace ConsumeWebApi.services
{

    public class FreePlaceholderApiService
    {
        // Store the base URL of the API we are calling so it is easy to reference.
        private static readonly string BASE_URL = "https://jsonplaceholder.typicode.com";

        // Store the HttpClient as a reusable, static field so it uses less resources.
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Retrieves a TODO by its ID from the json placeholder web API.
        /// </summary>
        /// <param name="todoId">The ID of the TODO to retrieve.</param>
        /// <returns>The Task<Todo?> to return.</returns>
        /// <exception cref="ExternalServiceException">Throws ExternalServiceException if the HTTP Response code is not 200.</exception>
        public async Task<Todo?> RetrieveTodoById(int todoId)
        {
            Console.WriteLine($"CALLING FOR TODO WITH ID {todoId}");
            HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/todos/{todoId}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"DONE CALLING FOR TODO WITH ID {todoId}");
                return JsonSerializer.Deserialize<Todo>(responseBody);
            }
            else
            {
                throw new ExternalServiceException($"Error occured calling for TODO's. {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

    }

}