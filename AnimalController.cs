using System.Data.SqlClient;
using apbd_6.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace apbd_6;
[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateAnimal(ControllerCreateAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "INSERT INTO Animal (Name, Description, Category, Area) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            if (request.Description == null)
            {
                sqlCommand.Parameters.AddWithValue("@2", DBNull.Value);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@2", request.Description);

            }
            
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Connection.Open();
            
            var id = sqlCommand.ExecuteScalar();
            
            return Created($"api/animal/{id}", new CreateAnimalResponse((int)id, request));
        }
    }
}