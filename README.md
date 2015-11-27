[![Build status](https://ci.appveyor.com/api/projects/status/github/dejanstojanovic/IP-Address-Filtering?branch=master&svg=true)](https://ci.appveyor.com/project/dejanstojanovic/ip-address-filtering/branch/master)

# IP-Address-Filtering
Lightweight C# ASP.NET MVC and Web API IP address filtering library

The library allows validating IP address against:
* Single IP address
* List of multiple IP addresses
* Single IP address range
* Multiple IP address ranges

## How to use

```cs
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("api/data/{recordId}")]
        [IPAddressFilter("94.201.50.212", IPAddressFilteringAction.Restrict)]
        public HttpResponseMessage GetData(int recordId)
        {
            /* Create response logic here */
            return this.Request.CreateResponse<object>(HttpStatusCode.OK, new object());
        }
    }
```

[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/dejanstojanovic/ip-address-filtering/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

