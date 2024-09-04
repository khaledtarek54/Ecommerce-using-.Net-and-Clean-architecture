using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class BrandService
    {
        private readonly IGenericRepository<Brand> _genericRepository;
        private readonly IMapper _mapper;

        public BrandService(IGenericRepository<Brand> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            var brand = await _genericRepository.GetByIdAsync(id, c => c.Products);
            return brand;
        }
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _genericRepository.GetAllAsync(c => c.Products);
        }
        public async Task AddBrandAsync(BrandDto brandDto)
        {
           var brand =  _mapper.Map<Brand>(brandDto);
           await _genericRepository.AddAsync(brand);
        }
        public async Task UpdateBrandAsync(Guid id ,BrandDto brandDto)
        {
            var brand = await _genericRepository.GetByIdAsync(id);
            _mapper.Map(brandDto,brand);
            brand.CreatedDate = brand.CreatedDate;
            await _genericRepository.UpdateAsync(brand);
        }
        public async Task DeleteBrandAsync(Guid id)
        {
            await _genericRepository.DeleteAsync(id);
        }
    }
}
