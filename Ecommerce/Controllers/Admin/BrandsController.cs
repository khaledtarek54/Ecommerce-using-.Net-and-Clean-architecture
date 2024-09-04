using Ecommerce.Application.DTOs;
using Ecommerce.Application.Services;
using Ecommerce.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandsController(BrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> getAllBrandsAsync()
        {
            return Ok(await _brandService.GetAllBrandsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandByIdAsync(Guid id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        [HttpPost]
        public async Task<ActionResult> AddBrandAsync(BrandDto brandDto)
        {
            await _brandService.AddBrandAsync(brandDto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrandAsync(Guid id, BrandDto brandDto)
        {
            if (id != brandDto.Id)
            {
                return BadRequest();
            }
            await _brandService.UpdateBrandAsync(id, brandDto);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrandAsync(Guid id)
        {
            await _brandService.DeleteBrandAsync(id); 
            return NoContent();
        }
    }
}
