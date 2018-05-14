using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IO;

namespace Connection
{
    public class Service : ServiceBase
    {
        public static FileStream FileAuthentication { get; set; }
        public Service() : base (ConnectionBase.GetFactory()) {

        }
    }
}
