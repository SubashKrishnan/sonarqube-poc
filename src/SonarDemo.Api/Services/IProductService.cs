using SonarDemo.Api.Models;

namespace SonarDemo.Api.Services;

public interface IProductService
{
    IReadOnlyList<Product> GetAll();
    Product? GetById(int id);
    Product Create(CreateProductRequest request);
}
