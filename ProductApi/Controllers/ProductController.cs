using DataLayer;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using System.Data.SqlClient;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase 
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IConfiguration _config; 
      
        private readonly IProductDataStore _productDataStore;
       

        public ProductController(IProductDataStore productDataStore, IProductRepository ProductRepository, 
            IConfiguration config)
        {
            //instance of ProductDataStore has been created
            _productDataStore = productDataStore;
            _ProductRepository = ProductRepository;
            _config = config;
        }


        // GET: api/<ProductController>
        // returns all products
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {

            string sql = "SELECT * FROM product";
            // var products = await _dataAccess.LoadData<ProductModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));
            var products = await _ProductRepository.LoadData<ProductModel, dynamic>(sql, new { });

            return Ok(products);
        }

        // insert a new product to our dataStore
        [HttpPost("postProduct")]
        public async Task<IActionResult> PostProduct( ProductModel product)
        {
            string sql = "INSERT INTO product (product_name) VALUES (@product_name);";
            var newProduct = new {product_name =  product.product_name};
            await _ProductRepository.SaveData(sql, newProduct);


            // create a new product object to be inserted into the ProductDataStore 
            //ProductModel newProduct = new ProductModel()
            //{
            //   // product_id = MaxProductId,
            //    product_name = productname,

            //};
            // adding the new product to the productDataStore
            // _productDataStore.Product.Add(newProduct);
            return Ok("new product added sucessfully");

            //this endpoint is working
        }

        //return a single product
        [HttpGet("{id}")]
        public ActionResult<ProductModel> SpecificProduct(int id) 
        {
            // this variable uses LINQ Language Intergrated Query. basically mysql code but written out in c#
            var ProductQuery = from product in _productDataStore.Product
                               where product.product_id == id
                               select product;
            return Ok(ProductQuery);

            //this endpoint is working


        }

        // delete a product
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            // LINQ FirstOrDefault gets the first occurance of the id that matches the parameter id.
            var DeleteProduct = _productDataStore.Product.FirstOrDefault(p => p.product_id == id);
            _productDataStore.Product.Remove(DeleteProduct);
            return Ok($"'{DeleteProduct.product_name}' has been deleted");

            // this endpoint is working
        }

    }
}
