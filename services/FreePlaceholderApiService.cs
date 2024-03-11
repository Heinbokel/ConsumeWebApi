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

            // If the HTTP Response was successful...
            if (response.IsSuccessStatusCode)
            {
                // Read the 
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"DONE CALLING FOR TODO WITH ID {todoId}");

                // Use the JsonSerializer class to turn the JSON response from the API into our Todo class.
                return JsonSerializer.Deserialize<Todo>(responseBody);
            }
            else
            {
                // If the HTTP Response was not successful, we throw our custom ExternalServiceException detailing the error.
                throw new ExternalServiceException($"Error occured calling for todo with id {todoId}. {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Todo>> RetrieveTodos()
        {
            Console.WriteLine($"CALLING FOR TODOS");
            HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/todos");

            // If the HTTP Response was successful...
            if (response.IsSuccessStatusCode)
            {
                // Read the 
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"DONE CALLING FOR TODOS");

                // Use the JsonSerializer class to turn the JSON response from the API into our Todo class.
                return JsonSerializer.Deserialize<List<Todo>>(responseBody);
            }
            else
            {
                // If the HTTP Response was not successful, we throw our custom ExternalServiceException detailing the error.
                throw new ExternalServiceException($"Error occured calling for todos. {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteTodoById(int todoId)
        {
            Console.WriteLine($"CALLING TO DELETE TODO WITH ID {todoId}");
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{BASE_URL}/todos/{todoId}");

            // If the HTTP Response was successful...
            if (response.IsSuccessStatusCode)
            {
                // Read the 
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"DONE CALLING TO DELETE TODO WITH ID {todoId}");

                return;
            }
            else
            {
                // If the HTTP Response was not successful, we throw our custom ExternalServiceException detailing the error.
                throw new ExternalServiceException($"Error occured attempting to delete todo with ID {todoId}. {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

    }

}