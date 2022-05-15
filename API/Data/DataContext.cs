
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data;

    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedCrafts(builder);
            this.SeedCountries(builder);
            this.SeedGovernorates(builder); 
            this.SeedCities(builder);            

            builder.Entity<Country>()
                .HasMany(g => g.Governorates)
                .WithOne(c=>c.Country)
                .HasForeignKey(g => g.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<Governorate>()
                .HasMany(c => c.Cities)
                .WithOne(g=>g.Governorate)
                .HasForeignKey(c => c.GovernorateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany(x => x.Connections)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();


            builder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.LikedUserId });

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyUtcDateTimeConverter();
        }
        public void SeedCrafts(ModelBuilder builder)  
        { 
             var Crafts = new List<Craft> 
             {
           new Craft {  
                Id=1,
                Name="Carpenter"
                },
                new Craft {  
                Id=2,
                Name="Plumber"
                },
                new Craft {  
                Id=3,
                Name="Electrician"
                },
                new Craft {  
                Id=4,
                Name="Tailor"
                },
                new Craft {  
                Id=5,
                Name="Builder"
                },new Craft {  
                Id=6,
                Name="Painter"
                }
                };
             builder.Entity<Craft>().HasData(Crafts);
        }
     public void SeedCountries(ModelBuilder builder)  
        { 
             var country = new Country 
            {  
                Id=1,
                Name="Egypt"
                };
             builder.Entity<Country>().HasData(country);
        }
        
public void SeedGovernorates(ModelBuilder builder)  
        {  
            var governorates = new List<Governorate>  
            {
                new Governorate {
                    Id=1,
                    Name= "Cairo" ,
                    CountryId=1      
                },
                new Governorate {
                    Id=2,
                    Name= "Giza" ,
                    CountryId=1         
                },
                new Governorate {
                    Id=3,
                    Name= "Alexandria",
                    CountryId=1          
                },
                new Governorate {
                    Id=4,
                    Name= "Al Beheira",
                    CountryId=1          
                } 
     
            };
             builder.Entity<Governorate>().HasData(governorates);
        }
         
      public void SeedCities(ModelBuilder builder)  
        { 
             var cities = new List <City>{ 
           new City {  
                Id=1,
                Name="Abaseya",
                GovernorateId=1    
                },
                new City {  
                Id=2,
                Name="Helwan",
                GovernorateId=1    
                },
                new City {  
                Id=3,
                Name="Maadi",
                GovernorateId=1    
                },
                new City {  
                Id=4,
                Name="Kerdasa",
                GovernorateId=2    
                },
                new City {  
                Id=5,
                Name="Dokki",
                GovernorateId=2    
                },
                new City {  
                Id=6,
                Name="Sheikh Zayed",
                GovernorateId=2    
                },new City {  
                Id=7,
                Name="Smouha",
                GovernorateId=3    
                },
                new City {  
                Id=8,
                Name="El-Agamy",
                GovernorateId=3    
                },
                new City {  
                Id=9,
                Name="Abees",
                GovernorateId=3    
                },
                new City {  
                Id=10,
                Name="Shubrakhit",
                GovernorateId=4    
                },
                new City {  
                Id=11,
                Name="Damanhour",
                GovernorateId=4    
                },
                new City {  
                Id=12,
                Name="Rashid",
                GovernorateId= 4   
                }};
             builder.Entity<City>().HasData(cities);
        }
    }
   

    public static class UtcDateAnnotation
    {
        private const String IsUtcAnnotation = "IsUtc";
        private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
          new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
          new ValueConverter<DateTime?, DateTime?>(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

        public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, Boolean isUtc = true) =>
          builder.HasAnnotation(IsUtcAnnotation, isUtc);

        public static Boolean IsUtc(this IMutableProperty property) =>
          ((Boolean?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

        /// <summary>
        /// Make sure this is called after configuring all your entities.
        /// </summary>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (!property.IsUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(UtcConverter);
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcNullableConverter);
                    }
                }
            }
        }
    }
