import ConfigParser
from neo4j.v1 import GraphDatabase, basic_auth

Config = ConfigParser.ConfigParser()
Config.read("neo.ini")

neo_server = Config.get('General','Server')
neo_username = Config.get('General','Username')
neo_pw = Config.get('General','Password')

driver = GraphDatabase.driver("bolt://"+neo_server, auth=basic_auth(neo_username,neo_pw))
session = driver.session()
#session.run("CREATE (a:Person {name:'Bob'})")
result = session.run("MATCH (a:Actor) RETURN a.name as name limit 3")
for record in result:
    print(record["name"])
session.close()