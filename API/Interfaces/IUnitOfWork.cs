using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Craft> CraftRepository {get; }
        IBaseRepository<Country> CountryRepository {get; }
        IBaseRepository<Governorate> GovernorateRepository {get; }
        IBaseRepository<City> CityRepository {get; }
        IUserRepository UserRepository {get; }
        IMessageRepository MessageRepository {get;}
        ILikesRepository LikesRepository {get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}