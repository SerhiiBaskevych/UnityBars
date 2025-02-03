using ApiPart.Models;
namespace ApiPart.TestData
{
    public class TestDataRetriever
    {
        public static IEnumerable<TestCaseData> GetPositiveTestData()
        {
            yield return new TestCaseData(new RequestBody { Name = "validUser", Email = "validUser@email.com", Balance = 0 });
            yield return new TestCaseData(new RequestBody { Name = "validUser1", Email = "validUser1@email.com", Balance = 1 });
            yield return new TestCaseData(new RequestBody { Name = "validUser99", Email = "validUser99@email.com", Balance = 99 });
        }

        public static IEnumerable<TestCaseData> GetNegativeTestData()
        {
            yield return new TestCaseData(new RequestBody { Name = "validUser", Email = "validUser@email.com", Balance = -1 });
            yield return new TestCaseData(new RequestBody { Name = "validUser1", Email = "1", Balance = 1 });
            yield return new TestCaseData(new RequestBody { Name = " ", Email = "validUser99@email.com", Balance = 99 });
        }
    }
}
