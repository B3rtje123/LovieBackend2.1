public interface IMongoContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<User> UserCollection { get; }

    IMongoCollection<ActivityLog> ActivityCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_settings.ConnectionString));
        settings.ConnectionMode = ConnectionMode.Automatic;
        _client = new MongoClient(settings);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<User> UserCollection
    {
        get
        {
            return _database.GetCollection<User>(_settings.UserCollection);
        }
    }

    public IMongoCollection<ActivityLog> ActivityCollection
    {
        get
        {
            return _database.GetCollection<ActivityLog>(_settings.ActivityCollection);
        }
    }
}