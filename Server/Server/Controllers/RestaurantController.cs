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
    [Route("restaurant")]
    public class RestaurantController : Controller
    {

        // GET api/[controller]
        [HttpGet]
        public JObject Get()
        {
            var lines = System.IO.File.ReadAllLines("RestInfoTable.txt");
            RInfo[] restaurants = new RInfo[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] restaurantInfo = lines[i].Split("||");
                string address = restaurantInfo[0];
                string name = restaurantInfo[1];
                string[] timeSlotsUgly = restaurantInfo[2].Split(" ");
                int[] timeSlots = new int[timeSlotsUgly.Length];
                for(int j = 0; j < timeSlotsUgly.Length; j++)
                {
                    Console.WriteLine(name + " " + timeSlotsUgly[j]);
                    timeSlots[j] = int.Parse(timeSlotsUgly[j]);
                }
                int capacity = int.Parse(restaurantInfo[3]);
                Food[] foodItems = new Food[(restaurantInfo.Length - 4) / 4];
                for(int j = 4; j < restaurantInfo.Length; j += 4)
                {
                    string[] itemsUgly = restaurantInfo[j].Split(" ");
                    int[] items = new int[itemsUgly.Length];
                    for (int k = 0; k < items.Length; k++)
                    {
                        items[k] = int.Parse(itemsUgly[k]);
                    }
                    string description = restaurantInfo[j + 1];
                    string foodName = restaurantInfo[j + 2];
                    float price = float.Parse(restaurantInfo[j + 3]);
                    
                    Console.WriteLine(description + " " + foodName + " " + price + " " + items.ToString());
                    foodItems[(j - 1) / 4] = new Food(description, foodName, price, items);
                }
                restaurants[i] = new RInfo(address, name, capacity, timeSlots, foodItems);
            }
            string json = "{ restaurants: " + JsonConvert.SerializeObject(restaurants) + " }";
            return JObject.Parse(json);
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

        // PUT restaurant/timeslot/{time}
        [HttpPut("timeslot/{address}/{time}")]
        public void Put(string address, int time)
        {
            var lines = System.IO.File.ReadAllLines("RestInfoTable.txt");
            StreamWriter sw = new StreamWriter(new FileStream("RestInfoTable.txt", FileMode.OpenOrCreate));

            for (int i = 0; i < lines.Length; i++)
            {
                string[] restaurantInfo = lines[i].Split("||");
                // if the address is the same, change the time slot
                if (restaurantInfo[0].Equals(address))
                {
                    sw.Write(restaurantInfo[0] + "||" + restaurantInfo[1] + "||");
                    string[] timeSlotsUgly = restaurantInfo[2].Split(" ");
                    int[] timeSlots = new int[timeSlotsUgly.Length];
                    for (int j = 0; j < timeSlots.Length; j++)
                    {
                        timeSlots[j] = int.Parse(timeSlotsUgly[j]);
                    }
                    timeSlots[time]++; // LATER, ADD A CHECK IF WE EXCEED THE CAPACITY
                    for (int j = 0; j < timeSlots.Length - 1; j++)
                    {
                        sw.Write(timeSlots[j] + " ");
                    }
                    sw.Write(timeSlots[timeSlots.Length - 1]);
                    for (int j = 3; j < restaurantInfo.Length; j++)
                    {
                        sw.Write("||" + restaurantInfo[j]);
                    }
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine(lines[i]);
                }
            }

            sw.Close();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }

    
}
