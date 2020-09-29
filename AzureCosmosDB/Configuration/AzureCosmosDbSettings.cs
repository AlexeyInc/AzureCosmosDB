using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosDB.Configuration
{
    public class AzureCosmosDbSettings
    {
        public string CosmosUrl { get; set; }

        public string CosmosKey { get; set; }
        public string DatabaseName { get; set; }
    }
}
