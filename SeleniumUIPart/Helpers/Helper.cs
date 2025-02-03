using System.Text.Json;
using SeleniumUIPart.DataModels;

namespace SeleniumUIPart.Helpers
{
    public class Helper
    {
        public static IEnumerable<T> JsonDataRetriever<T>(T a)
        {
            string dataName;
            switch (a)
            {
                case TestDataModel:
                    dataName = "TestData";
                    break;
                default:
                    throw new InvalidOperationException("Unsupported type");
            }
            string jsonDataName = $"{dataName}.json";


            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonDataName);
            string jsonString = File.ReadAllText(jsonFilePath);

            List<T> testDataModel = JsonSerializer.Deserialize<List<T>>(jsonString);

            foreach(T testdata in testDataModel)
            {
                yield return testdata;
            }
            
        }
    }
}
