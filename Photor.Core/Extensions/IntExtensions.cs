using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Extensions
{
    public static class IntExtensions
    {
        public static string GetEmoji(this int integer)
        {
            string returnValue;

            switch (integer)
            {
                case 0:
                    returnValue = "0&#65039;&#8419;";
                    break;
                case 1:
                    returnValue = "1&#65039;&#8419;";
                    break;
                case 2:
                    returnValue = "2&#65039;&#8419;";
                    break;
                case 3:
                    returnValue = "3&#65039;&#8419;";
                    break;
                case 4:
                    returnValue = "4&#65039;&#8419;";
                    break;
                case 5:
                    returnValue = "5&#65039;&#8419;";
                    break;
                case 6:
                    returnValue = "6&#65039;&#8419;";
                    break;
                case 7:
                    returnValue = "7&#65039;&#8419;";
                    break;
                case 8:
                    returnValue = "8&#65039;&#8419;";
                    break;
                case 9:
                    returnValue = "9&#65039;&#8419;";
                    break;
                default:
                    returnValue = "9&#65039;&#8419;";
                    break;
            }

            return returnValue;
        }
    }
}
