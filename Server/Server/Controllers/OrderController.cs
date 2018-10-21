using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RServer;

namespace Server.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        // GET api/[controller]
        [HttpGet]
        public JObject Get()
        {
            var lines = System.IO.File.ReadAllLines("OrderDataTable.txt");
            COrder[] orderDataTable = new COrder[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split("||");
                string address = tokens[0];
                string[] foodItems = tokens[1].Split("$");
                string specialRequest = tokens[2];
                int timeSlot = int.Parse(tokens[3]);
                string contactInfo = tokens[4];
                Console.WriteLine(address + " " + foodItems[0] + " " + specialRequest + " " + timeSlot + " " + contactInfo);
                orderDataTable[i] = new COrder(address, foodItems, specialRequest, timeSlot, contactInfo);
            }

            return JObject.Parse("{ Orders: " + JsonConvert.SerializeObject(orderDataTable) + " }");
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

        [HttpPost("{address}/{orders}/{request}/{timeslot}/{contactinfo}")]
        public void Post(string address, string orders, string request, int timeslot, string contactinfo)
        {
            var lines = System.IO.File.ReadAllLines("OrderDataTable.txt");
            StreamWriter sw = new StreamWriter(new FileStream("OrderDataTable.txt", FileMode.OpenOrCreate));
            for (int i = 0; i < lines.Length; i++)
                sw.WriteLine(lines[i]);
            sw.WriteLine(address + "||" + orders + "||" + request + "||" + timeslot + "||" + contactinfo);
            sw.Close();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}
