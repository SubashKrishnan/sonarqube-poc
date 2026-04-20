namespace SonarDemo.Api.Models;

public sealed record Product(int Id, string Name, decimal Price, DateTime CreatedAt);

public sealed record CreateProductRequest(string Name, decimal Price);
