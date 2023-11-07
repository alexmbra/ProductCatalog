using ProductCatalog.Domain.Entities;
using FluentAssertions;

namespace ProductCatalog.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact]
    public void CreateCategory_WithValidaParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");

        action.Should().NotThrow<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Category(-1, "Category Name");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Category(1, "Ca");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Too short. Minimum 3 characters");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Category(1, "");

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_ResultDomainExceptionInvalid()
    {
        Action action = () => new Category(1, null);

        action.Should().Throw<ProductCatalog.Domain.Validation.DomainExceptionValidation>();
    }
}