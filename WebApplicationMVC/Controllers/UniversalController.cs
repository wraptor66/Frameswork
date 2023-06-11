using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Programming.MethodRouting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FramesworkWebAPIDotNet7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversalController : ControllerBase
    {
        // GET: api/<UniversalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UniversalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UniversalController>
        [HttpPost]
        public string Post(string jobject)
        {
            MethodRouting methodRouting = new MethodRouting();
            var returnObject = methodRouting.ProcessRequest(jobject);
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(returnObject)}";
        }

        // PUT api/<UniversalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {          
        }

        // DELETE api/<UniversalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
