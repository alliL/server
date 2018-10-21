using System;
using System.IO;
using Newtonsoft.Json;

namespace RServer
{

    public class Info
    {
        /*
        static void Main(String [] args) {
            Food test = new Food(1.0, "Burger");
            string convert = JsonConvert.SerializeObject(test);
            StreamWriter sw = new StreamWriter("Desktop\\JsonTest");
            sw.Write(convert);

        }
        */
    }

    [System.Serializable]
    public class COrder {
        public string Address;
        public string[] FoodItems;
        public string SpecialRequest;
        public int TimeSlot;
        public string ContactInfo;

        public COrder(string address = null, string[] foodItems = null, 
                      string specialRequest = null,
                      int timeSlot = -1, string contactInfo = null) {
            Address = address;
            FoodItems = foodItems;
            SpecialRequest = specialRequest;
            TimeSlot = timeSlot;
            ContactInfo = contactInfo;
        }
    }

    [System.Serializable]
    public class RInfo {
        public string Address;
        public string DisplayName;
        public int Capacity;
        public int[] TimeSlots;
        public Food[] FoodItems;

        public RInfo(string address = null, string displayName = null, 
                     int capacity = -1, int[] timeSlots = null, 
                     Food[] foodItems = null) {
            Address = address;
            DisplayName = displayName;
            Capacity = capacity;
            TimeSlots = timeSlots;
            FoodItems = foodItems;
        }
    }

    [System.Serializable]
    public class FoodId
    {
        public string Address;
        public int Id;
        public string Name;

        public FoodId(string address, int id, string name)
        {
            Address = address;
            Id = id;
            Name = name;
        }
    }

    [System.Serializable]
    public class Food {
        public string Description;
        public string Name;
        public float Price;
        public int[] Items;

        public Food(string description, string name, float price, int[] items) {
            Description = description;
            Name = name;
            Price = price;
            Items = items;
        }
    }
}
