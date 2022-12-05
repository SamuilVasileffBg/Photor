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
    }
}
