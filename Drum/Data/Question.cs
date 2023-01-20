using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drum.Data
{
    public class Question
    {

        [BsonIgnoreIfDefault]
        [BsonId]
        ObjectId _id;

        public Question(string quest, string answer)
        {
            Quest = quest;
            Answer = answer;
        }

        public string Quest { get; set; }
        public string Answer { get; set; }
    }
}
