using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class CategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            _mapper.Map(categoryDto, category);
            category.CreatedDate = category.CreatedDate;
            await _categoryRepository.UpdateAsync(category);
        }
            
        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
