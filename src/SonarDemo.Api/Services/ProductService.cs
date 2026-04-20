using SonarDemo.Api.Models;

namespace SonarDemo.Api.Services;

public sealed class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Notebook", 12.50m, DateTime.UtcNow),
        new Product(2, "Pen", 1.99m, DateTime.UtcNow),
    };

    private int _nextId = 3;

    public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Create(CreateProductRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required.", nameof(request));
        }
        if (request.Price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(request), "Price cannot be negative.");
        }

        var product = new Product(_nextId++, request.Name, request.Price, DateTime.UtcNow);
        _products.Add(product);
        return product;
    }
}
