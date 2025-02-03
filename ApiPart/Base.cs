using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;

namespace ApiPart
{
    public class Base
    {
        protected RestClient? client;
        protected const string CONTENT_TYPE_APP_JSON = "application/json";
        protected const string CLIENT_END_POINT = "/client";
        protected readonly static string baseUrl = $"{ConfigurationManager.ConnectionStrings["basicUrl"]}";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(new RestClientOptions { BaseUrl = new Uri(baseUrl) });
        }
        [TearDown]
        public void TearDown()    
        {
            client?.Dispose();
        }
    }

}
