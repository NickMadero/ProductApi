namespace ProductApi.Models
{
    public class ProductDataStore : IProductDataStore
    {

        // a pretend database


        public List<ProductModel> Product { get; set; }

        public static ProductDataStore Current { get; set; } = new ProductDataStore();

        public ProductDataStore()
        {

            Product = new List<ProductModel>()
            {
                new ProductModel()
                {
                    product_id = 1,
                    product_name = "Test",
                },
                new ProductModel()
                {
                    product_id = 2,
                    product_name = "phone",
                }
            };

        }
    }
}
