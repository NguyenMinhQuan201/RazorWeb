using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common.VM
{
    public class Token
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
    public class GetToken
    {
        public string message { get; set; }
        public Token token { get; set; }
    }
}
