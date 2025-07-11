using BattAPI.App.Specific.Products.Models;

namespace BattAPI.App.Specific.Products
{
    public interface IProductService
    {
        Task<IList<GeneralProductDto>> ListAsync();
    }
}
