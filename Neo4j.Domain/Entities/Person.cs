using Newtonsoft.Json;

namespace Neo4j.Domain.Entities;

public class Person : BaseEntity
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public byte Age { get; set; }
    public bool IsDeleted { get; set; } = false;
}