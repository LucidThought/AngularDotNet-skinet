using Core.Entities;

namespace Core.Interfaces
{
  public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string backetId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string backetId);
    }
}