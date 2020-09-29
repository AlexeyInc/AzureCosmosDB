using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCosmosDB.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace AzureCosmosDB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CosmosDbController : ControllerBase
    {
        private AzureCosmosDbSettings _cosmosDbSettings;

        public CosmosDbController(IOptions<AzureCosmosDbSettings> cosmosDbSettings)
        {
            _cosmosDbSettings = cosmosDbSettings.Value;
        }
         
        [HttpGet]
        public string Index()
        {
            return "Hello!";
        }

        [HttpPost]
        public async Task CreateDatabase()
        {
            CosmosClient cosmosClient = new CosmosClient(
                _cosmosDbSettings.CosmosUrl,
                _cosmosDbSettings.CosmosKey
                );

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_cosmosDbSettings.DatabaseName);

            Container container = await database.CreateContainerIfNotExistsAsync(
                "ChiSoftware", "/department", 400);

            dynamic testItem = new { 
                id = Guid.NewGuid().ToString(),
                department = ".Net",
                details = "long story short"
            };

            var response = await container.CreateItemAsync(testItem);
        }
    }
}