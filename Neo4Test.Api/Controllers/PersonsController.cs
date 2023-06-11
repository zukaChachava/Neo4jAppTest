using Microsoft.AspNetCore.Mvc;
using Neo4j.Application.Dtos;
using Neo4j.Application.Services;
using Neo4j.Domain.Entities;

namespace Neo4Test.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    
    public PersonsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson(PersonDto person)
    {
        return Ok(await _serviceManager.PersonService.AddPersonAsync(person));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        return Ok(await _serviceManager.PersonService.GetAllPersonsAsync());
    }
}