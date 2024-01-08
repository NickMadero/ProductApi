using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;


namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // injecting the IProductDataStore interface into the constructor
        private readonly IProductDataStore _productDataStore;
        public ProductController(IProductDataStore productDataStore)
        {
            //instance of ProductDataStore has been created
                    _productDataStore = productDataStore;
        }


        // GET: api/<ProductController>
        // returns all products
        [HttpGet]
        public ActionResult <List<ProductModel>> GetProduct()
        {   
            if (!_productDataStore.Product.Any() )
            {
                return NotFound();
            }

            // Current is Static Variable that is why we can call through the class.
            return  Ok(_productDataStore.Product);

            // this endpoint is working 
        }

        // insert a new product to our dataStore
        [HttpPost("postProduct")]
        public ActionResult<ProductModel> PostProduct(ProductModel model)
        {
            //LINQ, storing max Id increment it by 1 
            var MaxProductId = _productDataStore.Product.Max(p => p.product_id + 1 );

            // create a new product object to be inserted into the ProductDataStore 
            ProductModel newProduct = new ProductModel()
            {
                product_id = MaxProductId,
                product_name = model.product_name,

            };
            // adding the new product to the productDataStore
            _productDataStore.Product.Add(newProduct);
            return Ok(newProduct);

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
