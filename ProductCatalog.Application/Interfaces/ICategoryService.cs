﻿using ProductCatalog.Application.DTOs;

namespace ProductCatalog.Application.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<CategoryDTO> GetCategoryById(int? id);
    Task Add(CategoryDTO categoryDTO);
    Task Update(CategoryDTO categoryDTO);
    Task Remove(int? id);
}
