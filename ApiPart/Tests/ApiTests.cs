using ApiPart.Models;
using ApiPart.TestData;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ApiPart.Tests
{
    [TestFixture]
    public class ApiTests : Base
    {
        [Test, TestCaseSource(typeof(TestDataRetriever), nameof(TestDataRetriever.GetPositiveTestData))]
        public async Task CreateNewClient_Positive(RequestBody requestModel)
        {
            ResponseBody response = await SendApiCall(requestModel, CLIENT_END_POINT);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(response.Id, Is.GreaterThan(0));
            Assert.That(response.Name, Is.EqualTo(requestModel.Name));
            Assert.That(response.Email, Is.EqualTo(requestModel.Email));
            Assert.That(response.Balance, Is.EqualTo(requestModel.Balance));
        }

        [Test, TestCaseSource(typeof(TestDataRetriever), nameof(TestDataRetriever.GetNegativeTestData))]
        public async Task CreateNewClient_Negative(RequestBody requestModel)
        {
            ResponseBody response = await SendApiCall(requestModel, CLIENT_END_POINT);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test, TestCaseSource(typeof(TestDataRetriever), nameof(TestDataRetriever.GetPositiveTestData))]
        public async Task CreateNewClient_WithWrongHeader_Negative(RequestBody requestModel)
        {
            ResponseBody response = await SendApiCall(requestModel, CLIENT_END_POINT);

            Assert.IsNotNull(response);          
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest)
            .Or.EqualTo((int)HttpStatusCode.Unauthorized)
            .Or.EqualTo((int)HttpStatusCode.NotFound));
        }

        private static async Task<ResponseBody> SendApiCall(RequestBody requestBody, string clientEndPoint, string contentType = CONTENT_TYPE_APP_JSON)
        {
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(clientEndPoint);
            string jsonRequest = JsonConvert.SerializeObject(requestBody);

            request.AddHeader("Content-Type", contentType);
            request.AddJsonBody(jsonRequest);
            request.AddJsonBody(requestBody);

            ResponseBody response = new ResponseBody();

            try
            {
                response = await client.PostAsync<ResponseBody>(request);
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Requested Uri {baseUrl} with {clientEndPoint} end point is unsuccessful." +
                    $"Exception: {ex}");
            }
            return response;
        }
    }
}