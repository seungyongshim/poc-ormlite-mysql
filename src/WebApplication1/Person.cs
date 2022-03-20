using ServiceStack.DataAnnotations;

namespace WebApplication1;

public class Person
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
}
