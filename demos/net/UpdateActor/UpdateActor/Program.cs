using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateActor
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
                var actorName = "Rob Lowe";
                var actorAge = 99;
                var result = session.Run("MATCH (a:Actor) where a.name = {actorname} set a.age = {actorage} return a.name,a.age",
                    new Dictionary<string, object> { { "actorname", actorName }, { "actorage", actorAge } });

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
