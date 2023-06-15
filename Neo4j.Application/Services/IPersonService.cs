using Neo4j.Application.Dtos;
using Neo4j.Domain.Entities;

namespace Neo4j.Application.Services;

public interface IPersonService
{
    Task<PersonDto> AddPersonAsync(PersonDto person);
    Task<IEnumerable<PersonDto>> GetAllPersonsAsync();
    Task RegisterMarriageAsync(string firstPersonId, string secondPersonId, MarriedRelationDto marriedRelation);
}