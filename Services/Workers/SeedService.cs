using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UncleApp.Context;
using Microsoft.EntityFrameworkCore;

namespace UncleApp.Services.Workers;

public class StartUpData
{

    public static void IntialData(IServiceProvider service)
    {
        using (DataContext c = new DataContext())
        {
            
            var temp = c.Users.FirstOrDefault(p => p.UserName.Equals("joker@gmail.com"));
            if (temp != null)
            {
                IdentityUser i = new IdentityUser()
                {
                    UserName = "joker@gmail.com",
                    NormalizedUserName = "JOKER@GMAIL.COM",
                    Email = "joker@gmail.com",
                    NormalizedEmail = "JOKER@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "09254489334",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var p = new PasswordHasher<IdentityUser>();
                var hashed = p.HashPassword(i, "Kyawayu2001@");
                i.PasswordHash = hashed;
                var userstore = new UserStore<IdentityUser>(c);
                var result = userstore.CreateAsync(i);
                AssignRoles(service, i.Email, new String[] { "Admin", "Shopkeeper" });
                c.SaveChangesAsync();
            }
        }


    }
    public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
    {
        UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();
        IdentityUser user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result;
    }
}