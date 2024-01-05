
namespace ProductApi.Models
{
    public interface IProductDataStore
    {
        List<ProductModel> Product { get; set; }
    }
}