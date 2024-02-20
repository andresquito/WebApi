using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Server.Migrations;
using Products.Server.Models;

namespace Products.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("createProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            //save the product to the database
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            //return a success message
            return Ok();
        }

        [HttpGet]
        [Route("listProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> ListProducts()
        {
            //Get the list of products from the database
            var products = await _context.Products.ToListAsync();

            //returns a list of products
            return Ok(products);
        }

        [HttpGet]
        [Route("getProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //get product from database
            Product product = await _context.Products.FindAsync(id);

            //return the product
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPut]
        [Route("updatetProduct")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            //Update the product in the database
            var existingProduct = await _context.Products.FindAsync(id);
            existingProduct!.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();

            //return a success message
            return Ok();
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //Delete product from database
            var deleteProduct = await _context.Products.FindAsync(id);
            _context.Products.Remove(deleteProduct!);

            await _context.SaveChangesAsync();

            //return a success message
            return Ok();
        }




    }
}
