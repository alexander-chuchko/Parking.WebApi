using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoolParking.UI
{
    public class Validation
    {
        public static bool IsValidId(string id)
        {
            return new Regex(@"^[A-Z]{2}-[0-9]{4}-[A-Z]{2}$").IsMatch(id);
        }

        public static bool IsPositive(string input) 
        {
            bool isPositive = false;
            if (decimal.TryParse(input, out decimal result))
            {
                isPositive = result > 0;
            }

            return isPositive;
        }

        public static bool IsRightIndex()
        {
            return true;
        }
    }
}
