using System.Data.SqlClient;
using apbd_6.DTOs;
using Dapper;
using FluentValidation;

namespace apbd_6.Endpoints;

public static class AnimalEndpoints
{
    public static void RegisterAnimalEndpoints(this WebApplication app)
    {
        
        app.MapGet("api/animals", (IConfiguration configuration, string orderBy = "name") =>
        {
            var animals = new List<GetAllAnimalsResponse>();
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                string query = "SELECT * FROM ANIMAL";
                
                List<string> avalibleOrderByOptions = new List<string>() {"name","description","category","area"};
                if (!avalibleOrderByOptions.Contains(orderBy))
                {
                    return Results.BadRequest("Wrong Order By Value");
                }

                query += $" ORDER BY {orderBy}";
                

                var sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    animals.Add(new GetAllAnimalsResponse(
                         
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.IsDBNull(2) ? null : reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            
                            ))
                    ;
                }

                return Results.Ok(animals);
            }
        });
        
       
        
        app.MapPut("api/animals/{idAnimal:int}", (IConfiguration configuration, ReplaceAnimalRequest request,
            IValidator<ReplaceAnimalRequest> validator, int idAnimal) =>
        {
            var validation = validator.Validate(request);
            if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());
            
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var affectedRows = sqlConnection.Execute(
                    "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @Id",
                    new
                    {
                        Name = request.Name, 
                        Description = request.Description, 
                        Category = request.Category,
                        Area = request.Area, 
                        Id = idAnimal
                    }
                );
            
                if (affectedRows == 0) return Results.NotFound();
            }
        
            return Results.NoContent();
            
        });

        
        
        app.MapDelete("api/animals/{idAnimal:int}", (IConfiguration configuration, int idAnimal) =>
        {
            
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var affectedRows = sqlConnection.Execute(
                    "DELETE FROM Animal WHERE IdAnimal = @Id",
                    new
                    {
                        Id = idAnimal
                    }
                );
            
                if (affectedRows == 0) return Results.NotFound();
            }
        
            return Results.NoContent();
            
        });
        
    }
}