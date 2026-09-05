using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieWatchlistApi.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public required string Title { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<string> Genres { get; set; } = new();

    public int ReleaseYear { get; set; }

    public int DurationMinutes { get; set; }

    public double Rating { get; set; }

    public List<string> CaptionLanguages { get; set; } = new();

    public bool HasAudioDescription { get; set; }
}
