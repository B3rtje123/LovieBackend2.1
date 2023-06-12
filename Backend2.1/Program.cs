var builder = WebApplication.CreateBuilder(args);

// Database Settings
var settings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(settings);

// intefaces
builder.Services.AddTransient<IMongoContext, MongoContext>(); // mongo context

// repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();

// services
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users", async (IUserService userService) =>
{
    List<User> users = await userService.GetUsers();
    return Results.Ok(users);
});

app.MapGet("/users/{id}", async (IUserService userService, string id) =>
{
    User user = await userService.GetUser(id);
    return Results.Ok(user);
});

app.MapPost("/users", async (IUserService userService, User user) =>
{
    User newUser = await userService.AddUser(user);
    return Results.Ok(newUser);
});

app.MapDelete("/users/{id}", async (IUserService userService, string id) =>
{
    await userService.DeleteUser(id);
    return Results.Ok();
});

app.MapPut("/users/{id}", async (IUserService userService, string id, User user) =>
{
    user.Id = id;
    await userService.UpdateUser(user);
    return Results.Ok();
});



app.Run();
