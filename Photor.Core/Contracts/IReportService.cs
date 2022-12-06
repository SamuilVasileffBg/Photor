using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface IReportService
    {
        public Task<Guid> ReportPost(Guid postId, string userId, string description);

        public Task<UserPostReport?> GetReportAsync(int page, bool newestFirst);

        public Task DeleteReportAsync(Guid id);

        public Task<UserPostReport?> GetReportAsync(Guid id);

        public Task<int> AllReportsCountAsync();
    }
}
