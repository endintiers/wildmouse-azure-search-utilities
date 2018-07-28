using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMouse.AzureSearch.UpdateIndexer
{
    class Program

    {
        public static IConfiguration _config;

        private static string SearchServiceName => _config["SearchServiceName"];
        private static string SearchServiceAPIKey => _config["SearchServiceAPIKey"];
        private static string IndexerName => _config["IndexerName"];
        private static string QueryTimeout => _config["QueryTimeout"];

        static void Main(string[] args)
        {
            GetConfiguration(args);

            try
            {
                var serviceClient = new SearchServiceClient(SearchServiceName, new SearchCredentials(SearchServiceAPIKey));
                var indexer = serviceClient.Indexers.Get(IndexerName);
                if (indexer.Parameters.Configuration.ContainsKey("queryTimeout"))
                    indexer.Parameters.Configuration["queryTimeout"] = QueryTimeout;
                else
                    indexer.Parameters.Configuration.Add("queryTimeout", QueryTimeout);
                serviceClient.Indexers.CreateOrUpdate(indexer);
                Console.WriteLine($"Updated Indexer {IndexerName}, Timeout {QueryTimeout} minutes");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    if (ex.Message.Contains("Forbidden"))
                        Console.WriteLine("Is your API key correct?");
                }
            }
            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }

        static void GetConfiguration(string[] args)
        {
            _config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", false, true)
                            .AddCommandLine(args)
                            .Build();
        }
    }
}
