using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System.Threading.Tasks;

namespace TestCustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllBrand());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(List<Brand> brand)
        {
            if (brand == null)
            {
                return BadRequest(); // Return a 400 Bad Request response if the request body is empty or invalid.
            }

             _brandService.InsertAsync(brand);
            return Ok(brand);

            // The following line is not needed as it's already handled in the Insert method of BrandService.
            // await _brandService.SaveChangesAsync(); // Save changes to the database

           // return CreatedAtAction(nameof(GetById), new { id = createdBrand.Id }, createdBrand);
        }
    }
}
