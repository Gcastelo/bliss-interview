using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bliss.Data
{
    public abstract class Question
    {

        [Required]
        [JsonPropertyName("image_url")]
        public String ImageUrl { get; set; }
        [Required]
        [JsonPropertyName("thumb_url")]
        public String ThumbUrl { get; set; }

        [JsonPropertyName("question")]
        [BsonElement("Question")]
        [Required]
        public String QuestionText { get; set; }

    }

    public class NewQuestion : Question
    {
        [JsonPropertyName("choices")]
        public IList<string> Choices { get; set; }

    }

    public class SavedQuestion : Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //MongoDB Driver is not auto incrementing Int32 _id. Using ObjectId instead
        public string Id { get; set; }
        public DateTime PublishedAt { get; set; }
        public IList<ChoicesVotes> Choices { get; set; }
    }
}
