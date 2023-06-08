using Microsoft.AspNetCore.Mvc;
using Neo4j.Application.Services;
using Neo4j.Domain.Entities;

namespace Neo4Test.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    
    public PersonController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson(Person person)
    {
        return Ok(await _serviceManager.PersonService.AddPersonAsync(person));
    }
}