using Newtonsoft.Json;

namespace Neo4j.Domain.Entities;

public class Person : BaseEntity
{
    [JsonProperty()]
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public byte Age { get; set; }
}