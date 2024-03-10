using ConsumeWebApi.models.responses;
using ConsumeWebApi.services;

// Instantiate our service that calls the external API.
FreePlaceholderApiService freePlaceholderApiService = new FreePlaceholderApiService();

// Call for a single todo and wait for it to complete.
Todo? myTodo = await freePlaceholderApiService.RetrieveTodoById(1);

Console.WriteLine($"TODO ID: {myTodo?.Id} | TITLE: {myTodo?.Title} | COMPLETED: {myTodo?.IsComplete} | USER ID: {myTodo?.UserId}");

// Call for several todo's at the same time.
Task<Todo?> todoTask1 = freePlaceholderApiService.RetrieveTodoById(2);
Task<Todo?> todoTask2 = freePlaceholderApiService.RetrieveTodoById(3);
Task<Todo?> todoTask3 = freePlaceholderApiService.RetrieveTodoById(4);
Task<Todo?> todoTask4 = freePlaceholderApiService.RetrieveTodoById(5);
Task<Todo?> todoTask5 = freePlaceholderApiService.RetrieveTodoById(6);

// Store all tasks in a collection.
List<Task<Todo?>> todoTasks = [todoTask1, todoTask2, todoTask3, todoTask4, todoTask5];

// Await all tasks to complete before continuing.
await Task.WhenAll(todoTasks);

// Write each todo to the console.
todoTasks.ForEach(task => {
    Todo? todo = task.Result;
    Console.WriteLine($"TODO ID: {todo?.Id} | TITLE: {todo?.Title} | COMPLETED: {todo?.IsComplete} | USER ID: {todo?.UserId}");
});

