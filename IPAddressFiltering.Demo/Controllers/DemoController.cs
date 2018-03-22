using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPAddressFiltering.Demo.Controllers
{

    [IPAddressFilter("ipFIlter")]
    public class DemoController : ApiController
    {
        public HttpResponseMessage Get()
        {
           return Request.CreateResponse(HttpStatusCode.OK, "Hello");
        }
    }
}
