using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrushMe.Api.Controllers
{
    public class UserController : ApiController
    {
        //GET: User
        public string Get()
        {
            return "value";
        }

        //GET: Friends
        public string Friends()
        {
            return "";
        }
        
    }
}
