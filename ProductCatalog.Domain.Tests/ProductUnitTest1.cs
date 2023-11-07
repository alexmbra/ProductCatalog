using ProductCatalog.Domain.Entities;
using FluentAssertions;

namespace ProductCatalog.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidaParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");

        action.Should().NotThrow<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(1, "Ca", "Product Description", 9.99m, 99, "product image");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Too short. Minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_MissingNameValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "product image");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "product image");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");

        action.Should().NotThrow<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }


    [Fact]
    public void CreateProduct_WithNullImageName_ResultDomainExceptionInvalid()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);

        action.Should().NotThrow<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ResultDomainExceptionInvalidId(int value)
    {
        Action action = () => new Product(1, null, "Product Description", 9.99m, value, "product image");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }
}