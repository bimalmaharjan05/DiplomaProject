using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Services
{
    public static class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                DataService<Profile> profileService = new DataService<Profile>();

                //add customer role
                if( await roleManager.FindByNameAsync("Customer") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }
                //add default customer
                if (await userManger.FindByNameAsync("customer") == null)
                {
                    IdentityUser customer = new IdentityUser("customer");
                    customer.Email = "customer@yahoo.com";
                    await userManger.CreateAsync(customer, "Apple3###");
                    //add profile
                    Profile customerProfile = new Profile
                    {
                        FirstName = "customer",
                        LastName = "Default",
                        UserId = customer.Id
                    };
                    profileService.Create(customerProfile);
                    //add this default customer to Customer role
                    await userManger.AddToRoleAsync(customer, "Customer");
                }

                //add admin role
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                //add default admin
                if (await userManger.FindByNameAsync("admin") == null)
                {
                    IdentityUser admin = new IdentityUser("admin");
                    admin.Email = "admin@yahoo.com";
                    await userManger.CreateAsync(admin, "Apple3###");
                    //add profile
                    Profile adminProfile = new Profile
                    {
                        FirstName = "Adminstrator",
                        LastName = "Default",
                        UserId = admin.Id
                    };
                    profileService.Create(adminProfile);
                    //add this default admin to Admin role
                    await userManger.AddToRoleAsync(admin, "Admin");
                }

            }

        }

    }
}
