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

        public async Task<Guid> ReportPost(Guid postId, string userId, string description)
        {
            var report = new UserPostReport()
            {
                PostId = postId,
                UserId = userId,
                Reason = description,
            };

            await repository
                .AddAsync(report);

            await repository.SaveChangesAsync();

            return report.Id;
        }
    }
}
