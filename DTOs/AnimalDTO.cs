using System.ComponentModel.DataAnnotations;

namespace apbd_6.DTOs;

public record GetAllAnimalsResponse(int IdAnimal, string Name,string? Description,string Category,string Area);

public record ReplaceAnimalRequest(
    [Required] [MaxLength(200)] string Name, 
    [MaxLength(200)] string Description, 
    [Required] [MaxLength(200)] string Category, 
    [Required] [MaxLength(200)] string Area 
    );


public record ControllerCreateAnimalRequest(
    [Required] [MaxLength(200)] string Name, 
    [MaxLength(200)] string? Description, 
    [Required] [MaxLength(200)] string Category, 
    [Required] [MaxLength(200)] string Area 

);


public record CreateAnimalResponse(int IdAnimal, string Name, string? Description, string Category, string Area)
{
    public CreateAnimalResponse(int IdAnimal, ControllerCreateAnimalRequest request): this(IdAnimal, request.Name, request.Description, request.Category, request.Area){}
};