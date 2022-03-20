using System.Data;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.OrmLite;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase, IDisposable
{
    IDbConnection Db;

    public PeopleController(IDbConnection db)
    {
        Db = db;

        db.DropAndCreateTable<Person>();
    }

    [HttpGet]
    public Task<List<Person>> ListPeople() =>
        Db.LoadSelectAsync<Person>(x => true);

    [HttpGet("{id}")]
    public Task<Person?> GetPerson(Guid id) =>
        Db.LoadSingleByIdAsync<Person>(id);

    [HttpDelete("{id}")]
    public Task<int> DeletePerson(Guid id) =>
        Db.DeleteByIdAsync<Person>(id);

    [HttpPatch]
    public Task<int> UpdatePerson(Person person) =>
        Db.UpdateAsync(person);

    [HttpPatch("{id}/new-name")]
    public Task<int> UpdatePersonName(Guid id, string newName) =>
        Db.UpdateOnlyAsync(() => new Person { Name = newName },
                           person => person.Id == id);

    [HttpPut]
    public Task<long> InsertPerson(Person person) =>
        Db.InsertAsync(person);

    public void Dispose()
    {
        Db.Dispose();
    }
}
