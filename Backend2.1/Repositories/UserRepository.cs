namespace Lovie.Repository;
public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string id);
    Task<User> CreateUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(string id);
}

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;

    public UserRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.UserCollection.Find(_ => true).ToListAsync();
    }

    public async Task<User> GetUser(string id)
    {
        return await _context.UserCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.UserCollection.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateUser(User user)
    {
        await _context.UserCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task DeleteUser(string id)
    {
        await _context.UserCollection.DeleteOneAsync(user => user.Id == id);
    }
}