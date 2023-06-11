using Neo4j.Application.Dtos;
using Neo4j.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Neo4j.Application.Mappers;

[Mapper]
public partial class PersonMapper
{
    public partial PersonDto PersonToPersonDto(Person person);
    public partial Person PersonDtoToPerson(PersonDto personDto);
    public partial IEnumerable<PersonDto> PersonsToPersonsDto(IEnumerable<Person> persons);
}