using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber,string? SortBy, SortDirection SortDirection);
        Task<Restaurant?> GetRestaurantById(int id);
        Task<int> Create(Restaurant entity);
        Task Delete(Restaurant entity);

        Task SaveChanges();


    }
}
