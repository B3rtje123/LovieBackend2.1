namespace Models.User;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    
    [BsonElement("name")]
    public string Name { get; set; }    
    [BsonElement("service")]
    public int Service { get; set; }
    [BsonElement("status")]
    public string Status { get; set; }
}