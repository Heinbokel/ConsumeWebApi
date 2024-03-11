using System.Text;
using System.Text.Json;
using ConsumeWebApi.exceptions;
using ConsumeWebApi.models.requests;
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
                // Read the response and output it to a string.
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
                // Read the response and output it to a string.
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

        public async Task<Todo> CreateTodo(TodoCreateRequest request) {
            Console.WriteLine($"CALLING TO CREATE TODO");

            // Serialize our TodoCreateRequest to JSON so it can be included as the HTTP Request Body.
            string jsonRequestBody = JsonSerializer.Serialize(request);

            // Create our HttpContent (the HTTP Request Body), including our TodoCreateRequest and some standard encoding
            // as well as define that we want to use application/json.
            HttpContent httpContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // Finally, make the HTTP request to create a new TODO.
            HttpResponseMessage response = await _httpClient.PostAsync($"{BASE_URL}/todos", httpContent);

            if (response.IsSuccessStatusCode) {
                // Read the response and output it to a string.
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Todo>(responseBody);
            } else {
                // If the HTTP Response was not successful, we throw our custom ExternalServiceException detailing the error.
                throw new ExternalServiceException($"Error occured attempting to create todo. {response.StatusCode}: {response.ReasonPhrase}");
            }

        }

        public async Task UpdateTodo(TodoCreateRequest request, int todoId) {
            Console.WriteLine($"CALLING TO UPDATE TODO");

            // Serialize our TodoCreateRequest to JSON so it can be included as the HTTP Request Body.
            string jsonRequestBody = JsonSerializer.Serialize(request);

            // Create our HttpContent (the HTTP Request Body), including our TodoCreateRequest and some standard encoding
            // as well as define that we want to use application/json.
            HttpContent httpContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // Finally, make the HTTP request to create a new TODO.
            HttpResponseMessage response = await _httpClient.PutAsync($"{BASE_URL}/todos/{todoId}", httpContent);

            if (response.IsSuccessStatusCode) {
                Console.WriteLine($"DONE CALLING TO UPDATE TODO");
                return;
            } else {
                // If the HTTP Response was not successful, we throw our custom ExternalServiceException detailing the error.
                throw new ExternalServiceException($"Error occured attempting to update todo with id {todoId}. {response.StatusCode}: {response.ReasonPhrase}");
            }

        }

        public async Task DeleteTodoById(int todoId)
        {
            Console.WriteLine($"CALLING TO DELETE TODO WITH ID {todoId}");
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{BASE_URL}/todos/{todoId}");

            // If the HTTP Response was successful...
            if (response.IsSuccessStatusCode)
            {
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