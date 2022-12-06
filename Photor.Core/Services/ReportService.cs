using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository repository;

        public ReportService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> AllReportsCountAsync()
        {
            return await repository
                .All<UserPostReport>()
                .Where(r => r.IsDeleted == false)
                .CountAsync();
        }

        public async Task DeleteReportAsync(Guid id)
        {
            var report = await GetReportAsync(id);

            if (report == null)
            {
                throw new Exception("Post not found");
            }

            report.IsDeleted = true;

            await repository
                .SaveChangesAsync();
        }

        public async Task<UserPostReport?> GetReportAsync(int page, bool newestFirst)
        {
            var reports = repository
                .All<UserPostReport>()
                .Where(r => r.IsDeleted == false);

            if (newestFirst == true)
            {
                reports = reports
                    .OrderByDescending(r => r.DateTime);
            }
            else
            {
                reports = reports
                    .OrderBy(r => r.DateTime);
            }

            return await reports
                .Skip(page - 1)
                .Take(1)
                .Include(r => r.Post)
                .Include(r => r.User)
                .FirstOrDefaultAsync();
        }

        public async Task<UserPostReport?> GetReportAsync(Guid id)
        {
            return await repository
                .All<UserPostReport>()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Guid> ReportPost(Guid postId, string userId, string description)
        {
            var report = new UserPostReport()
            {
                PostId = postId,
                UserId = userId,
                Reason = description,
                DateTime = DateTime.UtcNow,
            };

            await repository
                .AddAsync(report);

            await repository.SaveChangesAsync();

            return report.Id;
        }
    }
}
