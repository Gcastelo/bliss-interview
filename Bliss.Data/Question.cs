using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Bliss.Data
{
    public class Question
    {
 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id {get; set;}
        public String ImageUrl {get;set;}
        public String ThumbUrl {get;set;}

        [JsonPropertyName("Question")]
        [BsonElement("Question")]
        public String QuestionText {get;set;}
        public DateTime PublishedAt {get;set;}
        public IEnumerable<Choices> Choices {get;set;}

    }
}
