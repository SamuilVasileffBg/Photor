using Photor.Infrastructure.Data.Models;

namespace Photor.Core.Models.Report
{
    public class ManageReportViewModel
    {
        public Guid Id { get; set; }

        public int? Page { get; set; }

        public int AllMatchesCount { get; set; }

        public bool Newest { get; set; }

        public string? ReturnUrl { get; set; }

        public UserPostReport Report { get; set; } = null!;
    }
}
