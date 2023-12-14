using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SeleniumCSharp.FunctionalTests.Models
{
    internal class TrainingCredentials
    {
        public string Name { get; set; }
    }

    internal class TestTrainingData
    {
        public List<TrainingCredentials> training { get; set; }

        public static TestTrainingData LoadFromJson(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TestTrainingData>(jsonData);
        }
    }
}
