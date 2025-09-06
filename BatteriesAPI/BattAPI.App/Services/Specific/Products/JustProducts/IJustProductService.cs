using BattAPI.App.Services.Specific.Products.JustProducts.Models;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Products;

namespace BattAPI.App.Services.Specific.Products.JustProducts
{
    public interface IJustProductService : IDtoServiceBase<JustProductInput, JustProductDto, JustProductPatch>
    {
        Task<IList<JustProductDto>> ListOnlyBaseProductsAsync();

        Task DeleteProductOfAnyKindAsync(Guid id) => DeleteAsync(id);


        string? GetImageUrl(Product product);
    }
}
