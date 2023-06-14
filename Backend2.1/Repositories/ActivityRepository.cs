namespace Lovie.Repository;

public interface IActivityRepository
{
    Task<ActivityLog> CreateActivityLog(ActivityLog activityLog);
    Task<List<ActivityLog>> GetActivityLogsByUserId(string userId);
    Task<ActivityLog> GetLatestActivityLogByUserId(string userId);
}

public class ActivityRepository : IActivityRepository
{
    private readonly IMongoContext _context;

    public ActivityRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityLog>> GetActivityLogsByUserId(string userId)
    {
        return await _context.ActivityCollection.Find(a => a.UserId == userId).ToListAsync();
    }

    public async Task<ActivityLog> CreateActivityLog(ActivityLog activityLog)
    {
        await _context.ActivityCollection.InsertOneAsync(activityLog);
        return activityLog;
    }

    public async Task<ActivityLog> GetLatestActivityLogByUserId(string userId)
    {
        return await _context.ActivityCollection.Find(a => a.UserId == userId).SortByDescending(a => a.TimeStamp).FirstOrDefaultAsync();
    }
}
