using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Passenger.API.Controllers
{
    [Route("api/[controller]")]
    public class ValueController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>(){
                "value1",
                "value2",
                "value3",
                "value4"
            };
        }
    }
}