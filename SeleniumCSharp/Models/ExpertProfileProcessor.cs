using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeleniumCSharp.FunctionalTests.Models
{
    internal class ExpertProfileProcessor
    {
        public string NameSurname { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

    }
    internal class TestExpertProfileProcessor
    {
        public List<ExpertProfileProcessor> expert { get; set; }
        public static TestExpertProfileProcessor LoadFromJson(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TestExpertProfileProcessor>(jsonData);
        }

    }
}
