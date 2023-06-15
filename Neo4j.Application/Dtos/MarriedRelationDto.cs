namespace Neo4j.Application.Dtos;

public class MarriedRelationDto : BaseRelationDto
{
    public DateTime At { get; set; }
    public string? Location { get; set; }
}