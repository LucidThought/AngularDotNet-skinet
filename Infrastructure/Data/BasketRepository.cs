using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
  public class BasketRepository : IBasketRepository
  {
    private readonly IDatabase _database;
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
      return await _database.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
      var data = await _database.StringGetAsync(basketId);

      return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
      // Create or update a basket in REDIS database
      var created = await _database.StringSetAsync(basket.Id, 
        JsonSerializer.Serialize(basket), TimeSpan.FromDays(30)); // Expire basket after 30 days
      
      if (!created) return null; // Check to see if the basket was created\

      return await GetBasketAsync(basket.Id);
    }
  }
}