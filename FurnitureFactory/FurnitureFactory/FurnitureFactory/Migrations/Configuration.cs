namespace FurnitureFactory.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FurnitureFactory.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            ////create Role Admin able to edit stuff in the movie store manager
            //if (!context.Roles.Any(r => r.Name == "ContentAdmin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "ContentAdmin" };
            //    manager.Create(role);
            //    context.SaveChanges();
            //}
            ////create Master Admin role able to edit other users and all content
            //if (!context.Roles.Any(r => r.Name == "MasterAdmin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "MasterAdmin" };
            //    manager.Create(role);
            //    context.SaveChanges();
            //}
            ////create User role for normal users
            //if (!context.Roles.Any(r => r.Name == "User"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "User" };
            //    manager.Create(role);
            //    context.SaveChanges();
            //}

            //           context.Furniture.AddOrUpdate(p => p.BarCode,
            //new Furniture { Name = "Wooden Table", Description = "Large wooden table with decoration", Weight = 30.15m, BarCode = "00346223464", PricePerUnit = 135.12m },
            //new Furniture { Name = "Lounge Chair", Description = "Comfy lounge chair, custom built", Weight = 25.50m, BarCode = "00347784231", PricePerUnit = 112.33m },
            //new Furniture { Name = "Coffee Table", Description = "Small coffee table with glass top", Weight = 13.45m, BarCode = "00347334234", PricePerUnit = 78.99m },
            //new Furniture { Name = "Home Couch", Description = "Large leather couch, custom built", Weight = 90.44m, BarCode = "00347334232", PricePerUnit = 213.22m },
            //new Furniture { Name = "Book Shelf", Description = "Medium sized book shelf, wooden, custom built", Weight = 65.11m, BarCode = "00347366239", PricePerUnit = 90.33m }
            //);

            //Seed 1000 Products with random Name and Bar Code for testing
            //for (int i = 0; i < 1000; i++)
            //{
            //    context.Furniture.AddOrUpdate(f => f.Name,
            //        new Furniture { Name = string.Concat(i.ToString(), RandomString(5).ToString()), BarCode = (RandomString(9)), PricePerUnit = i, Weight = i });
            //}

            //context.Users.AddOrUpdate(p => p.Bulstat,
            //new ApplicationUser { UserName = "papa", PasswordHash="AS32" , FullName = "Papa John's", Adress = "8'th St. Peter st.", Bulstat = "073356743", DDSReg = true, MOL = "Richard Anderson", Email = "yolo@abv.bg" },
            //new ApplicationUser { UserName ="fresh,", PasswordHash = "AS33", FullName = "Fresh Cafe", Adress = "13'th Vuzrojdenie st.", Bulstat = "123674563", DDSReg = true, MOL = "Nikolay Ivanov", Email = "swag@abv.bg" },
            //new ApplicationUser {UserName ="nolan", PasswordHash = "AS34" ,FullName = "Christopher Nolan", Adress = "67'th Virgo st", DDSReg = false, Email = "cn@abv.bg" }
            //);

            //context.Order.AddOrUpdate(p => p.Date,
            //new Order { ClientId = "3", Date = DateTime.Parse("2016-08-18 10:34:04 AM"), RecieptNumber = "4123457547", TotalPrice = 437.22m },
            //new Order { ClientId = "6", Date = DateTime.Parse("2016-08-18 10:34:13 AM"), RecieptNumber = "4235457555", TotalPrice = 628.33m },
            //new Order { ClientId = "1", Date = DateTime.Parse("2015-03-02 03:42:55 PM"), RecieptNumber = "4235466551", TotalPrice = 450.28m }
            //);

            //context.FurnitureOrder.AddOrUpdate(
            //   new FurnitureOrder { FurnitureId = 3, OrderId = 1, Quantity = 2 },
            //   new FurnitureOrder { FurnitureId = 4, OrderId = 1, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 1, OrderId = 2, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 2, OrderId = 2, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 3, OrderId = 2, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 4, OrderId = 2, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 5, OrderId = 2, Quantity = 1 },
            //   new FurnitureOrder { FurnitureId = 1, OrderId = 3, Quantity = 2 },
            //   new FurnitureOrder { FurnitureId = 5, OrderId = 3, Quantity = 2 }
            //   );

            base.Seed(context);
        }
    }
}
 

