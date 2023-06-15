using Neo4j.Application.Dtos;
using Neo4j.Application.Mappers;
using Neo4j.Application.Repositories;
using Neo4j.Domain.Entities;
using Neo4j.Domain.Relations;

namespace Neo4j.Application.Services.Impl;

public class PersonService : IPersonService
{
    private readonly IRepositoryManager _repositoryManager;
    
    public PersonService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<PersonDto> AddPersonAsync(PersonDto personDto)
    {
        PersonMapper personMapper = new PersonMapper();
        var person = personMapper.PersonDtoToPerson(personDto);
        return personMapper.PersonToPersonDto(await _repositoryManager.PersonRepository.AddAsync(person));
    }

    public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
    {
        PersonMapper personMapper = new PersonMapper();
        var persons = personMapper.PersonsToPersonsDto(await _repositoryManager.PersonRepository.GetAllAsync());
        return persons;
    }

    public async Task RegisterMarriageAsync(string firstPersonId, string secondPersonId, MarriedRelationDto marriedRelation)
    {
        MarriedRelationMapper marriedRelationMapper = new MarriedRelationMapper();
        await _repositoryManager.PersonRepository.AddWithRelationAsync<Married, Person>(
            p1 => p1.Id == firstPersonId,
            p2 => p2.Id == secondPersonId,
            marriedRelationMapper.MarriedRelationDtoToMarried(marriedRelation));
    }
}