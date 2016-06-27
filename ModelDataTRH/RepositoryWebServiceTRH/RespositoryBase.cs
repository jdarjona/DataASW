using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryWebServiceTRH
{
    public class RespositoryBase
    {

        public RespositoryBase(HostWebService hostRespositorio)
        {

            Context.CreateContext(hostRespositorio);
        }
    }
}
