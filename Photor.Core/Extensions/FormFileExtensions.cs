using Microsoft.AspNetCore.Http;
using static Photor.Infrastructure.Data.Constants.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Extensions
{
    public static class FormFileExtensions
    {
        public static bool ImageFormatIsValid(this IFormFile image)
        {
            var extensionsAllowed = AllowedFormats.Split(", ");

            var result = extensionsAllowed.Any(e => image.FileName.EndsWith(e));

            return result;
        }
    }
}
