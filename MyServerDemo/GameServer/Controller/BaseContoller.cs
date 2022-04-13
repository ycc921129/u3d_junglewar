using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    abstract class BaseContoller
    {
        protected RequestCode requestCode = RequestCode.None;
        public RequestCode GetRequestCode { get { return requestCode; } }
        public virtual string DefultHandler(string data, Client client,Server server) { return null; }
    }
}
