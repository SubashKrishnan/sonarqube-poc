using SonarDemo.Api.Models;
using SonarDemo.Api.Services;

namespace SonarDemo.Api.Tests;

public class ProductServiceTests
{
    [Fact]
    public void GetAll_ReturnsSeededProducts()
    {
        var sut = new ProductService();

        var result = sut.GetAll();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetById_ExistingId_ReturnsProduct()
    {
        var sut = new ProductService();

        var result = sut.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("Notebook", result!.Name);
    }

    [Fact]
    public void GetById_UnknownId_ReturnsNull()
    {
        var sut = new ProductService();

        var result = sut.GetById(9999);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ValidRequest_AddsAndReturnsProduct()
    {
        var sut = new ProductService();

        var created = sut.Create(new CreateProductRequest("Stapler", 4.25m));

        Assert.Equal("Stapler", created.Name);
        Assert.Equal(4.25m, created.Price);
        Assert.Equal(3, sut.GetAll().Count);
    }

    [Fact]
    public void Create_EmptyName_Throws()
    {
        var sut = new ProductService();

        Assert.Throws<ArgumentException>(() => sut.Create(new CreateProductRequest("", 1m)));
    }

    [Fact]
    public void Create_NegativePrice_Throws()
    {
        var sut = new ProductService();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.Create(new CreateProductRequest("Eraser", -1m)));
    }
}
