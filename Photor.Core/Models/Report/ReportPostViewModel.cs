using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.UserPostReport;

namespace Photor.Core.Models.Report
{
    public class ReportPostViewModel
    {
        [Required]
        [StringLength(ReasonMaxLength), MinLength(ReasonMinLength)]
        public string Reason { get; set; } = null!;

        [Required]
        public Guid PostId { get; set; }
    }
}
