namespace Neo4j.Application.Dtos;

public record RegisterMarriageDto(string FirstPersonId, string SecondPersonId, MarriedRelationDto MarriedRelation);