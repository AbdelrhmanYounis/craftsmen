namespace API.Data;

    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Craftsman"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            
            var admin = new AppUser
            {
                UserName = "admin",
                CityId=1
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRoleAsync(admin,"Admin");
        }
    }

