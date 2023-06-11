namespace Neo4j.Application.Dtos;

public class PersonDto : BaseDto
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public short Age { get; set; }
}