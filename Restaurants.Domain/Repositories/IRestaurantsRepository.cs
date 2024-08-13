using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber);
        Task<Restaurant?> GetRestaurantById(int id);
        Task<int> Create(Restaurant entity);
        Task Delete(Restaurant entity);

        Task SaveChanges();


    }
}
