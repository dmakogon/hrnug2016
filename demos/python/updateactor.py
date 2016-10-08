import ConfigParser
import argparse
from neo4j.v1 import GraphDatabase, basic_auth

Config = ConfigParser.ConfigParser()
Config.read("neo.ini")

parser = argparse.ArgumentParser(description='Neo4j')
parser.add_argument('--actor', help='Actor name',required=True)
parser.add_argument('--age',help='Age', required=True)
args = parser.parse_args()

neo_server = Config.get('General','Server')
neo_username = Config.get('General','Username')
neo_pw = Config.get('General','Password')

driver = GraphDatabase.driver("bolt://"+neo_server, auth=basic_auth(neo_username,neo_pw))
session = driver.session()
#session.run("CREATE (a:Person {name:'Bob'})")
result = session.run("MATCH (a:Actor {name: {name}}) set a.age={age} return a.name, a.age", {"name":args.actor, "age": args.age})
for record in result:
    print(record)
session.close()