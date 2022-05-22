using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
             
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        } 

        public async Task<PagedList<MemberDto>> GetCraftsmenAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Include(s=>s.Craft).Include(x=>x.City).ThenInclude(g=>g.Governorate).ThenInclude(c=>c.Country)
            .Where(u => u.UserName != userParams.CurrentUsername &&u.CraftId >0);

            if(userParams.CityId>0)
            query = query.Where(u => u.CityId == userParams.CityId);

            else if(userParams.GovernorateId>0)
            query = query.Where(u => u.City.GovernorateId == userParams.GovernorateId);

            else if(userParams.CountryId>0)
            query = query.Where(u => u.City.Governorate.CountryId == userParams.CountryId);

            if(userParams.CraftId>0)
            query = query.Where(u => u.CraftId == userParams.CraftId);


            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };
            
            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper
                .ConfigurationProvider).AsNoTracking(), 
                    userParams.PageNumber, userParams.PageSize);
                    
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<int> GetUserCityId(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.CityId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
         public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}