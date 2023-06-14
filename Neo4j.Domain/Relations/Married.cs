namespace Neo4j.Domain.Relations;

public class Married : BaseRelation
{
    public DateTime At { get; set; }
    public string? Location { get; set; }
}