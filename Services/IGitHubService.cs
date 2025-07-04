using PPSProject.Database;
using PPSProject.Models;

namespace PPSProject.Services
{
    public interface IGitHubService
    {
        public interface IGitHubService
        {
            Task<IEnumerable<Activity>> GetLatestAsync(string developer);
        }
    }
}
