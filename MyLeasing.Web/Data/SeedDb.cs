using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            var user = await _userHelper.GetUserByEmailAsync("pedrosilva@gmail.com");

            if (user == null)
            {
                user = new User()
                {
                    Document = "123456789",
                    FirstName = "Pedro",
                    LastName = "Silva",
                    Email = "pedrosilva@gmail.com",
                    UserName = "pedrosilva@gmail.com",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (!result.Succeeded)
                {
                    throw new InvalidCastException("Could not create user in seeder");
                }
            }

            if (!_context.Owners.Any())
            {
                AddOwner("João", "Ratão", user);
                AddOwner("Orlando", "Maria", user);
                AddOwner("Otávio", "Massada", user);
                AddOwner("Francisco", "Simões", user);
                AddOwner("Pedro", "Silva", user);
                AddOwner("Tiago", "Pinho", user);
                AddOwner("Diogo", "Cruz", user);
                AddOwner("Mário", "Dias", user);
                AddOwner("Hugo", "Pereira", user);
                AddOwner("Helder", "Amorim", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstname, string lastname, User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstname,
                LastName = lastname,
                FixedPhone = _random.Next(100000000, 999999999).ToString(),
                CellPhone = _random.Next(100000000, 999999999).ToString(),
                Address = "Placeholder adress",
                User = user
            });
        }
    }
}
