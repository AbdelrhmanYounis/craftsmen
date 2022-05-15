namespace API.Data.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public IBaseRepository<Craft> CraftRepository => new BaseRepository<Craft>(_context);

        public IBaseRepository<Country> CountryRepository => new BaseRepository<Country>(_context);
        public IBaseRepository<Governorate> GovernorateRepository => new BaseRepository<Governorate>(_context);
        public IBaseRepository<City> CityRepository => new BaseRepository<City>(_context);
        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

        public ILikesRepository LikesRepository => new LikesRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();

            return changes;
        }
    }
