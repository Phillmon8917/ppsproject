using PPSProject.Database;
using PPSProject.Models;

namespace PPSProject.Services
{
    // it returns data already stored in the database
    public class GitHubStubService : IGitHubService
    {
        private readonly ApplicationDbContext _db;
        public GitHubStubService(ApplicationDbContext db) => _db = db;

        public Task<IEnumerable<Activity>> GetLatestAsync(string dev) =>
            Task.FromResult(_db.Activities
                               .OrderByDescending(a => a.DateUtc)
                               .AsEnumerable());
    }
}
