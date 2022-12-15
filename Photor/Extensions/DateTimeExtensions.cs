namespace Photor.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetDateTimeDifferenceText(this DateTime dateTime)
        {
            var delta = DateTime.UtcNow - dateTime;

            var deltaYears = Math.Floor(delta.TotalSeconds / 31556926);
            var deltaMonths = Math.Floor(delta.TotalSeconds / 2629743.83);
            var deltaDays = Math.Floor(delta.TotalSeconds / 86400);
            var deltaHours = Math.Floor(delta.TotalSeconds / 3600);
            var deltaMinutes = Math.Floor(delta.TotalSeconds / 60);
            var deltaSeconds = Math.Floor(delta.TotalSeconds);

            if (deltaYears > 1)
            {
                return $"{deltaYears} years ago";
            }
            else if (deltaYears == 1)
            {
                return $"{deltaYears} year ago";
            }
            else if (deltaMonths > 1)
            {
                return $"{deltaMonths} months ago";
            }
            else if (deltaMonths == 1)
            {
                return $"{deltaMonths} month ago";
            }
            else if (deltaDays > 1)
            {
                return $"{deltaDays} days ago";
            }
            else if (deltaDays == 1)
            {
                return $"{deltaDays} day ago";
            }
            else if (deltaHours > 1)
            {
                return $"{deltaHours} hours ago";
            }
            else if (deltaHours == 1)
            {
                return $"{deltaHours} hours ago";
            }
            else if (deltaMinutes > 1)
            {
                return $"{deltaMinutes} minutes ago";
            }
            else if (deltaMinutes == 1)
            {
                return $"{deltaMinutes} minute ago";
            }
            else if (deltaSeconds > 1)
            {
                return $"{deltaSeconds} seconds ago";
            }
            else
            {
                return $"1 second ago";
            }
        }
    }
}
