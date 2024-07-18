using CrudOperationsInNetCore.Context;
using CrudOperationsInNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperationsInNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;

        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var brands = await _dbContext.Brands.ToListAsync();
            if (brands == null || brands.Count == 0)
            {
                return NotFound();
            }

            return brands;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        [HttpPost]
        public async Task<IActionResult> PostBrand(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]   

        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool BrandAvailable(int id)

        {
            return (_dbContext.Brands?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteBrand(int id)
        {
            
           if(_dbContext.Brands == null)
            {
                return NotFound();
            }

           var brand = await _dbContext.Brands.FindAsync(id);

            if(brand == null)
            {
                return NotFound();
            }

            _dbContext.Brands.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
