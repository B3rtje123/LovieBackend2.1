namespace Models.ActivityLog;
public class ActivityLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("movement")]
    public string Movement { get; set; }


    [BsonElement("timestamp")]
    public DateTime TimeStamp { get; set; }
}
