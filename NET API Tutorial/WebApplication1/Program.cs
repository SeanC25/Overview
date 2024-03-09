using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var todos = new List<Todo>();

app.MapGet("/todos", () => todos);


app.MapGet ("/todos/{id}", Results<Ok<Todo>, NotFound> (int id) =>
{
    var targetTodo = todos.SingleOrDefault (t => id == t.Id);
    return targetTodo is null
    ? TypedResults.NotFound()
    : TypedResults.Ok (targetTodo);
});

app.MapPost("/todos", (Todo task) =>
{
    todos.Add(task);
    return TypedResults.Created("/todos/{id}", task);
});


app.MapDelete("/todos/{id}", (int id) =>
{
    todos.RemoveAll (t => id == t.Id);
    return TypedResults.NoContent();

});


app.Run(); 

public record Todo(int Id, String Name, DateTime DueDate, bool IsCompleted);






// //miniumal hosing model 
// var builder = WebApplication.CreateBuilder(args);    //creates a builder object for configuring the web application. 
// // Configuring our host:  //

// //run method// 
// var app = builder.Build();  //creates a builder object for configuring the web application. 

// // app.MapGet("/", () => "Hello World!");  //defines a route for handling GET requests to the root URL ("/") of the web application. When a GET request is made to the root URL


// var todos = new List<Todo>(); //initialization of a list of Todo objects
// //initializes a new list of Todo objects. Here, Todo is a specific type representing a todo item.


// //defines a route handler for handling HTTP POST requests to the "/todos" endpoint in a web application

// app.MapPost("/todos", (Todo task) =>
// //maps an HTTP POST request to the "/todos" endpoint. When a POST request is made to this endpoint, the provided lambda expression (Todo task) => { ... } will be executed. Inside the lambda expression, task represents the data sent in the POST request body, which is expected to be of type Todo.


// {
//     todos.Add(task);
//     //adds the received task object (of type Todo) to the todos list.


//     return TypedResults.Created("/todos/{id}", task);
// });


// //After adding the task to the list, this line returns an HTTP response indicating that the resource has been created. It uses the TypedResults.Created method to generate a response with a status code of 201 (Created). The response also includes the location of the newly created resource in the "Location" header, which is set to "/todos/{id}". The {id} placeholder will be replaced with the actual ID of the newly created todo item.


// app.Run();   //starts the web application. Once started, the application will listen for incoming HTTP requests and route them according to the defined routes.

// //making a todo list application: 
// public record Todo(int Id, String Name, DateTime DueDate, bool IsCompleted);





// // public class Todo
// // {
// //     public int Id { get; set; }
// //     public string Name { get; set; }
// //     public DateTime DueDate { get; set; }
// //     public bool IsCompleted { get; set; }

// //     public Todo(int id, string name, DateTime dueDate, bool isCompleted)
// //     {
// //         Id = id;
// //         Name = name;
// //         DueDate = dueDate;
// //         IsCompleted = isCompleted;
// //     }
// // }
