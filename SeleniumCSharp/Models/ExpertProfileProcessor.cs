using Newtonsoft.Json;

namespace SeleniumCSharp.FunctionalTests.Models
{
    public class ExpertProfileProcessor
    {
        public string NameSurname { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }

    public class TestExpertProfileProcessor
    {
        public List<ExpertProfileProcessor> expert { get; set; }

        public static TestExpertProfileProcessor LoadFromJson(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TestExpertProfileProcessor>(jsonData);
        }
    }
}