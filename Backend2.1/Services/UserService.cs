namespace Lovie.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string id);
    Task<User> AddUser(User user);
    Task DeleteUser(string id);
    Task UpdateUser(User user);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AddUser(User user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task DeleteUser(string id)
    {
        await _userRepository.DeleteUser(id);
    }

    public async Task<User> GetUser(string id)
    {
        return await _userRepository.GetUser(id);
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }

    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateUser(user);
    }
}