using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drum.Data
{
    public class MongoExamples
    {
        public static void AddToDB(Question question)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("QuestionDB");
            var collection = database.GetCollection<Question>("Questions");
            collection.InsertOne(question);

        }

        public static List<Question> FindAll()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("QuestionDB");
            var collection = database.GetCollection<Question>("Questions");
            var list = collection.Find(x => true).ToList();
            return list;
        }

        /*public static Character Find(string name)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("CharacterDB");
            var collection = database.GetCollection<Character>("Characters");
            var one = collection.Find(x => x.Name == name).FirstOrDefault();
            switch (one.GetType().Name)
            {
                case "Rogue": return one as Rogue; break;
                case "Wizard": return one as Wizard; break;
                case "Warrior": return one as Warrior; break;
                default: break;
            }
            return one;
*//*
            Console.WriteLine($" {one?.Name} {one?.Email} {one?.Age} {one?.DriverCard}");*//*


        }
        public static void ReplaceByName(string name, Character character)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("CharacterDB");
            var collection = database.GetCollection<Character>("Characters");
            collection.ReplaceOne(x => x.Name == name, character);
        }*/
    }
}
