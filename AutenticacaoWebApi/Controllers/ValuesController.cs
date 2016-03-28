using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutenticacaoWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        [Authorize()]
        public string Get()
        {
            return User.Identity.Name;
        }
    }
}
