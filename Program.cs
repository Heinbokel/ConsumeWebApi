using ConsumeWebApi.models.responses;
using ConsumeWebApi.services;

FreePlaceholderApiService freePlaceholderApiService = new FreePlaceholderApiService();

// Task<Todo?> myTodo = freePlaceholderApiService.RetrieveTodoById(1);

Todo myTodo = await freePlaceholderApiService.RetrieveTodoById(1);

// Console.WriteLine($"TITLE: {myTodo?.Title} | COMPLETED: {myTodo?.IsComplete} | USER ID: {myTodo?.UserId} | TODO ID: {myTodo?.Id}");