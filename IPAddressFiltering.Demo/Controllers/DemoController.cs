using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPAddressFiltering.Demo.Controllers
{
    public class DemoController : ApiController
    {
        [IPAddressFilter("ipFIlter")]
        public HttpResponseMessage Get()
        {
           return Request.CreateResponse(HttpStatusCode.OK, "Hello");
        }
    }
}
