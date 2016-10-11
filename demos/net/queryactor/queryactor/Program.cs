using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 
namespace queryactor
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var authToken = AuthTokens.Basic(appSettings["Username"], appSettings["Password"]);
            using (var driver = GraphDatabase.Driver("bolt://"
                + appSettings["Server"], authToken)

             )
            using (var session = driver.Session())
            {
                var myTitle = "The Matrix";
                var result = session.Run("MATCH (a:Actor)-[:ACTS_IN]->(m:Movie) where m.title = {title} return a.name  limit 15",
                    new Dictionary<string, object> { { "title", myTitle } });

                    foreach (var record in result)
                    {
                        var list = record.Keys.Select(key => $"{key}: {record[key]}").ToList();
                        Console.WriteLine(string.Join(", ", list));
                        
            
                    }
                Console.ReadLine();
            }
        }
    }
}
