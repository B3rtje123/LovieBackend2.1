namespace Lovie.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string id);
    Task<User> AddUser(User user);
    Task DeleteUser(string id);
    Task UpdateUser(User user);
    Task<ActivityLog> AddActivityLog(ActivityLog activityLog);
    Task<List<ActivityLog>> GetActivityLogsByUserId(string userId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IActivityRepository _activityRepository;

    public UserService(IUserRepository userRepository, IActivityRepository activityRepository)
    {
        _userRepository = userRepository;
        _activityRepository = activityRepository;
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

    public async Task<ActivityLog> AddActivityLog(ActivityLog activityLog)
    {
        return await _activityRepository.CreateActivityLog(activityLog);
    }

    public async Task<List<ActivityLog>> GetActivityLogsByUserId(string userId)
    {
        return await _activityRepository.GetActivityLogsByUserId(userId);
    }
}