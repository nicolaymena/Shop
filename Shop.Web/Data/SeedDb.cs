
namespace Shop.Web.Data
{
    using Shop.Web.Data.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public IUserHelper UserHelper { get; }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("nimeom75@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Nicolay",
                    LastName = "Mena",
                    Email = "nimeom75@gmail.com",
                    UserName = "nimeom75@gmail.com",
                    PhoneNumber = "0649519741"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            //if (!this.context.Products.Any())
            //{
            //    this.AddProduct("First Product", user);
            //    this.AddProduct("Second Product", user);
            //    this.AddProduct("Third Product", user);
            //    await this.context.SaveChangesAsync();
            //}
        //}


            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}


