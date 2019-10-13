using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using MSA.Entities.Entities;
using MSA.Entities.Message;
using Newtonsoft.Json;

namespace MSA.Api.Controllers
{
    [Route("api/product/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        [ActionName("Add")]
        public async Task<string> Add(string product)
        {
            //Debugger.Launch();
            Product model = JsonConvert.DeserializeObject<Product>(product);
            var result = await Globals.Router.Ask<string>(new AddMessage(model));
            return result;
        }
    }
}