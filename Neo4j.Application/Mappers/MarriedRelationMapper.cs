using Neo4j.Application.Dtos;
using Neo4j.Domain.Relations;
using Riok.Mapperly.Abstractions;

namespace Neo4j.Application.Mappers;

[Mapper]
public partial class MarriedRelationMapper
{
    public partial MarriedRelationDto MarriedToMarriedRelationDto(Married married);
    public partial Married MarriedRelationDtoToMarried(MarriedRelationDto marriedRelationDto);
}