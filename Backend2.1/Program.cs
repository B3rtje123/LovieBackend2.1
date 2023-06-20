var builder = WebApplication.CreateBuilder(args);

// Database Settings
var settings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(settings);

// intefaces
builder.Services.AddTransient<IMongoContext, MongoContext>(); // mongo context

// repositories
builder.Services.AddTransient<IUserRepository, UserRepository>()
.AddTransient<IActivityRepository, ActivityRepository>();

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
    var oldUser = await userService.GetUser(id);
    foreach (var prop in user.GetType().GetProperties())
    {
        if (prop.GetValue(user) == null)
        {
            prop.SetValue(user, prop.GetValue(oldUser));
        }
    }
    await userService.UpdateUser(user);
    return Results.Ok(user);
});

app.MapGet("/activity/{userId}", async (IUserService userService, string userId) =>
{
    List<ActivityLog> activityLogs = await userService.GetActivityLogsByUserId(userId);
    return Results.Ok(activityLogs);
});

app.MapPost("/activity", async (IUserService userService, ActivityLog activityLog) =>
{
    //adding current time as timestamp
    activityLog.TimeStamp = DateTime.Now;

    ActivityLog newActivityLog = await userService.AddActivityLog(activityLog);
    return Results.Ok(newActivityLog);
});

app.MapGet("activity/latest/{userId}", async (IUserService userService, string userId) =>
{
    ActivityLog activityLog = await userService.GetLatestActivityLogByUserId(userId);
    return Results.Ok(activityLog);
});


app.Run();
