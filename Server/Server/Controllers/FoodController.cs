using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RServer;

namespace Server.Controllers
{
    [Route("food")]
    public class FoodController : Controller
    {
        // GET api/[controller]
        [HttpGet]
        public JObject Get()
        {
            var lines = System.IO.File.ReadAllLines("FoodTable.txt");
            FoodId[] foodTable = new FoodId[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split("||");
                string address = tokens[0];
                int id = int.Parse(tokens[1]);
                string name = tokens[2];
                foodTable[i] = new FoodId(address, id, name);
            }

            return JObject.Parse("{ FoodItems: " + JsonConvert.SerializeObject(foodTable) + " }");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}
