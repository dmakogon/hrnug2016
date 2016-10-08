var neo4j = require('neo4j-driver').v1;

var config = require('./config')
  , servername = config.connection.server
  , username = config.connection.username
  , password = config.connection.password


if (process.argv.length <= 2) {
    console.log("Usage: queryactors <movietitle>");
    process.exit(-1);
}

var title = process.argv[2];
var driver = neo4j.driver("bolt://novacc.eastus.cloudapp.azure.com", neo4j.auth.basic(username, password));

var session = driver.session();

session
  .run("match (a:Actor)-[:ACTS_IN]->(m:Movie) where m.title = '" + title + "' return a.name limit 15")
  .subscribe({
    onNext: function(record) {
     console.log(record._fields);
    },
    onCompleted: function() {
      // Completed!
      session.close();
      process.exit();
    },
    onError: function(error) {
      console.log(error);
    }
  });
