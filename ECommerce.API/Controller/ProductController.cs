using ECommerce.Respiratory.Entities;
using ECommerce.Respiratory.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = _unitOfWork.ProductRepository.Get().ToList();
            return Ok(products);
        }

        // GET: api/Product/page
        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsPaginated(int pageIndex = 1, int pageSize = 10)
        {
            var products = _unitOfWork.ProductRepository.Get(
                null,
                q => q.OrderBy(p => p.ProductId),
                "",
                pageIndex,
                pageSize
            ).ToList();

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.Save();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _unitOfWork.ProductRepository.Update(product);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        // POST: api/Product/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadProductImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { file.FileName, file.Length });
        }

        // POST: api/Product/delete-image
        [HttpPost("delete-image")]
        public async Task<IActionResult> DeleteProductImage([FromBody] string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok("File deleted.");
            }

            return NotFound("File not found.");
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.ProductRepository.GetByID(id) != null;
        }
    }
}
