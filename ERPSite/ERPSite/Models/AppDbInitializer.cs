using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPSite.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {



        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            
            var role1 = new IdentityRole { Name = "Администратор" };
            var role2 = new IdentityRole { Name = "Обычный пользователь" };
            roleManager.Create(role1);
            roleManager.Create(role2);
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com", FName = "FAdmin", Name = "Admin", SurName = "SAdmin" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }
            var user = new ApplicationUser { Email = "user@gmail.com", UserName = "user@gmail.com", FName = "FUser", Name = "User", SurName = "SUser" };
            password = "ad46D_ewr3";
            var resultUser = userManager.Create(user, password);
            if (resultUser.Succeeded)
            {
                userManager.AddToRole(user.Id, role1.Name);
            }

            base.Seed(context);
        }
    }
}