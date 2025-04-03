using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboDrive.Classes
{
    internal class LoginUser
    {
        public string LoginName { get; set; }
        public string TmpHash { get; set; }

        public LoginUser(string loginName, string tmpHash)
        {
            LoginName = loginName;
            TmpHash = tmpHash;
        }
    }
}
