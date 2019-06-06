using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DragonRun.Helpers
{
    public static class Helper
    {
        public static bool CheckValidEmail(string email)
        {
            return Regex.IsMatch(email, "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        }
    }
}
