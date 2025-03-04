namespace Main.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }  // MngoDB ID

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("department")]
    public string Department { get; set; }
    
    [BsonElement("position")]
    public string Position { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; }
    
    [BsonElement("salary")]
    public string Salary { get; set; }
    
    [BsonElement("hiredate")]
    public string HireDate { get; set; }

    [BsonElement("dateofbirth")]
    public string DateOfBirth { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)] 
    public EmployeeType Type { get; set; }  // Enum for Contract/Permanent
}

public enum EmployeeType
{
    Contract,
    Permanent
}
