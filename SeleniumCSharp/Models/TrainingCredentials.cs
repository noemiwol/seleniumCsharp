using Newtonsoft.Json;

namespace SeleniumCSharp.FunctionalTests.Models
{
    public class TrainingCredentials
    {
        public string Name { get; set; }
    }

    public class TestTrainingData
    {
        public List<TrainingCredentials> training { get; set; }

        public static TestTrainingData LoadFromJson(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TestTrainingData>(jsonData);
        }
    }
}